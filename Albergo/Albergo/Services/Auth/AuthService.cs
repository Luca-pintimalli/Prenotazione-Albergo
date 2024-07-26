using System;
using System.Data.SqlClient;
using Albergo.Models;
using Microsoft.Extensions.Configuration;

namespace Albergo.Services.Auth
{
    public class AuthService : SqlServerServiceBase, IAuthService
    {
        public AuthService(IConfiguration config) : base(config)
        {
        }

        private const string Login_Command = "SELECT ID, NomeUtente, Password FROM Dipendenti WHERE NomeUtente = @NomeUtente AND Password = @Password";

        public Dipendenti Login(string nomeUtente, string password)
        {
            
            try
            {
                using var conn = GetConnection();
                conn.Open();

                using SqlCommand cmd = new SqlCommand(Login_Command, (SqlConnection)conn); // Aggiungi connessione qui
                cmd.Parameters.AddWithValue("@NomeUtente", nomeUtente);
                cmd.Parameters.AddWithValue("@Password", password);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    var dipendente = new Dipendenti
                    {
                        Id = reader.GetInt32(0),
                        NomeUtente = reader.GetString(1),
                        Password = reader.GetString(2)
                    };
                    return dipendente;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }

            return null;
        }
    }
}