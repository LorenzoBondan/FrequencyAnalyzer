using System;
using System.Buffers;
using System.Numerics;

namespace TCC1
{
    public static class TransformadaDeFourier
    {
        // #RETORNO -> verdadeiro ou falso
        // Verifica se o vetor possui um número de amostras que é potência de 2
        public static bool EhPotenciaDeDois(int x)
        {
            bool teste = ((x & (x - 1)) == 0) && (x > 0); // as potências de 2 tem apenas um bit com valor 1
            // A expressão (x - 1) é responsável por transformar o bit mais à direita com valor 1 em 0 e todos os bits à direita dele em 1. O operador & realiza uma operação de "AND" entre x e (x - 1). Essa operação resulta em zero se x for uma potência de dois, porque o único bit 1 em x será anulado pela operação.
            // Se o resultado for zero, significa que x tem apenas um bit 1 e é, portanto, uma potência de dois.
            return teste;
        }

        // #RETORNO -> valor inteiro
        // Inverte o vetor de bits inserido
        private static int FuncaoBitReverso(int valor, int valorMaximo)
        {
            int contagemMaxima = (int)(Math.Log(valorMaximo, 2)); // Calcula o número máximo de bits necessários para representar o valor máximo. Função logarítmica base 2, que retorna o expoente ao qual a base (neste caso, 2) deve ser elevada para produzir o valor máximo.
            // Como os números inteiros são representados em binário (base 2), o número de bits necessários para representar um número é igual ao expoente mais um. Por exemplo, para representar o número 7 em binário, precisamos de 3 bits (111 em binário). O logaritmo base 2 de 7 é aproximadamente 2,8, então arredondando para baixo e adicionando um, obtemos 3 bits.
            int saida = valor; // Inicializa a variável de saída com o valor de entrada.
            int contagem = contagemMaxima - 1; // Inicializa a variável de contagem com o número máximo de bits menos um.

            valor >>= 1; // Desloca o valor de entrada um bit para a direita.

            while (valor > 0) // Enquanto o valor de entrada for maior que zero, o loop continuará.
            {
                saida = (saida << 1) | (valor & 1); // Desloca a variável de saída um bit para a esquerda e faz uma operação OR bit a bit com o último bit do valor de entrada.
                contagem -= 1; // Decrementa a variável de contagem em um. ( -1 pois como o primeiro bit do valor de entrada já foi processado antes do início do loop (valor >>== 1), o loop precisa ser executado uma vez a menos)
                valor >>= 1; // Desloca o valor de entrada um bit para a direita novamente.
            }

            return (saida << contagem) & ((1 << contagemMaxima) - 1); // Desloca a variável de saída para a esquerda pelo número restante de bits e faz uma operação AND bit a bit com uma máscara para garantir que apenas os bits válidos sejam retornados.
        }

