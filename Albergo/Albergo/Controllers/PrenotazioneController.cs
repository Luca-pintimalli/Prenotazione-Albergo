using System;
using System.Linq;
using Albergo.Models;
using Albergo.Models.Camere;
using Albergo.Services;
using Albergo.Services.CLIENTI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Albergo.Controllers
{
    [Authorize]

    public class PrenotazioneController : Controller
    {
        private readonly IPrenotazioniService _prenotazioniService;
        private readonly ICamereService _camereService;
        private readonly IClientiService _clientiService;

        public PrenotazioneController(IPrenotazioniService prenotazioniService, ICamereService camereService, IClientiService clientiService)
        {
            _prenotazioniService = prenotazioniService ?? throw new ArgumentNullException(nameof(prenotazioniService));
            _camereService = camereService ?? throw new ArgumentNullException(nameof(camereService));
            _clientiService = clientiService ?? throw new ArgumentNullException(nameof(clientiService));
        }

        public IActionResult Prenotazioni()
        {
            var prenotazioni = _prenotazioniService.GetPrenotazioni();
            return View(prenotazioni);
        }

        // GET: /Prenotazione/NewPrenotazione
        [HttpGet]
        public IActionResult NewPrenotazione()
        {
            var model = new PrenotazioneForm
            {
                Clienti = _clientiService.GetClienti().ToList(),
                Camere = _camereService.GetCamere().ToList(),
                Prenotazione = new Prenotazione()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult NewPrenotazione(PrenotazioneForm model)
        {
            if (ModelState.IsValid)
            {
                _prenotazioniService.newPrenotazione(model.Prenotazione);
                return RedirectToAction(nameof(Prenotazioni));
            }

            // Ricarica le liste nel caso di un errore di validazione
            model.Clienti = _clientiService.GetClienti().ToList();
            model.Camere = _camereService.GetCamere().ToList();
            return View(model);
        }

        [HttpDelete]
        public IActionResult DeletePrenotazione(int id)
        {
            try
            {
                _prenotazioniService.DeletePrenotazione(id);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public IActionResult EditPrenotazione(int id)
        {
            var prenotazione = _prenotazioniService.GetPrenotazione(id);
            if (prenotazione == null)
            {
                return NotFound();
            }

            var model = new PrenotazioneForm
            {
                Prenotazione = prenotazione,
                Clienti = _clientiService.GetClienti().ToList(),
                Camere = _camereService.GetCamere().ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditPrenotazione(int id,PrenotazioneForm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _prenotazioniService.UpdatePrenotazione(id, model.Prenotazione);
                    return RedirectToAction(nameof(Prenotazioni));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Errore durante l'aggiornamento della prenotazione: " + ex.Message);
                }
            }

            model.Clienti = _clientiService.GetClienti().ToList();
            model.Camere = _camereService.GetCamere().ToList();
            return View(model);
        }
    }
}
    
