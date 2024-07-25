using System;
using Albergo.Models;
using Albergo.Services.CLIENTI;
using Albergo.Services.Servizi;
using Microsoft.AspNetCore.Mvc;

namespace Albergo.Controllers
{
	public class ServiziController : Controller
	{
        private readonly ILogger<ServiziController> _logger;
        private readonly IServiziService _serviziService;
        public ServiziController(ILogger<ServiziController> logger, IServiziService serviziService)
        {
            _logger = logger;
            _serviziService = serviziService;
        }


        [HttpGet]
        public IActionResult Servizi()
        {
            var servizi = _serviziService.GetServizi();
            return View(servizi);
        }


        [HttpGet]
        public IActionResult NewServizio()
        {
            return View(new Servizio());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewServizio(Servizio model)
        {
            if (ModelState.IsValid)
            {
                _serviziService.NewServizio(model);
                return RedirectToAction("Servizi");
            }
            return View(model);
        }



        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var servizio = _serviziService.GetServizio(Id);
            if (servizio == null)
            {
                return NotFound();
            }
            return View(servizio);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int ID, Servizio model)
        {
            if (ID != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _serviziService.UpdateServizio(ID, model);
                return RedirectToAction("Servizi");
            }
            return View(model);
        }




        [HttpDelete]
        public IActionResult Delete(int ID)
        {
            try
            {
                _serviziService.DeleteServizio(ID);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }




    }
}

