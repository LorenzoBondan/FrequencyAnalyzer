using System;
using System.Windows.Forms;

namespace TCC1
{
    public partial class frmMenu : Form
    {
        string nomedousuario;
        string adm;
        public frmMenu(string NomeDoUsuario, string Administrador)
        {
            InitializeComponent();
            nomedousuario = NomeDoUsuario;
            adm = Administrador;
            if (Administrador == "s"){btnAdm.Visible = true;}
        }

        private void btnGraduacao_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Software sem fins comerciais desenvolvido para \n" +
                "obtenção do grau de Bacharel em Engenharia Mecânica do \n" +
                "Centro de Ciências Exatas e Tecnologia da \n" +
                "Universidade de Caxias do Sul.");
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desenvolvido por Lorenzo Bondan.\n\nVersão 1.0","Informações",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            frmPrincipal f = new frmPrincipal(nomedousuario, adm );
            f.Show();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            frmSimulador f = new frmSimulador();
            f.Show();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            frmCarregarAudio f = new frmCarregarAudio();
            f.Show();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("www.facebook.com\\ucsoficial");
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("www.instagram.com\\ucs_oficial");
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("www.twitter.com\\ucs_oficial");
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            frmAdministradores f = new frmAdministradores();
            f.Show();
        }


    }
}
