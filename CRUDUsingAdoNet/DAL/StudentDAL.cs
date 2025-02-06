using CRUDUsingAdoNet.Models;
using System.Data.SqlClient;

namespace CRUDUsingAdoNet.DAL
{
    public class StudentDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private readonly IConfiguration configuration;
        public StudentDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            string constr = configuration["ConnectionStrings:defaultConnection"];
            con = new SqlConnection(constr);
        }
        public List<Student> GetAllStudents()
        {
            List<Student> studentlist = new List<Student>();
            string qry = "Select * from Student";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Student student = new Student();
                    student.Id = Convert.ToInt32(dr["Id"]);
                    student.Name = dr["Name"].ToString();
                    student.SchoolName = dr["SchoolName"].ToString();
                    student.Percentage = Convert.ToInt32(dr["Percentage"]);
                    studentlist.Add(student);
                }
            }
            con.Close();
            return studentlist;

        }
        public Student GetStudentById(int id)
        {
            Student student = new Student();

            string qry = "Select * from Student where Id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    student.Id = Convert.ToInt32(dr["Id"]);
                    student.Name = dr["Name"].ToString();
                    student.SchoolName = dr["SchoolName"].ToString();
                    student.Percentage = Convert.ToInt32(dr["Percentage"]);

                }
            }
            con.Close();
            return student;


        }
        public int AddStudent(Student stud)
        {
            string qry = "insert into Student values(@name,@scho,@per)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", stud.Name);
            cmd.Parameters.AddWithValue("@scho", stud.SchoolName);
            cmd.Parameters.AddWithValue("@per", stud.Percentage);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;




        }
        public int UpdateStudent(Student stud)
        {
            string qry = "update Student set Name=@name,SchoolName=@scho,Percentage=@per where Id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", stud.Id);
            cmd.Parameters.AddWithValue("@name", stud.Name);
            cmd.Parameters.AddWithValue("@scho", stud.SchoolName);
            cmd.Parameters.AddWithValue("@per", stud.Percentage);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;

        }
        public int DeleteStudent(int id)
        {
            string qry = "delete from Student where Id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
    }
}
