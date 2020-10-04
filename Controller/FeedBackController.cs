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
    }
}
