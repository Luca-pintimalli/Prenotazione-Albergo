using System;
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
        public IActionResult NewPrenotazione()
        {
            var camere = _camereService.GetCamere();
            var clienti = _clientiService.GetClienti();

            var model = new PrenotazioneForm
            {
                Prenotazione = new Prenotazione(),
                Camere = camere ?? new List<Camera>(),
                Clienti = clienti ?? new List<Cliente>()
            };

            return View(model);
        }

        // POST: /Prenotazione/Create
        [HttpPost]
        public IActionResult Create(PrenotazioneForm model)
        {
            if (ModelState.IsValid)
            {
                _prenotazioniService.newPrenotazione(model.Prenotazione);
                return RedirectToAction("Prenotazioni"); 
            }

            // ricarico le liste se il modello non è valido
            model.Camere = _camereService.GetCamere();
            model.Clienti = _clientiService.GetClienti();

            return View("NewPrenotazione", model);
        }


       
    }
}
