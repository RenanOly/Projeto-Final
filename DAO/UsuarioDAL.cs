using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL
{
    public static class UsuarioDAL
    {

        public static bool logar(string nome, string senha)
        {
            
            bool a =false;
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
            comando.Connection = conexao;
            comando.CommandText = "select*from Usuario where Login='"+nome+"' and Senha= '"+senha+"' ;";
            reader = comando.ExecuteReader();
            ;
            if (reader.Read())
            {
                conexao.Close();
                return  true;

            }
            return false;
            

        }
    }
}
