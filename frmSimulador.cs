using FftSharp;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;
using TCC1.Reproduzir_Senoide;

namespace TCC1
{
    public partial class frmSimulador : Form
    {
        public int taxaAmostragem = 48000; //48000
        public int tamanhoVetor = 4096; // 2^12
        public double[] AudioPlotado;
        SoundPlayer som;

        // variaveis globais
        int seq = 10;
        List<double> frequencias = new List<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // guardar as frequencias do treeview
        List<double> amplitudes = new List<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // guardar as magnitudes do treeview

        public frmSimulador()
        {
            InitializeComponent();

            var plt1 = plotAudio.Plot;
            plt1.Clear();
            plt1.Margins(0);
            plt1.Title("Sinal");
            plt1.YLabel("Amplitude");
            plt1.XLabel("Tempo [ms]");
            plt1.AxisAuto(0);
            plt1.Style(Color.FromArgb(41, 39, 40), Color.FromArgb(41, 39, 40), Color.FromArgb(15, 15, 15), Color.DimGray, Color.FromArgb(255, 171, 0), Color.FromArgb(255, 171, 0));

            var plt2 = plotFFT.Plot;
            plt2.Clear();
            plt2.Margins(0);
            plt2.Title("FFT");
            plt2.XLabel("Frequência [Hz]");
            plt2.AxisAuto(0);
            plt2.Style(Color.FromArgb(41, 39, 40), Color.FromArgb(41, 39, 40), Color.FromArgb(15, 15, 15), Color.DimGray, Color.FromArgb(255, 171, 0), Color.FromArgb(255, 171, 0));

        }

        private void GerarSinalGrafico()
        {
            AudioPlotado = new double[tamanhoVetor];

            int i = 0;
            foreach (double freq in frequencias)
            {
                if (freq != 0)
                {
                    if (rbRuido.Checked)
                    {
                        SampleData.AddWhiteNoise(AudioPlotado, amplitudes[i]);
                    }
                    SampleData.AddSin(AudioPlotado, taxaAmostragem, freq, amplitudes[i]);
                }
                i++;
            }

            PlotarGraficoSinal();
            PlotarGraficoFFT(AudioPlotado);
        }

        private void PlotarGraficoSinal()
        {
            var p = plotAudio.Plot;
            p.Clear();
            p.AddSignal(AudioPlotado, taxaAmostragem / 1e3, Color.FromArgb(255, 171, 0));

            plotAudio.Refresh();
        }

        private void PlotarGraficoFFT(double[] audio)
        {
            double[] eixoY = cbLog.Checked ? Transform.FFTpower(audio) : Transform.FFTmagnitude(audio);
            string linhadoeixoY = cbLog.Checked ? "dB" : "Magnitude";

            plotFFT.Plot.Clear();
            plotFFT.Plot.AddSignal(eixoY, (double)tamanhoVetor / taxaAmostragem, Color.FromArgb(255, 171, 0));
            plotFFT.Plot.YLabel(linhadoeixoY);
            plotFFT.Refresh();
        }

        private void btnGerarSinal_Click(object sender, EventArgs e)
        {
            GerarSinalGrafico();
            ReproduzirFrequencias(10);
        }