        // #MÉTODO
        // Transformada de Fourier
        public static void FFT(Span<Complex> amostras)
        {
            if (amostras.Length == 0) // verifica se a entrada é vazia
            {
                throw new ArgumentException("A amostragem não pode estar vazia."); // lança uma exceção se for
            }

            if (!EhPotenciaDeDois(amostras.Length)) // verifica se o comprimento da entrada é uma potência de 2
            {
                throw new ArgumentException("O comprimento do vetor de amostragem precisa ser uma potência de 2."); // lança uma exceção se não for
            }

            // Passou!

            for (int x = 1; x < amostras.Length; x++) // Para cada elemento do vetor de entrada, exceto o primeiro
            {
                int y = FuncaoBitReverso(x, amostras.Length); // Calcula o índice invertido dos bits do índice atual

                if (y > x) // Se o índice invertido é maior que o índice atual
                {
                    (amostras[y], amostras[x]) = (amostras[x], amostras[y]); // Troca os elementos nas posições x e y do vetor de entrada
                }
                    
            }

            // Essa parte do código implementa a reordenação dos dados de entrada de acordo com a ordem inversa dos bits dos índices.
            // Isso é necessário para aplicar o algoritmo da FFT de Cooley-Tukey, que é um dos algoritmos mais comuns para calcular a transformada discreta de Fourier (DFT).
            // O algoritmo da FFT de Cooley-Tukey reduz a complexidade computacional da DFT de O(N^2) para O(N log N), onde N é o número de pontos da entrada.
            // A ideia do algoritmo é decompor a DFT de um tamanho arbitrário em DFTs menores de tamanhos fatores, recursivamente, até chegar em DFTs de tamanho 2 ou 1.
            // A reordenação dos dados facilita essa decomposição, pois permite combinar as partes pares e ímpares do vetor usando a fórmula da borboleta. 

            // separando os elementos pares e ímpares
            for (int i = 1; i <= amostras.Length / 2; i *= 2) // Para cada estágio da FFT, começando com blocos de tamanho 2 e dobrando a cada iteração
            {
                double mult1 = -1 * (Math.PI / i); // Calcula o ângulo negativo da raiz de unidade complexa para o tamanho do bloco atual

                for (int j = 0; j < amostras.Length; j += (i * 2)) // Para cada bloco do estágio atual, avançando dois blocos a cada iteração
                {
                    for (int k = 0; k < i; k++) // Para cada par de elementos dentro do bloco, avançando um elemento a cada iteração
                    {
                        int indicePar = j + k; // Índice do elemento par
                        int indiceImpar = j + k + i; // Índice do elemento ímpar

                        Complex w = new Complex(Math.Cos(mult1 * k), Math.Sin(mult1 * k)); // calcula a parte real (cosseno) e a parte imaginária (seno) da raiz de unidade complexa "w" para a posição atual das amostras dentro do intervalo de combinação. Essa raiz de unidade complexa é então utilizada na combinação das partes pares e ímpares durante o cálculo da Transformada de Fourier.
                        w *= amostras[indiceImpar];

                        // combinando as partes pares e ímpares
                        amostras[indiceImpar] = amostras[indicePar] - w; // Calcula X[q] usando a fórmula da borboleta: X[q] = X[p] - W_N^n * X[q]
                        amostras[indicePar] += w; // Calcula X[p] usando a fórmula da borboleta: X[p] = X[p] + W_N^n * X[q]
                    }
                }
            }
            // !!!
            // Essa parte do código implementa a combinação das partes pares e ímpares do vetor usando a fórmula da borboleta.
            // A fórmula da borboleta é uma expressão que relaciona duas DFTs de tamanho N/2 com uma DFT de tamanho N, onde N é uma potência de 2.
            // A fórmula é dada por: X[k] = E[k] + W_N^k * O[k], onde X[k] é o k-ésimo elemento da DFT de tamanho N, E[k] é o k-ésimo elemento da DFT par de tamanho N/2, O[k] é o k-ésimo elemento da DFT ímpar de tamanho N/2, e W_N^k é a k-ésima raiz de unidade complexa de ordem N.
            // A fórmula da borboleta permite calcular a DFT de um tamanho arbitrário a partir de DFTs menores, reduzindo o número de operações necessárias. 

        }

        // #RETORNO -> vetor complexo (parte real , parte imaginária)
        // Calcula a Transformada de Fourier discreta 1D usando o algoritmo FFT
        public static Complex[] FFT(double[] entrada)
        {

            if (entrada.Length == 0) // Verifica se a entrada é vazia
            {
                throw new ArgumentException("A entrada não pode ser vazia."); // Lança uma exceção se for
            }

            if (!EhPotenciaDeDois(entrada.Length)) // Verifica se o comprimento da entrada é uma potência de 2
            {
                throw new ArgumentException("O comprimento do vetor de entrada tem que ser uma potência de 2."); // Lança uma exceção se não for
            }

            Complex[] amostras = TransformarEmComplexo(entrada); // Converte a entrada em um vetor de números complexos com parte imaginária zero
            FFT(amostras); // Chama o método acima para aplicar o algoritmo da FFT no vetor de números complexos
            return amostras; // Retorna o vetor transformado
        }

