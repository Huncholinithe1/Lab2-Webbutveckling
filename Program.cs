using Microsoft.EntityFrameworkCore;
using ReviewApi.Data;
using ReviewApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Lägg till tjänster i containern
// Swagger/OpenAPI konfiguration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "Recensioner API", 
        Description = "API för hantering av recensioner",
        Version = "v1" 
    });
});

// Konfigurera CORS för att tillåta anrop från frontend
var corsPolicy = "AllowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
        policy =>
        {
            policy.WithOrigins("https://review-express-frontend-yusuf-aef7epgbbjheh8g0.swedencentral-01.azurewebsites.net")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Lägg till databaskontexten
builder.Services.AddDbContext<ReviewDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

// Aktivera Swagger även i produktion för denna uppgift
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Recensioner API V1");
    c.RoutePrefix = string.Empty; // Gör Swagger UI tillgänglig på rot-URL:en
});

// Använd HTTPS-omdirigering och CORS
app.UseHttpsRedirection();
app.UseCors(corsPolicy);

// CRUD Endpoints för recensioner
// GET: Hämta alla recensioner
app.MapGet("/recensioner", async (ReviewDbContext db) =>
    await db.Recensioner.ToListAsync())
    .WithName("HämtaAllaRecensioner")
    .WithDescription("Hämtar alla recensioner från databasen");

// GET: Hämta en specifik recension med ID
app.MapGet("/recensioner/{id}", async (int id, ReviewDbContext db) =>
    await db.Recensioner.FindAsync(id)
        is Recension recension
        ? Results.Ok(recension)
        : Results.NotFound())
    .WithName("HämtaRecension")
    .WithDescription("Hämtar en specifik recension baserat på ID");

// POST: Skapa en ny recension
app.MapPost("/recensioner", async (Recension recension, ReviewDbContext db) =>
{
    db.Recensioner.Add(recension);
    await db.SaveChangesAsync();
    return Results.Created($"/recensioner/{recension.Id}", recension);
})
.WithName("SkapaRecension")
.WithDescription("Skapar en ny recension");

// PUT: Uppdatera en befintlig recension
app.MapPut("/recensioner/{id}", async (int id, Recension inputRecension, ReviewDbContext db) =>
{
    var recension = await db.Recensioner.FindAsync(id);
    if (recension is null) return Results.NotFound();

    recension.Titel = inputRecension.Titel;
    recension.Innehall = inputRecension.Innehall;
    recension.Betyg = inputRecension.Betyg;
    recension.Forfattare = inputRecension.Forfattare;
    recension.ProduktNamn = inputRecension.ProduktNamn;

    await db.SaveChangesAsync();
    return Results.NoContent();
})
.WithName("UppdateraRecension")
.WithDescription("Uppdaterar en befintlig recension");

// DELETE: Ta bort en recension
app.MapDelete("/recensioner/{id}", async (int id, ReviewDbContext db) =>
{
    if (await db.Recensioner.FindAsync(id) is Recension recension)
    {
        db.Recensioner.Remove(recension);
        await db.SaveChangesAsync();
        return Results.Ok(recension);
    }
    return Results.NotFound();
})
.WithName("TaBortRecension")
.WithDescription("Tar bort en recension baserat på ID");

app.Run();
