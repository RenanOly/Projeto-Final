using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;
namespace DAL
{
    public static class AlunoDAL
    {
        public static void Inserir(Aluno novo)
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
            comando.CommandText = "select*from Usuario where Cpf='" + novo.Cpf + "' ;";
            reader = comando.ExecuteReader();
            
            if (reader.Read()==false)
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
                throw new Exception("Cpf já cadastrado!");
            }
            conexao.Close();
        }
    }
}
