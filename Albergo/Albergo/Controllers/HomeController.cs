using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Albergo.Models;
using Albergo.Services;
using Microsoft.AspNetCore.Authorization;

namespace Albergo.Controllers;

public class HomeController : Controller
{
    private readonly PrenotazioniService _prenotazioniService;

    public HomeController(PrenotazioniService prenotazioniService)
    {
        _prenotazioniService = prenotazioniService;
    }

    public IActionResult Index()
    {
        return View();
    }



    //numero totale prenotazioni 
    [Authorize]
    [HttpGet]
    public IActionResult GetPrenotazioniCounts()
    {
        var prenotazioni = _prenotazioniService.GetPrenotazioni();

        var totalPrenotazioni = prenotazioni.Count();
        var mezzaPensioneCount = prenotazioni.Count(p => p.Dettagli == "Mezza Pensione");
        var pensioneCompletaCount = prenotazioni.Count(p => p.Dettagli == "Pensione Completa");
        var pernottamentoColazioneCount = prenotazioni.Count(p => p.Dettagli == "Pernottamento con Colazione");

        var counts = new
        {
            TotalPrenotazioni = totalPrenotazioni,
            MezzaPensioneCount = mezzaPensioneCount,
            PensioneCompletaCount = pensioneCompletaCount,
            PernottamentoColazioneCount = pernottamentoColazioneCount
        };

        return Json(counts);
    }


public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

