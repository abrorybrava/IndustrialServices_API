using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace IndustrialServices_API.Models
{
    public class Artikel
    {
        private readonly string _connectionString;

        public Artikel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<ArtikelModel> GetAllArtikels()
        {
            List<ArtikelModel> artikels = new List<ArtikelModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Artikel WHERE status != 0";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ArtikelModel artikel = new ArtikelModel
                            {
                                id_artikel = Convert.ToInt32(reader["id_artikel"]),
                                id_pengelola = Convert.ToInt32(reader["id_pengelola"]),
                                tanggal_rilis = Convert.ToDateTime(reader["tanggal_rilis"]),
                                judul_artikel = reader["judul_artikel"].ToString(),
                                isi_artikel = reader["isi_artikel"].ToString(),
                                sampul_artikel = reader["sampul_artikel"].ToString(),
                                status = Convert.ToInt32(reader["status"])
                            };
                            artikels.Add(artikel);
                        }
                    }
                }
            }
            return artikels;
        }

        public List<ArtikelModel> GetAllArtikelsDone()
        {
            List<ArtikelModel> artikels = new List<ArtikelModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Artikel WHERE status = 2 ORDER BY NEWID();";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ArtikelModel artikel = new ArtikelModel
                            {
                                id_artikel = Convert.ToInt32(reader["id_artikel"]),
                                id_pengelola = Convert.ToInt32(reader["id_pengelola"]),
                                tanggal_rilis = Convert.ToDateTime(reader["tanggal_rilis"]),
                                judul_artikel = reader["judul_artikel"].ToString(),
                                isi_artikel = reader["isi_artikel"].ToString(),
                                sampul_artikel = reader["sampul_artikel"].ToString(),
                                status = Convert.ToInt32(reader["status"])
                            };
                            artikels.Add(artikel);
                        }
                    }
                }
            }
            return artikels;
        }

        public List<ArtikelModel> GetAllArtikelsinHome()
        {
            List<ArtikelModel> artikels = new List<ArtikelModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT TOP 6 * FROM Artikel WHERE status = 2 ORDER BY NEWID();";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ArtikelModel artikel = new ArtikelModel
                            {
                                id_artikel = Convert.ToInt32(reader["id_artikel"]),
                                id_pengelola = Convert.ToInt32(reader["id_pengelola"]),
                                tanggal_rilis = Convert.ToDateTime(reader["tanggal_rilis"]),
                                judul_artikel = reader["judul_artikel"].ToString(),
                                isi_artikel = reader["isi_artikel"].ToString(),
                                sampul_artikel = reader["sampul_artikel"].ToString(),
                                status = Convert.ToInt32(reader["status"])
                            };
                            artikels.Add(artikel);
                        }
                    }
                }
            }
            return artikels;
        }

        public ArtikelModel GetArtikelById(int id)
        {
            ArtikelModel artikel = new ArtikelModel();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Artikel WHERE id_artikel = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            artikel.id_artikel = Convert.ToInt32(reader["id_artikel"]);
                            artikel.id_pengelola = Convert.ToInt32(reader["id_pengelola"]);
                            artikel.tanggal_rilis = Convert.ToDateTime(reader["tanggal_rilis"]);
                            artikel.judul_artikel = reader["judul_artikel"].ToString();
                            artikel.isi_artikel = reader["isi_artikel"].ToString();
                            artikel.sampul_artikel = reader["sampul_artikel"].ToString();
                            artikel.status = Convert.ToInt32(reader["status"]);
                        }
                    }
                }
            }
            return artikel;
        }

        public ArtikelModel GetDetailArtikel(int id)
        {
            ArtikelModel artikel = new ArtikelModel();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT Artikel.*, Pengelola_Web.peran " +
                               "FROM Artikel " +
                               "INNER JOIN Pengelola_Web ON Artikel.id_pengelola = Pengelola_Web.id_pengelola " +
                               "WHERE Artikel.id_artikel = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            artikel.id_artikel = Convert.ToInt32(reader["id_artikel"]);
                            artikel.id_pengelola = Convert.ToInt32(reader["id_pengelola"]);
                            artikel.tanggal_rilis = Convert.ToDateTime(reader["tanggal_rilis"]);
                            artikel.judul_artikel = reader["judul_artikel"].ToString();
                            artikel.isi_artikel = reader["isi_artikel"].ToString();
                            artikel.sampul_artikel = reader["sampul_artikel"].ToString();
                            artikel.status = Convert.ToInt32(reader["status"]);

                            // Ambil peran dari tabel Pengelola_Web
                            artikel.role = reader["peran"].ToString();
                        }
                    }
                }
            }
            return artikel;
        }

        public void InsertArtikel(ArtikelModel artikel)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Artikel VALUES (@id_pengelola, @tanggal_rilis, @judul_artikel, @isi_artikel, @sampul_artikel, @status)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_pengelola", artikel.id_pengelola);
                    command.Parameters.AddWithValue("@tanggal_rilis", artikel.tanggal_rilis);
                    command.Parameters.AddWithValue("@judul_artikel", artikel.judul_artikel);
                    command.Parameters.AddWithValue("@isi_artikel", artikel.isi_artikel); // Handle TEXT field as string
                    command.Parameters.AddWithValue("@sampul_artikel", artikel.sampul_artikel);
                    command.Parameters.AddWithValue("@status", artikel.status);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public void UpdateArtikel(ArtikelModel artikel)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                if (artikel.sampul_artikel == "")
                {
                    string query1 = "UPDATE Artikel SET id_pengelola = @id_pengelola, tanggal_rilis = @tanggal_rilis, " +
               "judul_artikel = @judul_artikel, isi_artikel = @isi_artikel, " +
               "status = @status WHERE id_artikel = @id";
                    using (SqlCommand command1 = new SqlCommand(query1, connection))
                    {
                        command1.Parameters.AddWithValue("@id_pengelola", artikel.id_pengelola);
                        command1.Parameters.AddWithValue("@tanggal_rilis", artikel.tanggal_rilis);
                        command1.Parameters.AddWithValue("@judul_artikel", artikel.judul_artikel);
                        command1.Parameters.AddWithValue("@isi_artikel", artikel.isi_artikel);
                        command1.Parameters.AddWithValue("@status", artikel.status);
                        command1.Parameters.AddWithValue("@id", artikel.id_artikel);
                        connection.Open();
                        command1.ExecuteNonQuery();
                    }
                }
                else
                {
                    string query = "UPDATE Artikel SET id_pengelola = @id_pengelola, tanggal_rilis = @tanggal_rilis, " +
                                   "judul_artikel = @judul_artikel, isi_artikel = @isi_artikel, " +
                                   "sampul_artikel = @sampul_artikel, status = @status WHERE id_artikel = @id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id_pengelola", artikel.id_pengelola);
                        command.Parameters.AddWithValue("@tanggal_rilis", artikel.tanggal_rilis);
                        command.Parameters.AddWithValue("@judul_artikel", artikel.judul_artikel);
                        command.Parameters.AddWithValue("@isi_artikel", artikel.isi_artikel);
                        command.Parameters.AddWithValue("@sampul_artikel", artikel.sampul_artikel);
                        command.Parameters.AddWithValue("@status", artikel.status);
                        command.Parameters.AddWithValue("@id", artikel.id_artikel);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void DeleteArtikel(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Artikel SET status = 0 WHERE id_artikel = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DoneArtikel(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Artikel SET status = 2 WHERE id_artikel = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public List<ArtikelModel> GetAnotherArtikel(int id)
        {
            List<ArtikelModel> artikels = new List<ArtikelModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT TOP 3 * FROM Artikel WHERE id_artikel != @id AND status = 2 ORDER BY NEWID();";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ArtikelModel artikel = new ArtikelModel
                            {
                                id_artikel = Convert.ToInt32(reader["id_artikel"]),
                                id_pengelola = Convert.ToInt32(reader["id_pengelola"]),
                                tanggal_rilis = Convert.ToDateTime(reader["tanggal_rilis"]),
                                judul_artikel = reader["judul_artikel"].ToString(),
                                isi_artikel = reader["isi_artikel"].ToString(),
                                sampul_artikel = reader["sampul_artikel"].ToString(),
                                status = Convert.ToInt32(reader["status"])
                            };
                            artikels.Add(artikel);
                        }
                    }
                }
            }
            return artikels;
        }
    }
}
