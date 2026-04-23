namespace GiocoRuolo.Models
{
    /// <summary>
    /// Esito del turno di combattimento.
    /// </summary>
    public enum EsitoTurno
    {
        Successo = 0,     // Attacco andato a segno
        Schivato = 1,     // Il bersaglio ha evitato l'attacco
        Critico = 2,      // Colpo critico
        Fallito = 3       // Attacco fallito (es. probabilità bassa)
    }
}