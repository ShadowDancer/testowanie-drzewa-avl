using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AVLTrees
{
    public partial class Form2 : Form
    {
        Form par;

        public Form2(Form par, int LLRC, int RRRC, int LRRC, int RLRC)
        {
            InitializeComponent();
            this.par = par;
            label5.Text = LLRC.ToString();
            label6.Text = RRRC.ToString();
            label7.Text = LRRC.ToString();
            label8.Text = RLRC.ToString();
            if (par is Form3) par.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
