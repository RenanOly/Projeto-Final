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
using Controller;
namespace Academia
{
    public partial class ValidaSenhaAdmin : Form
    {
        public ValidaSenhaAdmin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(UsuarioDAL.logar("admin", textBox1.Text))
            {
                if (DialogResult.Yes == MessageBox.Show("Tem certeza que deseja apagar TODOS os dados?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    FeedBackController.DeletaFeedBack();
                    AlunoController.DeletaAlunos();
                    ExercicioController.DeletarExerciciosProgramados();
                    ExercicioController.DeletarExercicios();
                    ProfessorController.DeletaProfessores();
                    MessageBox.Show("DataBase deletado!");
                }
               
            }
            else
            {
                MessageBox.Show("Senha incorreta!");
            }
        }
    }
}
