using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ContactApp.Models;

namespace ContactApp.Services
{
    public class ContactService
    {
        private readonly string _connectionString;

        public ContactService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddContactToDatabase(int userId, string name, string surname, string email, string phone, string imagePath)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Contacts (UserId, Name, Surname, Email, Phone, ImagePath) VALUES (@userId, @name, @surname, @email, @phone, @imagePath)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@surname", surname);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@imagePath", imagePath ?? string.Empty); 
                    command.ExecuteNonQuery();
                }
            }
        }


        public List<Contact> GetContacts(int userId)
        {
            var contacts = new List<Contact>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, UserId, Name, Surname, ImagePath, Phone, Email FROM Contacts WHERE UserId = @userId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            contacts.Add(new Contact
                            {
                                Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                                UserId = reader["UserId"] != DBNull.Value ? Convert.ToInt32(reader["UserId"]) : 0,
                                Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : string.Empty,
                                Surname = reader["Surname"] != DBNull.Value ? reader["Surname"].ToString() : string.Empty,
                                ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : string.Empty,
                                Phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : string.Empty,
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty
                            });
                        }
                    }
                }
            }
            return contacts;
        }


        public Contact GetContactById(int contactId, int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Contacts WHERE Id = @contactId AND UserId = @userId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@contactId", contactId);
                    command.Parameters.AddWithValue("@userId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Contact
                            {
                                Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                                UserId = reader["UserId"] != DBNull.Value ? Convert.ToInt32(reader["UserId"]) : 0,
                                Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : string.Empty,
                                Surname = reader["Surname"] != DBNull.Value ? reader["Surname"].ToString() : string.Empty,
                                ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : string.Empty,
                                Phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : string.Empty,
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void UpdateContactInDatabase(int contactId, int userId, string name, string surname, string email, string phone, string imagePath)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE Contacts SET Name = @name, Surname = @surname, Email = @email, Phone = @phone, ImagePath = @imagePath WHERE Id = @contactId AND UserId = @userId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@surname", surname);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@imagePath", imagePath ?? (object)DBNull.Value); 
                    command.Parameters.AddWithValue("@contactId", contactId);
                    command.Parameters.AddWithValue("@userId", userId);

                    command.ExecuteNonQuery();
                }
            }
        }


        public void DeleteContactFromDatabase(int contactId, int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Contacts WHERE Id = @contactId AND UserId = @userId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@contactId", contactId);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}