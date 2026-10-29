namespace GestionOrdinateurs.API;

using GestionOrdinateurs.Shared;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<OrdinateursController> _logger;
    private readonly IConfiguration _config;
    private readonly ParcInformatiqueContext _context;

    public UsersController(IUserService service, ILogger<OrdinateursController> logger, IConfiguration configuration, ParcInformatiqueContext context)
    {
        _userService = service;
        _logger = logger;
        _config = configuration;
        _context = context;
    }

    [HttpGet]
    public IActionResult RecupererUtlisateurs()
    {
        List<User> users = _userService.ListerUtilisateurs();
        return Ok(users);
    }

    [HttpGet("/login")]
    public IActionResult RetournerUtilisateurParLogin(string login)
    {
        User? user = _userService.ChercherUtilisateurParLogin(login);
        return Ok(user);
    }
}