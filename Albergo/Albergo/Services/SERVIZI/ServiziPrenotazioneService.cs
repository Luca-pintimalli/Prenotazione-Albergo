using System;
using System.Data.SqlClient;
using Albergo.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.Common;

namespace Albergo.Services.SERVIZI
{
    public class ServiziPrenotazioneService : SqlServerServiceBase, IServiziPrenotazioneService
    {
        private ServizioPrenotazione servizioPrenotazione;

        public ServiziPrenotazioneService(IConfiguration config) : base(config)
        {
        }

        public void DeleteServizioPrenotazione(int id)
        {
            try
            {
                var query = "DELETE FROM ServiziPrenotazioni WHERE ID = @ID";

                using (var conn = GetConnection())
                {
                    var cmd = GetCommand(query);
                    cmd.Parameters.Add(new SqlParameter("@ID", id));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

            }catch(Exception e )
            {
                throw e;
            }
          
        }


        public ServizioPrenotazione Create(DbDataReader reader)
        {
            return new ServizioPrenotazione
            {
                Data = reader.GetDateTime(reader.GetOrdinal("Data")),
                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                IDPrenotazione = reader.GetInt32(reader.GetOrdinal("IDPrenotazione")),
                IDServizio = reader.GetInt32(reader.GetOrdinal("IDServizio")),
                Prezzo = reader.GetDouble(reader.GetOrdinal("Prezzo")),
                Quantita = reader.GetInt32(reader.GetOrdinal("Quantita"))

            };
        }








        public ServizioPrenotazione GetServizioPrenotazione(int id)
        {
           
            var query = @"
                SELECT sp.ID, sp.IDPrenotazione, sp.IDServizio, sp.Data, sp.Quantita, sp.Prezzo, 
                       p.CodiceFiscale, p.NumeroCamera, p.DataPrenotazione, p.NumeroProgressivo, 
                       p.Anno, p.Dal, p.Al, p.Caparra, p.Tariffa, p.Dettagli
                FROM ServiziPrenotazioni sp
                INNER JOIN Prenotazioni p ON sp.IDPrenotazione = p.ID
                WHERE sp.ID = @ID";

            using (var conn = GetConnection())
            {
                var cmd = GetCommand(query);
                cmd.Parameters.Add(new SqlParameter("@ID", id));
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        servizioPrenotazione = new ServizioPrenotazione
                        {
                            ID = reader.GetInt32(0),
                            IDPrenotazione = reader.GetInt32(1),
                            IDServizio = reader.GetInt32(2),
                            Data = reader.GetDateTime(3),
                            Quantita = reader.GetInt32(4),
                            Prezzo = reader.GetDouble(5)
                        };
                    }
                }
            }

            return servizioPrenotazione;
        }







        public IEnumerable<ServizioPrenotazione> GetServiziPrenotazione()
        {
            var serviziPrenotazione = new List<ServizioPrenotazione>();
            var query = @"
                SELECT sp.ID, sp.IDPrenotazione, sp.IDServizio, sp.Data, sp.Quantita, sp.Prezzo, 
                       p.CodiceFiscale, p.NumeroCamera, p.DataPrenotazione, p.NumeroProgressivo, 
                       p.Anno, p.Dal, p.Al, p.Caparra, p.Tariffa, p.Dettagli
                FROM ServiziPrenotazioni sp
                INNER JOIN Prenotazioni p ON sp.IDPrenotazione = p.ID";

            using (var conn = GetConnection())
            {
                var cmd = GetCommand(query);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var servizioPrenotazione = new ServizioPrenotazione
                        {
                            ID = reader.GetInt32(0),
                            IDPrenotazione = reader.GetInt32(1),
                            IDServizio = reader.GetInt32(2),
                            Data = reader.GetDateTime(3),
                            Quantita = reader.GetInt32(4),
                            Prezzo = reader.GetDouble(5)
                        };
                        serviziPrenotazione.Add(servizioPrenotazione);
                    }
                }
            }

            return serviziPrenotazione;
        }

        public void NewServizioPrenotazione(ServizioPrenotazione servizioPrenotazione)
        {
            try
            {
                var query = @"
                INSERT INTO ServiziPrenotazioni (IDPrenotazione, IDServizio, Data, Quantita, Prezzo) 
                VALUES (@IDPrenotazione, @IDServizio, @Data, @Quantita, @Prezzo)";

                using (var conn = GetConnection())
                {
                    var cmd = GetCommand(query);
                    cmd.Parameters.Add(new SqlParameter("@IDPrenotazione", servizioPrenotazione.IDPrenotazione));
                    cmd.Parameters.Add(new SqlParameter("@IDServizio", servizioPrenotazione.IDServizio));
                    cmd.Parameters.Add(new SqlParameter("@Data", servizioPrenotazione.Data));
                    cmd.Parameters.Add(new SqlParameter("@Quantita", servizioPrenotazione.Quantita));
                    cmd.Parameters.Add(new SqlParameter("@Prezzo", servizioPrenotazione.Prezzo));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

            }
            catch(Exception e )
            {
                throw e;
            }


        }

        public void UpdateServizioPrenotazione(int id, ServizioPrenotazione servizioPrenotazione)
        {
            var query = @"
                UPDATE ServiziPrenotazioni 
                SET IDPrenotazione = @IDPrenotazione, IDServizio = @IDServizio, Data = @Data, Quantita = @Quantita, Prezzo = @Prezzo 
                WHERE ID = @ID";

            using (var conn = GetConnection())
            {
                var cmd = GetCommand(query);
                cmd.Parameters.Add(new SqlParameter("@ID", id));
                cmd.Parameters.Add(new SqlParameter("@IDPrenotazione", servizioPrenotazione.IDPrenotazione));
                cmd.Parameters.Add(new SqlParameter("@IDServizio", servizioPrenotazione.IDServizio));
                cmd.Parameters.Add(new SqlParameter("@Data", servizioPrenotazione.Data));
                cmd.Parameters.Add(new SqlParameter("@Quantita", servizioPrenotazione.Quantita));
                cmd.Parameters.Add(new SqlParameter("@Prezzo", servizioPrenotazione.Prezzo));
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
