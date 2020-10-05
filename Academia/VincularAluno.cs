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
    public partial class VincularAluno : Form
    {
        public VincularAluno()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (AlunoController.RetornarAluno(textBox1.Text))
            {
                MessageBox.Show("Aluno cadastrado!");
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Aluno não cadastrado!");
            }
        }
    }
}
