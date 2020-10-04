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
using Model;
namespace Academia
{
    public partial class TelaProfessor : Form
    {
        public TelaProfessor()
        {
            InitializeComponent();
            List<Aluno> alunos=AlunoController.BuscarAlunos();
            for(int i =0; i < alunos.Count; i++)
            {
                dataGridView1.Rows.Add(alunos[i].Nome, alunos[i].Idade, alunos[i].Cpf);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            new VincularAluno().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new CadastroExercicios().ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new ProgramarExercicios().ShowDialog();
        }
    }
}
