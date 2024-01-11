using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace IndustrialServices_API.Models
{
    public class FotoProduk
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public FotoProduk(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }
        public void insertphotoproduk(List<FotoProdukModel> photoproduk)
        {
            foreach (var foto in photoproduk)
            {
                try
                {
                    string query = "INSERT INTO Foto_Produk VALUES (@p1, @p2)";
                    SqlCommand command = new SqlCommand(query, _connection);
                    command.Parameters.AddWithValue("@p1", foto.path_foto_produk);
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

        public List<FotoProdukModel> getPhotoProduk(int id)
        {
            List<FotoProdukModel> fotoProduk = new List<FotoProdukModel>();

            string query = "SELECT Foto_Produk_Detail.id_foto_produk, Foto_Produk.path_foto_produk " +
                           "FROM Foto_Produk_Detail " +
                           "JOIN Foto_Produk ON Foto_Produk_Detail.id_foto_produk = Foto_Produk.id_foto_produk " +
                           "WHERE Foto_Produk_Detail.id_produk = @p1";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@p1", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    FotoProdukModel foto = new FotoProdukModel()
                    {
                        id_foto_produk = Convert.ToInt32(reader["id_foto_produk"]),
                        path_foto_produk = reader["path_foto_produk"].ToString()
                    };
                    fotoProduk.Add(foto);
                }
            }

            return fotoProduk;
        }
    }
}
