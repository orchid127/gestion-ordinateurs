using GestionOrdinateurs.API;
using GestionOrdinateurs.Shared;
using Microsoft.EntityFrameworkCore;
using static GestionOrdinateurs.Shared.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IOrdinateurService, OrdinateurService>();
builder.Services.AddScoped<IReparationService, ReparationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<StockService>();
builder.Services.AddDbContext<ParcInformatiqueContext>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//Initialisation de la base de données
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ParcInformatiqueContext>();

    db.Database.Migrate();

    if (!db.Users.Any())
    {
        db.Users.AddRange(
            new User { Login = "user1", Role = RoleEnum.Admin },
            new User { Login = "user2", Role = RoleEnum.Gestionnaire },
            new User { Login = "user3", Role = RoleEnum.Gestionnaire }
        );

        db.SaveChanges();
    }
}

app.MapControllers();

app.Run();

