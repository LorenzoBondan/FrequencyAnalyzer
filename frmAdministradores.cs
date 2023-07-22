using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Windows.Forms;

namespace TCC1
{
    public partial class frmAdministradores : Form
    {
        public frmAdministradores()
        {
            InitializeComponent();

            AtualizarTabela();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void AtualizarTabela()
        {
            dataAdministradores.Rows.Clear();

            string baseDados = Application.StartupPath + @"\BancoDeDadosLogin.sdf";
            string strConnection = @"DataSource = " + baseDados + "; Password = '1234'";

            SqlCeConnection conexao = new SqlCeConnection(strConnection);

            try
            {
                string query = "SELECT * FROM tabelaLogin";

                DataTable dados = new DataTable();

                SqlCeDataAdapter adaptador = new SqlCeDataAdapter(query, strConnection);

                conexao.Open();

                adaptador.Fill(dados);

                foreach (DataRow linha in dados.Rows)
                {
                    dataAdministradores.Rows.Add(linha.ItemArray);
                }
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Deseja excluir o usuário?", "Confirmação", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.No) { return; }

            string baseDados = Application.StartupPath + @"\BancoDeDadosLogin.sdf";
            string strConection = @"DataSource = " + baseDados + "; Password = '1234'";

            SqlCeConnection conexao = new SqlCeConnection(strConection);

            try
            {
                conexao.Open();

                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = conexao;

                string usuario = dataAdministradores.SelectedRows[0].Cells[0].Value.ToString();

                comando.CommandText = "DELETE FROM tabelaLogin WHERE usuario = '" + usuario + "'";

                comando.ExecuteNonQuery();

                MessageBox.Show("Usuário excluído.");
                comando.Dispose();
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string baseDados = Application.StartupPath + @"\BancoDeDadosLogin.sdf";
            string strConection = @"DataSource = " + baseDados + "; Password = '1234'";

            SqlCeConnection conexao = new SqlCeConnection(strConection);

            try
            {
                conexao.Open();

                SqlCeCommand comando = new SqlCeCommand();
                comando.Connection = conexao;

                string usuario = dataAdministradores.SelectedRows[0].Cells[0].Value.ToString();
                string senha = txtSenha.Text;
                string adm = cbAdministrador.Checked ? "s" : "n";

                string query = "UPDATE tabelaLogin SET senha = '" + senha + "', adm = '" + adm + "' WHERE usuario LIKE '" + usuario + "'";

                comando.CommandText = query;

                comando.ExecuteNonQuery();

                MessageBox.Show("Usuário alterado.");
                comando.Dispose();
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
}
