using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace IndustrialServices_API.Models
{
    public class Testimoni
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public Testimoni(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<TestimoniModel> GetAllTestimonies()
        {
            List<TestimoniModel> testimonies = new List<TestimoniModel>();
            try
            {
                string query = "SELECT * FROM Testimoni WHERE status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TestimoniModel testimonial = new TestimoniModel
                    {
                        id_testimoni = Convert.ToInt32(reader["id_testimoni"].ToString()),
                        id_pelatihan = reader["id_pelatihan"].ToString(),
                        nama_peserta = reader["nama_peserta"].ToString(),
                        asal_instansi = reader["asal_instansi"].ToString(),
                        foto_peserta = reader["foto_peserta"].ToString(),
                        testimoni_peserta = reader["testimoni_peserta"].ToString(),
                        status = Convert.ToInt32(reader["status"].ToString())
                    };
                    testimonies.Add(testimonial);
                }
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return testimonies;
        }

        public TestimoniModel GetTestimonyById(int id)
        {
            TestimoniModel testimonial = new TestimoniModel();
            try
            {
                string query = "SELECT * FROM Testimoni WHERE id_testimoni = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    testimonial.id_testimoni = Convert.ToInt32(reader["id_testimoni"].ToString());
                    testimonial.id_pelatihan = reader["id_pelatihan"].ToString();
                    testimonial.nama_peserta = reader["nama_peserta"].ToString();
                    testimonial.asal_instansi = reader["asal_instansi"].ToString();
                    testimonial.foto_peserta = reader["foto_peserta"].ToString();
                    testimonial.testimoni_peserta = reader["testimoni_peserta"].ToString();
                    testimonial.status = Convert.ToInt32(reader["status"].ToString());
                }
                reader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return testimonial;
        }

        public List<TestimoniModel> GetTestimonyByPelatihan(int id)
        {
            List<TestimoniModel> testimonials = new List<TestimoniModel>();
            try
            {
                string query = "SELECT * FROM Testimoni WHERE id_pelatihan = @p1 AND status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TestimoniModel testimonial = new TestimoniModel
                    {
                        id_testimoni = Convert.ToInt32(reader["id_testimoni"].ToString()),
                        id_pelatihan = reader["id_pelatihan"].ToString(),
                        nama_peserta = reader["nama_peserta"].ToString(),
                        asal_instansi = reader["asal_instansi"].ToString(),
                        foto_peserta = reader["foto_peserta"].ToString(),
                        testimoni_peserta = reader["testimoni_peserta"].ToString(),
                        status = Convert.ToInt32(reader["status"].ToString())
                    };
                    testimonials.Add(testimonial);
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
            return testimonials;
        }


        public void InsertTestimony(TestimoniModel testimonial)
        {
            try
            {
                string query = "INSERT INTO Testimoni VALUES (@p1, @p2, @p3, @p4, @p5, @p6)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", testimonial.id_pelatihan);
                command.Parameters.AddWithValue("@p2", testimonial.nama_peserta);
                command.Parameters.AddWithValue("@p3", testimonial.asal_instansi);
                command.Parameters.AddWithValue("@p4", testimonial.foto_peserta);
                command.Parameters.AddWithValue("@p5", testimonial.testimoni_peserta);
                command.Parameters.AddWithValue("@p6", testimonial.status);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateTestimony(TestimoniModel testimonial)
        {
            try
            {
                string query = "UPDATE Testimoni SET id_pelatihan = @p2, nama_peserta = @p3, asal_instansi = @p4, foto_peserta = @p5, testimoni_peserta = @p6, status = @p7 WHERE id_testimoni = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", testimonial.id_testimoni);
                command.Parameters.AddWithValue("@p2", testimonial.id_pelatihan);
                command.Parameters.AddWithValue("@p3", testimonial.nama_peserta);
                command.Parameters.AddWithValue("@p4", testimonial.asal_instansi);
                command.Parameters.AddWithValue("@p5", testimonial.foto_peserta);
                command.Parameters.AddWithValue("@p6", testimonial.testimoni_peserta);
                command.Parameters.AddWithValue("@p7", testimonial.status);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteTestimony(int id)
        {
            try
            {
                string query = "UPDATE Testimoni SET status = 0 WHERE id_testimoni = @p1";
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
    }
}
