using System;
using Albergo.Models;

namespace Albergo.Services.Auth
{
	public interface IAuthService
	{
		Dipendenti Login(string NomeUtente, string Password);
	}
}

