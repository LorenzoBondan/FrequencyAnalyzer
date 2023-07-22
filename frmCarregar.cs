using System;
using System.Windows.Forms;

namespace TCC1
{
    public partial class frmCarregar : Form
    {
        int contador = 0;
        frmLogin f = new frmLogin();
        public frmCarregar()
        {
            f.Show();
            f.Hide();
            InitializeComponent();

            label1.Parent = pbFundo;
            label2.Parent = pbFundo;
            btnEntrar.Parent = pbFundo;
            lblPorcentagem.Parent = pbFundo;

            btnEntrar.Visible = false;

            label2.Visible = false;
            text = "Bem Vindo " + Environment.UserName + "!";
            len = text.Length;
            timer1.Start();
            timer3.Start();
        }

        int counter = 0;
        int len = 0;
        string text;

        private void timer1_Tick(object sender, EventArgs e)
        {
            ++counter;

            if (counter > len)
            {
                timer1.Stop();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            progressBar.Value = contador;
            lblPorcentagem.Text = contador.ToString() + "%";
            contador++;

            if (contador == 100)
            {
                lblPorcentagem.Text = "100%";
                timer3.Stop();
                label2.Visible = true;
                label1.Visible = false;
                btnEntrar.Visible = true;
            }
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            f.ShowDialog();
            Close();
            Dispose();
        }
    }
}
