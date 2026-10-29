namespace GestionOrdinateurs.Shared;

public class Caracteristiques
{

    public enum TypeOrdinateur
    {
        FIXE,
        PORTABLE,
        SERVER
    }
    public int RamGo { get; set; }
    public int StockageGo { get; set; }
    public string Marque { get; set; } = "";
    public TypeOrdinateur TypeOrdi { get; set; }

    public Caracteristiques(int ramGo, int stockageGo, string marque, TypeOrdinateur typeOrdi)
    {
        RamGo = ramGo;
        StockageGo = stockageGo;
        Marque = marque;
        TypeOrdi = typeOrdi;
    }
}