        // #RETORNO -> vetor double
        // Calcula a frequência de amostragem para cada ponto em uma FFT
        // Taxa de amostragem (Hz) do sinal original
        // Número de pontos a serem gerados (normalmente o comprimento da FFT)
        // Se as frequências são ou não para uma FFT unilateral(contendo apenas números reais)
        public static double[] FFTfreq(double taxaDeAmostragem, int numeroDePontos, bool unilateral = true)
        {
            double[] freqs = new double[numeroDePontos]; // Vetor de saída

            if (unilateral == true) // Se a FFT é unilateral
            {
                double fftPeriodoHz = taxaDeAmostragem / (numeroDePontos - 1) / 2; // Período da frequência em Hz

                // freqs começam em 0 e se aproximam de maxFreq
                for (int i = 0; i < numeroDePontos; i++) // Para cada índice do vetor de saída
                {
                    freqs[i] = i * fftPeriodoHz; // Calcula a frequência correspondente ao índice i
                }
                    
                return freqs; // Retorna o vetor de frequências
            }
            else // Se a FFT é bilateral
            {
                double fftPeriodHz = taxaDeAmostragem / numeroDePontos; // Período da frequência em Hz

                // primeira metade: freqs começam em 0 e se aproximam de maxFreq
                int primeiraMetade = numeroDePontos / 2;
                for (int i = 0; i < primeiraMetade; i++) // Para cada índice do vetor de saída até a metade
                {
                    freqs[i] = i * fftPeriodHz; // Calcula a frequência correspondente ao índice i
                }
                    
                // segunda metade: então comece em -maxFreq e se aproxime de 0
                for (int i = primeiraMetade; i < numeroDePontos; i++) // Para cada índice do vetor de saída da metade até o final
                {
                    freqs[i] = -(numeroDePontos - i) * fftPeriodHz; // Calcula a frequência correspondente ao índice i
                }
                    
                return freqs; // Retorna o vetor de frequências
            }

            //!!! 

            // Esse método calcula as frequências correspondentes aos pontos da FFT, dado o número de pontos e a taxa de amostragem do sinal original.
            // A taxa de amostragem é o número de amostras por segundo que foram coletadas do sinal original.
            // O número de pontos é o tamanho do vetor que contém os valores da FFT.
            // A FFT pode ser unilateral ou bilateral, dependendo se o sinal original é real ou complexo.
            // A FFT unilateral contém apenas as frequências positivas, enquanto a FFT bilateral contém as frequências positivas e negativas.
            // O vetor de saída desse método contém as frequências em Hz que correspondem aos índices do vetor da FFT.

        }

        // #RETORNO -> vetor double
        // Calcula a frequência de amostragem para cada ponto em uma FFT
        // Taxa de amostragem (Hz) do sinal original
        // Vetor FFT para a qual as frequências devem ser geradas
        // Se as frequências são ou não para uma FFT unilateral (contendo apenas números reais)
        public static double[] FFTfreq(double taxaDeAmostragem, double[] fft, bool unilateral = true)
        {
            return FFTfreq(taxaDeAmostragem, fft.Length, unilateral); // Chama o método anterior com o comprimento do vetor fft como parâmetro

            // Esse método é uma sobrecarga do método anterior, que permite passar um vetor fft em vez do número de pontos.
            // O vetor fft contém os valores da transformada discreta de Fourier do sinal original.
            // O método retorna as mesmas frequências que o método anterior, mas usa o comprimento do vetor fft como o número de pontos.
        }

        // Cria uma matriz de dados complexos, dado o componente real
        public static Complex[] TransformarEmComplexo(double[] real)
        {
            Complex[] complexo = new Complex[real.Length]; // Vetor de saída
            TransformarEmComplexo(complexo, real); // Chama o método abaixo para preencher o vetor de saída com os valores complexos correspondentes aos valores reais
            return complexo; // Retorna o vetor de saída

            // Esse método cria um vetor de números complexos a partir de um vetor de números reais, atribuindo zero à parte imaginária de cada número complexo.
            // Isso é útil para converter um sinal real em um sinal complexo, que pode ser usado na FFT.
        }

        // #MÉTODO
        // Insere um vetor real e transforma o mesmo em complexo
        public static void TransformarEmComplexo(Span<Complex> complexo, Span<double> real)
        {
            if (complexo.Length != real.Length) // Verifica se os vetores têm o mesmo comprimento
            {
                throw new ArgumentOutOfRangeException("Os comprimentos dos vetores real e complexo devem ser iguais."); // Lança uma exceção se não tiverem
            }
                
            for (int i = 0; i < real.Length; i++) // Para cada elemento do vetor real
            {
                complexo[i] = new Complex(real[i], 0); // Cria um número complexo com parte real igual ao elemento do vetor real e parte imaginária zero e atribui ao elemento correspondente do vetor complexo
            }

            // Esse método é uma sobrecarga do método anterior, que permite passar um span de números complexos e um span de números reais em vez de vetores.
            // Um span é uma estrutura que representa uma fatia contígua de memória, que pode ser usada para manipular dados sem alocação adicional.
            // Esse método é útil para operar sobre partes de vetores sem criar cópias desnecessárias.

        }

