namespace GestionOrdinateurs.API;

using GestionOrdinateurs.Shared;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReparationController : ControllerBase
{
    private readonly IReparationService _reparService;
    private readonly StockService _stockService;
    private readonly ILogger<ReparationController> _logger;


    public ReparationController(IReparationService reparService, ILogger<ReparationController> logger, StockService stock)
    {
        _reparService = reparService;
        _logger = logger;
        _stockService = stock;
    }

    private void AfficherReparation(Reparation repar)
    {
        _logger.LogInformation($"Réparation n° {repar.Id} - ");
        _logger.LogInformation($"Infos ordinateur || Nom : {repar.UnOrdinateur?.Nom} || Marque : {repar.UnOrdinateur?.Caracteristiques.Marque}");
    }

    [HttpGet]
    public async Task<IActionResult> RetournerListeReparation()
    {
        var listeReparation = await _reparService.ListerReparations();
        listeReparation.ForEach(AfficherReparation);

        return Ok(listeReparation);
    }

    [HttpGet("getstockssd")]
    public async Task RetournerLogSSD()
    {
        Parallel.For(0, 10, i => _stockService.ConsommerSSD());
        _logger.LogInformation($"Le stock de SSD s'élève actuellement à {_stockService.StockSSD}");
        // le résultat n'est pas cohérent, il passe soudainement d'une valeur aléatoire entre 10 et 0, puis à 0
        // après avoir utilisé un verrou, le résultat passe directement à 0
    }

    [HttpGet("historique")]
    public async Task<IActionResult> RenvoyerHistorique()
    {
        var historique = await _reparService.ListerHistorique();
        return Ok(historique);
    }

    [HttpPost]
    public async Task<IActionResult> SimulerReparation(List<Ordinateur> listeOrdinateurs)
    {
        //Récupération de la liste d'ordinateurs.
        await Task.WhenAll(
            _reparService.SimulerReparationAsync(listeOrdinateurs[0]),
            _reparService.SimulerReparationAsync(listeOrdinateurs[1]),
            _reparService.SimulerReparationAsync(listeOrdinateurs[2]));

        return Created(); // une fois que les tâches sont toutes terminées, on renvoie la confirmation que les réparations ont été faites
    }


}