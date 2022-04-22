using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace CRUD_Operation_In_MVC.Models
{
    public class StudentDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public StudentDAL() 
        {
            con = new SqlConnection(Startup.ConnectionString);
        }

        public List<Student> GetAllStudent()
        {
            cmd = new SqlCommand("Select * from Student", con);
            con.Open();
            dr = cmd.ExecuteReader();
            List<Student> studentList = new List<Student>();
            studentList = ArrangeList(dr);
            con.Close();
            return studentList;
        }
        public List<Student> ArrangeList(SqlDataReader dr)
        {
            List<Student> studentList = new List<Student>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Student student = new Student();

                    student.Id = Convert.ToInt32(dr["Id"]);
                    student.Name = (dr["Name"]).ToString();
                    student.CourseName = dr["CourseName"].ToString();

                    studentList.Add(student);
                }
                return studentList;
            }
            else
            {
                return null;
            }
        }
        public int Save(Student student)
        {
            string qry = "Insert into Student values (@Name, @CourseName)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@CourseName", student.CourseName);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public Student GetStudentById(int id)
        {
            cmd = new SqlCommand("select * from Student where Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            Student student = new Student();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    student.Id = Convert.ToInt32(dr["Id"]);
                    student.Name = dr["Name"].ToString();
                    student.CourseName = dr["CourseName"].ToString();

                }
            }
            con.Close();
            return student;
        }

        public int Update(Student student)
        {
            cmd = new SqlCommand("update Student set Name=@name,CourseName=@CourseName where Id=@id", con);
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@CourseName", student.CourseName);
            cmd.Parameters.AddWithValue("@id", student.Id);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int Delete(int id)
        {

            cmd = new SqlCommand("delete from Student where Id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

    }
}
