using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace IndustrialServices_API.Models
{
    public class Produk
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public Produk(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<ProdukModel> GetAllProduk()
        {
            List<ProdukModel> produkList = new List<ProdukModel>();
            try
            {
                string query = "SELECT * FROM Produk WHERE status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProdukModel ProdukModel = new ProdukModel
                    {
                        id_produk = Convert.ToInt32(reader["id_produk"]),
                        nama_produk = reader["nama_produk"].ToString(),
                        id_tipe_produk = Convert.ToInt32(reader["id_tipe_produk"]),
                        pesanan = reader["pesanan"].ToString(),
                        deskripsi_produk = reader["deskripsi_produk"].ToString(),
                        status = Convert.ToInt32(reader["status"])
                    };
                    produkList.Add(ProdukModel);
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
            return produkList;
        }

        public void InsertProduk(ProdukModel ProdukModel)
        {
            try
            {
                string query = "INSERT INTO Produk VALUES (@nama, @tipe, @pesanan, @deskripsi, @status)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@nama", ProdukModel.nama_produk);
                command.Parameters.AddWithValue("@tipe", ProdukModel.id_tipe_produk);
                command.Parameters.AddWithValue("@pesanan", ProdukModel.pesanan);
                command.Parameters.AddWithValue("@deskripsi", ProdukModel.deskripsi_produk);
                command.Parameters.AddWithValue("@status", ProdukModel.status);
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

        public void UpdateProduk(ProdukModel ProdukModel)
        {
            try
            {
                string query = "UPDATE Produk SET nama_produk = @nama, id_tipe_produk = @tipe, pesanan = @pesanan, deskripsi_produk = @deskripsi, status = @status WHERE id_produk = @id";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@nama", ProdukModel.nama_produk);
                command.Parameters.AddWithValue("@tipe", ProdukModel.id_tipe_produk);
                command.Parameters.AddWithValue("@pesanan", ProdukModel.pesanan);
                command.Parameters.AddWithValue("@deskripsi", ProdukModel.deskripsi_produk);
                command.Parameters.AddWithValue("@status", ProdukModel.status);
                command.Parameters.AddWithValue("@id", ProdukModel.id_produk);
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

        public void DeleteProduk(int id)
        {
            try
            {
                string query = "UPDATE Produk SET status = 0 WHERE id_produk = @id";
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

        public ProdukModel GetProdukById(int id)
        {
            ProdukModel ProdukModel = new ProdukModel();
            try
            {
                string query = "SELECT * FROM Produk WHERE id_produk = @id AND status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@id", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ProdukModel.id_produk = Convert.ToInt32(reader["id_produk"]);
                    ProdukModel.nama_produk = reader["nama_produk"].ToString();
                    ProdukModel.id_tipe_produk = Convert.ToInt32(reader["id_tipe_produk"]);
                    ProdukModel.pesanan = reader["pesanan"].ToString();
                    ProdukModel.deskripsi_produk = reader["deskripsi_produk"].ToString();
                    ProdukModel.status = Convert.ToInt32(reader["status"]);
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
            return ProdukModel;
        }
    }
}
