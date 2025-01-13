using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;

public class DatabaseService
{
    private readonly string _connectionString;

    public DatabaseService()
    {
        // Utiliser le chemin absolu fourni pour la base de données
        string databasePath = @"C:\Users\Thibaut Joncour\source\repos\MagicNumber\wwwroot\db.sqlite3";
        _connectionString = $"Data Source={databasePath}";
    }

    // Récupérer tous les enregistrements d'une table et retourner un JSON
    public List<Dictionary<string, object>> GetData(string query, Dictionary<string, object> parameters = null)
    {
        var results = new List<Dictionary<string, object>>();

        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            using (var command = new SqliteCommand(query, connection))
            {
                // Ajout des paramètres à la commande SQL
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[reader.GetName(i)] = reader.GetValue(i);
                        }
                        results.Add(row);
                    }
                }
            }

            return results;
        }
    }
}
