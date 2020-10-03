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
    public static class AlunoDAL
    {




        public static List<Aluno> BuscarUsuarios()
        {
            StreamReader arq = new StreamReader("login.txt");
            string CPFProfessor = arq.ReadLine();


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
            comando.CommandText = "select*from Usuario where Professor= '" + CPFProfessor + "' ;";
            reader = comando.ExecuteReader();
            List<Aluno> alunos = new List<Aluno>();
            while (reader.Read())
            {
                Aluno aluno = new Aluno();
                aluno.Nome = reader["Nome"].ToString();
                aluno.Idade = int.Parse(reader["Idade"].ToString());
                aluno.Cpf = reader["Cpf"].ToString();
                alunos.Add(aluno);
            }
            arq.Close();
            conexao.Close();
            return alunos;
        }
        public static bool VincularProfessor(string CPFAluno)
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
            comando.CommandText = "select*from Usuario where Cpf= '" +CPFAluno + "' ;";
            reader = comando.ExecuteReader();
            if (reader.Read())
            {
                if (reader["Professor"].ToString()=="")
                {
                    reader.Close();
                    StreamReader arq = new StreamReader("login.txt");
                    string cpfProfessor = arq.ReadLine();
                    comando.CommandText = "Update Usuario set Professor= '" + cpfProfessor + "' WHERE Cpf = '" + CPFAluno + "';";
                    comando.ExecuteNonQuery();
                    arq.Close();
                    conexao.Close();
                    return true;
                }
                else
                {
                    throw new Exception("Este aluno já possui um professor!");
                }
                
                
            }
            else
            {
                throw new Exception("Aluno não cadastrado!");
            }
            
            conexao.Close();
            return false;
        }

        public static bool Inserir(Aluno novo)
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
            comando.CommandText = "select*from Professor where Login= '" + novo.Login + "' ;";
            reader = comando.ExecuteReader();
            
            if (!reader.Read())
            {
                reader.Close();
                comando.CommandText = "select*from Usuario where Cpf='" + novo.Cpf + "' or Login= '" + novo.Login + "' ;";
                reader = null;
                reader = comando.ExecuteReader();
                if (!reader.Read())
                {
                    reader.Close();
                    comando.CommandText = "insert into Usuario(Nome, Idade, Cpf, Login, Senha, Situacao)Values(@Nome, @Idade,@Cpf,@Login,@Senha,@Situacao)";
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
