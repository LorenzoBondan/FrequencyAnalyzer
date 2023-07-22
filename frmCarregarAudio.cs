using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace TCC1
{
    public partial class frmCarregarAudio : Form
    {
        public frmCarregarAudio()
        {
            InitializeComponent();
        }

        string NomeDoArquivo;

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            NomeDoArquivo = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Arquivo wav (*.wav)|*.wav";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                NomeDoArquivo = openFileDialog.FileName;
                txtAudioCarregado.Text = openFileDialog.SafeFileName;

                label1.Visible = true;
                label1.Text = "Áudio carregado com sucesso!";
                label1.ForeColor = Color.FromArgb(0, 192, 0);

                var LerArquivo = new WaveFileReader(NomeDoArquivo);

                // informaçoes 
                string informacoes =
                    "Tempo total: " + LerArquivo.TotalTime.TotalSeconds + " s. \n" +
                    "Total de bytes: " + LerArquivo.Length + ". \n" +
                    "Formato do Arquivo: " + LerArquivo.WaveFormat + ". \n" +
                    "Número de Amostras: " + LerArquivo.SampleCount + ".";
                richTextBox.Text = informacoes;

                double amostrarIdeais = LerArquivo.SampleCount / leitorDeOnda1.Width; // 1s = 44100 amostras. tempo do audio (x segundos) = SampleCount
                txtAmostrasPorPixel.Text = amostrarIdeais.ToString();

                DisposeWave();

                wave = new WaveFileReader(openFileDialog.FileName);
                output = new DirectSoundOut();

                output.Init(new WaveChannel32(wave));
                output.Play();
            }
            else
            {
                label1.Visible = false;
            }

        }

        private NAudio.Wave.WaveFileReader wave = null;

        private NAudio.Wave.DirectSoundOut output = null;

        private void btnPlotar_Click(object sender, EventArgs e)
        {
            //plot
            
            leitorDeOnda1.WaveStream = new WaveFileReader(NomeDoArquivo);
            leitorDeOnda1.FitToScreen(leitorDeOnda1.Width);
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            // exporta o painel que contém a wavereader e a richtextbox

            SaveFileDialog savefiledialog = new SaveFileDialog();
            savefiledialog.Filter = "Imagem PNG (.png)|*.png";
            if (savefiledialog.ShowDialog() == DialogResult.OK)
            {
                int largura = panel2.Size.Width;
                int altura = panel2.Size.Height;

                Bitmap bitmap = new Bitmap(largura, altura);
                panel2.DrawToBitmap(bitmap, new Rectangle(0, 0, largura, altura));

                string caminho = savefiledialog.FileName;
                bitmap.Save(caminho, ImageFormat.Png);

                MessageBox.Show("Arquivo exportado com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnPlotarPersonalizado_Click(object sender, EventArgs e)
        {
            if (txtAmostrasPorPixel.Text != "")
            {
                leitorDeOnda1.SamplesPerPixel = int.Parse(txtAmostrasPorPixel.Text);
                leitorDeOnda1.WaveStream = new WaveFileReader(NomeDoArquivo);
            }
            else
            {
                MessageBox.Show("Preencha todos os campos.","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }

        private void DisposeWave()
        {
            if (output != null)
            {
                if (output.PlaybackState == PlaybackState.Playing) output.Stop();
                output.Dispose();
                output = null;
            }
            if (wave != null)
            {
                wave.Dispose();
                wave = null;
            }
        }

        private void btnReproduzirPausar_Click(object sender, EventArgs e)
        {
            
            if (output != null)
            {
                
                if (output.PlaybackState == PlaybackState.Playing) output.Pause();
                else if (output.PlaybackState == PlaybackState.Paused) output.Play();
            }
        }

        private void frmCarregarAudio_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposeWave();
        }

        public static IWaveProvider CutAudio(WaveStream wave, TimeSpan startPosition, TimeSpan endPosition)
        {
            ISampleProvider sourceProvider = wave.ToSampleProvider();
            long currentPosition = wave.Position; // Save stream position

            OffsetSampleProvider offset = new OffsetSampleProvider(sourceProvider)
            {
                SkipOver = startPosition,
                Take = endPosition - startPosition
            };

            wave.Position = currentPosition; // Restore stream position
            return (offset.ToWaveProvider());
        }

        public void CortarAudio()
        {
            string ArquivoEntrada;
            string ArquivoSaida;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Arquivo wav (*.wav)|*.wav";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ArquivoEntrada = openFileDialog.FileName;

                using (AudioFileReader reader = new AudioFileReader(ArquivoEntrada))
                {
                    TimeSpan startPosition = TimeSpan.Parse(txtMomentoInicial.Text);
                    TimeSpan endPosition = TimeSpan.Parse(txtMomentoFinal.Text);
                    IWaveProvider wave = CutAudio(reader, startPosition, endPosition);

                    SaveFileDialog savefiledialog = new SaveFileDialog();
                    savefiledialog.Filter = "Arquivo wav (.wav)|*.wav";
                    if (savefiledialog.ShowDialog() == DialogResult.OK)
                    {
                        ArquivoSaida = savefiledialog.FileName;

                        WaveFileWriter.CreateWaveFile(ArquivoSaida, wave);
                    }
                }

            }

        }

        private void btnCortarAudio_Click(object sender, EventArgs e)
        {
            CortarAudio();
        }

    }
}
