using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using Model;
namespace DAL
{
    public static class ExercicioDAL
    {


        public static void DeletarExercicios()
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
            comando.CommandText = "delete from Exercicios";
            try
            {
                comando.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception("Erro ao deletar Exercicios!");
            }
            conexao.Close();
        }
        public static void DeletarExerciciosProgramados()
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
            comando.CommandText = "delete from ExerciciosProgramados";
            try
            {
                comando.ExecuteNonQuery();
            }
            catch
            {
                throw new Exception("Erro ao deletar ExerciciosProgramados!");
            }
            conexao.Close();
        }
        public static List<List<object>> RetornaExercicios(string CPFProfessor)
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
            List<object> Exercicios = new List<object>();
            List<Exercicios> ExerciciosCopia = new List<Exercicios>();
            List<ExerciciosProgramados> ExerciciosProgramadosCopia = new List<ExerciciosProgramados>();
            List<List<object>> listaExercicios = new List<List<object>>();
            List<int> IDExercicios = new List<int>();
            Exercicios a1 = new Exercicios();
            ExerciciosProgramados a2 = new ExerciciosProgramados();
            
            comando.CommandText = "select*from ExerciciosProgramados where Professor= '" + CPFProfessor + "' ;";
            reader = comando.ExecuteReader();
            while (reader.Read())
            {
                IDExercicios.Add((int)reader["Exercicio"]);
                a2 = new ExerciciosProgramados();
                a2.DataInicio = (DateTime)reader["DataInicio"];
                a2.DataTermino = (DateTime)reader["DataTermino"];
                a2.Qtde = (int)reader["Qtde"];
                a2.DiasDaSemana = reader["DiasSemana"].ToString();
                ExerciciosProgramadosCopia.Add(a2);
            }
            reader.Close();
            
           
            comando.CommandText = "select*from Exercicios;";
            reader = comando.ExecuteReader();
            int count = 0;
            int aux = count;
            while (reader.Read())
            {
                
                for (int i = 0; i < IDExercicios.Count-aux; i++)
                {
                    
                    if (IDExercicios[aux] == (int)reader["ID"])
                    {
                        
                        a1 = new Exercicios();
                        a1.Nome = reader["Nome"].ToString();
                        a1.Especificacoes = reader["Spec"].ToString();
                        ExerciciosCopia.Add(a1);
                    }
                    aux++;
                }
                count++;
                aux = count;
                
            }
            for (int i = 0; i < ExerciciosProgramadosCopia.Count; i++)
            {
                Exercicios = new List<object>();
                Exercicios.Add(ExerciciosCopia[i]);
                Exercicios.Add(ExerciciosProgramadosCopia[i]);
                listaExercicios.Add(Exercicios);
            }
            reader.Close();
            conexao.Close();

            return listaExercicios;
        }
        public static bool ProgramarExercicio(string nome, string inicio, string termino, string repeticoes, string diasSemana)
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
            
            comando.CommandText = "select*from Exercicios where Nome= '" + nome + "' ;";
            reader = comando.ExecuteReader();
            if (reader.Read())
            {   

                int IdExercicio = int.Parse(reader["ID"].ToString());
                int id = (int)reader["ID"];
                reader.Close();
                comando.CommandText = "select*from ExerciciosProgramados where Exercicio= '" + id + "' ;";
                reader = comando.ExecuteReader();
                if (!reader.Read())
                {
                    reader.Close();
                    StreamReader arq = new StreamReader("login.txt", false);
                    string CPFProfessor = arq.ReadLine();
                    arq.Close();

                    comando.CommandText = "insert into ExerciciosProgramados(DataInicio, DataTermino,Qtde,DiasSemana,Professor,Exercicio)Values(@DataInicio, @DataTermino,@Qtde,@DiasSemana,@Professor,@Exercicio)";
                    SqlParameter param = new SqlParameter("@DataInicio", System.Data.SqlDbType.VarChar);
                    param.Value = inicio;
                    comando.Parameters.Add(param);

                    param = new SqlParameter("@DataTermino", System.Data.SqlDbType.VarChar);
                    param.Value = termino;
                    comando.Parameters.Add(param);

                    param = new SqlParameter("@Qtde", System.Data.SqlDbType.VarChar);
                    param.Value = repeticoes;
                    comando.Parameters.Add(param);

                    param = new SqlParameter("@DiasSemana", System.Data.SqlDbType.VarChar);
                    param.Value = diasSemana;
                    comando.Parameters.Add(param);

                    param = new SqlParameter("@Professor", System.Data.SqlDbType.VarChar);
                    param.Value = CPFProfessor;
                    comando.Parameters.Add(param);

                    param = new SqlParameter("@Exercicio", System.Data.SqlDbType.VarChar);
                    param.Value = IdExercicio;
                    comando.Parameters.Add(param);

                    comando.ExecuteNonQuery();
                    conexao.Close();
                    return true;
                }
                else
                {
                    conexao.Close();
                    throw new Exception("você não pode cadastrar exercícios iguais!");
                }


            }
            else
            {
                conexao.Close();
                throw new Exception("O exercício digitado não existe!");
            }
            


            

        }

        public static bool Inserir(string nome, string spec)
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

            comando.CommandText = "select*from Exercicios where Nome= '" + nome + "' ;";
            reader = comando.ExecuteReader();
            if (!reader.Read())
            {
                reader.Close();
                comando.CommandText = "insert into Exercicios(Nome, Spec)Values(@Nome, @Spec)";
                SqlParameter param = new SqlParameter("@Nome", System.Data.SqlDbType.VarChar);
                param.Value = nome;
                comando.Parameters.Add(param);

                param = new SqlParameter("@Spec", System.Data.SqlDbType.VarChar);
                param.Value = spec;
                comando.Parameters.Add(param);
                comando.ExecuteNonQuery();
                return true;
            }
            else
            {
                throw new Exception("Exercício já cadastrado!");
            }
        }

    }
}
