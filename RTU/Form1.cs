using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTU
{
    public partial class Form1 : Form
    {
        Model mod;
        bool check = true;
        public Form1()
        {
            InitializeComponent();
            mod = new Model(this);
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            mod.run();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            mod.editing(check);
            check = !check;
        }
    }
}
