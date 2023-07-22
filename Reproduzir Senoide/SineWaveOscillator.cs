using NAudio.Wave;
using System;

namespace TCC1.Reproduzir_Senoide
{
    class SineWaveOscillator : WaveProvider16
    {
        public double Frequency { set; get; } // em inglês pois herda o mesmo atributo da classe WaveProvider16
        public short Amplitude { set; get; }

        public SineWaveOscillator(int taxaAmostragem) : base(taxaAmostragem, 1) { }

        double theta = 0;

        public override int Read(short[] vetor, int offset, int taxaAmostragem) // override é pq herda do metodo Read do WaveProvider16
        { // vetor = byte[13230] | offset = 0 | taxaAmostragem = vetor.Count / 2 

            for (int i = 0; i < taxaAmostragem; i++) // sampleCount = 6615
            {
                vetor[i] = (short)(Amplitude * Math.Sin(theta)); // somar as amplitudes e frequencias aqui dentro
                theta += 2 * Math.PI * Frequency / WaveFormat.SampleRate; // angulo de fase += 2*PI*f / 44100 
                
            }
            return taxaAmostragem;
        }
    }

}
