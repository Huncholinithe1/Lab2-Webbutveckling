namespace ReviewApi.Models;

/// <summary>
/// Representerar en recension i systemet
/// </summary>
public class Recension
{
    /// <summary>
    /// Unikt ID för recensionen
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Titeln på recensionen
    /// </summary>
    public string Titel { get; set; } = string.Empty;

    /// <summary>
    /// Innehållet i recensionen
    /// </summary>
    public string Innehall { get; set; } = string.Empty;

    /// <summary>
    /// Betyg från 1-5
    /// </summary>
    public int Betyg { get; set; }

    /// <summary>
    /// Tidpunkt när recensionen skapades
    /// </summary>
    public DateTime SkapadDatum { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Författaren av recensionen
    /// </summary>
    public string Forfattare { get; set; } = string.Empty;

    /// <summary>
    /// Namnet på produkten som recenseras
    /// </summary>
    public string ProduktNamn { get; set; } = string.Empty;
} 