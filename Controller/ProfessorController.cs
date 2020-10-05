using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
namespace Controller
{
    public static class ProfessorController
    {

        public static bool Inserir(Professor novo)
        {
            bool retornado=ProfessorDAL.Inserir(novo);
            return retornado;
        }

        public static List<Professor> BuscarTodosprofessores()
        {
            return ProfessorDAL.BuscarTodosprofessores();

        }
        public static void DeletaProfessores()
        {
            ProfessorDAL.DeletaProfessores();
        }



    }
}
