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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AtualizarLista();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new CadastroAluno().ShowDialog();
        }

        private void AtualizarLista()
        {
            string coluna = "nome";
            if (rbCodigoMatricula.Checked)
            {
                coluna = "codigo_matricula";
            }
            else if (rbFrequencia.Checked)
            {
                coluna = "frequencia";
            }

            string tipoOrdenacao = "ASC";
            if (rbDesc.Checked)
            {
                tipoOrdenacao = "DESC";
            }

            dataGridView1.Rows.Clear();
            List<Aluno> alunos = new AlunoRepositorio().ObterTodos(textBox1.Text, coluna, tipoOrdenacao);
            foreach(Aluno aluno in alunos)
            {
                dataGridView1.Rows.Add(new object[]
                    {
                        aluno.Id,
                        aluno.Nome,
                        aluno.CodigoMatricula,
                        aluno.Frequencia
                    });
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Não tem nenhum aluno selecionado");
                return;
            }

            int linhaSelecionada = dataGridView1.CurrentRow.Index;
            int codigo = Convert.ToInt32(dataGridView1.Rows[linhaSelecionada].Cells[0].Value.ToString());
            string nome = dataGridView1.Rows[linhaSelecionada].Cells[1].Value.ToString();
            DialogResult resultado = MessageBox.Show("Deseja realmente apagar o registro " + nome + "?", "AVISO", MessageBoxButtons.YesNo);

            if (resultado == DialogResult.Yes)
            {
                bool apagado = new AlunoRepositorio().Apagar(codigo);
                MessageBox.Show(nome + " apagado com sucesso");
            }
            else
            {
                MessageBox.Show("Exclusão de " + nome + " cancelada com sucesso");
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            int linhaSelecionada = dataGridView1.CurrentRow.Index;
            int codigo = Convert.ToInt32(dataGridView1.Rows[linhaSelecionada].Cells[0].Value.ToString());
            new CadastroAluno(codigo).ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AtualizarLista();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            AtualizarLista();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AtualizarLista();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            AtualizarLista();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            AtualizarLista();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            AtualizarLista();
        }

        private void rbNome_CheckedChanged(object sender, EventArgs e)
        {
            AtualizarLista();
        }

        private void rbCodigoMatricula_CheckedChanged(object sender, EventArgs e)
        {
            AtualizarLista();
        }

        private void rbFrequencia_CheckedChanged(object sender, EventArgs e)
        {
            AtualizarLista();
        }

        private void rbAsc_CheckedChanged(object sender, EventArgs e)
        {
            AtualizarLista();
        }

        private void rbDesc_CheckedChanged(object sender, EventArgs e)
        {
            AtualizarLista();
        }
    }
}