        // #RETORNO -> vetor complexo
        // Calcula a Transformada de Fourier discreta 1D usando o algoritmo FFT (real)
        public static Complex[] RFFT(double[] entrada)
        {
            if (entrada.Length == 0) // Verifica se a entrada é vazia
            {
                throw new ArgumentException("A entrada não pode ser vazia."); // Lança uma exceção se for
            }

            if (!EhPotenciaDeDois(entrada.Length)) // Verifica se o comprimento da entrada é uma potência de 2
            {
                throw new ArgumentException("O comprimento do vetor de entrada tem que ser uma potência de 2.");// Lança uma exceção se não for 
            }

            Complex[] amostrasReais = new Complex[entrada.Length / 2 + 1]; // Vetor de saída com metade do comprimento da entrada mais um
            RFFT(amostrasReais, entrada); // Chama o método abaixo para preencher o vetor de saída com os valores da FFT real
            return amostrasReais; // Retorna o vetor de saída

            // Esse método calcula a transformada discreta de Fourier de um sinal real usando o algoritmo FFT.
            // A FFT real é uma versão otimizada da FFT que aproveita a simetria dos sinais reais para reduzir o número de operações e o tamanho da saída.
            // A FFT real produz apenas metade dos coeficientes da FFT complexa, pois os outros são redundantes.
            // A FFT real pode ser obtida a partir da FFT complexa usando as propriedades de conjugação e paridade dos números complexos.

        }

        // #MÉTODO
        // Calcula a Transformada de Fourier discreta 1D usando o algoritmo FFT (real)
        // destino -> Localização dos resultados na memória (deve ser igual ao comprimento de entrada / 2 + 1)
        // entrada -> deve ser uma matriz com comprimento que é uma potência de 2
        public static void RFFT(Span<Complex> destino, Span<double> entrada)
        {
            if (!EhPotenciaDeDois(entrada.Length)) // Verifica se o comprimento da entrada é uma potência de 2
            {
                throw new ArgumentException("O comprimento do vetor de entrada tem que ser uma potência de 2."); // Lança uma exceção se não for
            }

            if (destino.Length != entrada.Length / 2 + 1) // Verifica se o comprimento do vetor destino é igual ao comprimento da entrada dividido por 2 mais 1
            {
                throw new ArgumentException("O comprimento do vetor de destino deve ser igual ao comprimento de entrada / 2 + 1"); // Lança uma exceção se não for
            }
                
            Complex[] temp = ArrayPool<Complex>.Shared.Rent(entrada.Length); // Aluga uma matriz temporária do pool de matrizes compartilhadas

            try
            {
                Span<Complex> buffer = temp; // Cria um span a partir da matriz temporária
                TransformarEmComplexo(buffer, entrada); // Converte a entrada em um vetor de números complexos com parte imaginária zero
                FFT(buffer); // Aplica o algoritmo da FFT no vetor de números complexos
                buffer.Slice(0, destino.Length).CopyTo(destino); // Copia a primeira metade do vetor transformado para o vetor destino
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível calcular.", ex); // Lança uma exceção se ocorrer algum erro
            }
            finally
            {
                ArrayPool<Complex>.Shared.Return(temp);// Retorna a matriz temporária para o pool de matrizes compartilhadas
            }

            // Esse método é uma sobrecarga do método anterior, que permite passar um span de números complexos e um span de números reais em vez de vetores.
            // Um span é uma estrutura que representa uma fatia contígua de memória, que pode ser usada para manipular dados sem alocação adicional.
            // Esse método usa um pool de matrizes compartilhadas para evitar alocação desnecessária de memória e melhorar o desempenho.
            // Um pool de matrizes compartilhadas é um conjunto de matrizes pré-alocadas que podem ser alugadas e devolvidas por diferentes partes do código.
        }

        // RETORNO -> vetor double
        // FFT em escala linear
        public static double[] FFTLinear(double[] entrada)
        {
            double[] saida = new double[entrada.Length / 2 + 1]; // Vetor pela metade pra não espelhar 1024 -> 513, vetor de saída com metade do comprimento da entrada mais um
            FFTLinear(saida, entrada); // Chama o método abaixo para preencher o vetor de saída com os valores da FFT em escala linear
            return saida; // Retorna o vetor de saída

            // Esse método calcula a FFT em escala linear de um sinal real, que é uma forma de normalizar os valores da FFT para que sejam comparáveis entre diferentes sinais.
            // A FFT em escala linear divide os valores da FFT pelo comprimento da entrada e multiplica por 2 os valores que correspondem às frequências positivas e negativas combinadas.
            // Isso faz com que a soma dos quadrados dos valores da FFT em escala linear seja igual à potência média do sinal original.

        }

