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
using DAL;

namespace Academia
{
    public partial class TelaAdmin : Form
    {
        
        public TelaAdmin()
        {
            InitializeComponent();
            dataGridView1.ReadOnly = true;
            recarregarData();
        }

        public void recarregarData()
        {
            dataGridView1.Rows.Clear();
           List<Aluno> alunos = AlunoController.BuscarTodosalunos();
            for (int i = 0; i < alunos.Count; i++)
            {
                string situacao = "";
                if (alunos[i].Situacao==1)
                {
                    situacao = "Ativo";
                }
                else
                {
                    situacao = "Inativo";
                }
                dataGridView1.Rows.Add(alunos[i].Nome, alunos[i].Cpf, "Aluno",situacao);
            }
            List<Professor> professores = ProfessorController.BuscarTodosprofessores();
            for (int i = 0; i < professores.Count; i++)
            {
                string situacao = "";
                if (professores[i].Situacao == 1)
                {
                    situacao = "Ativo";
                }
                else
                {
                    situacao = "Inativo";
                }
                dataGridView1.Rows.Add(professores[i].Nome, professores[i].Cpf, "Professor", situacao);
            }


        }
        private void button1_Click(object sender, EventArgs e)
        {
            new CadastroGeral().ShowDialog();
            recarregarData();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new ValidaSenhaAdmin().ShowDialog();
            recarregarData();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            int Linha = dataGridView1.CurrentRow.Index;
            string situacao = dataGridView1.Rows[Linha].Cells[3].Value.ToString();
            string usuario = dataGridView1.Rows[Linha].Cells[2].Value.ToString();
            string cpf = dataGridView1.Rows[Linha].Cells[1].Value.ToString();
            UsuarioDAL.MudarSituacao(situacao,usuario,cpf);
            recarregarData();
        }
    }
}
