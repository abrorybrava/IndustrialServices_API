using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace IndustrialServices_API.Models
{
    public class TipeProduk
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public TipeProduk(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<TipeProdukModel> GetAllTipeProduk()
        {
            List<TipeProdukModel> tipeProdukList = new List<TipeProdukModel>();
            try
            {
                string query = "SELECT * FROM Tipe_Produk WHERE status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TipeProdukModel tipeProdukModel = new TipeProdukModel
                    {
                        id_tipe_produk = Convert.ToInt32(reader["id_tipe_produk"]),
                        tipe_produk = reader["tipe_produk"].ToString(),
                        status = Convert.ToInt32(reader["status"])
                    };
                    tipeProdukList.Add(tipeProdukModel);
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
            return tipeProdukList;
        }

        public void InsertTipeProduk(TipeProdukModel tipeProdukModel)
        {
            try
            {
                string query = "INSERT INTO Tipe_Produk (tipe_produk, status) VALUES (@tipe, @status)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@tipe", tipeProdukModel.tipe_produk);
                command.Parameters.AddWithValue("@status", tipeProdukModel.status);
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

        public void UpdateTipeProduk(TipeProdukModel tipeProdukModel)
        {
            try
            {
                string query = "UPDATE Tipe_Produk SET tipe_produk = @tipe WHERE id_tipe_produk = @id";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@tipe", tipeProdukModel.tipe_produk);
                command.Parameters.AddWithValue("@id", tipeProdukModel.id_tipe_produk);
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

        public void DeleteTipeProduk(int id)
        {
            try
            {
                string query = "UPDATE Tipe_Produk SET status = 0 WHERE id_tipe_produk = @id";
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

        public TipeProdukModel GetTipeProdukById(int id)
        {
            TipeProdukModel tipeProdukModel = new TipeProdukModel();
            try
            {
                string query = "SELECT * FROM Tipe_Produk WHERE id_tipe_produk = @id AND status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@id", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    tipeProdukModel.id_tipe_produk = Convert.ToInt32(reader["id_tipe_produk"]);
                    tipeProdukModel.tipe_produk = reader["tipe_produk"].ToString();
                    tipeProdukModel.status = Convert.ToInt32(reader["status"]);
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
            return tipeProdukModel;
        }
        public bool CheckNama(TipeProdukModel tipeProduk)
        {
            try
            {
                string query = "SELECT * FROM Tipe_Produk WHERE tipe_produk = @p1 AND status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", tipeProduk.tipe_produk);
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

        public bool CheckNamaEdit(TipeProdukModel tipeProduk)
        {
            try
            {
                string query = "SELECT * FROM Tipe_Produk WHERE id_tipe_produk != @p1 AND tipe_produk = @p2 AND status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", tipeProduk.id_tipe_produk);
                command.Parameters.AddWithValue("@p2", tipeProduk.tipe_produk);
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
