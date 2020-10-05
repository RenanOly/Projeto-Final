using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
namespace Academia
{
    public partial class TrocarSenhaAdmin : Form
    {
        public TrocarSenhaAdmin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == textBox3.Text)
            {
                if(UsuarioDAL.AlterarSenhaAdmin(textBox1.Text, textBox2.Text))
                {
                    MessageBox.Show("Senha alterada com sucesso!");
                }
                else
                {
                    MessageBox.Show("Senha não alterada!");
                }
            }
        }
    }
}