        // #MÉTODO
        // Calcula a FFT em escala linear
        public static void FFTLinear(Span<double> destino, Span<double> entrada)
        {

            if (!EhPotenciaDeDois(entrada.Length)) // Verifica se o comprimento da entrada é uma potência de 2
            {
                throw new ArgumentException("O comprimento do vetor de entrada tem que ser uma potência de 2."); // Lança uma exceção se não for
            }

            var temp = ArrayPool<Complex>.Shared.Rent(destino.Length); // Aluga uma matriz temporária do pool de matrizes compartilhadas
            // arraypool de números complexos -> complexos | complexos m_real | complexos m_imaginário | complexos.real | complexos.imaginário | complexos.magnitude | complexos.fase
            try
            {
                var amostra = temp.AsSpan(0, destino.Length); // Cria um span a partir da matriz temporária

                // Primeiro, calcula a FFT
                RFFT(amostra, entrada);

                // Primeiro ponto não é duplicado
                destino[0] = amostra[0].Magnitude / entrada.Length; // Divide a magnitude do primeiro ponto da FFT pelo comprimento da entrada e atribui ao primeiro ponto do vetor destino

                // Os pontos seguintes são duplicados para contabilizar as frequências positivas e negativas combinadas
                for (int i = 1; i < amostra.Length; i++) // Para cada ponto da FFT, exceto o primeiro
                {
                    destino[i] = 2 * amostra[i].Magnitude / entrada.Length; // Multiplica por 2 e divide pela magnitude do ponto da FFT pelo comprimento da entrada e atribui ao ponto correspondente do vetor destino
                }
                    
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível calular", ex); // Lança uma exceção se ocorrer algum erro
            }
            finally
            {
                ArrayPool<Complex>.Shared.Return(temp); // Retorna a matriz temporária para o pool de matrizes compartilhadas
            }

            // Esse método é uma sobrecarga do método anterior, que permite passar um span de números reais em vez de um vetor.
            // Um span é uma estrutura que representa uma fatia contígua de memória, que pode ser usada para manipular dados sem alocação adicional.
            // Esse método usa um pool de matrizes compartilhadas para evitar alocação desnecessária de memória e melhorar o desempenho.
            // Um pool de matrizes compartilhadas é um conjunto de matrizes pré-alocadas que podem ser alugadas e devolvidas por diferentes partes do código.

        }

        // #RETORNO -> vetor double
        // FFT em escala logaritmica
        public static double[] FFTLogaritmica(double[] entrada)
        {
            if (!EhPotenciaDeDois(entrada.Length)) // Verifica se o comprimento do vetor de entrada é uma potência de 2
            {
                throw new ArgumentException("O comprimento do vetor de entrada tem que ser uma potência de 2."); // Lança uma exceção se o comprimento não for uma potência de 2
            }

            double[] saida = FFTLinear(entrada); // Chama a função FFTLinear, que calcula a transformada discreta de Fourier (DFT) usando o algoritmo FFT

            for (int i = 0; i < saida.Length; i++) // Faz um loop para percorrer todos os elementos do vetor de saída
            {
                saida[i] = 2 * 10 * Math.Log10(saida[i]); // Aplica uma transformação logarítmica aos elementos do vetor de saída, multiplicando por 20 e aplicando o logaritmo na base 10
                // Essa transformação é usada para obter a magnitude em decibéis (dB) dos coeficientes da DFT, que representam as amplitudes das frequências do sinal de entrada
            }

            return saida; // Retorna o vetor de saída transformado
        }

        // #MÉTODO
        public static void FFTLogaritmica(Span<double> destino, double[] entrada)
        {
            if (!EhPotenciaDeDois(entrada.Length)) // Verifica se o comprimento do vetor de entrada é uma potência de 2
            {
                throw new ArgumentException("O comprimento do vetor de entrada tem que ser uma potência de 2."); // Lança uma exceção se o comprimento não for uma potência de 2
            }

            FFTLinear(destino, entrada); // Chama a função FFTLinear, que calcula a transformada discreta de Fourier (DFT) usando o algoritmo FFT

            for (int i = 0; i < destino.Length; i++) // Faz um loop para percorrer todos os elementos do objeto Span<double>
            {
                destino[i] = 2 * 10 * Math.Log10(destino[i]); // Aplica uma transformação logarítmica aos elementos do objeto Span<double>, multiplicando por 20 e aplicando o logaritmo na base 10
                // Essa transformação é usada para obter a magnitude em decibéis (dB) dos coeficientes da DFT, que representam as amplitudes das frequências do sinal de entrada
            }

        }

    }
}
