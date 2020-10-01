using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class Usuarios
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Login { get; set; }
        public string senha { get; set; }
        public bool Situacao { get; set; }

    }
}
