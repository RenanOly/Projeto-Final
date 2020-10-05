using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;

namespace DAL
{
    public static class UsuarioDAL
    {

        public static void MudarSituacao(string situacao, string TipoUsuario, string CPF)
        {
            string usuario = "Professor";
            if (TipoUsuario == "Aluno")
            {
                usuario = "Usuario";
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
            if (situacao == "Ativo")
            {
                comando.CommandText = "UPDATE "+usuario+" SET Situacao = 0 WHERE Cpf = '"+CPF+"';";

            }
            else
            {
                comando.CommandText = "UPDATE " + usuario + " SET Situacao = 1 WHERE Cpf = '" + CPF + "';";

            }
            try
            {
                comando.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception("Erro ao alterar situação!");
            }
        }
        public static bool AlterarSenhaAdmin(string SenhaAtual, string NovaSenha)
        {
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
            comando.CommandText = "select*from Usuario where Senha= '" + senhaHash + "' ;";
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
                comando.CommandText = "UPDATE Usuario SET Senha = '" + senhaHash + "' WHERE Login = 'admin';";
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
        public static bool PrimeiroAcesso(string senha)
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
            comando.CommandText = "select*from Usuario where Senha= '" + senhaHash + "' ;";
            reader = comando.ExecuteReader();
            if (reader.Read())
            {
                if (senha == "admin")
                {
                    return true;
                }
               
            }
            return false;
        }
        public static bool logar(string nome, string senha)
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
            comando.CommandText = "select*from Usuario where Login='"+nome+"' and Senha= '"+senhaHash+"' and Situacao = 1 ;";
            reader = comando.ExecuteReader();
           
            if (reader.Read())
            {
                StreamWriter arq = new StreamWriter("login.txt");
                arq.WriteLine(reader["Cpf"]);
                arq.Close();
                reader.Close();
                conexao.Close();
                return  true;

            }
            conexao.Close();
            return false;
            

        }
    }
}
