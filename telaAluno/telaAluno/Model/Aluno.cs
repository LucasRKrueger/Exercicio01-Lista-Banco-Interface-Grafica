using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telaAluno.Model
{
     public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CodigoMatricula { get; set; }
        public float Nota1{ get; set; }
        public float Nota2 { get; set; }
        public float Nota3{ get; set; }
        public byte Frequencia  { get; set; }
        public float Media { get; set; }


    }
}
