using System;
using System.Data.SqlServerCe;
using System.IO;
using System.Windows.Forms;


namespace TCC1
{
    public partial class frmLogin : Form
    {
        private Login LoginInfo;

        public string EnviarLogin_Usuario()
        {
            return LoginInfo.Usuario.ToString();
        }

        public frmLogin()
        {
            InitializeComponent();
            CriarBase();
        }

        private void txtSenha_KeyUp(object sender, KeyEventArgs e)
        {
            txtSenha.PasswordChar = '*';
        }

        public void CriarBase()
        {
            string baseDados = Application.StartupPath + @"\BancoDeDadosLogin.sdf";
            string strConnection = @"DataSource = " + baseDados + ";Password = '1234'";
            SqlCeEngine db = new SqlCeEngine(strConnection);
            if (!File.Exists(baseDados))
            {
                db.CreateDatabase();
            }
            db.Dispose();
            SqlCeConnection conexao = new SqlCeConnection();
            conexao.ConnectionString = strConnection;
            try
            {
                conexao.Open();
                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = conexao;

                comando.CommandText = "CREATE TABLE tabelaLogin (usuario NVARCHAR(60) NOT NULL PRIMARY KEY, senha NVARCHAR(60), adm NVARCHAR(3))";
                comando.ExecuteNonQuery();

                labelResultado.Text = "Tabela criada.";
            }
            catch (Exception ex)
            {
                //labelResultado.Text = ex.Message;
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnCadastrarse_Click(object sender, EventArgs e)
        {
            string baseDados = Application.StartupPath + @"\BancoDeDadosLogin.sdf";
            string strConection = @"DataSource = " + baseDados + "; Password = '1234'";

            SqlCeConnection conexao = new SqlCeConnection(strConection);

            try
            {
                conexao.Open();

                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = conexao;

                string usuario = txtUsuario.Text;
                string senha = txtSenha.Text;
                string adm = "n";

                comando.CommandText = "INSERT INTO tabelaLogin VALUES ('" + usuario + "', '" + senha + "', '" + adm + "')";

                comando.ExecuteNonQuery();

                labelResultado.Text = "Usuário cadastrado.";
                comando.Dispose();
            }
            catch (Exception ex)
            {
                labelResultado.Text = ex.Message;
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string UsuarioDigitado = txtUsuario.Text;
            string SenhaDigitada = txtSenha.Text;

            string baseDados = Application.StartupPath + @"\BancoDeDadosLogin.sdf";
            string strConnection = @"DataSource = " + baseDados + ";Password = '1234'";

            SqlCeConnection conexao = new SqlCeConnection(strConnection);
            conexao.Open();

            try
            {
                // ver se o usuário está cadastrado
                string query1 = "SELECT usuario FROM tabelaLogin WHERE usuario = '" + UsuarioDigitado + "'  ";
                SqlCeCommand comando1 = new SqlCeCommand(query1, conexao);
                try
                {
                    string usuarioBanco = comando1.ExecuteScalar().ToString();
                }
                catch (System.NullReferenceException)
                {
                    MessageBox.Show("Usuário não cadastrado.");
                    return;
                }

                string query2 = "SELECT senha FROM tabelaLogin WHERE usuario = '" + UsuarioDigitado + "'  ";

                SqlCeCommand comando = new SqlCeCommand(query2, conexao);
                string senhaBanco = comando.ExecuteScalar().ToString();

                if (senhaBanco != SenhaDigitada)
                {
                    txtSenha.Text = "";
                    txtSenha.Focus();
                    MessageBox.Show("Senha incorreta.");
                    return;
                }

                // adm ou não
                string query3 = "SELECT adm FROM tabelaLogin WHERE usuario = '" + UsuarioDigitado + "'  ";
                SqlCeCommand comando3 = new SqlCeCommand(query3, conexao);
                string adm = comando3.ExecuteScalar().ToString();

                LoginInfo = new Login(usuario: UsuarioDigitado, senha: SenhaDigitada, adm: adm);

                MessageBox.Show("Logado com sucesso.","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                frmMenu f = new frmMenu(LoginInfo.Usuario, LoginInfo.Adm);
                f.ShowDialog();
                Hide();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

    }
}
