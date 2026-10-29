namespace GestionOrdinateurs.Shared;

using System.ComponentModel.DataAnnotations;

public class User
{
    public enum RoleEnum
    {
        Admin,
        Gestionnaire,
        Visiteur
    }

    public int Id { get; set; }

    [Required]
    public string Login { get; set; } = string.Empty;

    public RoleEnum Role { get; set; }
}
