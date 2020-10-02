using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
namespace Controller
{
    public class AlunoController
    {
        public static void Inserir(Aluno novo)
        {
            AlunoDAL.Inserir(novo);
        }
    }
}
