using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Student_Registration_System
{
    public partial class Log_form : Form
    {
        studentClass student = new studentClass();
        public Log_form()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
           
        }

        private void Log_form_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUname.Text == "" || txtPass.Text == "")
            {
                MessageBox.Show("Need LogIn Informations !", "Empty LogIn⚠️", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                String UserName = txtUname.Text;
                String Pass = txtPass.Text;
                DataTable table = student.getList(new MySqlCommand("SELECT * FROM `user` WHERE `UserName`='" + UserName + "' AND `Password`='" + Pass + "'"));
                if (table.Rows.Count > 0)
                {
                    MessageBox.Show("Login Success !", "Success 🔓", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Main_form obj = new Main_form();
                    this.Hide();
                    obj.Show();



                }
                else
                {
                    MessageBox.Show("Login Not Success !", "Wrong LogIn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
