using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace IndustrialServices_API.Models
{
    public class Pelatihan
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public Pelatihan(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<PelatihanModel> GetAllPelatihan()
        {
            List<PelatihanModel> pelatihanList = new List<PelatihanModel>();
            try
            {
                string query = "SELECT * FROM Pelatihan WHERE status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PelatihanModel pelatihan = new PelatihanModel
                    {
                        id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]),
                        nama_pelatihan = reader["nama_pelatihan"].ToString(),
                        tanggal_pelatihan_awal = Convert.ToDateTime(reader["tanggal_pelatihan_awal"]),
                        tanggal_pelatihan_akhir = Convert.ToDateTime(reader["tanggal_pelatihan_akhir"]),
                        id_tipe_pelatihan = Convert.ToInt32(reader["id_tipe_pelatihan"]),
                        deskripsi_pelatihan = reader["deskripsi_pelatihan"].ToString(),
                        status = Convert.ToInt32(reader["status"])
                    };
                    pelatihanList.Add(pelatihan);
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
            return pelatihanList;
        }

        public List<PelatihanModel> GetAllPelatihaninHome()
        {
            List<PelatihanModel> pelatihanList = new List<PelatihanModel>();
            try
            {
                string query = "SELECT TOP 3 " +
                               "    Pelatihan.id_pelatihan, " +
                               "    Pelatihan.nama_pelatihan, " +
                               "    Pelatihan.tanggal_pelatihan_awal, " +
                               "    Pelatihan.tanggal_pelatihan_akhir, " +
                               "    Pelatihan.id_tipe_pelatihan, " +
                               "    MIN(Foto_Pelatihan.path_foto_pelatihan) AS path_foto_pelatihan " +
                               "FROM " +
                               "    Pelatihan " +
                               "INNER JOIN " +
                               "    Detail_Pengajar_Pelatihan ON Pelatihan.id_pelatihan = Detail_Pengajar_Pelatihan.id_pelatihan " +
                               "INNER JOIN " +
                               "    Tenaga_Pengajar ON Detail_Pengajar_Pelatihan.id_pengajar = Tenaga_Pengajar.id_pengajar " +
                               "LEFT JOIN " +
                               "    Foto_Pelatihan_Detail ON Pelatihan.id_pelatihan = Foto_Pelatihan_Detail.id_pelatihan " +
                               "LEFT JOIN " +
                               "    Foto_Pelatihan ON Foto_Pelatihan_Detail.id_foto_pelatihan = Foto_Pelatihan.id_foto_pelatihan " +
                               "WHERE " +
                               "    Pelatihan.status != 0 " +
                               "    AND Pelatihan.tanggal_pelatihan_awal >= GETDATE() " +
                               "GROUP BY " +
                               "    Pelatihan.id_pelatihan, " +
                               "    Pelatihan.nama_pelatihan, " +
                               "    Pelatihan.tanggal_pelatihan_awal, " +
                               "    Pelatihan.tanggal_pelatihan_akhir, " +
                               "    Pelatihan.id_tipe_pelatihan " +
                               "ORDER BY " +
                               "    ABS(DATEDIFF(day, GETDATE(), Pelatihan.tanggal_pelatihan_awal));";


                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PelatihanModel pelatihan = new PelatihanModel
                    {
                        id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]),
                        nama_pelatihan = reader["nama_pelatihan"].ToString(),
                        tanggal_pelatihan_awal = Convert.ToDateTime(reader["tanggal_pelatihan_awal"]),
                        tanggal_pelatihan_akhir = Convert.ToDateTime(reader["tanggal_pelatihan_akhir"]),
                        id_tipe_pelatihan = Convert.ToInt32(reader["id_tipe_pelatihan"]),
                        // tambahkan path_foto_pelatihan ke dalam model
                        path_foto_pelatihan = reader["path_foto_pelatihan"].ToString()
                    };
                    pelatihanList.Add(pelatihan);
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

            return pelatihanList;
        }

        public List<PelatihanModel> GetFasilitasPelatihan(int id)
        {
            List<PelatihanModel> pmodel = new List<PelatihanModel>();

            try
            {
                string query = "SELECT * FROM Detail_Fasilitas_Pelatihan WHERE id_pelatihan = @id";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@id", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PelatihanModel pelatihan = new PelatihanModel()
                    {
                        id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]),
                        id_fasilitas = Convert.ToInt32(reader["id_fasilitas"])
                    };
                    pmodel.Add(pelatihan);
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
            return pmodel;
        }

        public void InsertPelatihan(PelatihanModel pelatihan)
        {
            try
            {
                string query = "INSERT INTO Pelatihan VALUES (@nama, @tanggal_awal, @tanggal_akhir, @jenis, @deskripsi, @status)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@nama", pelatihan.nama_pelatihan);
                command.Parameters.AddWithValue("@tanggal_awal", pelatihan.tanggal_pelatihan_awal);
                command.Parameters.AddWithValue("@tanggal_akhir", pelatihan.tanggal_pelatihan_akhir);
                command.Parameters.AddWithValue("@jenis", pelatihan.id_tipe_pelatihan);
                command.Parameters.AddWithValue("@deskripsi", pelatihan.deskripsi_pelatihan);
                command.Parameters.AddWithValue("@status", pelatihan.status);
                _connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void InsertPengajarPelatihan(List<PelatihanModel> pelatihan)
        {
            PelatihanModel pmodel = new PelatihanModel();
            foreach (var p in pelatihan)
            {
                try
                {
                    string query = "SELECT TOP 1 id_pelatihan FROM Pelatihan ORDER BY id_pelatihan DESC";
                    SqlCommand command = new SqlCommand(query, _connection);
                    _connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {

                         pmodel.id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]);

                    }
                    reader.Close(); // Tutup reader setelah selesai membaca
                    _connection.Close();

                        string query1 = "INSERT INTO Detail_Pengajar_Pelatihan VALUES (@id_pelatihan, @id_pengajar)";
                        SqlCommand command1 = new SqlCommand(query1, _connection);
                        command1.Parameters.AddWithValue("@id_pelatihan", pmodel.id_pelatihan);
                        command1.Parameters.AddWithValue("@id_pengajar", p.id_pengajar);
                        _connection.Open();
                        command1.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    _connection.Close(); 
                }
            }
        }

        public List<PelatihanModel> GetPengajarPelatihan(int id)
        {
            List<PelatihanModel> pmodel = new List<PelatihanModel>();

            try
            {
                string query = "SELECT DPP.id_pelatihan, DPP.id_pengajar, TP.nama_pengajar, TP.bidang_keahlian " +
                               "FROM Detail_Pengajar_Pelatihan DPP " +
                               "INNER JOIN Tenaga_Pengajar TP ON DPP.id_pengajar = TP.id_pengajar " +
                               "WHERE DPP.id_pelatihan = @id";

                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@id", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PelatihanModel pelatihan = new PelatihanModel()
                    {
                        id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]),
                        id_pengajar = Convert.ToInt32(reader["id_pengajar"]),
                        nama_pengajar = reader["nama_pengajar"].ToString(),
                        bidang_keahlian = reader["bidang_keahlian"].ToString()
                    };
                    pmodel.Add(pelatihan);
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
            return pmodel;
        }


        public void UpdatePelatihan(PelatihanModel pelatihan)
        {
            try
            {
                string query = "UPDATE Pelatihan SET nama_pelatihan = @nama, tanggal_pelatihan_awal = @tanggal_awal, tanggal_pelatihan_akhir = @tanggal_akhir, deskripsi_pelatihan = @deskripsi, status = @status WHERE id_pelatihan = @id";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@nama", pelatihan.nama_pelatihan);
                command.Parameters.AddWithValue("@tanggal_awal", pelatihan.tanggal_pelatihan_awal);
                command.Parameters.AddWithValue("@tanggal_akhir", pelatihan.tanggal_pelatihan_akhir);
                command.Parameters.AddWithValue("@deskripsi", pelatihan.deskripsi_pelatihan);
                command.Parameters.AddWithValue("@status", pelatihan.status);
                command.Parameters.AddWithValue("@id", pelatihan.id_pelatihan);
                _connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void DeletePelatihan(int id)
        {
            try
            {
                string query = "UPDATE Pelatihan SET status = 0 WHERE id_pelatihan = @id";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@id", id);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();

                string query1 = "DELETE FROM Detail_Pengajar_Pelatihan WHERE id_pelatihan = @p1";
                SqlCommand command1 = new SqlCommand(query1, _connection);
                command1.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                command1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public PelatihanModel GetPelatihanById(int id)
        {
            PelatihanModel pelatihan = new PelatihanModel();
            try
            {
                string query = "SELECT * FROM Pelatihan WHERE id_pelatihan = @id AND status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@id", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    pelatihan.id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]);
                    pelatihan.nama_pelatihan = reader["nama_pelatihan"].ToString();
                    pelatihan.tanggal_pelatihan_awal = Convert.ToDateTime(reader["tanggal_pelatihan_awal"]);
                    pelatihan.tanggal_pelatihan_akhir = Convert.ToDateTime(reader["tanggal_pelatihan_akhir"]);
                    pelatihan.id_tipe_pelatihan = Convert.ToInt32(reader["id_tipe_pelatihan"]);
                    pelatihan.deskripsi_pelatihan = reader["deskripsi_pelatihan"].ToString();
                    pelatihan.status = Convert.ToInt32(reader["status"]);
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
            return pelatihan;
        }
        public PelatihanModel GetPelatihanDesk(int id)
        {
            PelatihanModel pelatihan = new PelatihanModel();
            try
            {
                string query = "SELECT * FROM Pelatihan WHERE id_pelatihan = @id AND status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@id", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    pelatihan.id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]);
                    pelatihan.nama_pelatihan = reader["nama_pelatihan"].ToString();
                    pelatihan.deskripsi_pelatihan = reader["deskripsi_pelatihan"].ToString();

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
            return pelatihan;
        }
        public List<PelatihanModel> GetAllPelatihanTechnical(string filter_tipe_pelatihan = null, int? filter_bulan = null, string sortOption = null)
        {
            List<PelatihanModel> pelatihanList = new List<PelatihanModel>();
            try
            {
                string query = "SELECT Pelatihan.* " +
                               "FROM Pelatihan " +
                               "INNER JOIN Detail_Pengajar_Pelatihan ON Pelatihan.id_pelatihan = Detail_Pengajar_Pelatihan.id_pelatihan " +
                               "INNER JOIN Tenaga_Pengajar ON Detail_Pengajar_Pelatihan.id_pengajar = Tenaga_Pengajar.id_pengajar " +
                               "WHERE Pelatihan.status != 0 AND Tenaga_Pengajar.bidang_keahlian = 'Technical'";

                // Tambahkan filter_tipe_pelatihan ke query jika diberikan dan tidak sama dengan 0
                if (!string.IsNullOrEmpty(filter_tipe_pelatihan) && filter_tipe_pelatihan != "0")
                {
                    query += " AND Pelatihan.id_tipe_pelatihan = " + filter_tipe_pelatihan;
                }

                // Tambahkan filter_bulan ke query jika diberikan dan tidak sama dengan 0
                if (filter_bulan.HasValue && filter_bulan.Value != 0)
                {
                    query += " AND MONTH(Pelatihan.tanggal_pelatihan_awal) = " + filter_bulan;
                }

                // Tambahkan penyortiran berdasarkan opsi yang diberikan
                if (!string.IsNullOrEmpty(sortOption))
                {
                    query += " ORDER BY ";

                    switch (sortOption)
                    {
                        case "tanggal":
                            query += "Pelatihan.tanggal_pelatihan_awal ASC, Pelatihan.tanggal_pelatihan_akhir ASC";
                            break;
                        case "nama_pelatihan":
                            query += "Pelatihan.nama_pelatihan ASC";
                            break;
                            // Tambahkan opsi penyortiran lainnya jika diperlukan
                    }
                }

                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PelatihanModel pelatihan = new PelatihanModel
                    {
                        id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]),
                        nama_pelatihan = reader["nama_pelatihan"].ToString(),
                        tanggal_pelatihan_awal = Convert.ToDateTime(reader["tanggal_pelatihan_awal"]),
                        tanggal_pelatihan_akhir = Convert.ToDateTime(reader["tanggal_pelatihan_akhir"]),
                        id_tipe_pelatihan = Convert.ToInt32(reader["id_tipe_pelatihan"]),
                        deskripsi_pelatihan = reader["deskripsi_pelatihan"].ToString(),
                        status = Convert.ToInt32(reader["status"])
                    };
                    pelatihanList.Add(pelatihan);
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

            return pelatihanList;
        }

        public List<PelatihanModel> GetAllPelatihanTechnical2(string filter_tipe_pelatihan = null, int? filter_bulan = null, string sortOption = null)
        {
            List<PelatihanModel> pelatihanList = new List<PelatihanModel>();
            try
            {
                string query = "SELECT " +
                               "    Pelatihan.id_pelatihan, " +
                               "    Pelatihan.nama_pelatihan, " +
                               "    Pelatihan.tanggal_pelatihan_awal, " +
                               "    Pelatihan.tanggal_pelatihan_akhir, " +
                               "    Pelatihan.id_tipe_pelatihan, " +
                               "    MIN(Foto_Pelatihan.path_foto_pelatihan) AS path_foto_pelatihan " +
                               "FROM " +
                               "    Pelatihan " +
                               "INNER JOIN " +
                               "    Detail_Pengajar_Pelatihan ON Pelatihan.id_pelatihan = Detail_Pengajar_Pelatihan.id_pelatihan " +
                               "INNER JOIN " +
                               "    Tenaga_Pengajar ON Detail_Pengajar_Pelatihan.id_pengajar = Tenaga_Pengajar.id_pengajar " +
                               "LEFT JOIN " +
                               "    Foto_Pelatihan_Detail ON Pelatihan.id_pelatihan = Foto_Pelatihan_Detail.id_pelatihan " +
                               "LEFT JOIN " +
                               "    Foto_Pelatihan ON Foto_Pelatihan_Detail.id_foto_pelatihan = Foto_Pelatihan.id_foto_pelatihan " +
                               "WHERE " +
                               "    Pelatihan.status != 0 AND Tenaga_Pengajar.bidang_keahlian = 'Technical' " +
                               "GROUP BY " +
                               "    Pelatihan.id_pelatihan, " +
                               "    Pelatihan.nama_pelatihan, " +
                               "    Pelatihan.tanggal_pelatihan_awal, " +
                               "    Pelatihan.tanggal_pelatihan_akhir, " +
                               "    Pelatihan.id_tipe_pelatihan";

                // Tambahkan filter_tipe_pelatihan ke query jika diberikan dan tidak sama dengan 0
                if (!string.IsNullOrEmpty(filter_tipe_pelatihan) && filter_tipe_pelatihan != "0")
                {
                    query += " AND Pelatihan.id_tipe_pelatihan = " + filter_tipe_pelatihan;
                }

                // Tambahkan filter_bulan ke query jika diberikan dan tidak sama dengan 0
                if (filter_bulan.HasValue && filter_bulan.Value != 0)
                {
                    query += " AND MONTH(Pelatihan.tanggal_pelatihan_awal) = " + filter_bulan;
                }

                // Tambahkan penyortiran berdasarkan opsi yang diberikan
                if (!string.IsNullOrEmpty(sortOption))
                {
                    query += " ORDER BY ";

                    switch (sortOption)
                    {
                        case "tanggal":
                            query += "Pelatihan.tanggal_pelatihan_awal ASC, Pelatihan.tanggal_pelatihan_akhir ASC";
                            break;
                        case "nama_pelatihan":
                            query += "Pelatihan.nama_pelatihan ASC";
                            break;
                            // Tambahkan opsi penyortiran lainnya jika diperlukan
                    }
                }

                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PelatihanModel pelatihan = new PelatihanModel
                    {
                        id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]),
                        nama_pelatihan = reader["nama_pelatihan"].ToString(),
                        tanggal_pelatihan_awal = Convert.ToDateTime(reader["tanggal_pelatihan_awal"]),
                        tanggal_pelatihan_akhir = Convert.ToDateTime(reader["tanggal_pelatihan_akhir"]),
                        id_tipe_pelatihan = Convert.ToInt32(reader["id_tipe_pelatihan"]),
                        // tambahkan path_foto_pelatihan ke dalam model
                        path_foto_pelatihan = reader["path_foto_pelatihan"].ToString()
                    };
                    pelatihanList.Add(pelatihan);
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

            return pelatihanList;
        }

        public List<PelatihanModel> GetPelatihanTechnical(int id)
        {
            List<PelatihanModel> pelatihanList = new List<PelatihanModel>();
            try
            {
                string query = "SELECT * FROM Pelatihan WHERE id_pelatihan = @id";

                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@id", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PelatihanModel pelatihan = new PelatihanModel
                    {
                        id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]),
                        nama_pelatihan = reader["nama_pelatihan"].ToString(),
                        tanggal_pelatihan_awal = Convert.ToDateTime(reader["tanggal_pelatihan_awal"]),
                        tanggal_pelatihan_akhir = Convert.ToDateTime(reader["tanggal_pelatihan_akhir"]),
                        id_tipe_pelatihan = Convert.ToInt32(reader["id_tipe_pelatihan"]),
                        deskripsi_pelatihan = reader["deskripsi_pelatihan"].ToString(),
                        status = Convert.ToInt32(reader["status"])
                    };
                    pelatihanList.Add(pelatihan);
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

            return pelatihanList;
        }

        public List<PelatihanModel> GetAllPelatihanNonTechnical(string filter_tipe_pelatihan = null, int? filter_bulan = null, string sortOption = null)
        {
            List<PelatihanModel> pelatihanList = new List<PelatihanModel>();
            try
            {
                string query = "SELECT Pelatihan.* " +
                               "FROM Pelatihan " +
                               "INNER JOIN Detail_Pengajar_Pelatihan ON Pelatihan.id_pelatihan = Detail_Pengajar_Pelatihan.id_pelatihan " +
                               "INNER JOIN Tenaga_Pengajar ON Detail_Pengajar_Pelatihan.id_pengajar = Tenaga_Pengajar.id_pengajar " +
                               "WHERE Pelatihan.status != 0 AND Tenaga_Pengajar.bidang_keahlian = 'Non-Technical'";

                // Tambahkan filter_tipe_pelatihan ke query jika diberikan dan tidak sama dengan 0
                if (!string.IsNullOrEmpty(filter_tipe_pelatihan) && filter_tipe_pelatihan != "0")
                {
                    query += " AND Pelatihan.id_tipe_pelatihan = " + filter_tipe_pelatihan;
                }

                // Tambahkan filter_bulan ke query jika diberikan dan tidak sama dengan 0
                if (filter_bulan.HasValue && filter_bulan.Value != 0)
                {
                    query += " AND MONTH(Pelatihan.tanggal_pelatihan_awal) = " + filter_bulan;
                }

                // Tambahkan penyortiran berdasarkan opsi yang diberikan
                if (!string.IsNullOrEmpty(sortOption))
                {
                    query += " ORDER BY ";

                    switch (sortOption)
                    {
                        case "tanggal":
                            query += "Pelatihan.tanggal_pelatihan_awal ASC, Pelatihan.tanggal_pelatihan_akhir ASC";
                            break;
                        case "nama_pelatihan":
                            query += "Pelatihan.nama_pelatihan ASC";
                            break;
                            // Tambahkan opsi penyortiran lainnya jika diperlukan
                    }
                }

                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PelatihanModel pelatihan = new PelatihanModel
                    {
                        id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]),
                        nama_pelatihan = reader["nama_pelatihan"].ToString(),
                        tanggal_pelatihan_awal = Convert.ToDateTime(reader["tanggal_pelatihan_awal"]),
                        tanggal_pelatihan_akhir = Convert.ToDateTime(reader["tanggal_pelatihan_akhir"]),
                        id_tipe_pelatihan = Convert.ToInt32(reader["id_tipe_pelatihan"]),
                        deskripsi_pelatihan = reader["deskripsi_pelatihan"].ToString(),
                        status = Convert.ToInt32(reader["status"])
                    };
                    pelatihanList.Add(pelatihan);
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

            return pelatihanList;
        }

        public List<PelatihanModel> GetAllPelatihanNonTechnical2(string filter_tipe_pelatihan = null, int? filter_bulan = null, string sortOption = null)
        {
            List<PelatihanModel> pelatihanList = new List<PelatihanModel>();
            try
            {
                string query = "SELECT " +
                               "    Pelatihan.id_pelatihan, " +
                               "    Pelatihan.nama_pelatihan, " +
                               "    Pelatihan.tanggal_pelatihan_awal, " +
                               "    Pelatihan.tanggal_pelatihan_akhir, " +
                               "    Pelatihan.id_tipe_pelatihan, " +
                               "    MIN(Foto_Pelatihan.path_foto_pelatihan) AS path_foto_pelatihan " +
                               "FROM " +
                               "    Pelatihan " +
                               "INNER JOIN " +
                               "    Detail_Pengajar_Pelatihan ON Pelatihan.id_pelatihan = Detail_Pengajar_Pelatihan.id_pelatihan " +
                               "INNER JOIN " +
                               "    Tenaga_Pengajar ON Detail_Pengajar_Pelatihan.id_pengajar = Tenaga_Pengajar.id_pengajar " +
                               "LEFT JOIN " +
                               "    Foto_Pelatihan_Detail ON Pelatihan.id_pelatihan = Foto_Pelatihan_Detail.id_pelatihan " +
                               "LEFT JOIN " +
                               "    Foto_Pelatihan ON Foto_Pelatihan_Detail.id_foto_pelatihan = Foto_Pelatihan.id_foto_pelatihan " +
                               "WHERE " +
                               "    Pelatihan.status != 0 AND Tenaga_Pengajar.bidang_keahlian = 'Non-Technical' " +
                               "GROUP BY " +
                               "    Pelatihan.id_pelatihan, " +
                               "    Pelatihan.nama_pelatihan, " +
                               "    Pelatihan.tanggal_pelatihan_awal, " +
                               "    Pelatihan.tanggal_pelatihan_akhir, " +
                               "    Pelatihan.id_tipe_pelatihan";

                // Tambahkan filter_tipe_pelatihan ke query jika diberikan dan tidak sama dengan 0
                if (!string.IsNullOrEmpty(filter_tipe_pelatihan) && filter_tipe_pelatihan != "0")
                {
                    query += " AND Pelatihan.id_tipe_pelatihan = " + filter_tipe_pelatihan;
                }

                // Tambahkan filter_bulan ke query jika diberikan dan tidak sama dengan 0
                if (filter_bulan.HasValue && filter_bulan.Value != 0)
                {
                    query += " AND MONTH(Pelatihan.tanggal_pelatihan_awal) = " + filter_bulan;
                }

                // Tambahkan penyortiran berdasarkan opsi yang diberikan
                if (!string.IsNullOrEmpty(sortOption))
                {
                    query += " ORDER BY ";

                    switch (sortOption)
                    {
                        case "tanggal":
                            query += "Pelatihan.tanggal_pelatihan_awal ASC, Pelatihan.tanggal_pelatihan_akhir ASC";
                            break;
                        case "nama_pelatihan":
                            query += "Pelatihan.nama_pelatihan ASC";
                            break;
                            // Tambahkan opsi penyortiran lainnya jika diperlukan
                    }
                }
                else
                {
                    // Jika tidak ada opsi penyortiran, gunakan urutan default
                    query += " ORDER BY Pelatihan.id_pelatihan DESC";
                }

                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PelatihanModel pelatihan = new PelatihanModel
                    {
                        id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]),
                        nama_pelatihan = reader["nama_pelatihan"].ToString(),
                        tanggal_pelatihan_awal = Convert.ToDateTime(reader["tanggal_pelatihan_awal"]),
                        tanggal_pelatihan_akhir = Convert.ToDateTime(reader["tanggal_pelatihan_akhir"]),
                        id_tipe_pelatihan = Convert.ToInt32(reader["id_tipe_pelatihan"]),
                        // tambahkan path_foto_pelatihan ke dalam model
                        path_foto_pelatihan = reader["path_foto_pelatihan"].ToString()
                    };
                    pelatihanList.Add(pelatihan);
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

            return pelatihanList;
        }

        public List<PelatihanModel> GetPelatihanNonTechnical(int id)
        {
            List<PelatihanModel> pelatihanList = new List<PelatihanModel>();
            try
            {
                string query = "SELECT * FROM Pelatihan WHERE id_pelatihan = @id";

                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@id", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PelatihanModel pelatihan = new PelatihanModel
                    {
                        id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]),
                        nama_pelatihan = reader["nama_pelatihan"].ToString(),
                        tanggal_pelatihan_awal = Convert.ToDateTime(reader["tanggal_pelatihan_awal"]),
                        tanggal_pelatihan_akhir = Convert.ToDateTime(reader["tanggal_pelatihan_akhir"]),
                        id_tipe_pelatihan = Convert.ToInt32(reader["id_tipe_pelatihan"]),
                        deskripsi_pelatihan = reader["deskripsi_pelatihan"].ToString(),
                        status = Convert.ToInt32(reader["status"])
                    };
                    pelatihanList.Add(pelatihan);
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

            return pelatihanList;
        }

        public List<PelatihanModel> GetPelatihanforBK(int id)
        {
            List<PelatihanModel> pelatihanList = new List<PelatihanModel>();
            try
            {
                string query = "SELECT Pelatihan.*, Tipe_Pelatihan.bidang_keahlian " +
                               "FROM Pelatihan " +
                               "INNER JOIN Tipe_Pelatihan ON Pelatihan.id_tipe_pelatihan = Tipe_Pelatihan.id_tipe_pelatihan " +
                               "WHERE Pelatihan.id_pelatihan = @id";

                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@id", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PelatihanModel pelatihan = new PelatihanModel
                    {
                        id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]),
                        nama_pelatihan = reader["nama_pelatihan"].ToString(),
                        tanggal_pelatihan_awal = Convert.ToDateTime(reader["tanggal_pelatihan_awal"]),
                        tanggal_pelatihan_akhir = Convert.ToDateTime(reader["tanggal_pelatihan_akhir"]),
                        id_tipe_pelatihan = Convert.ToInt32(reader["id_tipe_pelatihan"]),
                        deskripsi_pelatihan = reader["deskripsi_pelatihan"].ToString(),
                        status = Convert.ToInt32(reader["status"]),
                        bidang_keahlian = reader["bidang_keahlian"].ToString() // Menambahkan bidang_keahlian dari Tipe_Pelatihan
                    };
                    pelatihanList.Add(pelatihan);
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

            return pelatihanList;
        }
    }
}
