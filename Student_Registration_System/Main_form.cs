using System;
using System.Windows.Forms;

namespace Student_Registration_System
{
    public partial class Main_form : Form
    {
        studentClass student = new studentClass();
        private Form activeForm = null; // track the active child form

        public Main_form()
        {
            InitializeComponent();
            customizeDesign();
        }

        private void Main_form_Load(object sender, EventArgs e)
        {
            studentCount();
        }

        // Function to display student count
        private void studentCount()
        {
            try
            {
                lbl_totalestd.Text = "Total Students: " + student.totalStudents();
                lbl_male.Text = "Male: " + student.maleStudents();
                lbl_female.Text = "Female: " + student.femaleStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching student count: " + ex.Message);
            }
        }

        // Customize the submenu design
        private void customizeDesign()
        {
            panel_stdSubmenu.Visible = false;
            panel_courseSubmenu.Visible = false;
        }

        private void hideSubmenu()
        {
            if (panel_stdSubmenu.Visible == true)
                panel_stdSubmenu.Visible = false;
            if (panel_courseSubmenu.Visible == true)
                panel_courseSubmenu.Visible = false;
        }

        private void showSubmenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hideSubmenu();
                submenu.Visible = true;
            }
            else
                submenu.Visible = false;
        }

        private void btn_std_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_stdSubmenu);
        }

        #region StdSubmenu

        private void btn_reg_Click(object sender, EventArgs e)
        {
            openChildForm(new Registration_form());
            hideSubmenu();
        }

        private void btn_Mstd_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageStudent_Form());
            hideSubmenu();
        }

        private void btn_Status_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            openChildForm(new printStudent());
            hideSubmenu();
        }

        #endregion

        private void btn_course_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_courseSubmenu);
        }

        #region CourseSubmenu

        private void btn_Ncourse_Click(object sender, EventArgs e)
        {
            openChildForm(new Course_Form());
            hideSubmenu();
        }

        private void btn_manageCourse_Click_1(object sender, EventArgs e)
        {
            openChildForm(new ManageCourseForm());
            hideSubmenu();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new PrintCourse_Form());
            hideSubmenu();
        }

        #endregion

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // To show a form in the main panel
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();  // Close the previous form

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_main.Controls.Add(childForm);
            panel_main.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btn_Dashboard_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();

            panel_main.Controls.Clear();   // Clear all controls in the main panel
            panel_main.Controls.Add(panel_cover);  // Add the cover panel back
            panel_cover.BringToFront();
            studentCount();  // Refresh the student count
        }
    }
}
