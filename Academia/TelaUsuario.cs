﻿using System;
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
    public partial class TelaUsuario : Form
    {
        public TelaUsuario()
        {
            InitializeComponent();
            List<List<object>> exercicios = ExercicioController.BuscarExercicios();
            Exercicios a = new Exercicios();
            ExerciciosProgramados b = new ExerciciosProgramados();

            for (int i = 0; i < exercicios.Count; i++)
            {
                a = (Exercicios)exercicios[i][0];
                b = (ExerciciosProgramados)exercicios[i][1];
                dataGridView1.Rows.Add(a.Nome, b.DiasDaSemana, a.Especificacoes, b.Qtde, b.DataInicio, b.DataTermino);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
