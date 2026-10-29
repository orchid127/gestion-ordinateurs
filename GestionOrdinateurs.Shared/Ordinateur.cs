namespace GestionOrdinateurs.Shared;

public class Ordinateur
{
    // enum pour les différents états de l'ordinateur
    public enum EtatOrdinateur
    {
        EnService,
        EnPanne,
        EnMaintenance,
        HorsService,
        Lent,
        Reparable
    }

    // attributs
    public int Id { get; init; }
    public string Nom { get; set; } = "";
    public EtatOrdinateur Etat { get; set; }
    public int Stock { get; set; } = 0;
    public double Prix { get; set; } = 0.0;

    public Caracteristiques Caracteristiques { get; set; }

    public List<Reparation> Reparations { get; set; } = new();
}