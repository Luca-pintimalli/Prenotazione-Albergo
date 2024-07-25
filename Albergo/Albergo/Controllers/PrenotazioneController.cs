using System;
using System.Linq;
using Albergo.Models;
using Albergo.Models.Camere;
using Albergo.Services;
using Albergo.Services.CLIENTI;
using Microsoft.AspNetCore.Mvc;

namespace Albergo.Controllers
{
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
    }
}