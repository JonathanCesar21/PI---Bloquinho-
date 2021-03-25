using PI_3º_termo.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PI_3º_termo
{
    public partial class CadastroCliente : Form
    {
        SqlConnection cnn;
        SqlCommand comando;
        SqlDataAdapter da;
        SqlDataReader dr;
        public string connectionString;
        public string strSQL;
        public CadastroCliente()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                connectionString = @"Data Source=DESKTOP-D13RQ2Q\SQLExpress;Initial Catalog=AppPi;User ID=teste2;Password=123";
                cnn = new SqlConnection(connectionString);
                cnn.Open();
                MessageBox.Show("Connection Open !");
                cnn.Close();
            }

            catch (SqlException erro)
            {
                MessageBox.Show("Erro ao se conectar no banco de dados \n" + "Verifique os dados informados" + erro);
            }

        }
        

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            //Avisar o que substituiram (TextBox - Check Box - MaksedTextBox - RadioButton)
            Pessoas pessoa = new Pessoas ();

            pessoa.nome = txtNome.Text;
            pessoa.telefone = txtTelefone.Text;
            pessoa.telefone2 = txtTelefone2.Text;
            pessoa.cidade = txtCidade.Text;
            pessoa.email = txtEmail.Text;
            //pessoa.sexo = ;
            pessoa.datanasc = txtDataNasc.Text;
            pessoa.Rg = txtRg.Text;
            pessoa.Cpf = txtCpf.Text;
            pessoa.foto = pctFoto.Image.ToString();
            //pessoa.foto...
            
           MessageBox.Show("Nome:" + pessoa.nome + "\nTelefone1:" + pessoa.telefone + "\nTelefone2:" + pessoa.telefone2 + "\nCidade:" + pessoa.cidade + "\nEmail:" + pessoa.email + "\nData de Nascimento:" + pessoa.datanasc + "\nRG:" + pessoa.Rg + "\nCPF:" + pessoa.Cpf);

            try 
            {
                cnn = new SqlConnection(@"Data Source=DESKTOP-D13RQ2Q\SQLExpress;Initial Catalog=AppPi;User ID=teste2;Password=123");
                strSQL = "INSERT INTO tbl_Pessoas (Nome_Pessoa, Telefone_Pessoa, Telefone2_Pessoa, Cidade_Pessoa, Email_Pessoa, DataNascimento_Pessoa, Rg_Pessoa, Cpf_Pessoa) VALUES (@Nome_Pessoa, @fone_Pessoa, @fone2_Pessoa, @Cidade_Pessoa, @Email_Pessoa, @Datanasc_Pessoa, @Rg_Pessoa, @Cpf_Pessoa)";


            comando = new SqlCommand(strSQL, cnn);
            comando.Parameters.AddWithValue("@Nome_Pessoa", txtNome.Text);
            comando.Parameters.AddWithValue("@fone_Pessoa", txtTelefone.Text);
            comando.Parameters.AddWithValue("@fone2_Pessoa", txtTelefone2.Text);
            comando.Parameters.AddWithValue("@Cidade_Pessoa", txtCidade.Text);
            comando.Parameters.AddWithValue("@Email_Pessoa", txtEmail.Text);
            comando.Parameters.AddWithValue("@DataNasc_Pessoa", txtDataNasc.Text);
            comando.Parameters.AddWithValue("@Rg_Pessoa", txtRg.Text);
            comando.Parameters.AddWithValue("@Cpf_Pessoa", txtCpf.Text);

            cnn.Open();
            comando.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                cnn.Close();
                comando.Clone();
                cnn = null;
                comando = null;

            }
        
    }

        private void btnLocalizarFoto_Click(object sender, EventArgs e)
        {
            pctFoto.Image = Resources.Screenshot_6;
            
        }

        private void btnProximaFoto_Click(object sender, EventArgs e)
        {
            //Botão de próxima foto e foto anterior só vai servir pra exemplo, futuramente a ideia é a foto mudar com o Proximo ID do cadastro ou o Anterior...
            pctFoto.Image = Resources.Screenshot_7;
        }

        private void btnEscolherFoto_Click(object sender, EventArgs e)
        {
            if (ofdfotos.ShowDialog()== DialogResult.OK)
            {
                txtLocalFoto.Text = ofdfotos.FileName;
                pctFoto.ImageLocation = txtLocalFoto.Text;
            } 
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {

            cnn = new SqlConnection(@"Data Source=DESKTOP-D13RQ2Q\SQLExpress;Initial Catalog=AppPi;User ID=teste2;Password=123");
            comando = new SqlCommand("select * from tbl_Pessoas where Id_Pessoa=@Id_Pessoa", cnn);
            comando.Parameters.Add("@Id_Pessoa", SqlDbType.VarChar).Value = txtId.Text;

            try
            {
                cnn.Open();
                SqlDataReader dr = comando.ExecuteReader();
                if(dr.HasRows == false)
                {
                    throw new Exception("Id não encontrado");
                }
                dr.Read();
                txtId.Text = Convert.ToString(dr["Id_Pessoa"]);
                txtNome.Text = Convert.ToString(dr["Nome_Pessoa"]);
                txtTelefone.Text = Convert.ToString(dr["Telefone_Pessoa"]);
                txtTelefone2.Text = Convert.ToString(dr["Telefone2_Pessoa"]);
                txtCidade.Text = Convert.ToString(dr["Cidade_Pessoa"]);
                txtEmail.Text = Convert.ToString(dr["Email_Pessoa"]);
                txtDataNasc.Text = Convert.ToString(dr["DataNascimento_Pessoa"]);
                txtRg.Text = Convert.ToString(dr["Rg_Pessoa"]);
                txtCpf.Text = Convert.ToString(dr["Cpf_Pessoa"]);

                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            cnn = new SqlConnection(@"Data Source=DESKTOP-D13RQ2Q\SQLExpress;Initial Catalog=AppPi;User ID=teste2;Password=123");
            comando = new SqlCommand("DELETE FROM tbl_Pessoas WHERE Id_Pessoa=@Id_Pessoa", cnn);
            comando.Parameters.Add("@Id_Pessoa", SqlDbType.VarChar).Value = txtId.Text;
          
            try
            {
                cnn.Open();
                SqlDataReader dr = comando.ExecuteReader();
                if (dr.HasRows == false)
                {
                    throw new Exception("Excluido com sucesso!");
                }
                dr.Read();
                txtId.Text = Convert.ToString(dr["Id_Pessoa"]);
                //comando.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txtId.Text = "";
            txtNome.Text = "";
            txtTelefone.Text = "";
            txtTelefone2.Text = "";
            txtCidade.Text = "";
            txtEmail.Text = "";
            txtDataNasc.Text = "";
            txtRg.Text = "";
            txtCpf.Text = "";
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            cnn = new SqlConnection(@"Data Source=DESKTOP-D13RQ2Q\SQLExpress;Initial Catalog=AppPi;User ID=teste2;Password=123");
            comando = new SqlCommand("UPDATE tbl_Pessoas set Nome_Pessoa=@Nome_Pessoa, Telefone_Pessoa=@Telefone_Pessoa, Telefone2_Pessoa=@Telefone2_Pessoa, Cidade_Pessoa=@Cidade_Pessoa, Email_Pessoa=@Email_Pessoa, DataNascimento_Pessoa=@DataNascimento_Pessoa, Rg_Pessoa=@Rg_Pessoa, Cpf_Pessoa=@Cpf_Pessoa", cnn);
            comando.Parameters.Add("@Id_Pessoa", SqlDbType.VarChar).Value = txtId.Text;
            comando.Parameters.Add("@Nome_Pessoa", SqlDbType.VarChar).Value = txtNome.Text;
            comando.Parameters.Add("@Telefone_Pessoa", SqlDbType.VarChar).Value = txtTelefone.Text;
            comando.Parameters.Add("@Telefone2_Pessoa", SqlDbType.VarChar).Value = txtTelefone2.Text;
            comando.Parameters.Add("Cidade_Pessoa", SqlDbType.VarChar).Value = txtCidade.Text;
            comando.Parameters.Add("@Email_Pessoa", SqlDbType.VarChar).Value = txtEmail.Text;
            comando.Parameters.Add("@DataNascimento_Pessoa", SqlDbType.VarChar).Value = txtDataNasc.Text;
            comando.Parameters.Add("@Rg_Pessoa", SqlDbType.VarChar).Value = txtRg.Text;
            comando.Parameters.Add("@Cpf_Pessoa", SqlDbType.VarChar).Value = txtCpf.Text;

            if (txtId.Text != "" & txtNome.Text != "" & txtTelefone.Text != "" & txtTelefone2.Text != "" & txtCidade.Text != "" & txtEmail.Text != "" & txtDataNasc.Text != "" & txtRg.Text != "" & txtCpf.Text != "")
            {
                try
                {
                    cnn.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Atualizado com sucesso!");
                    cnn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                txtId.Text = "";
                txtNome.Text = "";
                txtTelefone.Text = "";
                txtTelefone2.Text = "";
                txtCidade.Text = "";
                txtEmail.Text = "";
                txtDataNasc.Text = "";
                txtRg.Text = "";
                txtCpf.Text = "";
            }
            
        }
    }
   
}

