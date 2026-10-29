using GestionOrdinateurs.Shared;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GestionOrdinateurs.API;

public class OrdinateurService : IOrdinateurService
{
    private readonly ParcInformatiqueContext _context;
    public event EventHandler<OrdinateurAjouteEventArgs>? OrdinateurAjoute;
    public event EventHandler<OrdinateurMisAJourArgs>? OrdinateurMisAJour;
    public event EventHandler<OrdinateurSupprimeEventArgs>? OrdinateurSupprime;

    public OrdinateurService(ParcInformatiqueContext context)
    {
        _context = context;
    }

    public Ordinateur CreerOrdinateur(string nom, int stock, double prix, int ramGo, int stockageGo, string marque)
    {
        Caracteristiques caracteristiques = new Caracteristiques(ramGo, stockageGo, marque, Caracteristiques.TypeOrdinateur.PORTABLE);
        Ordinateur ordi = new Ordinateur { Nom = nom, Etat = Ordinateur.EtatOrdinateur.EnService, Stock = stock, Prix = prix, Caracteristiques = caracteristiques };
        _context.Ordinateurs.Add(ordi);
        _context.SaveChanges();

        OrdinateurAjoute?.Invoke(this,
        new OrdinateurAjouteEventArgs
        {
            Nom = ordi.Nom
        });

        return ordi;
    }

    public Ordinateur? SupprimerOrdinateur(int id)
    {
        Ordinateur? ordi = _context.Ordinateurs.Find(id);

        if (ordi != null)
        {
            _context.Ordinateurs.Remove(ordi);
            _context.SaveChanges();

            OrdinateurSupprime?.Invoke(this, new OrdinateurSupprimeEventArgs
            {
                Nom = ordi.Nom
            });
        }

        return ordi;
    }

    public Ordinateur? MettreAJourOrdinateur(int id, Ordinateur newOrdi)
    {
        Ordinateur? ordi = _context.Ordinateurs.Find(id);

        if (ordi != null)
        {
            ordi.Nom = newOrdi.Nom;
            ordi.Etat = newOrdi.Etat;
            ordi.Stock = newOrdi.Stock;
            ordi.Prix = newOrdi.Prix;
            ordi.Caracteristiques = newOrdi.Caracteristiques;
            ordi.Reparations = newOrdi.Reparations;

            _context.SaveChanges();

            OrdinateurMisAJour?.Invoke(this, new OrdinateurMisAJourArgs
            {
                Nom = ordi.Nom
            });
        }

        return ordi;
    }

    public List<Ordinateur> ListerOrdinateurs()
    {
        return _context.Ordinateurs.ToList();
    }

    public Ordinateur? TrouverOrdinateurParId(int id)
    {
        Ordinateur? ordi = _context.Ordinateurs.Find(id);
        return ordi;
    }

    public List<Ordinateur> TrouverOrdinateurParMarque(string marque)
    {
        List<Ordinateur> lst = _context.Ordinateurs.Where(o => o.Caracteristiques.Marque == marque).ToList();

        return lst;
    }

    public List<Ordinateur> ListerOrdinateursDisponibles()
    {
        return _context.Ordinateurs.Where(o => o.Stock > 0).ToList();
    }

    public List<Ordinateur> ListerOrdinateursParPrix()
    {
        return _context.Ordinateurs.OrderBy(o => o.Prix).ToList();
    }

    public string Diagnostiquer(Ordinateur ordinateur)
    {
        return ordinateur switch
        {
            {
                Etat: Ordinateur.EtatOrdinateur.HorsService
            }
                => "Remplacement",

            {
                Etat: Ordinateur.EtatOrdinateur.Lent,
                Caracteristiques:
                {
                    RamGo: < 8
                }
            }
                => "Ajouter RAM",

            _
                => "Aucune action"
        };
    }

    public (int TotalOrdinateur, int OrdiDisponibles, decimal ValeurStock, int NbReparations) CalculerStatistiques()
    {
        List<Ordinateur> ordis = ListerOrdinateurs();

        int totalOrdinateur = ordis.Count;
        int ordiDisponibles = ordis.Count;
        decimal valeurStock = 0;
        int nbReparations = 0;

        string diagnostic; // on regarde le diagnostic pour déterminer si l'ordinateur est disponible, hors service ou en réparation

        foreach (Ordinateur o in ordis)
        {
            valeurStock += o.Stock; // quantité de stock d'ordinateur disponible
            diagnostic = Diagnostiquer(o);

            if (diagnostic == "Remplacement")
            {
                ordiDisponibles--;
            }

            if (diagnostic == "Ajouter RAM") // si un ordinateur doit se faire réparéer, il n'est plus disponible non plus
            {
                nbReparations++;
                ordiDisponibles--;
            }
        }

        return (totalOrdinateur, ordiDisponibles, valeurStock, nbReparations);
    }
}