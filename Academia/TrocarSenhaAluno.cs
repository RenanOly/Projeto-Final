using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controller;
namespace Academia
{
    public partial class TrocarSenhaAluno : Form
    {
        public TrocarSenhaAluno()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == textBox3.Text)
            {
                if (AlunoController.AlterarSenha(textBox1.Text, textBox2.Text))
                {
                    MessageBox.Show("Senha alterada com sucesso!");
                }
                else
                {
                    MessageBox.Show("Senha não alterada!");
                }
            }
            else
            {
                MessageBox.Show("As senhas não correspondem!");
            }
        }
    }
}
