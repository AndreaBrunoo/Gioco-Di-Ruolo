namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta lo stato di avanzamento di un obiettivo di quest.
    /// </summary>
    public enum StatoObiettivo
    {
        NonIniziato = 0,   // L'obiettivo non è ancora stato attivato
        InCorso = 1,       // Il giocatore sta completando l'obiettivo
        Completato = 2,    // L'obiettivo è stato completato
        Fallito = 3        // L'obiettivo non può più essere completato
    }
}