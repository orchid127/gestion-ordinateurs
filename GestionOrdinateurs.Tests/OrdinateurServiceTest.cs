using GestionOrdinateurs.Shared;
using GestionOrdinateurs.API;
using Microsoft.EntityFrameworkCore;

namespace GestionOrdinateurs.Tests;

public class GestionOrdinateursTest
{
    private ParcInformatiqueContext CreerContexte()
    {
        var options =
            new DbContextOptionsBuilder<ParcInformatiqueContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        return new ParcInformatiqueContext(options);
    }

    [Fact]
    public void CreerOrdinateur_DoitCreerUnOrdinateur()
    {
        // Arrange
        ParcInformatiqueContext context = CreerContexte();
        OrdinateurService ordinateurService = new OrdinateurService(context);

        // Act
        Ordinateur ordinateur = ordinateurService.CreerOrdinateur(
            "PC de Ronan",
            1,
            1200.0,
            32,
            500,
            "ASUS"
        );

        // Assert
        Assert.NotNull(ordinateur);
        Assert.Contains(context.Ordinateurs, o => o.Id == ordinateur.Id); // le nouveau ordinateur est bien ajouté à la liste
    }

    [Fact]
    public void SupprimerOrdinateur_DoitSupprimerOrdinateur()
    {
        // Arrange
        ParcInformatiqueContext context = CreerContexte();
        OrdinateurService ordinateurService = new OrdinateurService(context);


        // Act
        Ordinateur ordinateurAjoute = ordinateurService.CreerOrdinateur(
            "PC de Ronan",
            1,
            1200.0,
            32,
            500,
            "ASUS"
        );
        Ordinateur? ordinateurSupprime = ordinateurService.SupprimerOrdinateur(ordinateurAjoute.Id);

        // Assert
        Assert.NotNull(ordinateurSupprime);
        Assert.DoesNotContain(context.Ordinateurs, o => o.Id == ordinateurAjoute.Id);
    }
}
