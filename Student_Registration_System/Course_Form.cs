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
    public partial class Course_Form : Form
    {
        Course__DB course = new Course__DB();
        public Course_Form()
        {
            InitializeComponent();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (txtBox_Course.Text == "" || txtBox_hour.Text == "")
            {
                btn_clear.PerformClick();
                MessageBox.Show("Need Course data", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {

                String cName = txtBox_Course.Text;
                int chr = Convert.ToInt32(txtBox_hour.Text);
                String desc = txtBox_Description.Text;

                if (course.insertCourse(cName, chr, desc))
                {
                    MessageBox.Show("New Course Added", "Add course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Course Not Added", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                btn_clear.PerformClick();
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txtBox_Course.Clear();
            txtBox_hour.Clear();
            txtBox_Description.Clear();
        }

        private void Course_Form_Load(object sender, EventArgs e)
        {
            showData();  
        }

        private void showData()
        {
            //to show course list on datagridview
            DataGridView_Course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `coursedb`"));
        }
    }
}
