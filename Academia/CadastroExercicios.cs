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
namespace Academia
{
    public partial class CadastroExercicios : Form
    {
        public CadastroExercicios()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExercicioController.CadastraExercicio(textBox1.Text, textBox2.Text);
            this.Close();
        }
    }
}
