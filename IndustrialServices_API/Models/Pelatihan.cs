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
                        jenis_pelatihan = reader["jenis_pelatihan"].ToString(),
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
                command.Parameters.AddWithValue("@jenis", pelatihan.jenis_pelatihan);
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
                string query = "SELECT DPP.id_pelatihan, DPP.id_pengajar, TP.nama_pengajar " +
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
                        nama_pengajar = reader["nama_pengajar"].ToString()
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
                string query = "UPDATE Pelatihan SET nama_pelatihan = @nama, tanggal_pelatihan_awal = @tanggal_awal, tanggal_pelatihan_akhir = @tanggal_akhir, jenis_pelatihan = @jenis, deskripsi_pelatihan = @deskripsi, status = @status WHERE id_pelatihan = @id";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@nama", pelatihan.nama_pelatihan);
                command.Parameters.AddWithValue("@tanggal_awal", pelatihan.tanggal_pelatihan_awal);
                command.Parameters.AddWithValue("@tanggal_akhir", pelatihan.tanggal_pelatihan_akhir);
                command.Parameters.AddWithValue("@jenis", pelatihan.jenis_pelatihan);
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
                    pelatihan.jenis_pelatihan = reader["jenis_pelatihan"].ToString();
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
    }
}
