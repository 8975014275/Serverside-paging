using CRUDUsingAdoNet.Models;
using System.Data;
using System.Data.SqlClient;

namespace CRUDUsingAdoNet.DAL
{
    public class Categorydal
    {
        private readonly string Connectionstring;
        public Categorydal(string connectionstring)
        {
            Connectionstring = connectionstring;
        }
        public List<Category> GetAllCategory()
        {
            List <Category > categories= new List<Category>();
            using (SqlConnection con = new SqlConnection(Connectionstring))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("sp_GetAllCategories", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader sda = cmd.ExecuteReader())
                    {
                        while (sda.Read())
                        {
                            Category category = new Category();
                            category.Id = sda.GetInt32(0);
                            category.Name = sda.GetString(1);
                            category.Description = sda.GetString(2);
                            category.Type = sda.GetString(2);
                            categories.Add(category);



                        }

                    }
                    con.Close();
                   
                }

            }
            return categories;
        }
        public int InsertCategory(Category cat)
        {
            int insertCategoryId = 0;
            using (SqlConnection con = new SqlConnection(Connectionstring))
            {
                con.Open();
                using(SqlCommand cmd =new SqlCommand("sp_InsertCategory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                  
                    cmd.Parameters.AddWithValue("@Name", cat.Name);
                    cmd.Parameters.AddWithValue("@Description", cat.Description);
                    cmd.Parameters.AddWithValue("@Type", cat.Type);
                    insertCategoryId = cmd.ExecuteNonQuery();
                    
                }
            }
            return insertCategoryId;
        }
        public Category GetCategoryById(int id)
        {
            Category cat = new Category();
            using(SqlConnection con = new SqlConnection(Connectionstring))
            {
                con.Open();
                using (SqlCommand cmd=new SqlCommand("Sp_GetCategoryById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoryId", id);
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            cat.Id = sdr.GetInt32(0);
                            cat.Name = sdr.GetString(1);
                            cat.Description = sdr.GetString(2);
                            cat.Type=sdr.GetString(3);


                        }
                    }
                }
                con.Close();
            }
            return cat;

        }
        public List<Category> GetPagedCategories(int pageSize, int skip, string searchValue, out int totalRecords)
        {
            List<Category> categories = new List<Category>();
            totalRecords = 0;

            using (SqlConnection con = new SqlConnection(Connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("sp_paging_CategoriesData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    cmd.Parameters.AddWithValue("@SearchValue", searchValue ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Skip", skip);

                    SqlParameter outputParam = new SqlParameter("@PageCount", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new Category
                            {
                                Id =reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                Type = reader.GetString(3),
                            });
                        }
                    }
                    totalRecords = (int)cmd.Parameters["@PageCount"].Value;
                }
            }

            return categories;
        }
        public int UpdateCategory(Category cat)
        {
            int insertCategoryId = 0;
            using (SqlConnection con = new SqlConnection(Connectionstring))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Sp_UpdateCategory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoryId", cat.Id);
                    cmd.Parameters.AddWithValue("@Name", cat.Name);
                    cmd.Parameters.AddWithValue("@Description", cat.Description);
                    cmd.Parameters.AddWithValue("@Type", cat.Type);
                    insertCategoryId = cmd.ExecuteNonQuery();

                }
            }
            return insertCategoryId;
        }
        public void DeleteCategory(int id)
        {
            using (SqlConnection con = new SqlConnection(Connectionstring))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Sp_DeleteCategory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoryId", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
