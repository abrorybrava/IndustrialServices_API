using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace IndustrialServices_API.Models
{
    public class Flyer
    {
        private readonly string _connectionString;

        public Flyer(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void InsertFlyer(FlyerModel flyer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Flyer VALUES (@p1, @p2)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@p1", flyer.path_flyer);
                    command.Parameters.AddWithValue("@p2", flyer.status);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<FlyerModel> GetFlyers()
        {
            List<FlyerModel> flyers = new List<FlyerModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT id_flyer, path_flyer, status FROM Flyer WHERE status != 0";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        FlyerModel flyer = new FlyerModel()
                        {
                            id_flyer = Convert.ToInt32(reader["id_flyer"]),
                            path_flyer = reader["path_flyer"].ToString(),
                            status = Convert.ToInt32(reader["status"])
                        };
                        flyers.Add(flyer);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return flyers;
        }
        public List<FlyerModel> GetFlyersbyId(int id)
        {
            List<FlyerModel> flyers = new List<FlyerModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT id_flyer, path_flyer, status FROM Flyer WHERE id_flyer = @p1 AND status != 0";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@p1", id);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        FlyerModel flyer = new FlyerModel()
                        {
                            id_flyer = Convert.ToInt32(reader["id_flyer"]),
                            path_flyer = reader["path_flyer"].ToString(),
                            status = Convert.ToInt32(reader["status"])
                        };
                        flyers.Add(flyer);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return flyers;
        }

        public void UpdateFlyer(FlyerModel flyer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE Flyer SET path_flyer = @p1, status = @p2 WHERE id_flyer = @p3";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@p1", flyer.path_flyer);
                    command.Parameters.AddWithValue("@p2", flyer.status);
                    command.Parameters.AddWithValue("@p3", flyer.id_flyer);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteFlyer(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE Flyer SET status = 0 WHERE id_flyer = @p1";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@p1", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}