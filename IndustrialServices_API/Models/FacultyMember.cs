using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace IndustrialServices_API.Models
{
    public class FacultyMember
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public FacultyMember(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<FacultyMemberModel> GetAllFacultyMembers()
        {
            List<FacultyMemberModel> facultyMembers = new List<FacultyMemberModel>();
            try
            {
                string query = "SELECT * FROM Tenaga_Pengajar WHERE status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FacultyMemberModel facultyMember = new FacultyMemberModel
                    {
                        id_pengajar = Convert.ToInt32(reader["id_pengajar"].ToString()),
                        npk = reader["npk"].ToString(),
                        nama_pengajar = reader["nama_pengajar"].ToString(),
                        bidang_keahlian = reader["bidang_keahlian"].ToString(),
                        foto_pengajar = reader["foto_pengajar"].ToString(),
                        deskripsi_pengajar = reader["deskripsi_pengajar"].ToString(),
                        status = Convert.ToInt32(reader["status"].ToString())
                    };
                    facultyMembers.Add(facultyMember);
                }
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return facultyMembers;
        }

        public List<FacultyMemberModel> GetAllFacultyMembersinIndex()
        {
            List<FacultyMemberModel> facultyMembers = new List<FacultyMemberModel>();
            try
            {
                string query = "SELECT * FROM Tenaga_Pengajar";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FacultyMemberModel facultyMember = new FacultyMemberModel
                    {
                        id_pengajar = Convert.ToInt32(reader["id_pengajar"].ToString()),
                        npk = reader["npk"].ToString(),
                        nama_pengajar = reader["nama_pengajar"].ToString(),
                        bidang_keahlian = reader["bidang_keahlian"].ToString(),
                        foto_pengajar = reader["foto_pengajar"].ToString(),
                        deskripsi_pengajar = reader["deskripsi_pengajar"].ToString(),
                        status = Convert.ToInt32(reader["status"].ToString())
                    };
                    facultyMembers.Add(facultyMember);
                }
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return facultyMembers;
        }
        public List<FacultyMemberModel> GetAllFacultyMembersinWeb(string filter)
        {
            List<FacultyMemberModel> facultyMembers = new List<FacultyMemberModel>();
            try
            {
                string query = "SELECT * FROM Tenaga_Pengajar WHERE status != 0";

                // Tambahkan kondisi filter jika filter tidak kosong
                if (!string.IsNullOrEmpty(filter))
                {
                    switch (filter.ToLower())
                    {
                        case "nama_pengajar":
                            query += " ORDER BY nama_pengajar";
                            break;
                        case "technical":
                            query += " AND bidang_keahlian = 'Technical'";
                            break;
                        case "non-technical":
                            query += " AND bidang_keahlian = 'Non-Technical'";
                            break;
                        case "technical and non-technical":
                            query += " AND bidang_keahlian = 'Technical And Non-Technical'";
                            break;
                            // Tambahkan kondisi filter lainnya sesuai kebutuhan
                            // ...
                    }
                }

                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FacultyMemberModel facultyMember = new FacultyMemberModel
                    {
                        id_pengajar = Convert.ToInt32(reader["id_pengajar"].ToString()),
                        npk = reader["npk"].ToString(),
                        nama_pengajar = reader["nama_pengajar"].ToString(),
                        bidang_keahlian = reader["bidang_keahlian"].ToString(),
                        foto_pengajar = reader["foto_pengajar"].ToString(),
                        deskripsi_pengajar = reader["deskripsi_pengajar"].ToString(),
                        status = Convert.ToInt32(reader["status"].ToString())
                    };
                    facultyMembers.Add(facultyMember);
                }
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return facultyMembers;
        }

        public List<FacultyMemberModel> GetAllFacultyMembersbyBK(string bidang_keahlian)
        {
            List<FacultyMemberModel> facultyMembers = new List<FacultyMemberModel>();
            try
            {
                string query = "SELECT * FROM Tenaga_Pengajar WHERE bidang_keahlian IN (@p1, 'Technical and Non-Technical') AND status != 0 ";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", bidang_keahlian);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FacultyMemberModel facultyMember = new FacultyMemberModel
                    {
                        id_pengajar = Convert.ToInt32(reader["id_pengajar"].ToString()),
                        npk = reader["npk"].ToString(),
                        nama_pengajar = reader["nama_pengajar"].ToString(),
                        bidang_keahlian = reader["bidang_keahlian"].ToString(),
                        foto_pengajar = reader["foto_pengajar"].ToString(),
                        deskripsi_pengajar = reader["deskripsi_pengajar"].ToString(),
                        status = Convert.ToInt32(reader["status"].ToString())
                    };
                    facultyMembers.Add(facultyMember);
                }
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return facultyMembers;
        }

        public List<FacultyMemberModel> GetAllFacultyMembersTechnical()
        {
            List<FacultyMemberModel> facultyMembers = new List<FacultyMemberModel>();
            try
            {
                string query = "SELECT TP.*, COUNT(DPP.id_pelatihan) AS JumlahPelatihan FROM Tenaga_Pengajar TP " +
                               "LEFT JOIN Detail_Pengajar_Pelatihan DPP ON TP.id_pengajar = DPP.id_pengajar " +
                               "LEFT JOIN Pelatihan P ON DPP.id_pelatihan = P.id_pelatihan " +
                               "WHERE TP.bidang_keahlian = 'Technical' AND TP.status != 0 " +
                               "GROUP BY TP.id_pengajar, TP.npk, TP.nama_pengajar, TP.bidang_keahlian, TP.foto_pengajar, TP.status";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FacultyMemberModel facultyMember = new FacultyMemberModel
                    {
                        id_pengajar = Convert.ToInt32(reader["id_pengajar"].ToString()),
                        npk = reader["npk"].ToString(),
                        nama_pengajar = reader["nama_pengajar"].ToString(),
                        bidang_keahlian = reader["bidang_keahlian"].ToString(),
                        foto_pengajar = reader["foto_pengajar"].ToString(),
                        status = Convert.ToInt32(reader["status"].ToString()),
                        jumlah_pelatihan = Convert.ToInt32(reader["JumlahPelatihan"].ToString())
                    };
                    facultyMembers.Add(facultyMember);
                }
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return facultyMembers;
        }

        public List<FacultyMemberModel> GetAllFacultyMembersNonTechnical()
        {
            List<FacultyMemberModel> facultyMembers = new List<FacultyMemberModel>();
            try
            {
                string query = "SELECT TP.*, COUNT(DPP.id_pelatihan) AS JumlahPelatihan FROM Tenaga_Pengajar TP " +
                               "LEFT JOIN Detail_Pengajar_Pelatihan DPP ON TP.id_pengajar = DPP.id_pengajar " +
                               "LEFT JOIN Pelatihan P ON DPP.id_pelatihan = P.id_pelatihan " +
                               "WHERE TP.bidang_keahlian = 'Non-Technical' AND TP.status != 0 " +
                               "GROUP BY TP.id_pengajar, TP.npk, TP.nama_pengajar, TP.bidang_keahlian, TP.foto_pengajar, TP.status";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FacultyMemberModel facultyMember = new FacultyMemberModel
                    {
                        id_pengajar = Convert.ToInt32(reader["id_pengajar"].ToString()),
                        npk = reader["npk"].ToString(),
                        nama_pengajar = reader["nama_pengajar"].ToString(),
                        bidang_keahlian = reader["bidang_keahlian"].ToString(),
                        foto_pengajar = reader["foto_pengajar"].ToString(),
                        status = Convert.ToInt32(reader["status"].ToString()),
                        jumlah_pelatihan = Convert.ToInt32(reader["JumlahPelatihan"].ToString())
                    };
                    facultyMembers.Add(facultyMember);
                }
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return facultyMembers;
        }



        public FacultyMemberModel GetFacultyMemberById(int id)
        {
            FacultyMemberModel facultyMember = new FacultyMemberModel();
            try
            {
                string query = "SELECT * FROM Tenaga_Pengajar WHERE id_pengajar = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    facultyMember.id_pengajar = Convert.ToInt32(reader["id_pengajar"].ToString());
                    facultyMember.npk = reader["npk"].ToString();
                    facultyMember.nama_pengajar = reader["nama_pengajar"].ToString();
                    facultyMember.bidang_keahlian = reader["bidang_keahlian"].ToString();
                    facultyMember.foto_pengajar = reader["foto_pengajar"].ToString();
                    facultyMember.deskripsi_pengajar = reader["deskripsi_pengajar"].ToString();
                    facultyMember.status = Convert.ToInt32(reader["status"].ToString());
                }
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return facultyMember;
        }

        public FacultyMemberModel GetFacultyMemberDetails(int id)
        {
            FacultyMemberModel facultyMember = new FacultyMemberModel();
            try
            {
                string query = "SELECT * FROM Tenaga_Pengajar WHERE id_pengajar = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    facultyMember.id_pengajar = Convert.ToInt32(reader["id_pengajar"].ToString());
                    facultyMember.npk = reader["npk"].ToString();
                    facultyMember.nama_pengajar = reader["nama_pengajar"].ToString();
                    facultyMember.bidang_keahlian = reader["bidang_keahlian"].ToString();
                    facultyMember.foto_pengajar = reader["foto_pengajar"].ToString();
                    facultyMember.deskripsi_pengajar = reader["deskripsi_pengajar"].ToString();
                    facultyMember.status = Convert.ToInt32(reader["status"].ToString());
                }
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return facultyMember;
        }

        public void InsertFacultyMember(FacultyMemberModel facultyMember)
        {
            try
            {
                string query = "INSERT INTO Tenaga_Pengajar VALUES (@p1, @p2, @p3, @p4, @p5, @p6)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", facultyMember.npk);
                command.Parameters.AddWithValue("@p2", facultyMember.nama_pengajar);
                command.Parameters.AddWithValue("@p3", facultyMember.bidang_keahlian);
                command.Parameters.AddWithValue("@p4", facultyMember.foto_pengajar);
                command.Parameters.AddWithValue("@p5", facultyMember.deskripsi_pengajar);
                command.Parameters.AddWithValue("@p6", facultyMember.status);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateFacultyMember(FacultyMemberModel facultyMember)
        {
            try
            {
                if (facultyMember.foto_pengajar == null)
                {
                    string query1 = "UPDATE Tenaga_Pengajar SET npk = @p2, nama_pengajar = @p3, bidang_keahlian = @p4, deskripsi_pengajar = @p5, status = @p6 WHERE id_pengajar = @p1";
                    SqlCommand command1 = new SqlCommand(query1, _connection);
                    command1.Parameters.AddWithValue("@p1", facultyMember.id_pengajar);
                    command1.Parameters.AddWithValue("@p2", facultyMember.npk);
                    command1.Parameters.AddWithValue("@p3", facultyMember.nama_pengajar);
                    command1.Parameters.AddWithValue("@p4", facultyMember.bidang_keahlian);
                    command1.Parameters.AddWithValue("@p5", facultyMember.deskripsi_pengajar);
                    command1.Parameters.AddWithValue("@p6", facultyMember.status);
                    _connection.Open();
                    command1.ExecuteNonQuery();
                    _connection.Close();
                }
                else
                {
                    string query = "UPDATE Tenaga_Pengajar SET npk = @p2, nama_pengajar = @p3, bidang_keahlian = @p4, foto_pengajar = @p5, deskripsi_pengajar = @p6, status = @p7 WHERE id_pengajar = @p1";
                    SqlCommand command = new SqlCommand(query, _connection);
                    command.Parameters.AddWithValue("@p1", facultyMember.id_pengajar);
                    command.Parameters.AddWithValue("@p2", facultyMember.npk);
                    command.Parameters.AddWithValue("@p3", facultyMember.nama_pengajar);
                    command.Parameters.AddWithValue("@p4", facultyMember.bidang_keahlian);
                    command.Parameters.AddWithValue("@p5", facultyMember.foto_pengajar);
                    command.Parameters.AddWithValue("@p6", facultyMember.deskripsi_pengajar);
                    command.Parameters.AddWithValue("@p7", facultyMember.status);
                    _connection.Open();
                    command.ExecuteNonQuery();
                    _connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteFacultyMember(int id)
        {
            try
            {
                string query = "UPDATE Tenaga_Pengajar SET status = 0 WHERE id_pengajar = @p1";
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

        public void ActivateFacultyMember(int id)
        {
            try
            {
                string query = "UPDATE Tenaga_Pengajar SET status = 1 WHERE id_pengajar = @p1";
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
        public bool CheckNPK(FacultyMemberModel facultyMember)
        {
            try
            {
                string query = "SELECT * FROM Tenaga_Pengajar WHERE npk = @p1 AND status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", facultyMember.npk);
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

        public bool CheckNPKEdit(FacultyMemberModel facultyMember)
        {
            try
            {
                string query = "SELECT * FROM Tenaga_Pengajar WHERE id_pengajar != @p1 AND npk = @p2 AND status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", facultyMember.id_pengajar);
                command.Parameters.AddWithValue("@p2", facultyMember.npk);
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
