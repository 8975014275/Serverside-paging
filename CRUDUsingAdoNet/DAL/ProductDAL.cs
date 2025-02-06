using CRUDUsingAdoNet.Models;
using System.Data.SqlClient;

namespace CRUDUsingAdoNet.DAL
{
    public class ProductDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private readonly IConfiguration configuration;
        public ProductDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            string constr = configuration["ConnectionStrings:defaultConnection"];
            con = new SqlConnection(constr);
        }
        public List<Product> GetAllProducts()
        {
            List<Product> productlist = new List<Product>();
            string qry = "Select * from Product";
            cmd=new SqlCommand(qry, con);
            con.Open();
            dr=cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    Product product = new Product();
                    product.Id = Convert.ToInt32(dr["Id"]);//this Id should match in col
                    product.Name = dr["Name"].ToString();
                    product.CompanyName = dr["CompanyName"].ToString();
                    product.Price = Convert.ToInt32(dr["Price"]);
                    productlist.Add(product);
                }
            }
            con.Close();
            return productlist;

        }
        public Product GetProductById(int id)
        {
            Product product = new Product();

            string qry = "Select * from Product where Id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                   
                    product.Id = Convert.ToInt32(dr["Id"]);//this Id should match in col
                    product.Name = dr["Name"].ToString();
                    product.CompanyName = dr["CompanyName"].ToString();
                    product.Price = Convert.ToInt32(dr["Price"]);
                    
                }
            }
            con.Close();
            return product;


        }
        public int AddProduct(Product prod)
        {
            string qry = "insert into Product values(@name,@comp,@pric)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name",prod.Name);
            cmd.Parameters.AddWithValue("@comp", prod.CompanyName);
            cmd.Parameters.AddWithValue("@pric", prod.Price);
            con.Open();
            int res=cmd.ExecuteNonQuery();
            con.Close();
            return res;
            



        }
        public int UpdateProduct(Product prod)
        {
            string qry = "update Product set Name=@name,CompanyName=@comp,Price=@pric where Id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", prod.Id);
            cmd.Parameters.AddWithValue("@name", prod.Name);
            cmd.Parameters.AddWithValue("@comp", prod.CompanyName);
            cmd.Parameters.AddWithValue("@pric", prod.Price);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;

        }
        public int DeleteProduct(int id)
        {
            string qry = "delete from Product where Id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id",id);
          
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
    }
}
