using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Student_Registration_System
{
    public partial class Registration_form : Form
    {
        studentClass student = new studentClass();
        public Registration_form()
        {
            InitializeComponent();
        }

        private void Registration_form_Load(object sender, EventArgs e)
        {
           showTable();
        }
        //to show student list in DAtagridview
        public void showTable()
        {
            DataGridView_student.DataSource = student.getstudentList();
            // Assuming that the 'Photo' column is at index 8 or you can use its name
            DataGridViewImageColumn imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns["Photo"];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }






        //create a function to verify
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        bool verify()
        {
            if ((txtBox_Fname.Text == "") || (txtBox_Lname.Text == "") ||
                (txtBox_phone.Text == "") || (txtBox_Address.Text == "") ||
                (txtEmail.Text == "") || (pictureBox_student.Image == null))
            {
                return false;
            }
            else if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Invalid Email Address", "Email Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }






        private void btn_upload_Click(object sender, EventArgs e)
        {
            //brows to a photo to your computer
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
                pictureBox_student.Image = Image.FromFile(opf.FileName);


        }



        const int MinAge = 20;
        const int MaxAge = 100;

        private void btn_add_Click(object sender, EventArgs e)
        {
            String fname = txtBox_Fname.Text;
            String lname = txtBox_Lname.Text;
            DateTime bday = dateTimePicker1.Value;
            String gender = radioButton_Male.Checked ? "Male" : "Female";
            String phone = txtBox_phone.Text;
            String address = txtBox_Address.Text;
            String email = txtEmail.Text;

            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;

            // Use constants for age validation
            if ((this_year - born_year) < MinAge || (this_year - born_year) > MaxAge)
            {
                MessageBox.Show($"This student's age must be between {MinAge} and {MaxAge}.", "Invalid Birthday", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify())
            {
                try
                {
                    MemoryStream ms = new MemoryStream();
                    pictureBox_student.Image.Save(ms, pictureBox_student.Image.RawFormat);
                    byte[] img = ms.ToArray();

                    if (student.insertStudent(fname, lname, bday, gender, phone, email, address, img))
                    {
                        showTable();
                        MessageBox.Show("New Student Added", "Added Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Field", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            btn_clear.PerformClick();
        }



        private void btn_clear_Click(object sender, EventArgs e)
        {
            txtBox_Fname.Clear();
            txtBox_Lname.Clear();
            txtBox_phone.Clear();
            txtEmail.Clear();
            txtBox_Address.Clear();
            radioButton_Male.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
            pictureBox_student.Image = null; // Clear the image
        }


    }
}
