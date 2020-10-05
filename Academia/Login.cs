
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using DAL;


namespace Academia
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
           


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (UsuarioDAL.logar(textBox1.Text, textBox2.Text))
            {
                if (textBox1.Text == "admin")
                {
                    if (UsuarioDAL.PrimeiroAcesso(textBox2.Text))
                    {
                        new TrocarSenhaAdmin().ShowDialog();
                    }
                    this.Visible = false;
                    new TelaAdmin().ShowDialog();
                    this.Close();
                }
                else
                {
                    this.Visible = false;
                    new TelaUsuario().ShowDialog();
                    this.Close();
                }
                
            }
            else
            {
                if (ProfessorDAL.Logar(textBox1.Text, textBox2.Text))
                {
                    this.Visible = false;
                    new TelaProfessor().ShowDialog();
                    
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuário ou senha inválidos!\nVerifique seu usuário e senha ou a situação com o Administrador! ");
                    
                }
                
            }
           
                    

        }
    }
}
