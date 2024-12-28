using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using DGVPrinterHelper;
using MySqlX.XDevAPI.Relational;
using TheArtOfDevHtmlRenderer.Adapters;

namespace Student_Registration_System
{
    public partial class printStudent : Form
    {
        studentClass student = new studentClass();
        DGVPrinter printer = new DGVPrinter();

        public printStudent()
        {
            InitializeComponent();
        }

        private void printStudent_Load(object sender, EventArgs e)
        {
            showData(new MySqlCommand("SELECT * FROM `student`"));
            
            
}
        //create a function to show the student list in datagridView
        public void showData(MySqlCommand command)
        {
            DataGridView_student.ReadOnly = true;
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
           
            DataGridView_student.DataSource = student.getList(command);

            //column 8is the image column index
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[8];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;


        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            //check the radio button
            String selectQuery;
            if (radioButton_all.Checked)
            {
                selectQuery = "SELECT * FROM `student`";
            }
            else if (radioButton_Male.Checked)
            {
                selectQuery = "SELECT * FROM `student` WHERE `Gender` ='Male'";
            }
            else
            {
                selectQuery = "SELECT * FROM `student` WHERE `Gender` ='Female'";
            }
            showData(new MySqlCommand(selectQuery));
            
            
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            //we need DGVprinter helper for print pdf fil
            printer.Title = "SLIATE Students List";
            printer.SubTitle = string.Format("Date:(0)", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "Sri Lanka Institute of Advanced Technological Education";
            printer.FooterSpacing = 15;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(DataGridView_student);

        }
    }
}