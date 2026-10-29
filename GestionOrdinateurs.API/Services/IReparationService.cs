using GestionOrdinateurs.Shared;
using static GestionOrdinateurs.API.ReparationService;

namespace GestionOrdinateurs.API;

public interface IReparationService
{
    public Task SimulerReparationAsync(Ordinateur ordinateur);

    public Task<List<Reparation>> ListerReparations();

    public Task<List<HistoriqueReparationRecord>> ListerHistorique();
}