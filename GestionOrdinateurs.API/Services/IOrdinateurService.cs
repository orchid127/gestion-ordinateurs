using GestionOrdinateurs.Shared;

namespace GestionOrdinateurs.API;

public interface IOrdinateurService
{
    public event EventHandler<OrdinateurAjouteEventArgs> OrdinateurAjoute;
    public event EventHandler<OrdinateurMisAJourArgs> OrdinateurMisAJour;
    public event EventHandler<OrdinateurSupprimeEventArgs> OrdinateurSupprime;
    public Ordinateur CreerOrdinateur(string nom, int stock, double prix, int ramGo, int stockageGo, string marque);

    public Ordinateur? SupprimerOrdinateur(int id);

    public Ordinateur? MettreAJourOrdinateur(int id, Ordinateur newOrdi);

    public List<Ordinateur> ListerOrdinateurs();

    public Ordinateur? TrouverOrdinateurParId(int id);

    public List<Ordinateur> TrouverOrdinateurParMarque(string marque);

    public List<Ordinateur> ListerOrdinateursDisponibles();

    public List<Ordinateur> ListerOrdinateursParPrix();

    public string Diagnostiquer(Ordinateur ordinateur);

    public (int TotalOrdinateur, int OrdiDisponibles, decimal ValeurStock, int NbReparations) CalculerStatistiques();

}