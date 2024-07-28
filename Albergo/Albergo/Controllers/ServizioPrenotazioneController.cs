using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Albergo.Models;
using Albergo.Services.SERVIZI;
using Albergo.Services;
using Albergo.Services.Servizi;
using Microsoft.AspNetCore.Authorization;

namespace Albergo.Controllers
{

    [Authorize]
    public class ServizioPrenotazioneController : Controller
    {
        private readonly IServiziPrenotazioneService _servizioPrenotazioneService;
        private readonly IPrenotazioniService _prenotazioniService;
        private readonly IServiziService _serviziService;

        public ServizioPrenotazioneController(IServiziPrenotazioneService servizioPrenotazioneService, IPrenotazioniService prenotazioniService, IServiziService serviziService)
        {
            _servizioPrenotazioneService = servizioPrenotazioneService ?? throw new ArgumentNullException(nameof(servizioPrenotazioneService));
            _prenotazioniService = prenotazioniService ?? throw new ArgumentNullException(nameof(prenotazioniService));
            _serviziService = serviziService ?? throw new ArgumentNullException(nameof(serviziService)); 

        }

        public IActionResult ServiziPrenotazione()
        {
            var serviziPrenotazioni = _servizioPrenotazioneService.GetServiziPrenotazione();
            return View(serviziPrenotazioni);
        }


        [HttpGet]
        public IActionResult NewServizioPrenotazione()
        {
            var model = new ServizioPrenotazioneForm
            {
                Prenotazioni = _prenotazioniService.GetPrenotazioni().ToList(),
                Servizi = _serviziService.GetServizi().ToList(),
                ServizioPrenotazione = new ServizioPrenotazione()
            };
           return View(model);
        }

        [HttpPost]
        public IActionResult NewServizioPrenotazione(ServizioPrenotazioneForm model)
        {
            if (ModelState.IsValid)
            {
                _servizioPrenotazioneService.NewServizioPrenotazione(model.ServizioPrenotazione);
                return RedirectToAction(nameof(ServiziPrenotazione));
            }
            model.Prenotazioni = _prenotazioniService.GetPrenotazioni().ToList();
            model.Servizi = _serviziService.GetServizi().ToList();
            return View(model);
        }



        [HttpGet]
    public IActionResult Edit(int id)
        {
            var servizioPrenotazione = _servizioPrenotazioneService.GetServizioPrenotazione(id);
            if (servizioPrenotazione == null)
            {
                return NotFound();
            }

          
            var model = new ServizioPrenotazioneForm
            {
                ServizioPrenotazione = servizioPrenotazione,
               Prenotazioni=_prenotazioniService.GetPrenotazioni().ToList(),
               Servizi=_serviziService.GetServizi().ToList()
            };

            return View("Edit", model);
        }

        [HttpPost]
        public IActionResult Edit(int id, ServizioPrenotazioneForm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _servizioPrenotazioneService.UpdateServizioPrenotazione(id, model.ServizioPrenotazione);
                    return RedirectToAction(nameof(ServizioPrenotazione));
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Errore durante l'aggiornamento della prenotazione: " + ex.Message);
                }
                
            }
            // Ricarica le liste nel caso di un errore di validazione
            model.Prenotazioni = _prenotazioniService.GetPrenotazioni().ToList();
            return View("Edit", model);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _servizioPrenotazioneService.DeleteServizioPrenotazione(id);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }
    }
}
