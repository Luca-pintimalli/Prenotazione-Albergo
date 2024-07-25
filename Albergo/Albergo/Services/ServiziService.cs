using System;
using System.Data.Common;
using System.Data.SqlClient;
using Albergo.Models;

namespace Albergo.Services.Servizi
{
    public class ServiziService : SqlServerServiceBase, IServiziService
    {
        public ServiziService(IConfiguration config) : base(config)
        {
        }

        public void DeleteServizio(int id)
        {
            try
            {
                //CREO COMANDO
                var cmd = GetCommand("DELETE From Servizi WHERE Id = @Id");

                cmd.Parameters.Add(new SqlParameter("@Id", id));

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

        //CREAZIONE METODO CREATE PER L'AGGIUNTA DI UN NUOVO Servizio
        public Servizio Create(DbDataReader reader)
        {
            return new Servizio
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),

                Nome = reader["Nome"].ToString(),

            };
        }



        public IEnumerable<Servizio> GetServizi()
        {
            try
            {
                //CREO COMANDO
                var cmd = GetCommand("SELECT * FROM Servizi");

                //COMANDO CONNESIONE
                var conn = GetConnection();

                //APERTURA
                conn.Open();

                //GESTIONE COMANDO
                var reader = cmd.ExecuteReader();

                var list = new List<Servizio>();
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





        public Servizio GetServizio(int ID)
        {
            try
            {
                //CREO COMANDO
                var cmd = GetCommand("SELECT * FROM Servizi WHERE Id = @Id");
                cmd.Parameters.Add(new SqlParameter("@Id", ID));

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

        public void NewServizio(Servizio servizio)
        {
            try
            {
                //CREO COMANDO
                var cmd = GetCommand("INSERT INTO Servizi (Nome) VALUES (@Nome)");
                cmd.Parameters.Add(new SqlParameter("@Nome", servizio.Nome));


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


        public void UpdateServizio(int ID, Servizio servizio)
        {
            try
            {


                //CREO COMANDO 
                var cmd = GetCommand("UPDATE Servizi SET  Nome = @Nome WHERE Id = @Id");


                cmd.Parameters.Add(new SqlParameter("@Id", servizio.Id));
                cmd.Parameters.Add(new SqlParameter("@Nome", servizio.Nome));



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



    
