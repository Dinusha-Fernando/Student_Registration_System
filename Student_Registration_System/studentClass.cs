using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using Mysqlx.Crud;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Windows.Forms;

namespace Student_Registration_System
{
    class studentClass
    {
        DBconnect connect = new DBconnect();
        //create a function to add a new student to the database

        public bool insertStudent(String fname, String lname, DateTime bday, String gender, String phone, String email, String address, byte[] img)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `student`( `StdFirstName`, `StdLastName`, `Birthday`, `Gender`, `phone`, `Email`, `Address`, `Photo`) VALUES (@fn,@ln,@bd,@gd,@ph,@eml,@adr,@img)", connect.GetConnection);

            //@fn, @ln, @bd, @gd, @ph, @eml, @adr, @img

            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;

            command.Parameters.Add("@bd", MySqlDbType.Date).Value = bday.ToString("yyyy-MM-dd");


            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@eml", MySqlDbType.VarChar).Value = email;
            command.Parameters.Add("@adr", MySqlDbType.VarChar).Value = address;
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = img;

            connect.openConnect();
            if(command.ExecuteNonQuery() == 1)
            {
                connect.closeConnection();
                return true;
            }
            else
            {
                connect.closeConnection();
                return false;
            }
        }
       //to get student table
        public DataTable getstudentList()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `student`", connect.GetConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        
          }

        //create function to execute the count query(total,male,female)
        public String exeCount(String query)
        {
            MySqlCommand command = new MySqlCommand(query,connect.GetConnection);
            connect.openConnect();
            String count = command.ExecuteScalar().ToString();
            connect.closeConnection();
            return count;
        }
        //to get the total student
        public String totalStudents()
        {
            return exeCount("SELECT COUNT(*) FROM student");

        }
        //to get the male student count
        public String maleStudents()
        {
            return exeCount ("SELECT COUNT(*) FROM student WHERE Gender='Male'");

        }
        //to get the female students count
        public String femaleStudents()
        {
            return exeCount("SELECT COUNT(*) FROM student WHERE Gender='Female'");

        }
        //create function search for student (first name,last name,address)
          public DataTable searchstudentList(String searchdata)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `student` WHERE CONCAT(`StdFirstName`,`StdLastName`,`Address`) LIKE '%"+searchdata +"%'", connect.GetConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        
          }
        public bool updateStudent(int idno,String fname, String lname, DateTime bday, String gender, String phone, String email, String address, byte[] img)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `student` SET `StdFirstName`=@fn, `StdLastName`=@ln, `Birthday`=@bd, `Gender`=@gd, `phone`=@ph, `Email`=@eml, `Address`=@adr, `Photo`=@img WHERE `StdId`=@id", connect.GetConnection);

            //@id,@fn, @ln, @bd, @gd, @ph, @eml, @adr, @img

            command.Parameters.Add("@id", MySqlDbType.VarChar).Value =idno;
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bd", MySqlDbType.Date).Value = bday;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@eml", MySqlDbType.VarChar).Value = email;
            command.Parameters.Add("@adr", MySqlDbType.VarChar).Value = address;
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = img;


            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnection();
                return true;
            }
            else
            {
                connect.closeConnection();
                return false;
            }
        }
        //create a funtion for any command in studentdb
        public DataTable getList(MySqlCommand command)
        {
            command.Connection = connect.GetConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

       
    }
}
