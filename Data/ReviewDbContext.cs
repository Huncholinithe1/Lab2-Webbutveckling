using Microsoft.EntityFrameworkCore;
using ReviewApi.Models;

namespace ReviewApi.Data;

/// <summary>
/// Databaskontexten för recensioner
/// </summary>
public class ReviewDbContext : DbContext
{
    public ReviewDbContext(DbContextOptions<ReviewDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Collection av recensioner i databasen
    /// </summary>
    public DbSet<Recension> Recensioner { get; set; } = null!;
} 