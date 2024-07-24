using System;
namespace Albergo.Models.Camere
{
	public interface ICamereService
	{
		void newCamera(Camera camera);


		//INSERIMENTO DI TUTTE LE CAMERE
		IEnumerable<Camera> GetCamere();

		//recupero di una singola camera
		Camera GetCamera(int Numero);

		//MODIFICA DELLA CAMERA
		void UpdateCamera(int Numero, Camera camera);

		//Elimina camera
		void DeleteCamera(int Numero);

	}
}

