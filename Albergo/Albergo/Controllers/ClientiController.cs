using System;
using Albergo.Models;
using Albergo.Models.Camere;
using Albergo.Services.CLIENTI;
using Microsoft.AspNetCore.Mvc;

namespace Albergo.Controllers
{
	public class ClientiController : Controller
	{
        private readonly ILogger<ClientiController> _logger;
        private readonly IClientiService _clientiService;

        public ClientiController(ILogger<ClientiController> logger, IClientiService clientiService)
        {
            _logger = logger;
            _clientiService = clientiService;
        }

        [HttpGet]
        public IActionResult Clienti()
        {
            var clienti = _clientiService.GetClienti();
            return View(clienti);
        }


        [HttpGet]
        public IActionResult NewClienti()
        {
            return View(new Cliente());
        }

        // POST: Clienti/NewCliente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewClienti(Cliente model)
        {
            if (ModelState.IsValid)
            {
                _clientiService.NewCliente(model);
                return RedirectToAction("Clienti");
            }
            return View(model);
        }

        // GET: Clienti/Edit/5
        [HttpGet]
        public IActionResult Edit(int ID)
        {
            var cliente = _clientiService.GetCliente(ID);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clienti/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int ID, Cliente model)
        {
            if (ID != model.ID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _clientiService.UpdateCliente(ID, model);
                return RedirectToAction("Clienti");
            }
            return View(model);
        }

        // POST: Clienti/Delete/5
        [HttpDelete]
        public IActionResult Delete(int ID)
        {
            try
            {
                _clientiService.DeleteCliente(ID);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }
    }
}