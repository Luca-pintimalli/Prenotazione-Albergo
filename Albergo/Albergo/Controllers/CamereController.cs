using System;
using Albergo.Models;
using Albergo.Models.Camere;
using Albergo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Albergo.Controllers
{
    [Authorize]

    public class CamereController : Controller
    {
        private readonly ILogger<CamereController> _logger;
        private readonly ICamereService _camereService;

        public CamereController(ILogger<CamereController> logger, ICamereService camereService)
        {
            _logger = logger;
            _camereService = camereService;
        }

        //Recupero Camere
        [HttpGet]
        public IActionResult Camere()
        {
            var camere = _camereService.GetCamere();
            return View(camere);
        }


        [HttpGet]
        public IActionResult NewCamera()
        {
            return View(new Camera());
        }

        [HttpPost]
        public IActionResult NewCamera(Camera model)
        {
            if (ModelState.IsValid)
            {
                _camereService.newCamera(model);
                return RedirectToAction("Camere");
            }
            return View(model);
        }
       
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var camera = _camereService.GetCamera(id);
            if (camera == null)
            {
                return NotFound();
            }
            return View(camera);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Camera model)
        {
            if (id != model.Numero)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _camereService.UpdateCamera(id, model);
                return RedirectToAction("Camere");
            }
            return View(model);
        }

        
        [HttpDelete]
        public JsonResult DeleteCamera(int id)
        {
            try
            {
                _camereService.DeleteCamera(id);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }
    }
}

