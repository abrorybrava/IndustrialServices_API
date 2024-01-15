using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace IndustrialServices_API.Models
{
    public class TipePelatihan
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public TipePelatihan(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<TipePelatihanModel> GetAllTipePelatihan()
        {
            List<TipePelatihanModel> tipePelatihanList = new List<TipePelatihanModel>();
            try
            {
                string query = "SELECT * FROM Tipe_Pelatihan WHERE status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TipePelatihanModel tipePelatihanModel = new TipePelatihanModel
                    {
                        id_tipe_pelatihan = Convert.ToInt32(reader["id_tipe_pelatihan"]),
                        tipe_pelatihan = reader["tipe_pelatihan"].ToString(),
                        status = Convert.ToInt32(reader["status"])
                    };
                    tipePelatihanList.Add(tipePelatihanModel);
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
            return tipePelatihanList;
        }

        public void InsertTipePelatihan(TipePelatihanModel tipePelatihanModel)
        {
            try
            {
                string query = "INSERT INTO Tipe_Pelatihan (tipe_pelatihan, status) VALUES (@tipe, @status)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@tipe", tipePelatihanModel.tipe_pelatihan);
                command.Parameters.AddWithValue("@status", tipePelatihanModel.status);
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

        public void UpdateTipePelatihan(TipePelatihanModel tipePelatihanModel)
        {
            try
            {
                string query = "UPDATE Tipe_Pelatihan SET tipe_pelatihan = @tipe WHERE id_tipe_pelatihan = @id";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@tipe", tipePelatihanModel.tipe_pelatihan);
                command.Parameters.AddWithValue("@id", tipePelatihanModel.id_tipe_pelatihan);
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

        public void DeleteTipePelatihan(int id)
        {
            try
            {
                string query = "UPDATE Tipe_Pelatihan SET status = 0 WHERE id_tipe_pelatihan = @id";
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

        public TipePelatihanModel GetTipePelatihanById(int id)
        {
            TipePelatihanModel tipePelatihanModel = new TipePelatihanModel();
            try
            {
                string query = "SELECT * FROM Tipe_Pelatihan WHERE id_tipe_pelatihan = @id AND status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@id", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    tipePelatihanModel.id_tipe_pelatihan = Convert.ToInt32(reader["id_tipe_pelatihan"]);
                    tipePelatihanModel.tipe_pelatihan = reader["tipe_pelatihan"].ToString();
                    tipePelatihanModel.status = Convert.ToInt32(reader["status"]);
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
            return tipePelatihanModel;
        }
        public bool CheckNama(TipePelatihanModel tipePelatihan)
        {
            try
            {
                string query = "SELECT * FROM Tipe_Pelatihan WHERE tipe_pelatihan = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", tipePelatihan.tipe_pelatihan);
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

        public bool CheckNamaEdit(TipePelatihanModel tipePelatihan)
        {
            try
            {
                string query = "SELECT * FROM Tipe_Pelatihan WHERE id_tipe_pelatihan != @p1 AND tipe_pelatihan = @p2";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", tipePelatihan.id_tipe_pelatihan);
                command.Parameters.AddWithValue("@p2", tipePelatihan.tipe_pelatihan);
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
