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

        public static bool Inserir(Aluno novo)
        {
            return AlunoDAL.Inserir(novo);
        }
        public static bool RetornarAluno(string CPF)
        {
            return AlunoDAL.VincularProfessor(CPF); 
        }

        public static List<Aluno> BuscarAlunos()
        {
            return AlunoDAL.BuscarUsuarios();

        }
        public static List<Aluno> BuscarTodosalunos()
        {
            return AlunoDAL.BuscarTodosUsuarios();

        }

        public static void DeletaAlunos()
        {
            AlunoDAL.DeletaAlunos();
        }

    }
}
