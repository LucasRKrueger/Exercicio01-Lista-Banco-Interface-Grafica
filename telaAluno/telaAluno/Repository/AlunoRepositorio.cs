using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using telaAluno.Model;
using System.Globalization;

namespace telaAluno.Repository
{
    public class AlunoRepositorio
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:
                                          \Users\Lucas\Documents\
                                          alunoex.mdf;Integrated Security=True;Connect Timeout=30";
        private SqlConnection connection = null;

        public AlunoRepositorio()
        {
            connection = new SqlConnection(connectionString);
        }

        public int Inserir(Aluno aluno)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"INSERT INTO alunosexx (nome, codigo_matricula, nota_1, nota_2, nota_3, frequencia) OUTPUT INSERTED.ID VALUES (@NOME, @CODIGO_MATRICULA, @NOTA_1, @NOTA_2, @NOTA_3, @FREQUENCIA)";
            command.Parameters.AddWithValue("@NOME", aluno.Nome);
            command.Parameters.AddWithValue("@CODIGO_MATRICULA", aluno.CodigoMatricula);
            command.Parameters.AddWithValue("@NOTA_1", aluno.Nota1);
            command.Parameters.AddWithValue("@NOTA_2", aluno.Nota2);
            command.Parameters.AddWithValue("@NOTA_3", aluno.Nota3);
            command.Parameters.AddWithValue("@FREQUENCIA", aluno.Frequencia);
            int id = Convert.ToInt32(command.ExecuteScalar().ToString());
            connection.Close();
            return id;
        }

        public bool Editar(Aluno aluno)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"UPDATE alunosexx SET nome = @NOME, codigo_matricula = @CODIGO_MATRICULA, nota_1 = @NOTA_1, nota_2 = @NOTA_2, nota_3 = @NOTA_3, frequencia = @FREQUENCIA WHERE id = @ID";
            command.Parameters.AddWithValue("@NOME", aluno.Nome);
            command.Parameters.AddWithValue("@CODIGO_MATRICULA", aluno.CodigoMatricula);
            command.Parameters.AddWithValue("@NOTA_1", aluno.Nota1);
            command.Parameters.AddWithValue("@NOTA_2", aluno.Nota2);
            command.Parameters.AddWithValue("@NOTA_3", aluno.Nota3);
            command.Parameters.AddWithValue("@FREQUENCIA", aluno.Frequencia);
            command.Parameters.AddWithValue("@ID", aluno.Id);
            int quantidadeAlterada = command.ExecuteNonQuery();
            connection.Close();
            return quantidadeAlterada == 1;
        }

        public List<Aluno> ObterTodos(
            string textoParaPesquisar = "%%",
            string colunaOrdenacao = "nome",
            string tipoOrdenacao = "ASC") {
            textoParaPesquisar = "%" + textoParaPesquisar + "%";

            List<Aluno> alunos = new List<Aluno>();
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"SELECT id, nome, codigo_matricula, nota_1, nota_2, nota_3, frequencia FROM alunosexx WHERE nome LIKE @PESQUISA OR frequencia LIKE
            @PESQUISA ORDER BY" + colunaOrdenacao + " " + tipoOrdenacao;
            command.Parameters.AddWithValue("@PESQUISA", textoParaPesquisar);
            
            DataTable tabelaEmMemoria = new DataTable();
            tabelaEmMemoria.Load(command.ExecuteReader());
            for (int i = 0; i < tabelaEmMemoria.Rows.Count; i++)
			{
                Aluno aluno = new Aluno();
                aluno.Id = Convert.ToInt32(tabelaEmMemoria.Rows[i][0].ToString());
                aluno.Nome = tabelaEmMemoria.Rows[i][1].ToString();
                aluno.Frequencia= Convert.ToInt16(tabelaEmMemoria.Rows[i][2].ToString());
                alunos.Add(aluno);

			 
			}
            connection.Close();
            return alunos;
            }
            public Aluno ObterPeloCodigo(int codigo)
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = @"SELECT id, nome, codigo_matricula, nota_1, nota_2, nota_3, frequencia WHERE id = @ID";
                command.Parameters.AddWithValue("@ID", codigo);

                DataTable tabelaEmMemoria = new DataTable();
                tabelaEmMemoria.Load(command.ExecuteReader());
                if (tabelaEmMemoria.Rows.Count == 0)
                {
                    return null;
                }

                Aluno aluno = new Aluno();

                aluno.Id = Convert.ToInt32(tabelaEmMemoria.Rows[0][0].ToString());
                aluno.Nome = (tabelaEmMemoria.Rows[0][1].ToString());
                aluno.CodigoMatricula = (tabelaEmMemoria.Rows[0][3].ToString());
                aluno.Nota1 = Convert.ToSingle(tabelaEmMemoria.Rows[0][4].ToString());
                aluno.Nota2 = Convert.ToSingle(tabelaEmMemoria.Rows[0][5].ToString());
                aluno.Nota3 = Convert.ToSingle(tabelaEmMemoria.Rows[0][6].ToString());
                aluno.Frequencia = Convert.ToInt16(tabelaEmMemoria.Rows[0][0].ToString());
                connection.Close();
                return aluno;
            }

            public bool Apagar(int codigo)
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "DELETE FROM alunosexx WHERE id = @CODIGO";
                command.Parameters.AddWithValue("@CODIGO", codigo);
                int quantidade = command.ExecuteNonQuery();
                connection.Close();
                return quantidade == 1;
            }
               public float ObterMedias()
               {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT nota_1, nota_2, nota_3 (nota_1 + nota_2 + nota_3)/3 FROM alunosexx";
                float media = Convert.ToSingle(command.ExecuteScalar());
                connection.Close();
                return media;
               }

 
 
            
    }
}
