using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;
using System.IO;
namespace DAL
{
    public static class ProfessorDAL
    {

        public static bool Logar(string nome, string senha)
        {
            bool a = false;
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
            comando.CommandText = "select*from Professor where Login='" + nome + "' and Senha= '" + senha + "' ;";
            reader = comando.ExecuteReader();
            if (reader.Read())
            {
                StreamWriter arq = new StreamWriter("login.txt", false);
                arq.WriteLine(reader["Cpf"]);
                arq.Close();
                conexao.Close();
                return true;

            }
            return false;
        }


        public static bool Inserir(Professor novo)
        {

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
            
            comando.CommandText = "select*from Usuario where Login= '" + novo.Login + "' ;";
            reader = comando.ExecuteReader();
            if (!reader.Read())
            {
                reader.Close();
                comando.CommandText = "select*from Professor where Cpf='" + novo.Cpf + "' or Login= '" + novo.Login + "' ;";
                reader = null;
                reader = comando.ExecuteReader();
                if (!reader.Read())
                {
                    reader.Close();
                    comando.CommandText = "insert into Professor(Nome, Idade, Cpf, Login, Senha, Situacao)Values(@Nome, @Idade,@Cpf,@Login,@Senha,@Situacao)";
                    SqlParameter param = new SqlParameter("@Nome", System.Data.SqlDbType.VarChar);
                    param.Value = novo.Nome;
                    comando.Parameters.Add(param);

                    param = new SqlParameter("@Idade", System.Data.SqlDbType.VarChar);
                    param.Value = novo.Idade;
                    comando.Parameters.Add(param);

                    param = new SqlParameter("@Cpf", System.Data.SqlDbType.VarChar);
                    param.Value = novo.Cpf;
                    comando.Parameters.Add(param);

                    param = new SqlParameter("@Login", System.Data.SqlDbType.VarChar);
                    param.Value = novo.Login;
                    comando.Parameters.Add(param);

                    param = new SqlParameter("@Senha", System.Data.SqlDbType.VarChar);
                    param.Value = novo.senha;
                    comando.Parameters.Add(param);

                    param = new SqlParameter("@Situacao", System.Data.SqlDbType.VarChar);
                    param.Value = 1;
                    comando.Parameters.Add(param);


                    comando.ExecuteNonQuery();
                }
                else
                {
                    return false;
                }
               
              
            }
            else
            {
               return false;
            }
            conexao.Close();
            return true;

        }

    }
}
