using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Professor:Usuarios
    {
        public string Alunos { get; set; }
        public string[] Exercicios { get; set; }
    }
}
