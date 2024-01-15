using System.Data.SqlClient;

namespace IndustrialServices_API.Models
{
    public class CustomerMapping
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public CustomerMapping(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<CustomerMappingModel> GetAllCustomerMapping()
        {
            List<CustomerMappingModel> customerm = new List<CustomerMappingModel>();
            try
            {
                string query = "SELECT * FROM Customer_Mapping WHERE status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CustomerMappingModel cm = new CustomerMappingModel
                    {
                        id_customer_mapping = Convert.ToInt32(reader["id_customer_mapping"].ToString()),
                        nama_logo = reader["nama_logo"].ToString(),
                        grade_logo = Convert.ToInt32(reader["grade_logo"].ToString()),
                        path_logo = reader["path_logo"].ToString(),
                        status = Convert.ToInt32(reader["status"].ToString()),
                    };
                    customerm.Add(cm);
                }
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return customerm;
        }
        public CustomerMappingModel GetCustomerMappingById(int id)
        {
            CustomerMappingModel customerMapping = new CustomerMappingModel();
            try
            {
                string query = "SELECT * FROM Customer_Mapping WHERE id_customer_mapping = @id AND status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@id", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    customerMapping.id_customer_mapping = Convert.ToInt32(reader["id_customer_mapping"].ToString());
                    customerMapping.nama_logo = reader["nama_logo"].ToString();
                    customerMapping.grade_logo = Convert.ToInt32(reader["grade_logo"].ToString());
                    customerMapping.path_logo = reader["path_logo"].ToString();
                    customerMapping.status = Convert.ToInt32(reader["status"].ToString());
                }

                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return customerMapping;
        }

        public void InsertCustomerMapping(CustomerMappingModel customerMapping)
        {
            try
            {
                string query = "INSERT INTO Customer_Mapping VALUES (@nama_logo, @grade_logo, @path_logo, @status)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@nama_logo", customerMapping.nama_logo);
                command.Parameters.AddWithValue("@grade_logo", customerMapping.grade_logo);
                command.Parameters.AddWithValue("@path_logo", customerMapping.path_logo); 
                command.Parameters.AddWithValue("@status", customerMapping.status);

                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteCustomerMapping(int id)
        {
            try
            {
                string query = "UPDATE Customer_Mapping SET status = 0 WHERE id_customer_mapping = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public bool CheckLogo(CustomerMappingModel cm)
        {
            try
            {
                string query = "SELECT * FROM Customer_Mapping WHERE nama_logo = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", cm.nama_logo);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return true; // Username atau Password sudah digunakan
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return false; // Username dan Password belum digunakan
        }

    }
}
