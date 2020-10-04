using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
namespace DAL
{
    public static class FeedBackDAL
    {
        public static bool inserir(string feedback)
        {
            bool retur = false;
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = Configuracao.ConnectionString;
            try
            {
                conexao.Open();


            }
            catch
            {
                throw new Exception("Erro na conexão com o banco de dados");
            }

            SqlCommand comando = new SqlCommand();
            SqlDataReader reader = null;
            StreamReader arq = new StreamReader("login.txt");
            string CPFAluno = arq.ReadLine();
            arq.Close();
            comando.Connection = conexao;
            comando.CommandText = "select*from FeedBack where Aluno='" + CPFAluno + "' ;";
            reader = comando.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                comando.CommandText = "UPDATE FeedBack SET FeedBack = '"+feedback+ "' WHERE Aluno = '" + CPFAluno + "';";
                comando.ExecuteNonQuery();
                retur = true;
            }
            else
            {
                reader.Close();

                comando.CommandText = "insert into FeedBack(FeedBack,Aluno)Values(@FeedBack, @Aluno)";
                SqlParameter param = new SqlParameter("@FeedBack", System.Data.SqlDbType.VarChar);
                param.Value = feedback;
                comando.Parameters.Add(param);

                param = new SqlParameter("@Aluno", System.Data.SqlDbType.VarChar);
                param.Value = CPFAluno;
                comando.Parameters.Add(param);
                comando.ExecuteNonQuery();
                retur = true;
            }
            conexao.Close();
            return retur;
        }   
        
    }
}
