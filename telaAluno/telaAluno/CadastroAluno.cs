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
            aluno.Nome = txtNome.Text;
            aluno.CodigoMatricula = txtCodigoMatricula.Text;
            aluno.Nota1 = Convert.ToSingle(txtNota1.Text);
            aluno.Nota2 = Convert.ToSingle(txtNota2.Text);
            aluno.Nota3 = Convert.ToSingle(txtNota3.Text);
            aluno.Frequencia = Convert.ToInt16(txtFrequencia.Text);

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                int id = new AlunoRepositorio().Inserir(aluno);
                txtCodigo.Text = id.ToString();
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
                    LimparCampos();

                }
                else
                {
                    MessageBox.Show("Não foi possível alterar");
                }
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
    }
}
