using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
namespace DAL
{
    public static class ProfessorDAL
    {


        public static bool AlterarSenha(string SenhaAtual, string NovaSenha)
        {
            StreamReader arq = new StreamReader("login.txt");
            string CPFProfessor = arq.ReadLine();
            arq.Close();
            SHA256 mySHA256 = SHA256.Create();

            byte[] hashValue = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(SenhaAtual));
            string senhaHash = "";
            for (int i = 0; i < hashValue.Length; i++)
            {
                senhaHash += hashValue[i].ToString("x2");
            }

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
            comando.CommandText = "select*from Professor where Cpf= '" + CPFProfessor + "' and Senha = '"+senhaHash+"' ;";
            reader = comando.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                hashValue = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(NovaSenha));
                senhaHash = "";
                for (int i = 0; i < hashValue.Length; i++)
                {
                    senhaHash += hashValue[i].ToString("x2");
                }
                comando.CommandText = "UPDATE Professor SET Senha = '" + senhaHash + "' WHERE Cpf = '"+ CPFProfessor + "';";
                try
                {
                    comando.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    throw new Exception("Erro ao gravar senha!");
                }
                finally
                {
                    conexao.Close();

                }


            }
            return false;

        }
        public static bool Logar(string nome, string senha)
        {
            SHA256 mySHA256 = SHA256.Create();

            byte[] hashValue = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(senha));
            string senhaHash = "";
            for (int i = 0; i < hashValue.Length; i++)
            {
                senhaHash += hashValue[i].ToString("x2");
            }
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
            comando.CommandText = "select*from Professor where Login='" + nome + "' and Senha= '" + senhaHash + "' and Situacao = 1  ;";
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
        public static void DeletaProfessores()
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

            comando.Connection = conexao;
            comando.CommandText = "delete from Professor";
            try
            {
                comando.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception("Erro ao deletar Professores!");
            }
            conexao.Close();
        }
        public static List<Professor> BuscarTodosprofessores()
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
            comando.CommandText = "select*from Professor;";
            reader = comando.ExecuteReader();
            List<Professor> professores = new List<Professor>();
            while (reader.Read())
            {
                Professor professor = new Professor();
                professor.Nome = reader["Nome"].ToString();
                professor.Idade = int.Parse(reader["Idade"].ToString());
                professor.Cpf = reader["Cpf"].ToString();
                professor.Situacao = (int)reader["Situacao"];
                professores.Add(professor);
            }
            conexao.Close();
            return professores;
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
