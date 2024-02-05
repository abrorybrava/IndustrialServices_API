using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace IndustrialServices_API.Models
{
    public class TipeFasilitas
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public TipeFasilitas(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<TipeFasilitasModel> GetAllTipeFasilitas()
        {
            List<TipeFasilitasModel> tipeFasilitasList = new List<TipeFasilitasModel>();
            try
            {
                string query = "SELECT * FROM Tipe_Fasilitas WHERE status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TipeFasilitasModel tipeFasilitasModel = new TipeFasilitasModel
                    {
                        id_tipe_fasilitas = Convert.ToInt32(reader["id_tipe_fasilitas"]),
                        tipe_fasilitas = reader["tipe_fasilitas"].ToString(),
                        status = Convert.ToInt32(reader["status"])
                    };
                    tipeFasilitasList.Add(tipeFasilitasModel);
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
            return tipeFasilitasList;
        }

        public void InsertTipeFasilitas(TipeFasilitasModel tipeFasilitasModel)
        {
            try
            {
                string query = "INSERT INTO Tipe_Fasilitas (tipe_fasilitas, status) VALUES (@tipe, @status)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@tipe", tipeFasilitasModel.tipe_fasilitas);
                command.Parameters.AddWithValue("@status", tipeFasilitasModel.status);
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

        public void UpdateTipeFasilitas(TipeFasilitasModel tipeFasilitasModel)
        {
            try
            {
                string query = "UPDATE Tipe_Fasilitas SET tipe_fasilitas = @tipe WHERE id_tipe_fasilitas = @id";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@tipe", tipeFasilitasModel.tipe_fasilitas);
                command.Parameters.AddWithValue("@id", tipeFasilitasModel.id_tipe_fasilitas);
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

        public void DeleteTipeFasilitas(int id)
        {
            try
            {
                string query = "UPDATE Tipe_Fasilitas SET status = 0 WHERE id_tipe_fasilitas = @id";
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

        public TipeFasilitasModel GetTipeFasilitasById(int id)
        {
            TipeFasilitasModel tipeFasilitasModel = new TipeFasilitasModel();
            try
            {
                string query = "SELECT * FROM Tipe_Fasilitas WHERE id_tipe_fasilitas = @id AND status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@id", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    tipeFasilitasModel.id_tipe_fasilitas = Convert.ToInt32(reader["id_tipe_fasilitas"]);
                    tipeFasilitasModel.tipe_fasilitas = reader["tipe_fasilitas"].ToString();
                    tipeFasilitasModel.status = Convert.ToInt32(reader["status"]);
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
            return tipeFasilitasModel;
        }

        public bool CheckNama(TipeFasilitasModel tipeFasilitas)
        {
            try
            {
                string query = "SELECT * FROM Tipe_Fasilitas WHERE tipe_fasilitas = @p1 AND status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", tipeFasilitas.tipe_fasilitas);
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

        public bool CheckNamaEdit(TipeFasilitasModel tipeFasilitas)
        {
            try
            {
                string query = "SELECT * FROM Tipe_Fasilitas WHERE id_tipe_fasilitas != @p1 AND tipe_fasilitas = @p2 AND status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", tipeFasilitas.id_tipe_fasilitas);
                command.Parameters.AddWithValue("@p2", tipeFasilitas.tipe_fasilitas);
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
