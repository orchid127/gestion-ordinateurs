using GestionOrdinateurs.Shared;
using Microsoft.AspNetCore.Mvc;

namespace GestionOrdinateurs.API;

[ApiController]
[Route("api/[controller]")]
public class OrdinateursController : ControllerBase
{
    private readonly IOrdinateurService _ordinateurService;
    private readonly ILogger<OrdinateursController> _logger;
    private readonly IConfiguration _config;
    private readonly ParcInformatiqueContext _context;
    public OrdinateursController(IOrdinateurService service, ILogger<OrdinateursController> logger, IConfiguration configuration, ParcInformatiqueContext context)
    {
        _ordinateurService = service;
        _ordinateurService.OrdinateurAjoute += AfficherMessageAjoute;
        _ordinateurService.OrdinateurMisAJour += AfficherMessageMisAJour;
        _ordinateurService.OrdinateurSupprime += AfficherMessageSupprime;

        _logger = logger;
        _config = configuration;
        _context = context;
    }

    void AfficherMessageAjoute(object? sender, OrdinateurAjouteEventArgs e)
    {
        Console.WriteLine($"Un nouvel ordinateur a été ajouté : {e.Nom}");
    }

    void AfficherMessageMisAJour(object? sender, OrdinateurMisAJourArgs e)
    {
        Console.WriteLine($"Un ordinateur a été mis à jour : {e.Nom}");
    }

    void AfficherMessageSupprime(object? sender, OrdinateurSupprimeEventArgs e)
    {
        Console.WriteLine($"Un ordinateur a été supprimé : {e.Nom}");
    }

    [HttpGet]
    public IActionResult RetournerOrdinateurs()
    {
        List<Ordinateur> ordinateurs = _ordinateurService.ListerOrdinateurs();
        foreach (Ordinateur o in ordinateurs)
        {
            _logger.LogInformation("[Infos] ID : " + o.Id + " | Nom : " + o.Nom + " | Marque : "
            + o.Caracteristiques.Marque + " | Ram : " + o.Caracteristiques.RamGo + " | Etat : "
            + o.Etat + " | Stock : " + o.Stock + " | Prix : " + o.Prix + " €");
        }
        return Ok(ordinateurs);
    }

    [HttpGet("disponibles")]
    public IActionResult RetounerOrdinateursDisponibles()
    {
        List<Ordinateur> ordinateurs = _ordinateurService.ListerOrdinateurs();
        foreach (Ordinateur o in ordinateurs)
        {
            _logger.LogInformation("[Infos] ID : " + o.Id + " | Nom : " + o.Nom + " | Marque : "
            + o.Caracteristiques.Marque + " | Ram : " + o.Caracteristiques.RamGo + " | Etat : "
            + o.Etat + " | Stock : " + o.Stock + " | Prix : " + o.Prix + " €");
        }
        return Ok(ordinateurs);
    }

    [HttpGet("ordreprix")]
    public IActionResult RetournerOrdinateursParPrix()
    {
        List<Ordinateur> ordinateurs = _ordinateurService.ListerOrdinateursParPrix();
        foreach (Ordinateur o in ordinateurs)
        {
            _logger.LogInformation("[Infos] ID : " + o.Id + " | Nom : " + o.Nom + " | Marque : "
            + o.Caracteristiques.Marque + " | Ram : " + o.Caracteristiques.RamGo + " | Etat : "
            + o.Etat + " | Stock : " + o.Stock + " | Prix : " + o.Prix + " €");
        }
        return Ok(ordinateurs);
    }


    [HttpGet("{id}")]
    public IActionResult TrouverOrdinateurParId(int id)
    {
        Ordinateur? ordi = _ordinateurService.TrouverOrdinateurParId(id);

        if (ordi == null)
        {
            _logger.LogInformation("L'ordinateur d'ID " + id + " n'a pas été trouvé :((");
            return NotFound(); // renvoie not found si on ne trouve pas l'ordinateur
        }
        _logger.LogInformation("L'ordinateur d'ID " + id + " a été trouvé :)))");
        return Ok(ordi);
    }

