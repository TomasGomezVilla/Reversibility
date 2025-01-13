using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text.Json;

class DatabaseProvider
{
    static void Main(string[] args)
    {
        var users = GetUsersFromDatabase();

        // Filtrage avec LINQ
        var filteredUsers = users.Where(u => u.Age > 30 && u.City == "Paris");

        // Convertir en JSON
        string json = JsonSerializer.Serialize(filteredUsers, new JsonSerializerOptions { WriteIndented = true });

        Console.WriteLine(json);
    }

    static List<dynamic> GetUsersFromDatabase()
    {
        var results = new List<dynamic>();
        string connectionString = "Data Source=C:\\Users\\Thibaut Joncour\\source\\repos\\MagicNumber\\wwwroot\\db.sqlite3";

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Users"; // Récupérer toutes les colonnes

            using (var command = new SQLiteCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Utilisation de dynamic pour une flexibilité totale
                        var row = new
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Age = reader.GetInt32(reader.GetOrdinal("Age")),
                            City = reader.GetString(reader.GetOrdinal("City")),
                            // Ajoutez d'autres colonnes ici
                        };
                        results.Add(row);
                    }
                }
            }
        }

        return results;
    }
}
