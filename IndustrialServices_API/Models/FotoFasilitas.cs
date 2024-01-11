using System.Data.Common;
using System.Data.SqlClient;

namespace IndustrialServices_API.Models
{
    public class FotoFasilitas
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public FotoFasilitas(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }
        public void insertphotofasilitas(List<FotoFasilitasModel> photofasilitas)
        {
            foreach (var foto in photofasilitas)
            {
                try
                {
                    string query = "INSERT INTO Foto_Fasilitas VALUES (@p1, @p2)";
                    SqlCommand command = new SqlCommand(query, _connection);
                    command.Parameters.AddWithValue("@p1", foto.path_foto_fasilitas);
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

        public List<FotoFasilitasModel> getPhotoFasilitas(int id)
        {
            List<FotoFasilitasModel> fotoFasilitas = new List<FotoFasilitasModel>();

            string query = "SELECT Foto_Fasilitas_Detail.id_photo_fasilitas, Foto_Fasilitas.path_photo_fasilitas " +
                           "FROM Foto_Fasilitas_Detail " +
                           "JOIN Foto_Fasilitas ON Foto_Fasilitas_Detail.id_photo_fasilitas = Foto_Fasilitas.id_photo_fasilitas " +
                           "WHERE Foto_Fasilitas_Detail.id_fasilitas = @p1";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@p1", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    FotoFasilitasModel foto = new FotoFasilitasModel()
                    {
                        id_foto_fasilitas = Convert.ToInt32(reader["id_photo_fasilitas"]),
                        path_foto_fasilitas = reader["path_photo_fasilitas"].ToString()
                    };
                    fotoFasilitas.Add(foto);
                }
            }

            return fotoFasilitas;
        }
    }
}
