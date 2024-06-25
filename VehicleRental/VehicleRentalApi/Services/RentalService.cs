using System.Data.SqlClient;
using VehicleRentalApi.Contracts;
using VehicleRentalApi.Models;

namespace VehicleRentalApi.Services
{
    public class RentalService : IRental
    {
        private readonly IConfiguration _config;
        private readonly string? _connectionString;
        private readonly SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataReader _reader;

        public RentalService(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("ConnStr");
            _connection = new SqlConnection(_connectionString);
        }

        public Rental Add(Rental entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rental> GetAll()
        {
            try
            {
                List<Rental> rentals = new List<Rental>();
                string query = @"SELECT * FROM Rentals
                                ORDER BY RentalID DESC";
                _command = new SqlCommand(query, _connection);
                _connection.Open();
                _reader = _command.ExecuteReader();
                if (_reader.HasRows)
                {
                    while (_reader.Read())
                    {
                        rentals.Add(new Rental
                        {
                            RentalId = _reader["RentalId"].ToString(),
                            RentalDate = _reader.GetDateTime(_reader.GetOrdinal("RentalDate")),
                            //ReturnDate = _reader.GetDateTime(_reader.GetOrdinal("ReturnDate")),
                            ReturnDate = _reader.IsDBNull(_reader.GetOrdinal("ReturnDate"))
                            ? (DateTime?)null
                            : _reader.GetDateTime(_reader.GetOrdinal("ReturnDate")),
                            Status = _reader["Status"].ToString(),
                            UserId = Convert.ToInt32(_reader["UserID"]),
                            VehicleId = _reader["VehicleId"].ToString()
                        });
                    }
                }
                _reader.Close();
                return rentals;
            }
            catch (SqlException sqlEx)
            {
                throw new ArgumentException($"Error: {sqlEx.Message}");
            }
            finally
            {
                _command.Dispose();
                _connection.Close();
            }
        }

        public Rental GetById(int id)
        {
            try
            {
                Rental rental = new Rental();
                string query = @"SELECT * FROM Rentals
                                WHERE RentalId = @RentalId";

                _command = new SqlCommand(query, _connection);
                _command.Parameters.AddWithValue("@RentalId", id);
                _connection.Open();
                _reader = _command.ExecuteReader();
                if (_reader.HasRows)
                {
                    while (_reader.Read())
                    {
                        rental.RentalId = _reader["RentalId"].ToString();
                        rental.RentalDate = _reader.GetDateTime(_reader.GetOrdinal("RentalDate"));
                        //rental.ReturnDate = _reader.GetDateTime(_reader.GetOrdinal("ReturnDate"));
                        rental.ReturnDate = _reader.IsDBNull(_reader.GetOrdinal("ReturnDate"))
                        ? (DateTime?)null
                        : _reader.GetDateTime(_reader.GetOrdinal("ReturnDate"));
                        rental.Status = _reader["Status"].ToString();
                        rental.UserId = Convert.ToInt32(_reader["UserID"]);
                        rental.VehicleId = _reader["VehicleId"].ToString();
                    }
                }
                _reader.Close();
                return rental;
            }
            catch (SqlException sqlEx)
            {
                throw new ArgumentException($"Error: {sqlEx.Message}");
            }
            finally
            {
                _command.Dispose();
                _connection.Close();
            }
        }

        public Rental Update(Rental entity)
        {
            throw new NotImplementedException();
        }
    }
}
