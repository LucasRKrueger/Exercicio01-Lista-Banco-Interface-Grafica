using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using telaAluno.Model;
using telaAluno.Repository;

namespace telaAluno
{
    public partial class CadastroAluno : Form
    {
        private int codigo;
        public CadastroAluno()
        {
            InitializeComponent();
        }

        public CadastroAluno(int codigo)
        {
            InitializeComponent();
            this.codigo = codigo;
            Aluno aluno = new AlunoRepositorio().ObterPeloCodigo(codigo);
            PreencherCampos(aluno);
        }

        private void PreencherCampos(Aluno aluno)
        {
            txtCodigo.Text = aluno.Id.ToString();
            txtNome.Text = aluno.Nome;
            txtNota1.Text = aluno.Nota1.ToString();
            txtNota2.Text = aluno.Nota2.ToString();
            txtNota3.Text = aluno.Nota3.ToString();
            txtFrequencia.Text = aluno.Frequencia.ToString();
            txtCodigoMatricula.Text = aluno.CodigoMatricula;

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Aluno aluno = new Aluno();

            if (string.IsNullOrEmpty(txtNome.Text))
            {
                MessageBox.Show("Preencha o campo \"Nome\"");
                txtNome.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtNota1.Text))
            {
                MessageBox.Show("Preencha o campo \"Nota 1\"");
                txtNota1.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtNota2.Text))
            {
                MessageBox.Show("Preencha o campo \"Nota 2\"");
                txtNota2.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtNota3.Text))
            {
                MessageBox.Show("Preencha o campo \"Nota 3\"");
                txtNota3.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtCodigoMatricula.Text))
            {
                MessageBox.Show("Preencha o campo \"Matricula\"");
                txtCodigoMatricula.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtFrequencia.Text))
            {
                MessageBox.Show("Preencha o campo \"Frequência\"");
                txtFrequencia.Focus();
                return;
            }

            aluno.Nome = txtNome.Text;
            aluno.CodigoMatricula = txtCodigoMatricula.Text;
            aluno.Nota1 = Convert.ToSingle(txtNota1.Text);
            aluno.Nota2 = Convert.ToSingle(txtNota2.Text);
            aluno.Nota3 = Convert.ToSingle(txtNota3.Text);
            aluno.Frequencia = Convert.ToByte(txtFrequencia.Text);

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                int id = new AlunoRepositorio().Inserir(aluno);
                txtCodigo.Text = Convert.ToString(id);
                MessageBox.Show("Registro cadastro com sucesso");
            }
            else
            {
                int id = Convert.ToInt32(txtCodigo.Text);
                aluno.Id = id;
                bool alterado = new AlunoRepositorio().Editar(aluno);
                if (alterado)
                {
                    MessageBox.Show("Registro alterado com sucesso");


                }
                else
                {
                    MessageBox.Show("Não foi possível alterar");
                }
            }
        }

        private void CalcularMedia()
        {
            float nota1 = 0;
            float nota2 = 0;
            float nota3 = 0;

            if (string.IsNullOrEmpty(txtNota1.Text.Replace(",", "").Replace(" ", "")))
            {

            }
            else
            {
                nota1 = Convert.ToSingle(txtNota1.Text);
            }

            if (string.IsNullOrEmpty(txtNota2.Text.Replace(",", "").Replace(" ", "")))
            {
                nota2 = 0;
            }
            else
            {
                nota2 = Convert.ToSingle(txtNota2.Text);
            }

            if (string.IsNullOrEmpty(txtNota3.Text.Replace(",", "").Replace(" ", "")))
            {
                nota3 = 0;
            }
            else
            {
                nota3 = Convert.ToSingle(txtNota3.Text);
            }

            float media = (nota1 + nota2 + nota3) / 3;
            label12.Text = String.Format("{0:n}", media);
        }

        private void CalcularSituacao()
        {
            double media = Convert.ToDouble(lblMedia.Text);
            if (media < 5)
            {
                label13.Text = "Reprovado";
            }
            if ((media >= 5) && (media < 7))
            {
                label13.Text = "Recuperação";
            }
            if (media >= 7)
            {
                label13.Text = "Aprovado";
            }
        }

        private void LimparCampos()
        {
            txtCodigo.Text = string.Empty;
            txtCodigoMatricula.Text = string.Empty;
            txtFrequencia.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtNota1.Text = string.Empty;
            txtNota2.Text = string.Empty;
            txtNota3.Text = string.Empty;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                try
                {
                    int id = Convert.ToInt32(txtCodigo.Text);
                    Aluno aluno = new AlunoRepositorio().ObterPeloCodigo(id);
                    if (aluno != null)
                    {
                        PreencherCampos(aluno);
                    }
                    else
                    {
                        MessageBox.Show("Registro não existente");
                        txtCodigo.Focus();
                        txtCodigo.SelectionStart = 0;
                        txtCodigo.SelectionLength = txtCodigo.Text.Length;
                    }
                }
                catch (NotFiniteNumberException ex)
                {

                    MessageBox.Show("Digite um código válido");
                    txtCodigo.Focus();
                }
            }
        }

        private void txtNome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNota1.Focus();
            }
        }

        private void txtNota1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNota2.Focus();
                CalcularMedia();
                CalcularSituacao();
            }
        }

        private void txtNota2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNota3.Focus();
                CalcularMedia();
                CalcularSituacao();
            }
        }

        private void txtNota3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCodigo.Focus();
                CalcularMedia();
                CalcularSituacao();
            }
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCodigoMatricula.Focus();
            }
        }

        private void txtCodigoMatricula_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFrequencia.Focus();
            }
        }

        private void txtFrequencia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSalvar.Focus();
            }
        }

        private void CadastroAluno_Load(object sender, EventArgs e)
        {
            float media = new AlunoRepositorio().ObterMedias();
            label12.Text = Convert.ToString(media);
        }

        private void CadastroAluno_Leave(object sender, EventArgs e)
        {
            label12.Hide();
        }

        private void txtNota1_Leave(object sender, EventArgs e)
        {
            if (Convert.ToSingle(txtNota1.Text) > 10)
            {
                MessageBox.Show("Nota 1 deve ser menor 10");
                txtNota1.Focus();
                txtNota1.SelectionStart = 0;
                txtNota1.SelectionLength = txtNota1.Text.Length;
                return;
            }
            CalcularMedia();
            CalcularSituacao();
        }
        private void txtNota2_Leave(object sender, EventArgs e)
        {
            if (Convert.ToSingle(txtNota2.Text) > 10)
            {
                MessageBox.Show("Nota 2 deve ser menor 10");
                txtNota2.Focus();
                txtNota2.SelectionStart = 0;
                txtNota2.SelectionLength = txtNota2.Text.Length;
                return;
            }
            CalcularMedia();
            CalcularSituacao();
        }

        private void txtNota3_Leave(object sender, EventArgs e)
        {
            if (Convert.ToSingle(txtNota3.Text) > 10)
            {
                MessageBox.Show("Nota 3 deve ser menor 10");
                txtNota3.Focus();
                txtNota3.SelectionStart = 0;
                txtNota3.SelectionLength = txtNota3.Text.Length;
                return;
            }
            CalcularMedia();
            CalcularSituacao();
        }

    }
}
    
    