        private void btnAdicionarSinal_Click(object sender, EventArgs e)
        {
            if (treeViewSinais.Nodes.Count == 10)
            {
                MessageBox.Show("Limite de frequências atingido. Limpe a árvore e comece de novo.");
                return;
            }

            bool ruido = false;
            if (rbRuido.Checked)
            {
                ruido = true;
            }
            treeViewSinais.Nodes.Add(seq.ToString() + " - Frequência: " + txtFrequencia.Text + " hz | Magnitude: " + cbMagnitude.SelectedItem.ToString() + " | Ruído: " + ruido);
            seq += 10;

            frequencias.Insert(treeViewSinais.Nodes.Count, double.Parse(txtFrequencia.Text));
            amplitudes.Insert(treeViewSinais.Nodes.Count, double.Parse(cbMagnitude.SelectedItem.ToString()));
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            seq = 10;
            treeViewSinais.Nodes.Clear();
            frequencias.Clear();
            amplitudes.Clear();
            frequencias = new List<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            amplitudes = new List<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            plotAudio.Plot.Clear();
            plotAudio.Refresh();
            plotFFT.Plot.Clear();
            plotFFT.Refresh();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void cbLog_CheckedChanged(object sender, EventArgs e)
        {
            PlotarGraficoFFT(AudioPlotado);
        }

        // ## BARRA DE ROLAGEM INFERIOR 
        WaveOut OndaDeSaida = new WaveOut();
        SineWaveOscillator Oscilador = new SineWaveOscillator(44100);
        private void SliderFrequencia_Scroll(object sender, Utilities.BunifuSlider.BunifuHScrollBar.ScrollEventArgs e)
        {
            OndaDeSaida.Stop();

            lblFrequenciaScroll.Text = SliderFrequencia.Value.ToString() + " hz";

            Oscilador.Frequency = double.Parse(SliderFrequencia.Value.ToString());
            Oscilador.Amplitude = 5000;

            OndaDeSaida.Init(Oscilador);
            OndaDeSaida.Play();

            // grafico
            AudioPlotado = new double[tamanhoVetor];
            SampleData.AddSin(AudioPlotado, taxaAmostragem, SliderFrequencia.Value, 0.5);
            PlotarGraficoSinal();
            PlotarGraficoFFT(AudioPlotado);
        }

        // som

        public void ReproduzirFrequencias(int duracaoSegundos)
        {
            // objetos
            MemoryStream ArquivoNaMemoria = new MemoryStream(); // cria arquivo para ser aberto na memoria ao inves de precisar salvá-lo para depois abri-lo
            BinaryWriter ConteudoDoArquivo = new BinaryWriter(ArquivoNaMemoria); // objeto usado para "escrever" no arquivo

            // variaveis
            int amostrasPorSegundo = 44100;
            int tempo = (int)((decimal)amostrasPorSegundo * duracaoSegundos);
            short bitsPorAmostra = 16;
            short canais = 1;
            short tamanhoFrame = (short)(canais * ((bitsPorAmostra + 7) / 8));
            int dadosDaSecao = tempo * tamanhoFrame;
            int bytesPorSegundo = amostrasPorSegundo * tamanhoFrame;
            short formato = 1;
            int tamanhoDoCabecalho = 8;
            int secaoDeDados = 16;
            int tamanhoOnda = 4;
            int tamanhoDoArquivo = dadosDaSecao + tamanhoDoCabecalho + secaoDeDados + tamanhoOnda + tamanhoDoCabecalho;

            // transformando as palavras do cabeçalho para bytes 
            var informacoesDoArquivo = new System.Text.UTF8Encoding();
            var bytesRIFF = informacoesDoArquivo.GetBytes("RIFF");
            var bytesWAVE = informacoesDoArquivo.GetBytes("WAVE"); // 0x45564157
            var bytesfmt = informacoesDoArquivo.GetBytes("fmt"); // 0x20746D66
            var bytesdata = informacoesDoArquivo.GetBytes("data"); // 0x61746164

            // CABEÇALHO DO ARQUIVO WAV
            // https://docs.fileformat.com/audio/wav/ - Todas as partes do cabeçalho
            {
                ConteudoDoArquivo.Write(0x46464952); ConteudoDoArquivo.Write(tamanhoDoArquivo); ConteudoDoArquivo.Write(0x45564157);
                ConteudoDoArquivo.Write(0x20746D66); ConteudoDoArquivo.Write(secaoDeDados); ConteudoDoArquivo.Write(formato);
                ConteudoDoArquivo.Write(canais); ConteudoDoArquivo.Write(amostrasPorSegundo); ConteudoDoArquivo.Write(bytesPorSegundo);
                ConteudoDoArquivo.Write(tamanhoFrame); ConteudoDoArquivo.Write(bitsPorAmostra); ConteudoDoArquivo.Write(0x61746164);
                ConteudoDoArquivo.Write(dadosDaSecao);
            }

            // CONTEUDO DO ARQUIVO
            double theta = 2 * Math.PI / amostrasPorSegundo; // theta = 2 * PI / 44100

            for (int t = 0; t < tempo; t++) // repetição para cada instante de tempo
            {
                short soma = (short)(
                    (short)(amplitudes[0] * Math.Sin(frequencias[0] * theta * t)) + (short)(amplitudes[1] * Math.Sin(frequencias[1] * theta * t)) +
                    (short)(amplitudes[2] * Math.Sin(frequencias[2] * theta * t)) + (short)(amplitudes[3] * Math.Sin(frequencias[3] * theta * t)) +
                    (short)(amplitudes[4] * Math.Sin(frequencias[4] * theta * t)) + (short)(amplitudes[5] * Math.Sin(frequencias[5] * theta * t)) +
                    (short)(amplitudes[6] * Math.Sin(frequencias[6] * theta * t)) + (short)(amplitudes[7] * Math.Sin(frequencias[7] * theta * t)) +
                    (short)(amplitudes[8] * Math.Sin(frequencias[8] * theta * t)) + (short)(amplitudes[9] * Math.Sin(frequencias[9] * theta * t))
                    );
                ConteudoDoArquivo.Write(soma);
            }

            ArquivoNaMemoria.Seek(0, SeekOrigin.Begin); // determina o ponto de onde o arquivo deve começar a ser lido

            som = new SoundPlayer(ArquivoNaMemoria); // cria o reprodutor de som com base no arquivo gerado
            som.Play(); // reproduz o som

            ConteudoDoArquivo.Close(); // fecha os objetos instanciados
            ArquivoNaMemoria.Close();

            //GerarSinalGrafico();
            
        }
    }
}