    [HttpGet("{id}/diagnostic")]
    public IActionResult RenvoyerDiagnostic(int id)
    {
        Ordinateur? ordi = _ordinateurService.TrouverOrdinateurParId(id);

        if (ordi == null)
        {
            _logger.LogInformation("L'ordinateur d'ID " + id + " n'a pas été trouvé :((");
            return NotFound();
        }
        string diagnostic = _ordinateurService.Diagnostiquer(ordi);
        return Ok(diagnostic);
    }

    [HttpGet("statistiques")]
    public IActionResult RenvoyerStatistiques()
    {
        var stats = _ordinateurService.CalculerStatistiques();
        return Ok(stats);
    }

    [HttpGet("marque/{marque}")]
    public IActionResult TrouverOrdinateurParMarque(string marque)
    {
        List<Ordinateur> ordis = _ordinateurService.TrouverOrdinateurParMarque(marque);

        if (ordis.Count == 0)
        {
            _logger.LogInformation("Il n'y a aucun ordinateur de la marque " + marque + " :((");
        }
        else
        {
            _logger.LogInformation("Il y a " + ordis.Count + " ordinateur(s) de la marque " + marque + " !");
        }
        return Ok(ordis);
    }

    [HttpGet("inventaire/nom/{nom}")]
    public IActionResult ChercherParNom(string nom)
    {
        var inventaire = new InventaireOrdinateurs();
        inventaire.Ordinateurs = _context.Ordinateurs.ToList();

        var ordinateur = inventaire[nom]; // Exemple de recherche par nom

        return Ok(ordinateur);
    }

    [HttpGet("inventaire/id/{id}")]
    public IActionResult ChercherParId(int id)
    {
        var inventaire = new InventaireOrdinateurs();
        inventaire.Ordinateurs = _context.Ordinateurs.ToList();

        var ordinateur = inventaire[id];

        return Ok(ordinateur);
    }

    [HttpGet("info")]
    public IActionResult RetournerInfosConfig()
    {
        string? nom = _config["Application:Name"];
        string? version = _config["Application:Version"];
        _logger.LogInformation("[Infos config] Nom : " + nom + " | Version : " + version);

        return Ok(new { nom, version });
    }

    [HttpPost]
    public IActionResult CreerOrdinateur(Ordinateur newOrdi)
    {
        Ordinateur ordi = _ordinateurService.CreerOrdinateur(newOrdi.Nom, newOrdi.Stock, newOrdi.Prix, newOrdi.Caracteristiques.RamGo, newOrdi.Caracteristiques.StockageGo, newOrdi.Caracteristiques.Marque);
        _logger.LogInformation("L'ordinateur a bien été créé :D");
        return CreatedAtAction(nameof(TrouverOrdinateurParId), new { id = ordi.Id }, ordi); // confirme que l'ordi a bien été créé et renvoie le contenu de l'ordi
    }

    [HttpPut("{id}")]
    public IActionResult MettreAJourOrdinateur(int id, Ordinateur newOrdi)
    {
        Ordinateur? ordi = _ordinateurService.MettreAJourOrdinateur(id, newOrdi);

        if (ordi == null)
        {
            _logger.LogInformation("L'ordinateur n'a pas pu être mis à jour :((( (aucun ordinateur n'a l'ID " + id + ")");
            return NotFound();
        }

        _logger.LogInformation("L'ordinateur d'ID " + id + " a bien été mis à jour :D");
        return Ok(ordi);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteComputer(int id)
    {
        Ordinateur? ordi = _ordinateurService.SupprimerOrdinateur(id);

        if (ordi == null)
        {
            _logger.LogInformation("L'ordinateur n'a pas pu être supprimé :((( (aucun ordinateur n'a l'ID " + id + ")");
            return NotFound();
        }

        _logger.LogInformation("L'ordinateur d'ID" + id + " a bien été supprimé :D");
        return NoContent();
    }
}