using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Model;
using Controller;
using System.Security.Cryptography;
namespace Academia
{
    public partial class CadastroGeral : Form
    {
        public CadastroGeral()
        {
            InitializeComponent();
            comboBox1.Text = "Escolha o tipo";
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (comboBox1.Text!= "Escolha o tipo" && textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "" && textBox4.Text.Trim() != "" && textBox5.Text.Trim() != "" && textBox6.Text.Trim() != "")
            {
                SHA256 mySHA256 = SHA256.Create();

                byte[] hashValue = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(textBox6.Text));
                string senhaHash = "";
                for (int i = 0; i < hashValue.Length; i++)
                {
                    senhaHash += hashValue[i].ToString("x2");
                }
                
                try
                {
                    bool telefone = Regex.IsMatch(textBox4.Text, @"^\d{16}$");
                    bool telefone2 = Regex.IsMatch(textBox4.Text, @"^\d{15}$");
                    bool senha = Regex.IsMatch(textBox6.Text, @"(?!^[0-9]$)(?!^[a-zA-Z]$)^([a-zA-Z0-9]{8,15})$");
                    if (int.Parse(textBox2.Text) > 0 && int.Parse(textBox2.Text) < 120 && textBox3.Text.Count() == 11 && telefone == true || telefone2 == true && senha== true)
                    {
                        
                        if (comboBox1.Text == "Professor")
                        {
                            Professor novo = new Professor();
                            novo.Nome = textBox1.Text;
                            novo.Idade = int.Parse(textBox2.Text);
                            novo.Cpf = textBox3.Text;
                            novo.Telefone = textBox4.Text;
                            novo.Login = textBox5.Text;
                            novo.senha = senhaHash;
                            novo.Situacao = 1;
                            if (ProfessorController.Inserir(novo))
                            {
                                MessageBox.Show("Cadastro realizado com sucesso!");
                                textBox1.Clear();
                                textBox2.Clear();
                                textBox3.Clear();
                                textBox4.Clear();
                                textBox5.Clear();
                                textBox6.Clear();
                            }
                            else
                            {
                                MessageBox.Show("Usuário já existente!");
                            }
                           
                        }
                        else
                        {
                            Aluno novo = new Aluno();
                            novo.Nome = textBox1.Text;
                            novo.Idade = int.Parse(textBox2.Text);
                            novo.Cpf = textBox3.Text;
                            novo.Telefone = textBox4.Text;
                            novo.Login = textBox5.Text;
                            novo.senha = senhaHash;
                            novo.Situacao = 1;
                            if (AlunoController.Inserir(novo))
                            {
                                MessageBox.Show("Cadastro realizado com sucesso!");
                                textBox1.Clear();
                                textBox2.Clear();
                                textBox3.Clear();
                                textBox4.Clear();
                                textBox5.Clear();
                                textBox6.Clear();
                                
                            }
                            else
                            {
                                MessageBox.Show("Usuário já existente!");
                            }

                        }

                    }
                }
                catch
                {
                    MessageBox.Show("Dados fornecidos inválidos!\nConfira os dados novamente!");
                }


            }
            else
            {
                MessageBox.Show("Dados fornecidos inválidos!\nConfira os dados novamente!!!");
            }
        }
    }
}
