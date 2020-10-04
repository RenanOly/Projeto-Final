using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
namespace Controller
{
    public static class ExercicioController
    {
        public static void CadastraExercicio(string nome, string spec)
        {
            ExercicioDAL.Inserir(nome, spec);
        }

        public static void ProgramarExercicio(string nome, string inicio, string termino, string repeticoes, string diasSemana)
        {
            ExercicioDAL.ProgramarExercicio(nome, inicio, termino, repeticoes,diasSemana);
        }

        public static List<List<object>> BuscarExercicios()
        {

            return ExercicioDAL.RetornaExercicios();
        }


    }
}
