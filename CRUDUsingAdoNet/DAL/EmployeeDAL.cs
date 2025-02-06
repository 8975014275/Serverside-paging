using CRUDUsingAdoNet.Models;
using System.Data.SqlClient;

namespace CRUDUsingAdoNet.DAL
{
    public class EmployeeDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private readonly IConfiguration configuration;
        public EmployeeDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            string constr = configuration["ConnectionStrings:defaultConnection"];
            con = new SqlConnection(constr);
        }
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employeelist = new List<Employee>();
            string qry = "Select * from Employee";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Employee employee = new Employee();
                    employee.Id = Convert.ToInt32(dr["Id"]);
                    employee.Name = dr["Name"].ToString();
                    employee.CompanyName = dr["CompanyName"].ToString();
                    employee.Salary = Convert.ToInt32(dr["Salary"]);
                    employeelist.Add(employee);
                }
            }
            con.Close();
            return employeelist;

        }
        public Employee GetEmployeeById(int id)
        {
            Employee employee = new Employee();

            string qry = "Select * from Employee where Id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    employee.Id = Convert.ToInt32(dr["Id"]);//this Id should match in col
                    employee.Name = dr["Name"].ToString();
                    employee.CompanyName = dr["CompanyName"].ToString();
                    employee.Salary = Convert.ToInt32(dr["Salary"]);

                }
            }
            con.Close();
            return employee;


        }
        public int AddEmployee(Employee empl)
        {
            string qry = "insert into Employee values(@name,@comp,@sal)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", empl.Name);
            cmd.Parameters.AddWithValue("@comp", empl.CompanyName);
            cmd.Parameters.AddWithValue("@sal", empl.Salary);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;




        }
        public int UpdateEmployee(Employee empl)
        {
            string qry = "update Employee set Name=@name,CompanyName=@comp,Salary=@sal where Id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", empl.Id);
            cmd.Parameters.AddWithValue("@name", empl.Name);
            cmd.Parameters.AddWithValue("@comp", empl.CompanyName);
            cmd.Parameters.AddWithValue("@sal", empl.Salary);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;

        }
        public int DeleteEmployee(int id)
        {
            string qry = "delete from Employee where Id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
    }
}

