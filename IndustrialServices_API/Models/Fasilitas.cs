using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace IndustrialServices_API.Models
{
    public class Fasilitas
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public Fasilitas(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<FasilitasModel> GetAllFasilitas()
        {
            List<FasilitasModel> fasilitasList = new List<FasilitasModel>();
            try
            {
                string query = "SELECT * FROM Fasilitas WHERE status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FasilitasModel fasilitasModel = new FasilitasModel
                    {
                        id_fasilitas = Convert.ToInt32(reader["id_fasilitas"]),
                        nama_fasilitas = reader["nama_fasilitas"].ToString(),
                        id_tipe_fasilitas = Convert.ToInt32(reader["id_tipe_fasilitas"]),
                        deskripsi_fasilitas = reader["deskripsi_fasilitas"].ToString(),
                        status = Convert.ToInt32(reader["status"])
                    };
                    fasilitasList.Add(fasilitasModel);
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
            return fasilitasList;
        }

        public void InsertFasilitas(FasilitasModel fasilitasModel)
        {
            try
            {
                string query = "INSERT INTO Fasilitas VALUES (@nama, @jenis, @deskripsi, @status)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@nama", fasilitasModel.nama_fasilitas);
                command.Parameters.AddWithValue("@jenis", fasilitasModel.id_tipe_fasilitas);
                command.Parameters.AddWithValue("@deskripsi", fasilitasModel.deskripsi_fasilitas);
                command.Parameters.AddWithValue("@status", fasilitasModel.status);
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


        public void UpdateFasilitas(FasilitasModel fasilitasModel)
        {
            try
            {
                string query = "UPDATE Fasilitas SET nama_fasilitas = @nama, id_tipe_fasilitas = @jenis, deskripsi_fasilitas = @deskripsi, status = @status WHERE id_fasilitas = @id";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@nama", fasilitasModel.nama_fasilitas);
                command.Parameters.AddWithValue("@jenis", fasilitasModel.id_tipe_fasilitas);
                command.Parameters.AddWithValue("@deskripsi", fasilitasModel.deskripsi_fasilitas);
                command.Parameters.AddWithValue("@status", fasilitasModel.status);
                command.Parameters.AddWithValue("@id", fasilitasModel.id_fasilitas);
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

        public void DeleteFasilitas(int id)
        {
            try
            {
                string query = "UPDATE Fasilitas SET status = 0 WHERE id_fasilitas = @id";
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

        public FasilitasModel GetFasilitasById(int id)
        {
            FasilitasModel fasilitasModel = new FasilitasModel();
            try
            {
                string query = "SELECT * FROM Fasilitas WHERE id_fasilitas = @id AND status != 0";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@id", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    fasilitasModel.id_fasilitas = Convert.ToInt32(reader["id_fasilitas"]);
                    fasilitasModel.nama_fasilitas = reader["nama_fasilitas"].ToString();
                    fasilitasModel.id_tipe_fasilitas = Convert.ToInt32(reader["id_tipe_fasilitas"]);
                    fasilitasModel.deskripsi_fasilitas = reader["deskripsi_fasilitas"].ToString();
                    fasilitasModel.status = Convert.ToInt32(reader["status"]);
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
            return fasilitasModel;
        }
    }
}
