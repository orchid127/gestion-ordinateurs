using GestionOrdinateurs.Shared;
using static GestionOrdinateurs.API.ReparationService;

namespace GestionOrdinateurs.API;

public interface IUserService
{

    public User? ChercherUtilisateurParLogin(string login);
    public List<User> ListerUtilisateurs();
}