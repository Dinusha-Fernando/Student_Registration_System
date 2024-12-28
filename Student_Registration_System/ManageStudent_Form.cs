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
    public partial class ManageStudent_Form : Form
    {
        studentClass student = new studentClass();

        public ManageStudent_Form()
        {
            InitializeComponent();
        }

        private void ManageStudent_Form_Load(object sender, EventArgs e)
        {
            //showTable();
        }

       /* //to show student list in DAtagridview
        public void showTable()
        {
            DataGridView_student.DataSource = student.getstudentList();
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[8];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }*/
        //display student data from student  to textbox
        private void DataGridView_student_Click(object sender, EventArgs e)
        {
            txtBox_Idno.Text = DataGridView_student.CurrentRow.Cells[0].Value.ToString();
            txtBox_Fname.Text = DataGridView_student.CurrentRow.Cells[1].Value.ToString();
            txtBox_Lname.Text = DataGridView_student.CurrentRow.Cells[2].Value.ToString();

            dateTimePicker1.Value = (DateTime)DataGridView_student.CurrentRow.Cells[3].Value;
            if (DataGridView_student.CurrentRow.Cells[4].Value.ToString() == "Male")
                radioButton_Male.Checked = true;

            txtBox_phone.Text = DataGridView_student.CurrentRow.Cells[5].Value.ToString();
            txtEmail.Text = DataGridView_student.CurrentRow.Cells[6].Value.ToString();
            txtBox_Address.Text = DataGridView_student.CurrentRow.Cells[7].Value.ToString();
            byte[] img = (byte[])DataGridView_student.CurrentRow.Cells[8].Value;
            MemoryStream ms = new MemoryStream(img);
            pictureBox_student.Image = Image.FromStream(ms);

        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            //button to clear
            txtBox_Fname.Clear();
            txtBox_Lname.Clear();
            txtBox_phone.Clear();
            txtEmail.Clear();
            txtBox_Idno.Clear();
            txtBox_Address.Clear();
            radioButton_Male.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
            pictureBox_student.Image = null;
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            //brows to a photo to your computer
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
                pictureBox_student.Image = Image.FromFile(opf.FileName);
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            DataGridView_student.DataSource = student.searchstudentList(txtBox_search.Text);
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[8];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }
        //create a function to verify
        bool verify()
        {
            if ((txtBox_Fname.Text == "") || (txtBox_Lname.Text == "") ||
                (txtBox_phone.Text == "") || (txtBox_Address.Text == "") ||
                (txtEmail.Text == "") || (pictureBox_student.Image == null))
            {
                return false;
            }
            else
                return true;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            // button to update new students record
            int id = Convert.ToInt32(txtBox_Idno.Text);
            String fname = txtBox_Fname.Text;
            String lname = txtBox_Lname.Text;
            DateTime bday = dateTimePicker1.Value;
            String gender = radioButton_Male.Checked ? "Male" : "Female";
            String phone = txtBox_phone.Text;
            String address = txtBox_Address.Text;
            String email = txtEmail.Text;


            //we need to check student age between 10 and 100
            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;
            if ((this_year - born_year) < 10 || (this_year - born_year) > 100)
            {
                MessageBox.Show("This Student age must be between 10 and 100", "Invalid Birthday", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify())
            {
                try
                {

                    //to get photo from picture box
                    MemoryStream ms = new MemoryStream();
                    pictureBox_student.Image.Save(ms, pictureBox_student.Image.RawFormat);
                    byte[] img = ms.ToArray();

                    if (student.updateStudent(id,fname, lname, bday, gender, phone, email, address, img))
                    {
                        //showTable();
                        MessageBox.Show("Student data update", "update Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else


            {
                MessageBox.Show("Empty Field", "update Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

       
    }
}
