namespace GiocoRuolo.Models
{
    /// <summary>
    /// Identifica la tipologia della quest.
    /// Determina importanza, ricompense e impatto sulla storia.
    /// </summary>
    public enum TipoQuest
    {
        Principale = 0,   // Avanza la storia
        Secondaria = 1,   // Missioni opzionali
        Ripetibile = 2    // Può essere completata più volte
    }
}