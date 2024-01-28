using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace IndustrialServices_API.Models
{
    public class All
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public All(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }
        public List<AllModel> BigSearch(string search)
        {
            List<AllModel> allList = new List<AllModel>();
            try
            {
                _connection.Open();

                // Query untuk Artikel
                string artikelQuery = "SELECT * FROM Artikel WHERE judul_artikel LIKE @p1 AND status = 2";
                using (SqlCommand artikelCommand = new SqlCommand(artikelQuery, _connection))
                {
                    artikelCommand.Parameters.AddWithValue("@p1", "%" + search + "%");

                    using (SqlDataReader artikelReader = artikelCommand.ExecuteReader())
                    {
                        while (artikelReader.Read())
                        {
                            AllModel all = new AllModel();
                            all.id_artikel = Convert.ToInt32(artikelReader["id_artikel"]);
                            all.judul_artikel = artikelReader["judul_artikel"].ToString();
                            all.sampul_artikel = artikelReader["sampul_artikel"].ToString();
                            allList.Add(all);
                        }
                    }
                }
                _connection.Close();


                    // Query untuk Tenaga_Pengajar
                    string pengajarQuery = "SELECT * FROM Tenaga_Pengajar WHERE nama_pengajar LIKE @p2 AND status != 0";
                _connection.Open();
                using (SqlCommand pengajarCommand = new SqlCommand(pengajarQuery, _connection))
                    {
                        pengajarCommand.Parameters.AddWithValue("@p2", "%" + search + "%");

                        using (SqlDataReader pengajarReader = pengajarCommand.ExecuteReader())
                        {
                            while (pengajarReader.Read())
                            {
                                AllModel all = new AllModel();  
                                all.id_pengajar = Convert.ToInt32(pengajarReader["id_pengajar"]);
                                all.nama_pengajar = pengajarReader["nama_pengajar"].ToString();
                                all.bidang_keahlian = pengajarReader["bidang_keahlian"].ToString();
                            allList.Add(all);
                            }
                        }
                    }
                _connection.Close();

                // Query untuk Produk
                string produkQuery = "SELECT * FROM Produk WHERE nama_produk LIKE @p3 AND status != 0";
                _connection.Open();
                using (SqlCommand produkCommand = new SqlCommand(produkQuery, _connection))
                    {
                        produkCommand.Parameters.AddWithValue("@p3", "%" + search + "%");

                        using (SqlDataReader produkReader = produkCommand.ExecuteReader())
                        {
                            while (produkReader.Read())
                            {
                                AllModel all = new AllModel();
                                all.id_produk = Convert.ToInt32(produkReader["id_produk"]);
                                all.nama_produk = produkReader["nama_produk"].ToString();
                                all.id_tipe_produk = Convert.ToInt32(produkReader["id_tipe_produk"]);
                                // Populate other properties for Produk
                                allList.Add(all);
                            }
                        }
                    }
                _connection.Close();
                // Query untuk Pelatihan
                string pelatihanQuery = "SELECT * FROM (" +
                                        "  SELECT Pelatihan.*, Foto_Pelatihan_Detail.id_foto_pelatihan, Foto_Pelatihan.path_foto_pelatihan, " +
                                        "         ROW_NUMBER() OVER (PARTITION BY Pelatihan.id_pelatihan ORDER BY Foto_Pelatihan_Detail.id_foto_pelatihan) AS RowNum " +
                                        "  FROM Pelatihan " +
                                        "  JOIN Foto_Pelatihan_Detail ON Pelatihan.id_pelatihan = Foto_Pelatihan_Detail.id_pelatihan " +
                                        "  JOIN Foto_Pelatihan ON Foto_Pelatihan_Detail.id_foto_pelatihan = Foto_Pelatihan.id_foto_pelatihan " +
                                        "  WHERE Pelatihan.nama_pelatihan LIKE @p4 AND Pelatihan.status != 0" +
                                        ") AS Result " +
                                        "WHERE RowNum = 1";
                _connection.Open();
                using (SqlCommand pelatihanCommand = new SqlCommand(pelatihanQuery, _connection))
                {
                    pelatihanCommand.Parameters.AddWithValue("@p4", "%" + search + "%");

                    using (SqlDataReader pelatihanReader = pelatihanCommand.ExecuteReader())
                    {
                        while (pelatihanReader.Read())
                        {
                            AllModel all = new AllModel();
                            all.id_pelatihan = Convert.ToInt32(pelatihanReader["id_pelatihan"]);
                            all.nama_pelatihan = pelatihanReader["nama_pelatihan"].ToString();
                            all.id_tipe_pelatihan = Convert.ToInt32(pelatihanReader["id_tipe_pelatihan"]);
                            // Populate other properties for Pelatihan
                            all.id_foto_pelatihan = Convert.ToInt32(pelatihanReader["id_foto_pelatihan"]);
                            all.path_foto_pelatihan = pelatihanReader["path_foto_pelatihan"].ToString();
                            allList.Add(all);
                        }
                    }
                }
                _connection.Close();

                // Query untuk Fasilitas
                string fasilitasQuery = "SELECT * FROM Fasilitas WHERE nama_fasilitas LIKE @p5 AND status != 0";
                _connection.Open();
                using (SqlCommand fasilitasCommand = new SqlCommand(fasilitasQuery, _connection))
                    {
                        fasilitasCommand.Parameters.AddWithValue("@p5", "%" + search + "%");

                        using (SqlDataReader fasilitasReader = fasilitasCommand.ExecuteReader())
                        {
                            while (fasilitasReader.Read())
                            {
                                AllModel all = new AllModel();
                                all.id_fasilitas = Convert.ToInt32(fasilitasReader["id_fasilitas"]);
                                all.nama_fasilitas = fasilitasReader["nama_fasilitas"].ToString();
                                all.id_tipe_fasilitas = Convert.ToInt32(fasilitasReader["id_tipe_fasilitas"]);
                                // Populate other properties for Fasilitas
                                allList.Add(all);
                            }
                        }
                    }

                    _connection.Close();
                }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return allList;
        }


    }
}
