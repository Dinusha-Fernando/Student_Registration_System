using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Registration_System
{
    public partial class splashForm : Form
    {
        public splashForm()
        {
            InitializeComponent();
        }

        private void splashForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        int startpoint = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint += 1;
            ProgressIndicator1.Start();
            if(startpoint > 30)
            {
                Log_form login = new Log_form();
                ProgressIndicator1.Stop();
                timer1.Stop();
                this.Hide();
                login.Show();
            }
        }
    }
}
