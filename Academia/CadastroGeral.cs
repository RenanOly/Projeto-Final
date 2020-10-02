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
            /*Professor novo = new Professor();

            novo.Nome = "Caio";
            novo.Idade = 20;
            novo.Cpf = "12312312312";
            novo.Telefone = "123123123123123";
            novo.Login = "kakasampaio";
            novo.senha = "cainho";
            novo.Situacao = 1;
            ProfessorController.Inserir(novo);*/
            if (comboBox1.Text!= "Escolha o tipo" && textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "" && textBox4.Text.Trim() != "" && textBox5.Text.Trim() != "" && textBox6.Text.Trim() != "")
            {
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
                            novo.senha = textBox6.Text;
                            novo.Situacao = 1;
                            if (ProfessorController.Inserir(novo))
                            {
                                MessageBox.Show("Cadastro realizado com sucesso!");
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
                            novo.senha = textBox6.Text;
                            novo.Situacao = 1;
                            AlunoController.Inserir(novo);
                            MessageBox.Show("Cadastro realizado com sucesso!");

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
