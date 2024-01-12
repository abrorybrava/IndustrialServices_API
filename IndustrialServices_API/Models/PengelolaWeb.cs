using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace IndustrialServices_API.Models
{
    public class PengelolaWeb
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public PengelolaWeb(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<PengelolaWebModel> GetAllPengelolaWeb()
        {
            List<PengelolaWebModel> pengelolaWebList = new List<PengelolaWebModel>();
            try
            {
                string query = "SELECT * FROM Pengelola_Web WHERE status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PengelolaWebModel pengelolaWeb = new PengelolaWebModel
                    {
                        id_pengelola = Convert.ToInt32(reader["id_pengelola"]),
                        nama_pengelola = reader["nama_pengelola"].ToString(),
                        username = reader["username"].ToString(),
                        password = reader["password"].ToString(),
                        peran = reader["peran"].ToString(),
                        status = Convert.ToInt32(reader["status"])
                    };
                    pengelolaWebList.Add(pengelolaWeb);
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
            return pengelolaWebList;
        }

        public PengelolaWebModel GetPengelolaWebById(int id)
        {
            PengelolaWebModel pengelolaWeb = new PengelolaWebModel();
            try
            {
                string query = "SELECT * FROM Pengelola_Web WHERE id_pengelola = @id AND status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@id", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    pengelolaWeb.id_pengelola = Convert.ToInt32(reader["id_pengelola"]);
                    pengelolaWeb.nama_pengelola = reader["nama_pengelola"].ToString();
                    pengelolaWeb.username = reader["username"].ToString();
                    pengelolaWeb.password = reader["password"].ToString();
                    pengelolaWeb.peran = reader["peran"].ToString();
                    pengelolaWeb.status = Convert.ToInt32(reader["status"]);
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
            return pengelolaWeb;
        }

        public void InsertPengelolaWeb(PengelolaWebModel pengelolaWeb)
        {
            try
            {
                string query = "INSERT INTO Pengelola_Web VALUES (@nama, @username, @password, @peran, @status)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@nama", pengelolaWeb.nama_pengelola);
                command.Parameters.AddWithValue("@username", pengelolaWeb.username);
                command.Parameters.AddWithValue("@password", pengelolaWeb.password);
                command.Parameters.AddWithValue("@peran", pengelolaWeb.peran);
                command.Parameters.AddWithValue("@status", pengelolaWeb.status);
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

        public void UpdatePengelolaWeb(PengelolaWebModel pengelolaWeb)
        {
            try
            {
                string query = "UPDATE Pengelola_Web SET nama_pengelola = @nama, username = @username, password = @password, peran = @peran, status = @status WHERE id_pengelola = @id";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@nama", pengelolaWeb.nama_pengelola);
                command.Parameters.AddWithValue("@username", pengelolaWeb.username);
                command.Parameters.AddWithValue("@password", pengelolaWeb.password);
                command.Parameters.AddWithValue("@peran", pengelolaWeb.peran);
                command.Parameters.AddWithValue("@status", pengelolaWeb.status);
                command.Parameters.AddWithValue("@id", pengelolaWeb.id_pengelola);
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

        public void DeletePengelolaWeb(int id)
        {
            try
            {
                string query = "UPDATE Pengelola_Web SET status = 0 WHERE id_pengelola = @id";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@id", id);
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

        public bool CheckUsernamePassword(PengelolaWebModel pengelola)
        {
            try
            {
                string query = "SELECT * FROM Pengelola_Web WHERE username = @p1 OR password = @p2";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", pengelola.username);
                command.Parameters.AddWithValue("@p2", pengelola.password);
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

        public bool CheckUsernamePasswordEdit(PengelolaWebModel pengelola)
        {
            try
            {
                string query = "SELECT * FROM Pengelola_Web WHERE id_pengelola != @p1 AND username = @p2 OR password = @p3";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p2", pengelola.id_pengelola);
                command.Parameters.AddWithValue("@p2", pengelola.username);
                command.Parameters.AddWithValue("@p3", pengelola.password);
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
