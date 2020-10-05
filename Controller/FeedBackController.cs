using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace Controller
{
    public static class FeedBackController
    {
        public static bool AdicionarFeedBack(string feedback)
        {
            return FeedBackDAL.inserir(feedback);
        }
        public static string BuscarFeedBacks(string CPFAluno)
        {
            return FeedBackDAL.BuscarFeedBack(CPFAluno);

        }
        public static void DeletaFeedBack()
        {
            FeedBackDAL.DeletaFeedBack();

        }
    }
}
