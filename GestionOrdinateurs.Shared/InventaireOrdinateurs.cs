namespace GestionOrdinateurs.Shared;

public class InventaireOrdinateurs
{
    public List<Ordinateur> Ordinateurs { get; set; } = new();

    public Ordinateur this[string nom]
    {
        get { return Ordinateurs.First(o => o.Nom == nom); }
    }

    public Ordinateur this[int id]
    {
        get { return Ordinateurs.First(o => o.Id == id); }
    }
}