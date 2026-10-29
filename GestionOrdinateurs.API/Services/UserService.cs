using GestionOrdinateurs.Shared;

namespace GestionOrdinateurs.API;

public class UserService : IUserService
{
    private readonly ParcInformatiqueContext _context;

    public UserService(ParcInformatiqueContext context)
    {
        _context = context;
    }

    public User? ChercherUtilisateurParLogin(string login)
    {
        return _context.Users.First(user => user.Login == login);
    }

    public List<User> ListerUtilisateurs()
    {
        return _context.Users.ToList();
    }


}