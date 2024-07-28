using System;
using System.Data.Common;
using System.Data.SqlClient;
using Albergo.Models;

namespace Albergo.Services
{
	public class PrenotazioniService :SqlServerServiceBase, IPrenotazioniService
	{
		public PrenotazioniService(IConfiguration config) : base(config)

        {
		}

        public void DeletePrenotazione(int ID)
        {
            try
            {
                //CREO COMANDO
                var cmd = GetCommand("DELETE From Prenotazioni WHERE ID = @ID");

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



        //CREAZIONE METODO CREATE PER L'AGGIUNTA DI UNA NUOVA PRENOTAZIONE
        public Prenotazione Create(DbDataReader reader)
        {
            return new Prenotazione
            {
                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                CodiceFiscale = reader.GetString(reader.GetOrdinal("CodiceFiscale")),
                NumeroCamera = reader.GetInt32(reader.GetOrdinal("NumeroCamera")),
                DataPrenotazione = reader.GetDateTime(reader.GetOrdinal("DataPrenotazione")),
                NumeroProgressivo = reader.GetInt32(reader.GetOrdinal("NumeroProgressivo")),
                Anno = reader.GetInt32(reader.GetOrdinal("Anno")),
                Dal = reader.GetDateTime(reader.GetOrdinal("Dal")),
                Al = reader.GetDateTime(reader.GetOrdinal("Al")),
                Caparra = reader.GetDecimal(reader.GetOrdinal("Caparra")),
                Tariffa = reader.GetDecimal(reader.GetOrdinal("Tariffa")),
                Dettagli = reader.GetString(reader.GetOrdinal("Dettagli"))
            };
        }




        public Prenotazione GetPrenotazione(int ID)
        {
            try
            {
                //CREO COMANDO
                var cmd = GetCommand("SELECT * FROM Prenotazioni WHERE ID = @ID");
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

        public IEnumerable<PrenotazioneDetails> GetPrenotazioni()
        {
            try
            {
                var cmd = GetCommand(@"
            SELECT 
                p.ID, 
                p.CodiceFiscale, 
                p.NumeroCamera, 
                p.DataPrenotazione, 
                p.NumeroProgressivo, 
                p.Anno, 
                p.Dal, 
                p.Al, 
                p.Caparra, 
                p.Tariffa, 
                p.Dettagli, 
                c.Nome AS NomeCliente, 
                c.Cognome AS CognomeCliente,
                cam.Numero AS NumeroCamera,
                cam.Descrizione AS DescrizioneCamera
            FROM 
                Prenotazioni p
            INNER JOIN 
                Clienti c ON p.CodiceFiscale = c.CodiceFiscale
            INNER JOIN 
                Camere cam ON p.NumeroCamera = cam.Numero");

                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        var list = new List<PrenotazioneDetails>();
                        while (reader.Read())
                        {
                            list.Add(new PrenotazioneDetails
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                CodiceFiscale = reader.GetString(reader.GetOrdinal("CodiceFiscale")),
                                NumeroCamera = reader.GetInt32(reader.GetOrdinal("NumeroCamera")),
                                DataPrenotazione = reader.GetDateTime(reader.GetOrdinal("DataPrenotazione")),
                                NumeroProgressivo = reader.GetInt32(reader.GetOrdinal("NumeroProgressivo")),
                                Anno = reader.GetInt32(reader.GetOrdinal("Anno")),
                                Dal = reader.GetDateTime(reader.GetOrdinal("Dal")),
                                Al = reader.GetDateTime(reader.GetOrdinal("Al")),
                                Caparra = reader.GetDecimal(reader.GetOrdinal("Caparra")),
                                Tariffa = reader.GetDecimal(reader.GetOrdinal("Tariffa")),
                                Dettagli = reader.GetString(reader.GetOrdinal("Dettagli")),
                                NomeCliente = reader.GetString(reader.GetOrdinal("NomeCliente")),
                                CognomeCliente = reader.GetString(reader.GetOrdinal("CognomeCliente")),
                                DescrizioneCamera = reader.GetString(reader.GetOrdinal("DescrizioneCamera"))
                            });
                        }
                        return list;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Errore durante il recupero delle prenotazioni", e);
            }
        }


        public void newPrenotazione(Prenotazione prenotazione)
        {
            try
            {
                var cmd = GetCommand("INSERT INTO PRENOTAZIONI (CodiceFiscale, NumeroCamera, DataPrenotazione, NumeroProgressivo, Anno , Dal, Al, Caparra, Tariffa , Dettagli ) VALUES (@CodiceFiscale, @NumeroCamera, @DataPrenotazione , @NumeroProgressivo, @Anno, @Dal , @Al, @Caparra, @Tariffa, @Dettagli)");
                cmd.Parameters.Add(new SqlParameter("@CodiceFiscale", prenotazione.CodiceFiscale));
                cmd.Parameters.Add(new SqlParameter("@NumeroCamera", prenotazione.NumeroCamera));
                cmd.Parameters.Add(new SqlParameter("@DataPrenotazione", prenotazione.DataPrenotazione));
                cmd.Parameters.Add(new SqlParameter("@NumeroProgressivo", prenotazione.NumeroProgressivo));
                cmd.Parameters.Add(new SqlParameter("@Anno", prenotazione.Anno));
                cmd.Parameters.Add(new SqlParameter("@Dal", prenotazione.Dal));
                cmd.Parameters.Add(new SqlParameter("@Al", prenotazione.Al));
                cmd.Parameters.Add(new SqlParameter("@Caparra", prenotazione.Caparra));
                cmd.Parameters.Add(new SqlParameter("@Tariffa", prenotazione.Tariffa));
                cmd.Parameters.Add(new SqlParameter("@Dettagli", prenotazione.Dettagli));

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

        public void UpdatePrenotazione(int ID, Prenotazione prenotazione)
        {

            try
            {
                //CREO COMANDO 
                var cmd = GetCommand("UPDATE Prenotazioni SET CodiceFiscale = @CodiceFiscale,NumeroCamera =  @NumeroCamera,DataPrenotazione= @DataPrenotazione , NumeroProgressivo =  @NumeroProgressivo, Anno =  @Anno, Dal = @Dal , Al =  @Al, Caparra =  @Caparra,Tariffa =  @Tariffa, Dettagli = @Dettagli WHERE ID = @ID");

                cmd.Parameters.Add(new SqlParameter("@ID", prenotazione.ID));

                cmd.Parameters.Add(new SqlParameter("@CodiceFiscale", prenotazione.CodiceFiscale));
                cmd.Parameters.Add(new SqlParameter("@NumeroCamera", prenotazione.NumeroCamera));
                cmd.Parameters.Add(new SqlParameter("@DataPrenotazione", prenotazione.DataPrenotazione));
                cmd.Parameters.Add(new SqlParameter("@NumeroProgressivo", prenotazione.NumeroProgressivo));
                cmd.Parameters.Add(new SqlParameter("@Anno", prenotazione.Anno));
                cmd.Parameters.Add(new SqlParameter("@Dal", prenotazione.Dal));
                cmd.Parameters.Add(new SqlParameter("@Al", prenotazione.Al));
                cmd.Parameters.Add(new SqlParameter("@Caparra", prenotazione.Caparra));
                cmd.Parameters.Add(new SqlParameter("@Tariffa", prenotazione.Tariffa));
                cmd.Parameters.Add(new SqlParameter("@Dettagli", prenotazione.Dettagli));


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


        public IEnumerable<PrenotazioneDetails> GetPrenotazioniByCodiceFiscale(string codiceFiscale)
        {
            try
            {
                var cmd = GetCommand(@"
            SELECT 
                p.ID, 
                p.CodiceFiscale, 
                p.NumeroCamera, 
                p.DataPrenotazione, 
                p.NumeroProgressivo, 
                p.Anno, 
                p.Dal, 
                p.Al, 
                p.Caparra, 
                p.Tariffa, 
                p.Dettagli
            FROM 
                Prenotazioni p
            WHERE 
                p.CodiceFiscale = @CodiceFiscale");

                cmd.Parameters.Add(new SqlParameter("@CodiceFiscale", codiceFiscale));

                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        var list = new List<PrenotazioneDetails>();
                        while (reader.Read())
                        {
                            list.Add(new PrenotazioneDetails
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                CodiceFiscale = reader.GetString(reader.GetOrdinal("CodiceFiscale")),
                                NumeroCamera = reader.GetInt32(reader.GetOrdinal("NumeroCamera")),
                                DataPrenotazione = reader.GetDateTime(reader.GetOrdinal("DataPrenotazione")),
                                NumeroProgressivo = reader.GetInt32(reader.GetOrdinal("NumeroProgressivo")),
                                Anno = reader.GetInt32(reader.GetOrdinal("Anno")),
                                Dal = reader.GetDateTime(reader.GetOrdinal("Dal")),
                                Al = reader.GetDateTime(reader.GetOrdinal("Al")),
                                Caparra = reader.GetDecimal(reader.GetOrdinal("Caparra")),
                                Tariffa = reader.GetDecimal(reader.GetOrdinal("Tariffa")),
                                Dettagli = reader.GetString(reader.GetOrdinal("Dettagli"))
                            });
                        }
                        return list;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Errore durante il recupero delle prenotazioni", e);
            }
        }



    }
}

