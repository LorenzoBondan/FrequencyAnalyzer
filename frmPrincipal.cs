using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Management;
using NAudio.Wave;
using System.IO;
using Microsoft.VisualBasic;
using System.Data.SqlServerCe;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = System.Drawing.Font;
using System.Diagnostics;

namespace TCC1
{
    /* SCRIPT 
      #0 - Iniciar o aplicativo.
      #1 - Listar os microfones conectados.
      #2 - Selecionar o primeiro microfone da lista e aciona o evento de SelectedItemChange da ComboBox.
      #3 - O evento consta em a cada período de tempo capturar o áudio registrado pelo microfone.
      #4 - A cada período de tempo, o timer verifica se dados foram capturados pelo microfone.
      #5 - Se dados forem capturados, os mesmos são tratados e usados para os calculos.
      #6 - Os dados calculados são mostrados nos gráficos.
      #7 - Cada nível de pressão sonora registrado é gravado acumulativamente em sua classificação.
      #8 - Os resultados são mostrados e plotados em tempo real.
      #9 - Ao final da jornada, um relatório é gerado contendo as principais informações da amostragem.
    */
    public partial class frmPrincipal : Form
    {
        public frmPrincipal(string NomeDoUsuario, string Administrador) // Traz nome do usuário do form anterior e seu nível de permissão
        {
            InitializeComponent();
            lblUsuario.Text = NomeDoUsuario; // Nome do usuário na label
            lblData.Text = DateTime.Now.ToShortDateString(); // Dia atual 
            if (Administrador != "s") // Se for administrador
            {
                cardCalibrador.Visible = false; // Habilita o calibrador
            }

            EncontrarMicrofone(); // Listar todos os microfones encontrados
            cbMicrofone.selectedIndex = 0; // Seleciona o primeiro microfone e automaticamente chama o evento de selectedindex change da combobox
            cbTipoDeJanela.selectedIndex = 0; // Por padrão, inicia no primeiro janelamento (Hanning)

            // Interface dos graficos ao vivo
            var plt1 = formsPlot1.Plot;
            var plt2 = formsPlot2.Plot;
            var plt3 = formsPlot3.Plot;
            var plt4 = formsPlot4.Plot;
            var plt6 = formsPlot5.Plot;
            var plt7 = formsPlot6.Plot;

            plt1.YLabel(""); 
            plt1.XLabel("Frequência [Hz]");
            plt1.Margins(0, 0);
            plt1.Title("Espectro", true, Color.FromArgb(238, 73, 96), 18, "Century Gothic");
            plt1.Style(Color.FromArgb(1, 1, 3), Color.FromArgb(1, 1, 3), Color.FromArgb(15, 15, 15), Color.DimGray, Color.FromArgb(238, 73, 96), Color.FromArgb(238, 73, 96));

            plt2.XLabel("Tempo [ms]");
            plt2.Margins(0, 0);
            plt2.Title("", true, Color.FromArgb(238, 73, 96), 18, "Century Gothic");
            plt2.Style(Color.FromArgb(1, 1, 3), Color.FromArgb(1, 1, 3), Color.FromArgb(15, 15, 15), Color.DimGray, Color.FromArgb(238, 73, 96), Color.FromArgb(238, 73, 96));

            plt3.XLabel("Frequência [Hz]");
            plt3.Margins(0, 0);
            plt3.Title("dB", true, Color.FromArgb(238, 73, 96), 18, "Century Gothic");
            plt3.Style(Color.FromArgb(1, 1, 3), Color.FromArgb(1, 1, 3), Color.FromArgb(15, 15, 15), Color.DimGray, Color.FromArgb(238, 73, 96), Color.FromArgb(238, 73, 96));

            plt4.XLabel("Tempo [ms]");
            plt4.YLabel("Amplitude");
            plt4.Margins(0, 0);
            plt4.Title("Sinal Janelamento Hanning", true, Color.FromArgb(238, 73, 96), 18, "Century Gothic");
            plt4.Style(Color.FromArgb(1, 1, 3), Color.FromArgb(1, 1, 3), Color.FromArgb(15, 15, 15), Color.DimGray, Color.FromArgb(238, 73, 96), Color.FromArgb(238, 73, 96));

            plt6.Title("Nível de ruído x Tempo");
            plt6.YLabel("dB");
            plt6.AxisAuto(0);
            plt6.Style(Color.FromArgb(1, 1, 3), Color.FromArgb(1, 1, 3), Color.FromArgb(15, 15, 15), Color.DimGray, Color.Tomato, Color.Tomato);

            plt7.Title("Histograma");
            plt7.YLabel("Tempo");
            plt7.XLabel("dB");
            plt7.AxisAuto(0);
            plt7.Style(Color.FromArgb(1, 1, 3), Color.FromArgb(1, 1, 3), Color.FromArgb(15, 15, 15), Color.DimGray, Color.Tomato, Color.Tomato);

            #region CUSTOMIZAÇÃO DO DATAGRIDVIEW

            // Linhas alternadas
            dataDosimetria.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);

            // Linha selecionada
            dataDosimetria.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 125, 33);
            dataDosimetria.DefaultCellStyle.SelectionForeColor = Color.White;

            // Fonte
            //dataGridView2.DefaultCellStyle.Font = new Font("Century Gothic",8);

            // Bordas
            dataDosimetria.CellBorderStyle = DataGridViewCellBorderStyle.None;

