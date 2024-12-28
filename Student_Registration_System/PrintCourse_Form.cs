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
using DGVPrinterHelper;

namespace Student_Registration_System
{
    public partial class PrintCourse_Form : Form
    {
        Course__DB course = new Course__DB();
        DGVPrinter printer = new DGVPrinter();
        public PrintCourse_Form()
        {
            InitializeComponent();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            // to search course and show on datagriview
            DataGridView_Course.DataSource = course.getCourse(new MySqlCommand("SELECT* FROM `coursedb` WHERE CONCAT(`CourseName`)LIKE '%" + txt_search.Text + "%'"));
            txt_search.Clear();
        }

        private void PrintCourse_Form_Load(object sender, EventArgs e)
        {
            DataGridView_Course.DataSource = course.getCourse(new MySqlCommand("SELECT* FROM `coursedb`"));
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
            printer.PrintDataGridView(DataGridView_Course);
        }
    }
}
