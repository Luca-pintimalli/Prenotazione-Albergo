using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Albergo.Models;
using Albergo.Services.Auth;

namespace Albergo.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService authService;

        public AccountController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Dipendenti dipendenti)
        {
            if (!ModelState.IsValid)
            {
                return View(dipendenti);
            }

            try
            {
                var u = authService.Login(dipendenti.NomeUtente, dipendenti.Password);
                if (u == null)
                {
                    ModelState.AddModelError(string.Empty, "Nome utente o password errati");
                    return View(dipendenti);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, u.NomeUtente),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Gestisci l'eccezione in modo appropriato
                ModelState.AddModelError(string.Empty, "Errore durante il login");
                return View(dipendenti);
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
