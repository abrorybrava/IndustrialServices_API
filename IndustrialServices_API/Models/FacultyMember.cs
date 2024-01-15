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
                string query = "INSERT INTO Tenaga_Pengajar VALUES (@p1, @p2, @p3, @p4, @p5)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", facultyMember.npk);
                command.Parameters.AddWithValue("@p2", facultyMember.nama_pengajar);
                command.Parameters.AddWithValue("@p3", facultyMember.bidang_keahlian);
                command.Parameters.AddWithValue("@p4", facultyMember.foto_pengajar);
                command.Parameters.AddWithValue("@p5", facultyMember.status);
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
                    string query1 = "UPDATE Tenaga_Pengajar SET npk = @p2, nama_pengajar = @p3, bidang_keahlian = @p4, status = @p5 WHERE id_pengajar = @p1";
                    SqlCommand command1 = new SqlCommand(query1, _connection);
                    command1.Parameters.AddWithValue("@p1", facultyMember.id_pengajar);
                    command1.Parameters.AddWithValue("@p2", facultyMember.npk);
                    command1.Parameters.AddWithValue("@p3", facultyMember.nama_pengajar);
                    command1.Parameters.AddWithValue("@p4", facultyMember.bidang_keahlian);
                    command1.Parameters.AddWithValue("@p5", facultyMember.status);
                    _connection.Open();
                    command1.ExecuteNonQuery();
                    _connection.Close();
                }
                else
                {
                    string query = "UPDATE Tenaga_Pengajar SET npk = @p2, nama_pengajar = @p3, bidang_keahlian = @p4, foto_pengajar = @p5, status = @p6 WHERE id_pengajar = @p1";
                    SqlCommand command = new SqlCommand(query, _connection);
                    command.Parameters.AddWithValue("@p1", facultyMember.id_pengajar);
                    command.Parameters.AddWithValue("@p2", facultyMember.npk);
                    command.Parameters.AddWithValue("@p3", facultyMember.nama_pengajar);
                    command.Parameters.AddWithValue("@p4", facultyMember.bidang_keahlian);
                    command.Parameters.AddWithValue("@p5", facultyMember.foto_pengajar);
                    command.Parameters.AddWithValue("@p6", facultyMember.status);
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
        public bool CheckNPK(FacultyMemberModel facultyMember)
        {
            try
            {
                string query = "SELECT * FROM Tenaga_Pengajar WHERE npk = @p1";
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
                string query = "SELECT * FROM Tenaga_Pengajar WHERE id_pengajar != @p1 AND npk = @p2";
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
