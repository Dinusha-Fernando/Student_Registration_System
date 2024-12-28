using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace Student_Registration_System
{
    public partial class ManageCourseForm : Form
    {
        Course__DB course = new Course__DB();
        public ManageCourseForm()
        {
            InitializeComponent();
        }

        private void ManageCourseForm_Load(object sender, EventArgs e)
        {
            showData();
        }
        //show data of the course
        private void showData()
        {
            //to show course list on datagridview
            DataGridView_Course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `coursedb`"));
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txtCoursid.Clear();
            txtBox_Course.Clear();
            txtBox_hour.Clear();
            txtBox_Description.Clear();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (txtBox_Course.Text == "" || txtBox_hour.Text == ""|| txtCoursid.Text.Equals(""))
            {
                btn_clear.PerformClick();
                MessageBox.Show("Need Course data", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                int id = Convert.ToInt32(txtCoursid.Text);
                String cName = txtBox_Course.Text;
                int chr = Convert.ToInt32(txtBox_hour.Text);
                String desc = txtBox_Description.Text;

                if (course.updateCourse(id,cName, chr, desc))
                {
                    MessageBox.Show("Update Success!", "Update course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Update Not Success!", "Update Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

            if ( txtCoursid.Text.Equals(""))
            {
                btn_clear.PerformClick();
                MessageBox.Show("Need Course Id", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                try
                {
                    int id = Convert.ToInt32(txtCoursid.Text);


                    if (course.deleteCourse(id))
                    {
                        MessageBox.Show("Course Deleted!", "Remove course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch(Exception ex)
               
                {
                    MessageBox.Show(ex.Message, "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void DataGridView_Course_Click(object sender, EventArgs e)
        {
            txtCoursid.Text = DataGridView_Course.CurrentRow.Cells[0].Value.ToString();
            txtBox_Course.Text = DataGridView_Course.CurrentRow.Cells[1].Value.ToString();
            txtBox_hour.Text = DataGridView_Course.CurrentRow.Cells[2].Value.ToString();
            txtBox_Description.Text = DataGridView_Course.CurrentRow.Cells[3].Value.ToString();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            // to search course and show on datagriview
            DataGridView_Course.DataSource = course.getCourse(new MySqlCommand("SELECT* FROM `coursedb` WHERE CONCAT(`CourseName`)LIKE '%"+txtBox_search.Text+"%'"));
            txtBox_search.Clear();
        }

        private void txtBox_search_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
