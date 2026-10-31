namespace GestionOrdinateurs.API;

using GestionOrdinateurs.Shared;
using Microsoft.EntityFrameworkCore;

public class ReparationService : IReparationService
{
    private readonly ParcInformatiqueContext _context;

    public ReparationService(ParcInformatiqueContext context)
    {
        _context = context;
    }

    public async Task SimulerReparationAsync(Ordinateur ordinateur)
    {
        // Création object reparation (test)
        // verifie que l'ordinateur est en base
        Ordinateur? ordi = _context.Ordinateurs.Find(ordinateur.Id);
        if (ordi == null) return;

        //Méthode de création d'une réparation en base. (à créér par vous)
        Reparation test = new Reparation
        {
            UnOrdinateur = ordi,
            TypeReparation = "Changement batteries",
            DateDebut = DateTime.Now,
            Statut = "En cours",
            Technicien = "Kimberley"
        };
        var newReparation = await _context.Reparations.AddAsync(test);
        await _context.SaveChangesAsync();

        await Task.Delay(2000);
        newReparation.Entity.Statut = "Terminée"; // chnage le statut de la réparation en base
        await _context.SaveChangesAsync();
        Console.WriteLine($"{ordinateur.Nom} réparé");
    }

    public async Task<List<Reparation>> ListerReparations()
    {
        return await _context.Reparations.ToListAsync(); //on récupère les informations de l'ordinateur réparé en même temps
    }

    public async Task<List<HistoriqueReparationRecord>> ListerHistorique()
    {
        return await _context.Reparations
        .Where(repar => repar.UnOrdinateur != null) // verifie qu'il y a bien un ordinateur réparé
        .Select(repar => new HistoriqueReparationRecord(
            repar.UnOrdinateur!.Id,
            repar.UnOrdinateur.Nom,
            repar.Technicien ?? "Pas de technicien", // si le technicien n'est pas précisié, on considère qu'il y en n'a pas
            repar.TypeReparation ?? "Autre", // si le type n'est pas précisé, on la catégorise dans "Autre"
            repar.DateDebut ?? default
        ))
        .ToListAsync();
    }
}