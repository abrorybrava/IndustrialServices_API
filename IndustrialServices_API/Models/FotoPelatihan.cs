using System.Data.SqlClient;

namespace IndustrialServices_API.Models
{
    public class FotoPelatihan
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public FotoPelatihan(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }
        public void insertphotopelatihan(List<FotoPelatihanModel> photopelatihan)
        {
            foreach (var foto in photopelatihan)
            {
                try
                {
                    string query = "INSERT INTO Foto_Pelatihan VALUES (@p1, @p2)";
                    SqlCommand command = new SqlCommand(query, _connection);
                    command.Parameters.AddWithValue("@p1", foto.path_foto_pelatihan);
                    command.Parameters.AddWithValue("@p2", foto.status);
                    _connection.Open();
                    command.ExecuteNonQuery();
                    _connection.Close();


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public List<FotoPelatihanModel> getPhotoPelatihan(int id)
        {
            List<FotoPelatihanModel> fotoPelatihan = new List<FotoPelatihanModel>();

            string query = "SELECT Foto_Pelatihan_Detail.id_foto_pelatihan, Foto_Pelatihan.path_foto_pelatihan " +
                           "FROM Foto_Pelatihan_Detail " +
                           "JOIN Foto_Pelatihan ON Foto_Pelatihan_Detail.id_foto_pelatihan = Foto_Pelatihan.id_foto_pelatihan " +
                           "WHERE Foto_Pelatihan_Detail.id_pelatihan = @p1";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@p1", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    FotoPelatihanModel foto = new FotoPelatihanModel()
                    {
                        id_foto_pelatihan = Convert.ToInt32(reader["id_foto_pelatihan"]),
                        path_foto_pelatihan = reader["path_foto_pelatihan"].ToString()
                    };
                    fotoPelatihan.Add(foto);
                }
            }

            return fotoPelatihan;
        }

    }
}
