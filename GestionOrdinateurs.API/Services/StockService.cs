namespace GestionOrdinateurs.API;

public class StockService
{
    public int StockSSD { set; get; } = 10;
    private readonly object objet = new object();


    public async void ConsommerSSD()
    {
        Task.Delay(10).Wait();
        lock (objet)
        {
            StockSSD--;
        }
    }
}