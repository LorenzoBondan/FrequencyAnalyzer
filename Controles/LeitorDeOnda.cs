using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCC1.Controles
{
    class LeitorDeOnda : UserControl
    {
        private Container components;

        private WaveStream waveStream;

        private int samplesPerPixel = 128;

        private long startPosition;

        private int bytesPerSample;

        public WaveStream WaveStream
        {
            get
            {
                return waveStream;
            }
            set
            {
                waveStream = value;
                if (waveStream != null)
                {
                    bytesPerSample = waveStream.WaveFormat.BitsPerSample / 8 * waveStream.WaveFormat.Channels;
                }

                Invalidate();
            }
        }

        public int SamplesPerPixel
        {
            get
            {
                return samplesPerPixel;
            }
            set
            {
                samplesPerPixel = value;
                Invalidate();
            }
        }

        public long StartPosition
        {
            get
            {
                return startPosition;
            }
            set
            {
                startPosition = value;
            }
        }

        public LeitorDeOnda()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        public void FitToScreen(int largura)
        {
            if (waveStream == null) return;

            int samples = (int)(waveStream.Length / bytesPerSample);
            startPosition = 0;
            SamplesPerPixel = samples / largura;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            if (waveStream != null)
            {
                waveStream.Position = 0;
                int bytesRead;
                byte[] waveData = new byte[samplesPerPixel * bytesPerSample];
                waveStream.Position = startPosition + (e.ClipRectangle.Left * bytesPerSample * samplesPerPixel);

                for (float x = e.ClipRectangle.X; x < e.ClipRectangle.Right; x += 1)
                {

                    short low = 0;
                    short high = 0;
                    bytesRead = waveStream.Read(waveData, 0, samplesPerPixel * bytesPerSample);
                    if (bytesRead == 0)
                        break;
                    for (int n = 0; n < bytesRead; n += 2)
                    {
                        short sample = BitConverter.ToInt16(waveData, n);
                        if (sample < low) low = sample;
                        if (sample > high) high = sample;
                    }
                    float lowPercent = ((((float)low) - short.MinValue) / ushort.MaxValue);
                    float highPercent = ((((float)high) - short.MinValue) / ushort.MaxValue);
                    e.Graphics.DrawLine(Pens.DarkOrchid, x, this.Height * lowPercent, x, this.Height * highPercent);
                }


            }

            base.OnPaint(e);

        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
    }
}
