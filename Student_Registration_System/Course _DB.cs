using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Student_Registration_System
{
    class Course__DB
    {
        DBconnect connect = new DBconnect();
        //create function to insert course
        public bool insertCourse(String cName,int hr,String desc)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `coursedb`( `CourseName`, `CourseHour`, `Description`) VALUES (@cn,@ch,@desc)", connect.GetConnection);
            //@cn,@ch,@desc
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = cName;
            command.Parameters.Add("@ch", MySqlDbType.Int32).Value =hr;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = desc;
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
        //create function to get course list
        public DataTable getCourse(MySqlCommand command)
        {
            command.Connection = connect.GetConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        //create a update function
        public bool updateCourse( int id,String cName, int hr, String desc)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `coursedb` SET `CourseName`=@cn, `CourseHour`=@ch, `Description`=@desc WHERE `Courseid`=@id", connect.GetConnection);
            //@id,@cn,@ch,@desc
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = cName;
            command.Parameters.Add("@ch", MySqlDbType.Int32).Value = hr;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = desc;
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
             //create a function to delete a course
               // we only need course id
             public bool deleteCourse(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `coursedb` WHERE `Courseid`=@id", connect.GetConnection);
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
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

    }
}