            // Cabeçalho
            dataDosimetria.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 8, FontStyle.Bold);

            dataDosimetria.EnableHeadersVisualStyles = false; // Habilita a edição do cabeçalho

            dataDosimetria.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(211, 84, 21);
            dataDosimetria.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            #endregion

            CarregarDosimetrias(); // Chama o método que carrega as dosimetrias do banco de dados na combobox
        }

        ScottPlot.Plottable.SignalPlot signalPlot;
        ScottPlot.Plottable.SignalPlot signalPlot2;
        ScottPlot.Plottable.SignalPlot signalPlot3;
        ScottPlot.Plottable.SignalPlot signalPlot4;

        public WaveInEvent wvin;

        #region MENU DA DIREITA

        // Temporizador que apresenta a quantidade de memória utilizada
        private void timerCPURAM_Tick(object sender, EventArgs e)
        {
            float CPU = pCPU.NextValue();
            float RAM = pRAM.NextValue();

            progressbarCPU.Value = (int)CPU;
            progressbarRAM.Value = (int)RAM;

            lblCPU.Text = string.Format("{0:0.00}%", CPU);
            lblRAM.Text = string.Format("{0:0.00}%", RAM);
        }

        // Temporizador que apresenta a temperatura da CPU, necessita execução em modo administrador
        private void timerTemperatura_Tick(object sender, EventArgs e)
        {
            try // tenta caso esteja sendo executado no modo administrador
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher(@"root\WMI", "Select * From MSAcpi_ThermalZoneTemperature");

                foreach (ManagementObject mo in mos.Get())
                {
                    var CPUtprt = Convert.ToDouble(Convert.ToDouble(mo.GetPropertyValue("CurrentTemperature").ToString()) - 2732) / 10;
                    bunifuCircleProgress1.Text = CPUtprt.ToString("F1");
                    bunifuCircleProgress1.Value = (int)CPUtprt;
                }
            }
            catch (Exception)
            {
                // ignorar
            }
        }

        // Temporizador do relógio, mostra o horário em tempo real
        private void timerHora_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToLongTimeString().ToString();
        }

        #endregion

        #region MENU DA ESQUERDA
        // Método que fecha a aplicação
        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        // Ajustes do panel criado para ficar sempre no botão que está selecionado, sua altura varia de acordo com o botão clicado
        private void btnAoVivo_Click(object sender, EventArgs e)
        {
            panelCursor.Height = btnAoVivo.Height;
            panelCursor.Top = btnAoVivo.Top;

            Paginas.PageIndex = 1;
        }

        private void btnSimulador_Click(object sender, EventArgs e)
        {
            panelCursor.Height = btnSimulador.Height;
            panelCursor.Top = btnSimulador.Top;

            Paginas.PageIndex = 2;
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            panelCursor.Height = btnCalcular.Height;
            panelCursor.Top = btnCalcular.Top;

            Paginas.PageIndex = 3;
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            panelCursor.Height = btnInicio.Height;
            panelCursor.Top = btnInicio.Top;

            Paginas.PageIndex = 0;
        }

        private void btnPaginaFFT_Click(object sender, EventArgs e)
        {
            panelCursor.Height = btnPaginaFFT.Height;
            panelCursor.Top = btnPaginaFFT.Top;

            Paginas.PageIndex = 4;
        }

        private void btnDosimetria_Click(object sender, EventArgs e)
        {
            panelCursor.Height = btnDosimetria.Height;
            panelCursor.Top = btnDosimetria.Top;

            Paginas.PageIndex = 5;
        }

        #endregion

        #region METODO 

        // Variáveis globais (ao vivo)

        double[] dadosRegistrados; // Vetor que armazena os dados registrados pelo microfone -> som no domínio do tempo

        double[] vetorJanela;

        double[] Espectro_EscalaLinear;
        double[] frequenciaFFT;
        double[] Espectro_EscalaLogaritmica;

        // Método para encontrar o microfone conectado ao computador
        private void EncontrarMicrofone()
        {
            if (WaveIn.DeviceCount > 0) // verificar se há algum microfone conectado usando a classe WaveIn da biblioteca NAudio
            {
                cbMicrofone.Items.Clear(); // Limpa os itens do combobox que lista os microfones
                for (int i = 0; i < WaveIn.DeviceCount; i++) // Faz um loop para percorrer todos os dispositivos de entrada de áudio encontrados
                {
                    cbMicrofone.Items.Add(WaveIn.GetCapabilities(i).ProductName); // Adiciona o nome do dispositivo ao combobox usando o método GetCapabilities da classe WaveIn
                }
                //cbMicrofone.selectedIndex = 0; // listar o 1º microfone
            }
            else
            {
                cbMicrofone.Items.Add("Nenhum microfone conectado."); // Se não houver nenhum dispositivo de entrada de áudio, adiciona uma mensagem ao combobox informando isso
                cbMicrofone.selectedIndex = 0;
            }
        }

        // Método para iniciar a gravação do áudio usando o microfone selecionado
        private void IniciarGravacao(int microfoneEncontrado, int taxaAmostragem, int taxaBits, int milisegundos)
        {
            // Captura de Áudio do microfone, criando uma instância da classe WaveInEvent, que permite capturar o áudio do microfone usando eventos
            wvin = new NAudio.Wave.WaveInEvent();

            wvin.DeviceNumber = microfoneEncontrado; // Define o número do dispositivo de entrada de áudio a ser usado para a gravação
            wvin.WaveFormat = new NAudio.Wave.WaveFormat(taxaAmostragem, taxaBits, 1); // Define o formato do áudio a ser gravado, especificando a taxa de amostragem, a taxa de bits e o número de canais (44100, 16, 1)
            wvin.DataAvailable += RegistroDeDados; // Define o método que será chamado quando houver dados disponíveis para serem gravados, usando o evento DataAvailable da classe WaveInEvent
            wvin.BufferMilliseconds = milisegundos; // Define o tamanho do buffer em milisegundos que será usado para armazenar os dados capturados

            wvin.StartRecording(); // Inicia a gravação do áudio usando o método StartRecording da classe WaveInEvent
        }

        // Método para registrar os dados capturados pelo microfone
        private void RegistroDeDados(object sender, NAudio.Wave.WaveInEventArgs args) // args contém os dados capturados pelo microfone
        { // buffer = vetor de bytes registrados pelo microfone com valores de 0, 1 ou 255. -> 1764 amostras
            // Obtém o número de bytes por amostra de áudio usando a propriedade BitsPerSample da classe WaveFormat
            int bytesPorAmostra = wvin.WaveFormat.BitsPerSample / 8; // (16/8 = 2)
            int AmostrasRegistradas = args.BytesRecorded / bytesPorAmostra; // Obtém o número de amostras de áudio registradas usando a propriedade BytesRecorded da classe WaveInEventArgs (1764/2 = 882)

            if (dadosRegistrados == null) // Na primeira vez que executa
            {
                dadosRegistrados = new double[AmostrasRegistradas]; // <- Cria um vetor double vazio vazio com o tamanho do número de amostras registradas (882)
            }

            for (int i = 0; i < AmostrasRegistradas; i++)
            {
                // Som no dominio do tempo -> 882 amostras
                // Converte os bytes do buffer para valores inteiros usando o método BitConverter.ToInt16
                dadosRegistrados[i] = BitConverter.ToInt16(args.Buffer, i * bytesPorAmostra); // Para cada posição vazia do vetor, ele preenche, convertendo o vetor de double para o vetor de int
            }
        }

        // Evento do temporizador que calcula a frequência e plota os gráficos
        private void timerRegistroSom_Tick(object sender, EventArgs e)
        {
            if (txtTaxaAmostragem.Text == "") { return; }
            CalcularFrequencia();
            PlotarGraficos();
        }

        // Método de seleção do tipo de janelamento da FFT
        private void SelecionarTipoDeJanela()
        {
            FftSharp.Window janela; // Cria o objeto
            switch (cbTipoDeJanela.selectedIndex) // Condição de escolha
            {
                case 0: // Janelamento Hanning
                    janela = new FftSharp.Windows.Hanning(); // Cria novo objeto com o janelamento selecionado
                    vetorJanela = janela.Apply(dadosRegistrados); // Aplica o vetor no janelamento selecionado
                    richTextBox.Text = janela.Description; // Adiciona a descrição do janelamento selecionado na caixa de texto
                    break;
                case 1: // Janelamento Hamming
                    janela = new FftSharp.Windows.Hamming();
                    vetorJanela = janela.Apply(dadosRegistrados);
                    richTextBox.Text = janela.Description;
                    break;
                case 2: // Janelamento Blackman Harris
                    janela = new FftSharp.Windows.Blackman();
                    vetorJanela = janela.Apply(dadosRegistrados);
                    richTextBox.Text = janela.Description;
                    break;
                case 3: // Janelamento Cosseno
                    janela = new FftSharp.Windows.Cosine();
                    vetorJanela = janela.Apply(dadosRegistrados);
                    richTextBox.Text = janela.Description;
                    break;
                case 4: // Janelamento Retangular
                    janela = new FftSharp.Windows.Rectangular();
                    vetorJanela = janela.Apply(dadosRegistrados);
                    richTextBox.Text = janela.Description;
                    break;
                case 5: // Janelamento Bartlett
                    janela = new FftSharp.Windows.Bartlett();
                    vetorJanela = janela.Apply(dadosRegistrados);
                    richTextBox.Text = janela.Description;
                    break;
                case 6: // Janelamento FlatTop
                    janela = new FftSharp.Windows.FlatTop();
                    vetorJanela = janela.Apply(dadosRegistrados);
                    richTextBox.Text = janela.Description;
                    break;
                case 7: // Janelamento Kaiser
                    janela = new FftSharp.Windows.Kaiser();
                    vetorJanela = janela.Apply(dadosRegistrados);
                    richTextBox.Text = janela.Description;
                    break;
                case 8: // Janelamento Tukey
                    janela = new FftSharp.Windows.Tukey();
                    vetorJanela = janela.Apply(dadosRegistrados);
                    richTextBox.Text = janela.Description;
                    break;
                case 9: // Janelamento Welch
                    janela = new FftSharp.Windows.Welch();
                    vetorJanela = janela.Apply(dadosRegistrados);
                    richTextBox.Text = janela.Description;
                    break;
                default:
                    break;
            }
        }

        // Método que chama o cálculo da FFT para os valores registrados pelo microfone
        private void CalcularFrequencia()
        {
            if (dadosRegistrados is null) // Condição para encerrar o evento caso nenhum dado tenha sido captado pelo microfone
            {
                return;
            }

            // Tratamento de dados

            SelecionarTipoDeJanela(); // Reduzir o vazamento de informaçoes

            double[] correcaoJanelamento = FftSharp.Pad.ZeroPad(vetorJanela); // Potência de 2 (1024 pontos), zera as extremidades do vetor pra corrigir as perdas de acordo com o tipo de janelamento selecionado

            Espectro_EscalaLinear = TransformadaDeFourier.FFTLinear(correcaoJanelamento); // Som no dominio da frequencia em escala linear
            frequenciaFFT = TransformadaDeFourier.FFTfreq(double.Parse(txtTaxaAmostragem.Text), Espectro_EscalaLinear.Length); // Comprime as amostras pelo comprimento do vetor do espectro
            Espectro_EscalaLogaritmica = TransformadaDeFourier.FFTLogaritmica(correcaoJanelamento); // Som no dominio da frequencia em escala logaritmica

            // Maior frequencia registrada naquele instante
            double picoDeMagnitude = 0; double picoDeFrequencia = 0;

            for (int i = 0; i < Espectro_EscalaLinear.Length; i++)
            {
                if (Espectro_EscalaLinear[i] > picoDeMagnitude) // Compara valor por valor dentro do vetor pra saber qual é o maior
                {
                    picoDeMagnitude = Espectro_EscalaLinear[i]; // Traz a posição do maior do eixo y
                    picoDeFrequencia = frequenciaFFT[i]; // Pra refletir e registar a posição dele no eixo x
                }
            }
            lblDB.Text = "Pico de Frequência: " + picoDeFrequencia.ToString("F0") + " Hz"; // Trazer o máx do eixo y, só que trazendo a posição desse cara no eixo x, em qual frequencia ele se encontra

            //-----
            double valorMedio = 0; double valorAbsoluto = 0; double valorEficaz = 0;

            for (int i = 0; i < dadosRegistrados.Length; i++)
            {
                valorMedio += dadosRegistrados[i]; // Soma todos os valores do vetor

                double valorAuxiliar = dadosRegistrados[i]; // Cria uma variavel auxiliar para fazer o modulo
                if (valorAuxiliar < 0) { valorAuxiliar *= -1; } // Módulo
                valorAbsoluto += valorAuxiliar;
                valorEficaz += Math.Pow(valorAuxiliar, 2); // Soma dos quadrados das pressoes
            }
            valorMedio /= dadosRegistrados.Length;
            valorAbsoluto /= dadosRegistrados.Length;
            valorEficaz = Math.Sqrt(valorEficaz);

            lblValorMedio.Text = valorMedio.ToString("F2");
            lblValorAbsoluto.Text = valorAbsoluto.ToString("F2");
            lblValorEficaz.Text = valorEficaz.ToString("F2");
        }

        double dbMaximoGeral = 0; double dbMedioGeral = 0; double contadordbMedio = 0; double dbMaximo;
        private void PlotarGraficos()
        {
            if (dadosRegistrados is null) // Condição para encerrar o evento caso nenhum dado tenha sido captado pelo microfone
            {
                return;
            }
            // plotando FORMSPLOT1 = FFT (dominio da frequencia em escala linear) | FORMSPLOT2 = AMPLITUDE (dominio do tempo) | FORMSPLOT3 = dB (dominio da frequencia em escala logaritmica) | FORMSPLOT4 = Window

            if (formsPlot1.Plot.GetPlottables().Count() == 0) // PLOTAR PELA PRIMEIRA VEZ
            {
                signalPlot = formsPlot1.Plot.AddSignal(Espectro_EscalaLinear, 2 * Espectro_EscalaLinear.Length / double.Parse(txtTaxaAmostragem.Text), Color.FromArgb(238, 73, 96)); // double[] 513 amostras, taxa amostragem: 0,0236530 (2*513/44100)
                signalPlot3 = formsPlot3.Plot.AddSignal(Espectro_EscalaLogaritmica, 2 * Espectro_EscalaLogaritmica.Length / double.Parse(txtTaxaAmostragem.Text), Color.FromArgb(238, 73, 96)); // double[] 513 amostras, taxa amostragem: 0,0236530 (2*513/44100)
                signalPlot4 = formsPlot4.Plot.AddSignal(vetorJanela, 44.1, Color.FromArgb(238, 73, 96));
            }
            else // ATUALIZAR O PLOT Y DA SEGUNDA VEZ EM DIANTE
            {
                signalPlot.Ys = Espectro_EscalaLinear;
                signalPlot3.Ys = Espectro_EscalaLogaritmica;
                signalPlot4.Ys = vetorJanela;
            }

            ///// AMPLITUDE -> som no dominio do tempo
            double espacamento = double.Parse(txtTaxaAmostragem.Text) / 1000;
            if (formsPlot2.Plot.GetPlottables().Count() == 0)
            {
                signalPlot2 = formsPlot2.Plot.AddSignal(dadosRegistrados, espacamento, Color.FromArgb(238, 73, 96));
            }
            else
            {
                signalPlot2.Ys = dadosRegistrados;
            }

            if (cbAutoAjustarEixos.Checked) // Ajustar os eixos automaticamente
            {
                try
                {
                    formsPlot1.Plot.AxisAuto(horizontalMargin: 0); // FFT LINEAR
                    formsPlot2.Plot.AxisAuto(horizontalMargin: 0); // DOMÍNIO DO TEMPO
                    formsPlot3.Plot.AxisAuto(horizontalMargin: 0); // FFT LOGARITMICA
                    formsPlot4.Plot.AxisAuto(horizontalMargin: 0); // JANELAMENTO
                }
                catch (Exception)
                {
                    // pular
                }
            }

            // dB
            double picoDeDecibeis = 0; double picoDeFrequencia = 0;
            for (int i = 0; i < Espectro_EscalaLogaritmica.Length; i++)
            {
                if (Espectro_EscalaLogaritmica[i] > picoDeDecibeis) // Compara valor por valor dentro do vetor pra saber qual é o maior
                {
                    picoDeDecibeis = Espectro_EscalaLogaritmica[i]; // Traz a posição do maior do eixo y
                    picoDeFrequencia = frequenciaFFT[i]; // Pra refletir e registar a posição dele no eixo x
                }
            }
            lblDBFrequencia.Text = "Pico de Frequência: " + picoDeFrequencia.ToString("F0") + " Hz"; // Trazer o máx do eixo y, só que trazendo a posição desse cara no eixo x, em qual frequencia ele se encontra

            try // Try para evitar que valores fora dos limites determinados para os controles crashem a aplicação
            {
                //dbMaximo = Espectro_EscalaLogaritmica.Max() + 10 * Math.Log(43.06640625) + sliderCalibradordB.Value - 10; // 512 (2^8) linhas -> resolução de linha = 43 hz | correção de banda = dB + 10 * log10(res.linha) | -10 calibração
                dbMaximo = Espectro_EscalaLogaritmica.Max()   + sliderCalibradordB.Value; // 512 (2^8) linhas -> resolução de linha = 43 hz | correção de banda = dB + 10 * log10(res.linha)
                circleProgressdB.Value = (int)double.Parse(dbMaximo.ToString("F0")); // se aproxima mais do dbC do que do dbA
                circleProgressdB.Text = dbMaximo.ToString("F0") + " dB";

                if (dbMaximo > dbMaximoGeral) // Registra o maior valor registrado geral
                {
                    dbMaximoGeral = dbMaximo; // Substitui se encontrar um novo valor maior
                    lbldbMaximoGeral.Text = dbMaximoGeral.ToString("F0") + " dB";
                }

                contadordbMedio++; // Conta quantas vezes foi analisado
                dbMedioGeral += dbMaximo; // Soma todos os valores máximos obtidos
                double mediadBGeral = dbMedioGeral / contadordbMedio; // Faz a média, dividindo todos os valores máximos pela qte de vezes
                lbldbMediaGeral.Text = mediadBGeral.ToString("F0") + " dB"; // Relata
            }
            catch (Exception ex) // Possível excessão: diminuir o valor do sliderCalibrador ao ponto de obter um valor negativo fora do mínimo estipulado
            {
                MessageBox.Show("Erro: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // RENDERIZAR (ATUALIZAR A CADA EVENTO DO TIMER)
            formsPlot1.Render(); // FFT_Linear
            formsPlot2.Render(); // Domínio do tempo
            formsPlot3.Render(); // FFT_Logaritmica
            formsPlot4.Render(); // Janela
        }

        #endregion

        // Habilita a gravação, caso a mesma esteja pausada
        private void btnGravar_Click(object sender, EventArgs e)
        {
            timerRegistroSom.Enabled = false;
            // Limpa os gráficos
            formsPlot1.Plot.Clear();
            formsPlot2.Plot.Clear();
            formsPlot3.Plot.Clear();
            formsPlot4.Plot.Clear();

            // Limpa os dados
            dadosRegistrados = null;
            Espectro_EscalaLinear = null;
            frequenciaFFT = null;
            Espectro_EscalaLogaritmica = null;
            vetorJanela = null;

            // Habilita e começa a gravação
            timerRegistroSom.Enabled = true;
            timerRegistroSom.Start();

            IniciarGravacao(cbMicrofone.selectedIndex, int.Parse(txtTaxaAmostragem.Text), 16, int.Parse(txtIntervaloTempo.Text));
        }

        // Pausa a gravação, paralizando o temporizador
        private void btnPausar_Click(object sender, EventArgs e)
        {
            timerRegistroSom.Stop();
        }

        #region SALVAR GRÁFICOS

        // Métodos para salvar os gráficos como imagens
        private void btnSalvarScottPlot_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Arquivo (*.png)|*.png";
            if (save.ShowDialog(this) == DialogResult.OK)
            {
                formsPlot1.Plot.SaveFig(save.FileName);
            }
        }

        private void btnSalvarScottPlot2_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Arquivo (*.png)|*.png";
            if (save.ShowDialog(this) == DialogResult.OK)
            {
                formsPlot2.Plot.SaveFig(save.FileName);
            }
        }
        private void btnSalvarScottPlot3_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Arquivo (*.png)|*.png";
            if (save.ShowDialog(this) == DialogResult.OK)
            {
                formsPlot3.Plot.SaveFig(save.FileName);
            }
        }

        private void btnSalvarScottPlot4_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Arquivo (*.png)|*.png";
            if (save.ShowDialog(this) == DialogResult.OK)
            {
                formsPlot4.Plot.SaveFig(save.FileName);
            }
        }

        private void btnSalvarScottPlot_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Arquivo (*.png)|*.png";
            if (save.ShowDialog(this) == DialogResult.OK)
            {
                formsPlot1.Plot.SaveFig(save.FileName);
            }
        }
        #endregion

        // Evento que muda o valor selecionado na combobox do janelamento
        private void cbTipoDeJanela_onItemSelected(object sender, EventArgs e)
        {
            formsPlot4.Plot.Title("Sinal Janelamento " + cbTipoDeJanela.selectedValue, true, Color.FromArgb(238, 73, 96), 18, "Century Gothic");
        }

        // Evento que muda o valor do calibrador
        private void sliderCalibradordB_Scroll(object sender, EventArgs e)
        {
            lblValorCalibrador.Text = sliderCalibradordB.Value.ToString() + " dB";
        }

        // Evento que muda o microfone selecionado na combobox
        private void cbMicrofone_onItemSelected_2(object sender, EventArgs e)
        {
            IniciarGravacao(cbMicrofone.selectedIndex, int.Parse(txtTaxaAmostragem.Text), 16, int.Parse(txtIntervaloTempo.Text));
        }

        #region DOSIMETRIA
         
        string nomeDaTabela;  // Variável para armazenar o nome da tabela que será criada no banco de dados

        // Método para iniciar a dosimetria, que é acionado quando o botão Iniciar Dosimetria é clicado
        private void btnIniciarDosimetria_Click(object sender, EventArgs e)
        {
            nomeDaTabela = Interaction.InputBox("Nome da dosimetria:", ""); // // Solicita ao usuário que digite o nome da dosimetria com um dialog result
            if (nomeDaTabela == "") { MessageBox.Show("Insira um nome para a dosimetria.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; } // Verifica se o usuário digitou algum nome, se não digitou, exibe uma mensagem de erro
            CriarBase(); // Se digitou, chama o método CriarBase para criar o banco de dados e a tabela com o nome informado
            timerDosimetria.Enabled = true; // Ativa o temporizador da dosimetria, e começa a registrar os dados na tabela criada
        }

        // Método para criar o banco de dados e a tabela
        public void CriarBase()
        {
            string baseDados = Application.StartupPath + @"\BancoDeDados.sdf"; // O caminho do arquivo do banco de dados é direcionado para a pasta origem da aplicação
            string strConnection = @"DataSource = " + baseDados + ";Password = '1234'"; // Define a string de conexão com o banco de dados 
            SqlCeEngine db = new SqlCeEngine(strConnection); // Cria uma instância da classe SqlCeEngine, para criar e gerenciar um banco de dados local
            if (!File.Exists(baseDados)) // Verifica se o arquivo do banco de dados existe 
            {
                db.CreateDatabase(); // Se não existe, cria o banco de dados
            }
            db.Dispose(); // Libera os recursos usados pela instância da classe
            SqlCeConnection conexao = new SqlCeConnection(); // Cria uma instância da classe SqlCeConnection, que representa uma conexão com um banco de dados local
            conexao.ConnectionString = strConnection; // Define a string de conexão da instância da classe SqlCeConnection 
            try
            {
                conexao.Open(); // Abre a conexão com o banco de dados
                SqlCeCommand comando = new SqlCeCommand(); // Cria uma instância, que representa um comando SQL a ser executado no banco de dados
                comando.Connection = conexao; // Define a conexão do comando

                // Define o texto do comando
                comando.CommandText = "CREATE TABLE " + nomeDaTabela + " (id INT NOT NULL PRIMARY KEY, horario NVARCHAR(60), decibeis INT)"; // O comando SQL é para criar uma tabela com o nome informado pelo usuário e com três colunas: id, horario e decibeis 
                comando.ExecuteNonQuery(); // Executa o comando

                labelResultado.Text = "Dosimetria iniciada."; // Exibe uma mensagem na label informando que a dosimetria foi iniciada
            }
            catch (Exception ex) // Se ocorrer algum erro na execução do código
            {
                MessageBox.Show(ex.Message); // Exibe uma mensagem com o erro
            }
            finally
            {
                conexao.Close(); // Independente de ter ocorrido ou não um erro, fecha a conexão com o banco de dados
                AtualizarTabela(); // Chama o método AtualizarTabela para exibir os dados da tabela na dataDosimetria
            }
        }

        // Método para carregar as dosimetrias existentes no banco de dados
        public void CarregarDosimetrias()
        {
            cbTabelasDosimetrias.Items.Clear(); // Limpa os itens do combobox que lista as dosimetrias

            string baseDados = Application.StartupPath + @"\BancoDeDados.sdf";
            string strConection = @"DataSource = " + baseDados + "; Password = '1234'";

            SqlCeConnection conexao = new SqlCeConnection(strConection);

            try
            {
                conexao.Open();

                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = conexao;

                DataTable tabelas = conexao.GetSchema("Tables"); // Obtém um objeto DataTable que contém informações sobre as tabelas existentes no banco de dados usando o método GetSchema

                foreach (DataRow linha in tabelas.Rows) // Faz um loop para percorrer todas as linhas do objeto DataTable
                {
                    string nomedatabela = (string)linha[2]; // Obtém o nome da tabela na terceira coluna do objeto DataTable usando a propriedade ItemArray 
                    cbTabelasDosimetrias.Items.Add(nomedatabela); // Adiciona o nome da tabela ao combobox
                }
            }
            catch (Exception ex) // Caso ocorra algum erro
            {
                labelResultado.Text = ex.Message; // Exibe a mensagem de erro
            }
            finally
            {
                conexao.Close(); // Fecha a conexão
            }
        }

        // Método para atualizar os dados da tabela na dataDosimetria
        public void AtualizarTabela()
        {
            dataDosimetria.Rows.Clear();

            string baseDados = Application.StartupPath + @"\BancoDeDados.sdf";
            string strConnection = @"DataSource = " + baseDados + ";Password = '1234'";

            SqlCeConnection conexao = new SqlCeConnection(strConnection);

            try
            {
                string query = "SELECT * FROM " + nomeDaTabela + ""; // Consulta SQL para selecionar todos os dados da tabela com o nome informado pelo usuário 

                DataTable dados = new DataTable(); // Cria um objeto DataTable que irá armazenar os dados retornados pela consulta SQL 

                SqlCeDataAdapter adaptador = new SqlCeDataAdapter(query, strConnection); // Cria uma instância da classe SqlCeDataAdapter, que representa um conjunto de comandos e uma conexão usados para preencher um objeto DataTable

                conexao.Open(); // Abre a conexão com o banco

                adaptador.Fill(dados); // Preenche o objeto DataTable com os dados retornados pela consulta SQL

                foreach (DataRow linha in dados.Rows) // Para cada linha do dataTable
                {
                    dataDosimetria.Rows.Add(linha.ItemArray); // Preencher o dataGrid da dosimetria
                }
            }
            catch (Exception ex) // Em caso de erro
            {
                dataDosimetria.Rows.Clear(); // Limpar o dataGrid
                MessageBox.Show(ex.Message); // Exibir o erro
            }
            finally
            {
                conexao.Close(); // Fecha a conexão com o banco
            }
        }

        int amostra = 1; // Primeiro valor da coluna amostra da tabela

        // Método para inserir dados na tabela do banco de dados
        public void InserirDadosDosimetria()
        {
            string baseDados = Application.StartupPath + @"\BancoDeDados.sdf";
            string strConection = @"DataSource = " + baseDados + "; Password = '1234'";

            SqlCeConnection conexao = new SqlCeConnection(strConection);

            try
            {
                conexao.Open();

                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = conexao;

                string horario = DateTime.Now.ToLongTimeString().ToString(); // Horário do momento do registro

                comando.CommandText = "INSERT INTO " + nomeDaTabela + " VALUES (" + amostra + ", '" + horario + "', '" + (int)dbMaximo + "')"; // Comando de inserção de dados na tabela criada

                comando.ExecuteNonQuery(); // Execução do comando

                labelResultado.Text = "Registro inserido."; // Linha mostra que valor foi inserido
                comando.Dispose();

                amostra++; // Incrementa a variável 
            }
            catch (Exception ex)
            {
                labelResultado.Text = ex.Message;
            }
            finally
            {
                conexao.Close();
                AtualizarTabela(); // Chama o método para atualizar o Grid
            }
        }

        // Evento do temporizador da dosimetria
        private void timerDosimetria_Tick(object sender, EventArgs e)
        {
            InserirDadosDosimetria();
        }

        // Evento do botão de Finalizar Dosimetria
        private void btnFinalizarDosimetria_Click(object sender, EventArgs e)
        {
            amostra = 1; // Volta o valor da variável amostra para o início
            timerDosimetria.Enabled = false; // Desabilita o temporizador da dosimetria
            CarregarDosimetrias(); // Chama o método para carregar as dosimetrias no banco de dados
            labelResultado.Text = "Dosimetria finalizada."; // Exibe mensagem
        }

        double minimoDecibeis;

        // Método para trocar os dados com base na tabela selecionada
        private void cbTabelasDosimetrias_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataDosimetria.Rows.Clear(); // Limpa o Grid

            string baseDados = Application.StartupPath + @"\BancoDeDados.sdf";
            string strConnection = @"DataSource = " + baseDados + ";Password = '1234'";

            SqlCeConnection conexao = new SqlCeConnection(strConnection);

            try
            {
                string query = "SELECT * FROM " + cbTabelasDosimetrias.Text + ""; // Busca os dados da tabela selecionada

                DataTable dados = new DataTable();

                SqlCeDataAdapter adaptador = new SqlCeDataAdapter(query, strConnection);

                conexao.Open();

                adaptador.Fill(dados);

                foreach (DataRow linha in dados.Rows)
                {
                    dataDosimetria.Rows.Add(linha.ItemArray);
                }

                // Título dosimetria
                lblTituloDosimetria.Text = cbTabelasDosimetrias.Text;

                // Hora início
                query = "SELECT MIN(horario) FROM " + cbTabelasDosimetrias.Text + "";
                SqlCeCommand comando = new SqlCeCommand(query, conexao);
                string horaInicio = comando.ExecuteScalar().ToString();
                lblHoraInicioDosimetria.Text = horaInicio;

                // Hora término
                query = "SELECT MAX(horario) FROM " + cbTabelasDosimetrias.Text + "";
                comando = new SqlCeCommand(query, conexao);
                string horaTermino = comando.ExecuteScalar().ToString();
                lblHoraTerminoDosimetria.Text = horaTermino;

                // Tempo total
                DateTime inicio = DateTime.Parse(lblHoraInicioDosimetria.Text);
                DateTime termino = DateTime.Parse(lblHoraTerminoDosimetria.Text);
                TimeSpan tempototal = termino.Subtract(inicio);
                lblTempoTotalDosimetria.Text = tempototal.ToString();

                // Maior valor decibeis
                query = "SELECT MAX(decibeis) FROM " + cbTabelasDosimetrias.Text + "";
                comando = new SqlCeCommand(query, conexao);
                string maiorValorEncontrado = comando.ExecuteScalar().ToString();
                lblPicoRegistradoDosimetria.Text = maiorValorEncontrado + " dB";

                // Menor valor decibeis (parâmetro pra deixar o gráfico "com zoom"
                query = "SELECT MIN(decibeis) FROM " + cbTabelasDosimetrias.Text + "";
                comando = new SqlCeCommand(query, conexao);
                minimoDecibeis = double.Parse(comando.ExecuteScalar().ToString());

                // Média decibéis
                query = "SELECT AVG(decibeis) FROM " + cbTabelasDosimetrias.Text + "";
                comando = new SqlCeCommand(query, conexao);
                string mediaDecibeis = comando.ExecuteScalar().ToString();
                lblMediaDosimetria.Text = mediaDecibeis + " dB";

                // Plotar graficos
                PlotarGraficosDosimetria();
            }
            catch (Exception ex)
            {
                dataDosimetria.Rows.Clear();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();

            }
        }

        // Evento do botão Atualizar Dosimetrias, que chama o método CarregarDosimetrias novamente
        private void btnAtualizarDosimetrias_Click(object sender, EventArgs e)
        {
            CarregarDosimetrias();
        }

        // Evento do botão Excluir Dosimetria, que deleta a tabela selecionada do banco de dados
        private void btnExcluirDosimetria_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Deseja excluir a dosimetria: {cbTabelasDosimetrias.Text}?", "", MessageBoxButtons.YesNo); // Mensagem de confirmação
            if (dialogResult == DialogResult.Yes) // Caso sim
            {

                string baseDados = Application.StartupPath + @"\BancoDeDados.sdf";
                string strConnection = @"DataSource = " + baseDados + ";Password = '1234'";

                SqlCeConnection conexao = new SqlCeConnection(strConnection);

                try
                {
                    conexao.Open();

                    SqlCeCommand comando = new SqlCeCommand();
                    comando.Connection = conexao;

                    comando.CommandText = "DROP TABLE " + cbTabelasDosimetrias.Text + ""; // Comando de deletar a tabela
                    comando.ExecuteNonQuery();

                    comando.Dispose();

                    MessageBox.Show("Dosimetria excluída do banco de dados!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conexao.Close();
                    AtualizarTabela();
                }
            }
        }

        // Variáveis globais de geração dos gráficos da dosimetria
        List<DateTime> Listahorarios = new List<DateTime>();
        List<double> Listadecibeis = new List<double>();

        double[] faixas = { 30, 40, 50, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 };

        // Listas das faixas de pressão sonora
        List<double> faixa1 = new List<double>();
        List<double> faixa2 = new List<double>();
        List<double> faixa3 = new List<double>();
        List<double> faixa4 = new List<double>();
        List<double> faixa5 = new List<double>();
        List<double> faixa6 = new List<double>();
        List<double> faixa7 = new List<double>();
        List<double> faixa8 = new List<double>();
        List<double> faixa9 = new List<double>();
        List<double> faixa10 = new List<double>();
        List<double> faixa11 = new List<double>();
        List<double> faixa12 = new List<double>();
        List<double> faixa13 = new List<double>();

        List<DateTime> horarios = new List<DateTime>();
        List<double> decibeis = new List<double>();

        // Dose de ruído
        List<double> contagem85 = new List<double>(); // 480
        List<double> contagem86 = new List<double>(); // 420
        List<double> contagem87 = new List<double>(); // 360
        List<double> contagem88 = new List<double>(); // 300
        List<double> contagem89 = new List<double>(); // 270
        List<double> contagem90 = new List<double>(); // 240
        List<double> contagem91 = new List<double>(); // 210
        List<double> contagem92 = new List<double>(); // 180
        List<double> contagem93 = new List<double>(); // 160
        List<double> contagem94 = new List<double>(); // 135
        List<double> contagem95 = new List<double>(); // 120
        List<double> contagem96 = new List<double>(); // 105
        List<double> contagem98 = new List<double>(); // 75 
        List<double> contagem100 = new List<double>(); // 60
        List<double> contagem102 = new List<double>(); // 45
        List<double> contagem104 = new List<double>(); // 35
        List<double> contagem105 = new List<double>(); // 30

        // Método que gera os gráficos
        public void PlotarGraficosDosimetria()
        {
            // Limpa tudo
            Listadecibeis.Clear(); Listahorarios.Clear(); horarios.Clear(); decibeis.Clear();
            faixa1.Clear(); faixa2.Clear(); faixa3.Clear(); faixa4.Clear(); faixa5.Clear(); faixa6.Clear();
            faixa7.Clear(); faixa8.Clear(); faixa9.Clear(); faixa10.Clear(); faixa11.Clear(); faixa12.Clear(); faixa13.Clear();
            formsPlot5.Plot.Clear(); formsPlot6.Plot.Clear();

            contagem85.Clear(); contagem86.Clear(); contagem87.Clear(); contagem88.Clear(); contagem89.Clear(); contagem90.Clear();
            contagem91.Clear(); contagem92.Clear(); contagem93.Clear(); contagem94.Clear(); contagem95.Clear(); contagem96.Clear();
            contagem98.Clear(); contagem100.Clear(); contagem102.Clear(); contagem104.Clear(); contagem105.Clear();

            int i = 0;
            foreach (DataGridViewRow linha in dataDosimetria.Rows) // Percorre o Grid
            {
                horarios.Add(DateTime.ParseExact(linha.Cells[1].Value.ToString(), "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None)); // Adiciona os horários
                decibeis.Add(double.Parse(linha.Cells[2].Value.ToString())); // Adiciona os decibeis

                AdicionarFaixa(decibeis[i]); // Adiciona o valor à faixa de pressão sonora equivalente
                AdicionarDose(decibeis[i]); // Adiciona o valor à lista da dose de ruído equivalente
                i++;
            }

            // Realiza a contagem de quantos valores de decibéis foram registrados em cada faixa de nível de pressão sonora
            double contagemFaixa1 = faixa1.Count();
            double contagemFaixa2 = faixa2.Count();
            double contagemFaixa3 = faixa3.Count();
            double contagemFaixa4 = faixa4.Count();
            double contagemFaixa5 = faixa5.Count();
            double contagemFaixa6 = faixa6.Count();
            double contagemFaixa7 = faixa7.Count();
            double contagemFaixa8 = faixa8.Count();
            double contagemFaixa9 = faixa9.Count();
            double contagemFaixa10 = faixa10.Count();
            double contagemFaixa11 = faixa11.Count();
            double contagemFaixa12 = faixa12.Count();
            double contagemFaixa13 = faixa13.Count();

            // Realiza a contagem de quantos valores de decibéis foram registrados em cada lista de nível de pressão sonora
            double dose85 = contagem85.Count();
            double dose86 = contagem86.Count();
            double dose87 = contagem87.Count();
            double dose88 = contagem88.Count();
            double dose89 = contagem89.Count();
            double dose90 = contagem90.Count();
            double dose91 = contagem91.Count();
            double dose92 = contagem92.Count();
            double dose93 = contagem93.Count();
            double dose94 = contagem94.Count();
            double dose95 = contagem95.Count();
            double dose96 = contagem96.Count();
            double dose98 = contagem98.Count();
            double dose100 = contagem100.Count();
            double dose102 = contagem102.Count();
            double dose104 = contagem104.Count();
            double dose105 = contagem105.Count();

            // Agrupa todas as contagens de faixas em uma lista para plotar no gráfico, como eixo x
            double[] contagens = { contagemFaixa1, contagemFaixa2, contagemFaixa3, contagemFaixa4, contagemFaixa5, contagemFaixa6, contagemFaixa7, contagemFaixa8, contagemFaixa9, contagemFaixa10, contagemFaixa11, contagemFaixa12, contagemFaixa13 };

            double[] plotarDecibeis = decibeis.ToArray();

            // Gráfico de linha (Ruído captado ao longo do tempo)
            double taxaAmostragem = (24 * 60); // Uma amostra a cada minuto
            var plt = formsPlot5.Plot;
            var sig = plt.AddSignal(plotarDecibeis, taxaAmostragem, Color.Tomato);

            DateTime inicio = DateTime.ParseExact(lblHoraInicioDosimetria.Text, "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None);
            sig.OffsetX = inicio.ToOADate();
            plt.XAxis.DateTimeFormat(true);
            plt.YAxis2.SetSizeLimit(min: 30);
            plt.XAxis.Grid(false); // Desabilitar grid vertical
            formsPlot5.Render();

            // Gráfico de colunas (Histograma)
            var plt2 = formsPlot6.Plot;
            double[] posicoes = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            string[] faixas = { "30", "40", "50", "60", "65", "70", "75", "80", "85", "90", "95", "100", "105", ">105" };
            var coluna = plt2.AddBar(contagens, posicoes, Color.Tomato);
            coluna.ShowValuesAboveBars = true;
            plt2.XTicks(posicoes, faixas);
            plt2.XAxis.Grid(false);
            plt2.SetAxisLimits(yMin: 0);
            formsPlot6.Render();

            // Dose de ruído
            double dose = (dose85 / 480) + (dose86 / 420) + (dose87 / 360) + (dose88 / 300) + (dose89 / 270) + (dose90 / 240) + (dose91 / 210) +
                (dose92 / 180) + (dose93 / 160) + (dose94 / 135) + (dose95 / 120) + (dose96 / 105) + (dose98 / 75) + (dose100 / 60) + (dose102 / 45) +
                (dose104 / 35) + (dose105 / 30);
            lblDose.Text = dose.ToString("F2");
            if (dose > 1) // Se a dose estiver acima de 1
            {
                lblDose.ForeColor = Color.Red; // Pinta de vermelho, acima do permitido pela NR-15
            }
            else // Se estiver abaixo ou igual a 1
            {
                lblDose.ForeColor = Color.Green; // Pinta de verde, permitido pela NR-15
            }

        }

        // Método que adiciona o valor a sua devida faixa de pressão sonora
        public void AdicionarFaixa(double decibel)
        {
            if (decibel <= 30 && decibel < 40)
            {
                faixa1.Add(decibel);
            }
            else if (decibel <= 40 && decibel < 50)
            {
                faixa2.Add(decibel);
            }
            else if (decibel <= 50 && decibel < 60)
            {
                faixa3.Add(decibel);
            }
            else if (decibel <= 60 && decibel < 65)
            {
                faixa4.Add(decibel);
            }
            else if (decibel <= 65 && decibel < 70)
            {
                faixa5.Add(decibel);
            }
            else if (decibel <= 70 && decibel < 75)
            {
                faixa6.Add(decibel);
            }
            else if (decibel <= 75 && decibel < 80)
            {
                faixa7.Add(decibel);
            }
            else if (decibel <= 80 && decibel < 85)
            {
                faixa8.Add(decibel);
            }
            else if (decibel <= 85 && decibel < 90)
            {
                faixa9.Add(decibel);
            }
            else if (decibel <= 90 && decibel < 95)
            {
                faixa10.Add(decibel);
            }
            else if (decibel <= 95 && decibel < 100)
            {
                faixa11.Add(decibel);
            }
            else if (decibel <= 100 && decibel < 105)
            {
                faixa12.Add(decibel);
            }
            else if (decibel >= 105)
            {
                faixa13.Add(decibel);
            }

        }

        // Método que adiciona o valor a sua devida lista, para o cálculo da dose de ruído
        public void AdicionarDose(double decibel)
        {
            if (decibel == 85)
            {
                contagem85.Add(decibel);
            }
            else if (decibel == 86)
            {
                contagem86.Add(decibel);
            }
            else if (decibel == 87)
            {
                contagem87.Add(decibel);
            }
            else if (decibel == 88)
            {
                contagem88.Add(decibel);
            }
            else if (decibel == 89)
            {
                contagem89.Add(decibel);
            }
            else if (decibel == 90)
            {
                contagem90.Add(decibel);
            }
            else if (decibel == 91)
            {
                contagem91.Add(decibel);
            }
            else if (decibel == 92)
            {
                contagem92.Add(decibel);
            }
            else if (decibel == 93)
            {
                contagem93.Add(decibel);
            }
            else if (decibel == 94)
            {
                contagem94.Add(decibel);
            }
            else if (decibel == 95)
            {
                contagem95.Add(decibel);
            }
            else if (decibel == 96)
            {
                contagem96.Add(decibel);
            }
            else if (decibel == 98)
            {
                contagem98.Add(decibel);
            }
            else if (decibel == 100)
            {
                contagem100.Add(decibel);
            }
            else if (decibel == 102)
            {
                contagem102.Add(decibel);
            }
            else if (decibel == 104)
            {
                contagem104.Add(decibel);
            }
            else if (decibel == 105)
            {
                contagem105.Add(decibel);
            }
        }

        #endregion

        #region GERAR ARQUIVO PDF

        static BaseFont fonteBase = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false); // fonte base para todos os textos do documento
        public void GerarRelatorioEmPDF()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Salvar Relatório";
            saveDialog.Filter = "PDF (*.pdf)|*.pdf";
            if (saveDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string caminho = saveDialog.FileName;
            // Cálculo da quantidade total de páginas
            int totalPaginas = 1;
            int totalLinhas = dataDosimetria.Rows.Count;
            if (totalLinhas > 6)
            {
                totalPaginas += (int)Math.Ceiling((totalLinhas - 6) / 29F);
            }

            // Configuração do documento PDF
            var pxPorMm = 72 / 25.2F; // Relação entre a qte de pixels por mm (72 pontos por polegada / 25.2 mm em uma polegada)
            var pdf = new Document(PageSize.A4, 15 * pxPorMm, 15 * pxPorMm, 15 * pxPorMm, 20 * pxPorMm); // Margens
            var nomeArquivo = caminho; // Nome do arquivo gerado
            var arquivo = new FileStream(nomeArquivo, FileMode.Create); // Cria um novo arquivo
            var writer = PdfWriter.GetInstance(pdf, arquivo); // Associa o pdf criado ao arquivo criado
            writer.PageEvent = new EventosDePagina(totalPaginas);
            pdf.Open(); // Inicializa o objeto para que ele possa começar a receber conteúdo

            // Adição do título
            var fonteParagrafo = new iTextSharp.text.Font(fonteBase, 32, iTextSharp.text.Font.NORMAL, BaseColor.Black); // Fonte específica para o título
            var titulo = new Paragraph("Relatório da Dosimetria\n", fonteParagrafo);
            titulo.Alignment = Element.ALIGN_LEFT;
            titulo.SpacingAfter = 4;
            pdf.Add(titulo);

            // Subtitulo
            var fonteSubTitulo = new iTextSharp.text.Font(fonteBase, 10, iTextSharp.text.Font.ITALIC, BaseColor.Black); // Fonte específica para o subtítulo
            var subtitulo = new Paragraph("Gerado através do TCC de Lorenzo Bondan\n", fonteSubTitulo);
            subtitulo.Alignment = Element.ALIGN_LEFT;
            pdf.Add(subtitulo);

            // Nome da dosimetria
            var fonteDosimetria = new iTextSharp.text.Font(fonteBase, 10, iTextSharp.text.Font.NORMAL, BaseColor.Black); // Fonte específica para o nome da dosimetria
            var nomeDosimetria = new Paragraph(lblTituloDosimetria.Text + "\n\n", fonteDosimetria);
            nomeDosimetria.Alignment = Element.ALIGN_CENTER;
            pdf.Add(nomeDosimetria);

            // Adição da imagem (logo)
            var caminhoImagem = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icone\\radio-waves.png");
            if (File.Exists(caminhoImagem))
            {
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(caminhoImagem); // Carrega a imagem pra dentro da variavel no formato aceito pelo PDF
                float razaoAlturaLargura = logo.Width / logo.Height;
                float alturaLogo = 62; // Ajusta toda a imagem com base na altura
                float larguraLogo = alturaLogo * razaoAlturaLargura;
                logo.ScaleToFit(larguraLogo, alturaLogo);
                // Posicionamento da imagem
                var margemEsquerda = pdf.PageSize.Width - pdf.RightMargin - larguraLogo;
                var margemTopo = pdf.PageSize.Height - pdf.RightMargin - 54;
                logo.SetAbsolutePosition(margemEsquerda, margemTopo);

                writer.DirectContent.AddImage(logo, false); // Adiciona a imagem no documento
            }

            // Adição do primeiro gráfico
            string Figura1 = "Figura1 " + DateTime.Now.ToString("HH.mm.ss.dd.MM.yyyy") + ".png";
            formsPlot5.Plot.SaveFig("img\\" + Figura1); // Salva o gráfico como imagem
            var caminhoPrimeiroGrafico = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img\\" + Figura1); // Busca a imagem salva do gráfico
            if (File.Exists(caminhoPrimeiroGrafico)) // Se encontrou
            {
                iTextSharp.text.Image PrimeiroGrafico = iTextSharp.text.Image.GetInstance(caminhoPrimeiroGrafico); // Carrega a imagem pra dentro da variavel no formato aceito pelo PDF
                float razaoAlturaLargura = PrimeiroGrafico.Width / PrimeiroGrafico.Height;
                float alturaPrimeiroGrafico = 225; // Ajusta toda a imagem com base na altura
                float larguraPrimeiroGrafico = alturaPrimeiroGrafico * razaoAlturaLargura;
                PrimeiroGrafico.ScaleToFit(larguraPrimeiroGrafico, alturaPrimeiroGrafico);
                // Posicionamento da imagem
                var margemEsquerda = pdf.PageSize.Width - pdf.LeftMargin - larguraPrimeiroGrafico - 54;
                var margemTopo = pdf.PageSize.Height - pdf.RightMargin - 315;
                PrimeiroGrafico.SetAbsolutePosition(margemEsquerda, margemTopo);

                writer.DirectContent.AddImage(PrimeiroGrafico, false); // Adiciona a imagem no documento
            }

            // Adição do segundo gráfico
            string Figura2 = "Figura2 " + DateTime.Now.ToString("HH.mm.ss.dd.MM.yyyy") + ".png";
            formsPlot6.Plot.SaveFig("img\\" + Figura2); // Salva o gráfico como imagem
            var caminhoSegundoGrafico = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img\\" + Figura2); // Busca a imagem salva do gráfico
            if (File.Exists(caminhoSegundoGrafico)) // Se encontrou
            {
                iTextSharp.text.Image SegundoGrafico = iTextSharp.text.Image.GetInstance(caminhoSegundoGrafico); // Carrega a imagem pra dentro da variavel no formato aceito pelo PDF
                float razaoAlturaLargura = SegundoGrafico.Width / SegundoGrafico.Height;
                float alturaPrimeiroGrafico = 225; // ajusta toda a imagem com base na altura
                float larguraPrimeiroGrafico = alturaPrimeiroGrafico * razaoAlturaLargura;
                SegundoGrafico.ScaleToFit(larguraPrimeiroGrafico, alturaPrimeiroGrafico);
                // Posicionamento da imagem
                var margemEsquerda = pdf.PageSize.Width - pdf.LeftMargin - larguraPrimeiroGrafico - 54;
                var margemTopo = pdf.PageSize.Height - pdf.RightMargin - 550;
                SegundoGrafico.SetAbsolutePosition(margemEsquerda, margemTopo);

                writer.DirectContent.AddImage(SegundoGrafico, false); // Adiciona a imagem no documento
            }

            var espaco = new Paragraph("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n", fonteSubTitulo);
            pdf.Add(espaco);

            // Adição de dados da dosimetria
            var fonteDadosDosimetria = new iTextSharp.text.Font(fonteBase, 10, iTextSharp.text.Font.NORMAL, BaseColor.Black);
            var nomeDadoDosimetria = new Paragraph("Hora de início: " + lblHoraInicioDosimetria.Text +
                                                    "  Hora de término: " + lblHoraTerminoDosimetria.Text +
                                                    "  Duração: " + lblTempoTotalDosimetria.Text +
                                                    "  Média: " + lblMediaDosimetria.Text +
                                                    "  Pico: " + lblPicoRegistradoDosimetria.Text +
                                                    "  Dose: " + lblDose.Text + "\n\n", fonteDadosDosimetria);
            nomeDosimetria.Alignment = Element.ALIGN_CENTER;
            pdf.Add(nomeDadoDosimetria);


            // Adição da tabela de dados
            var tabela = new PdfPTable(3);
            float[] largurasColunas = { 0.6f, 2f, 1.5f };
            tabela.SetWidths(largurasColunas);
            tabela.DefaultCell.BorderWidth = 0;
            tabela.WidthPercentage = 100;

            // Adição das células de títulos das colunas
            CriarCelulaTexto(tabela, "Amostra", PdfPCell.ALIGN_CENTER, true);
            CriarCelulaTexto(tabela, "Horário", PdfPCell.ALIGN_CENTER, true);
            CriarCelulaTexto(tabela, "Nível de Pressão Sonora [dB]", PdfPCell.ALIGN_CENTER, true);

            // Adição dos dados da tabela
            foreach (DataGridViewRow linha in dataDosimetria.Rows)
            {
                CriarCelulaTexto(tabela, linha.Cells[0].Value.ToString(), PdfPCell.ALIGN_CENTER);
                CriarCelulaTexto(tabela, linha.Cells[1].Value.ToString(), PdfPCell.ALIGN_CENTER);
                CriarCelulaTexto(tabela, linha.Cells[2].Value.ToString(), PdfPCell.ALIGN_CENTER);
            }
            pdf.Add(tabela);

            pdf.Close();
            arquivo.Close();

            //MessageBox.Show("PDF gerado com sucesso!","",MessageBoxButtons.OK,MessageBoxIcon.Information);

            // Limpa a pasta com as imagens geradas
            string pasta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img\\");
            DirectoryInfo diretorio = new DirectoryInfo(pasta);
            FileInfo[] arquivos = diretorio.GetFiles();
            foreach (FileInfo figura in arquivos)
            {
                figura.Delete();
            }

            // Abre o PDF no visualizador padrão
            var caminhoPDF = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nomeArquivo);
            if (File.Exists(caminhoPDF))
            {
                Process.Start(new ProcessStartInfo() // usando o terminal mas não mostrando janela
                {
                    Arguments = $"/c start {caminhoPDF}",
                    FileName = "cmd.exe",
                    CreateNoWindow = true
                });
            }
        }

        // Método de customização da célula escrita no PDF
        static void CriarCelulaTexto(PdfPTable tabela, string texto, int alinhamentoHorz = PdfPCell.ALIGN_LEFT, bool negrito = false, bool italico = false, int tamanhoFonte = 12, int alturaCelula = 25)
        {
            int estilo = iTextSharp.text.Font.NORMAL;
            if (negrito && italico)
            {
                estilo = iTextSharp.text.Font.BOLDITALIC;
            }
            else if (negrito)
            {
                estilo = iTextSharp.text.Font.BOLD;
            }
            else if (italico)
            {
                estilo = iTextSharp.text.Font.ITALIC;
            }
            var fonteCelula = new iTextSharp.text.Font(fonteBase, tamanhoFonte, estilo, BaseColor.Black);

            // Zebra, linhas com cores alternadas
            var bgColor = iTextSharp.text.BaseColor.White;
            if (tabela.Rows.Count % 2 == 1)
            {
                bgColor = new BaseColor(0.95f, 0.95f, 0.95f);
            }

            var celula = new PdfPCell(new Phrase(texto, fonteCelula));
            celula.HorizontalAlignment = alinhamentoHorz;
            celula.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            celula.Border = 0;
            celula.BorderWidthBottom = 1;
            celula.FixedHeight = alturaCelula;
            celula.PaddingBottom = 5;
            celula.BackgroundColor = bgColor;
            tabela.AddCell(celula);
        }

        // Evento do botão Gerar PDF, que chama o método
        private void btnGerarPDF_Click(object sender, EventArgs e)
        {
            GerarRelatorioEmPDF();
        }

        #endregion
    }
}
