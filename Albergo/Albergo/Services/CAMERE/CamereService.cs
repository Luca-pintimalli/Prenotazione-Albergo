using System;
using System.Data.Common;
using System.Data.SqlClient;
using Albergo.Services;

namespace Albergo.Models.Camere
{
	public class CamereService : SqlServerServiceBase , ICamereService
	{
		public CamereService(IConfiguration config) : base(config)
		{
		}


        //eliminazione camera
        public void DeleteCamera(int id)
        {
            try
            {
                //CREO COMANDO
                var cmd = GetCommand("DELETE From Camere WHERE Numero = @Numero");

                cmd.Parameters.Add(new SqlParameter("@Numero", id));

                //GESTIONE CONNESIONE
                var conn = GetConnection();

                //APERTURA CONNESIONE
                conn.Open();

                //GESTIONE COMANDO
                int result = cmd.ExecuteNonQuery();

                //CHIUSURA CONNESIONE
                conn.Close();

            }
            catch(Exception e)
            {
                throw e;

            }
        }

        //CREAZIONE METODO CREATE PER L'AGGIUNTA DI NUOVE CAMERE
        public Camera Create(DbDataReader reader)
        {
            return new Camera
            {
                Numero = reader.GetInt32(0),
                Descrizione = reader["Descrizione"].ToString(),
                Tipologia = reader["Tipologia"].ToString()
            };
        }


        //METODO PER RICEVERE LA SINGOLA CAMERA 
        public Camera GetCamera(int Numero)
        {
            try
            {
                //CREO COMANDO
                var cmd = GetCommand("SELECT Numero , Descrizione , Tipologia FROM Camere WHERE Numero = @Numero");
                cmd.Parameters.Add(new SqlParameter("@Numero", Numero));

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
            catch(Exception e )
            {
                throw e;
            }
            
        }



        //RECUPERO CAMERE
        public IEnumerable<Camera> GetCamere()
        {
            try
            {
                //CREO COMANDO
                var cmd = GetCommand("SELECT Numero , Descrizione, Tipologia FROM Camere");

                //COMANDO CONNESIONE
                var conn = GetConnection();

                //APERTURA
                conn.Open();

                //GESTIONE COMANDO
                var reader = cmd.ExecuteReader();

                var list = new List<Camera>();
                while (reader.Read())
                    list.Add(Create(reader));

                //CHIUSURA
                conn.Close();

                return list;

            }
            catch(Exception e )
            {
                throw e;
            }
        }



        //CREAZIONE NUOVA CAMERA 
        public void newCamera(Camera camere)
        {
            try
            {
                //CREO COMANDO
                var cmd = GetCommand("INSERT INTO Camere(Numero,Descrizione,Tipologia) VALUES(@Numero, @Descrizione, @Tipologia)");
                cmd.Parameters.Add(new SqlParameter("@Numero", camere.Numero));
                cmd.Parameters.Add(new SqlParameter("@Descrizione", camere.Descrizione));
                cmd.Parameters.Add(new SqlParameter("@Tipologia", camere.Tipologia));

                //GESTIONE CONNESIONE
                var conn = GetConnection();

                //APERTURA CONNESIONE
                conn.Open();

                //GESTIONE COMANDO
                var result = cmd.ExecuteNonQuery();

                //CHIUSURA CONNESIONE
                conn.Close();




            }
            catch (Exception e )
            {
                throw e;
            }
        }

        public void UpdateCamera(int id, Camera camera)
        {
            try
            {
                //CREO COMANDO 
                var cmd = GetCommand("UPDATE Camere SET  Descrizione = @Descrizione ,  Tipologia = @Tipologia WHERE Numero = @Numero");


                cmd.Parameters.Add(new SqlParameter("@Numero", camera.Numero));
                cmd.Parameters.Add(new SqlParameter("@Descrizione", camera.Descrizione));
                cmd.Parameters.Add(new SqlParameter("@Tipologia", camera.Tipologia));

                //GESTIONE CONNESIONE
                var conn = GetConnection();

                //APRO CONNESIONE
                conn.Open();



                //ESEGUO COMANDO
                var result = cmd.ExecuteNonQuery();


                //CHIUSURA CONNESIONE
                conn.Close();

            }
            catch(Exception e )
            {
                throw e;
            }
           

        }
    }
}

