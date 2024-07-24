using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Albergo.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Albergo.Services.CLIENTI
{
	public class ClientiService : SqlServerServiceBase , IClientiService
	{
		public ClientiService(IConfiguration config) : base(config)
        {
		}

        //eliminazione CLIENTE
        public void DeleteCliente(int ID)
        {
            try
            {
                //CREO COMANDO
                var cmd = GetCommand("DELETE From Clienti WHERE ID = @ID");

                cmd.Parameters.Add(new SqlParameter("@ID", ID));

                //GESTIONE CONNESIONE
                var conn = GetConnection();

                //APERTURA CONNESIONE
                conn.Open();

                //GESTIONE COMANDO
                int result = cmd.ExecuteNonQuery();

                //CHIUSURA CONNESIONE
                conn.Close();

            }
            catch (Exception e)
            {
                throw e;

            }
        }


        //CREAZIONE METODO CREATE PER L'AGGIUNTA DIUN NUOVO CLIENTE
        public Cliente Create(DbDataReader reader)
        {
            return new Cliente
            {
                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                CodiceFiscale = reader["CodiceFiscale"].ToString(),
                Cognome = reader["Cognome"].ToString(),
                Nome = reader["Nome"].ToString(),
                Citta = reader["Citta"].ToString(),
                Provincia = reader["Provincia"].ToString(),
                Email = reader["Email"].ToString(),
                Telefono = reader["Telefono"].ToString(),
                Cellulare = reader["Cellulare"].ToString()
            };
        }

        public Cliente GetCliente(int ID)
        {
            try
            {
                //CREO COMANDO
                var cmd = GetCommand("SELECT * FROM Clienti WHERE ID = @ID");
                cmd.Parameters.Add(new SqlParameter("@ID", ID));

                //GESTIONE CONNESIONE
                var conn = GetConnection();

                //APERTURA CONNESIONE
                conn.Open();

                //ESEGUO COMANDO
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                    return Create(reader);
                throw new Exception("Non trovato");


            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public IEnumerable<Cliente> GetClienti()
        {
            try
            {
                //CREO COMANDO
                var cmd = GetCommand("SELECT * FROM Clienti");

                //COMANDO CONNESIONE
                var conn = GetConnection();

                //APERTURA
                conn.Open();

                //GESTIONE COMANDO
                var reader = cmd.ExecuteReader();

                var list = new List<Cliente>();
                while (reader.Read())
                    list.Add(Create(reader));

                //CHIUSURA
                conn.Close();

                return list;

            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public void NewCliente(Cliente cliente)
        {
            try
            {
                //CREO COMANDO
                var cmd = GetCommand("INSERT INTO Clienti (CodiceFiscale, Cognome, Nome, Citta, Provincia, Email, Telefono, Cellulare) VALUES (@CodiceFiscale, @Cognome, @Nome, @Citta, @Provincia, @Email, @Telefono, @Cellulare)");
                cmd.Parameters.Add(new SqlParameter("@CodiceFiscale", cliente.CodiceFiscale));
                cmd.Parameters.Add(new SqlParameter("@Cognome", cliente.Cognome));
                cmd.Parameters.Add(new SqlParameter("@Nome", cliente.Nome));
                cmd.Parameters.Add(new SqlParameter("@Citta", cliente.Citta));
                cmd.Parameters.Add(new SqlParameter("@Provincia", cliente.Provincia));
                cmd.Parameters.Add(new SqlParameter("@Email", cliente.Email));
                cmd.Parameters.Add(new SqlParameter("@Telefono", cliente.Telefono));
                cmd.Parameters.Add(new SqlParameter("@Cellulare", cliente.Cellulare));


                //GESTIONE CONNESIONE
                var conn = GetConnection();

                //APERTURA CONNESIONE
                conn.Open();

                //GESTIONE COMANDO
                var result = cmd.ExecuteNonQuery();

                //CHIUSURA CONNESIONE
                conn.Close();




            }
            catch (Exception e)
            {
                throw e;
            }
        }
    

        public void UpdateCliente(int ID, Cliente cliente)
        {
            try
            {
                //CREO COMANDO 
                var cmd = GetCommand("UPDATE Clienti SET Cognome = @Cognome, Nome = @Nome, Citta = @Citta, Provincia = @Provincia, Email = @Email, Telefono = @Telefono, Cellulare = @Cellulare WHERE ID = @ID");


                cmd.Parameters.Add(new SqlParameter("@ID", cliente.ID));
                cmd.Parameters.Add(new SqlParameter("@Cognome", cliente.Cognome));
                cmd.Parameters.Add(new SqlParameter("@Nome", cliente.Nome));
                cmd.Parameters.Add(new SqlParameter("@Citta", cliente.Citta ));
                cmd.Parameters.Add(new SqlParameter("@Provincia", cliente.Provincia));
                cmd.Parameters.Add(new SqlParameter("@Email", cliente.Email ));
                cmd.Parameters.Add(new SqlParameter("@Telefono", cliente.Telefono));
                cmd.Parameters.Add(new SqlParameter("@Cellulare", cliente.Cellulare)); 

                //GESTIONE CONNESIONE
                var conn = GetConnection();

                //APRO CONNESIONE
                conn.Open();



                //ESEGUO COMANDO
                var result = cmd.ExecuteNonQuery();


                //CHIUSURA CONNESIONE
                conn.Close();

            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}

