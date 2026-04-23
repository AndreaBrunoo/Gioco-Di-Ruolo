namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta il risultato di un attacco durante il combattimento.
    /// Contiene informazioni su danni, critici, schivate ed effetti applicati.
    /// </summary>
    public class RisultatoAttacco
    {
        // Id dell'attaccante
        public string AttaccanteId { get; set; }

        // Id del bersaglio
        public string BersaglioId { get; set; }

        // Attacco utilizzato
        public Attacco AttaccoUsato { get; set; }

        // Danni inflitti dopo i calcoli
        public int DanniInflitti { get; set; }

        // True se il colpo è stato critico
        public bool ColpoCritico { get; set; }

        // True se il bersaglio ha schivato l'attacco
        public bool Schivato { get; set; }

        // True se il bersaglio è stato sconfitto
        public bool BersaglioSconfitto { get; set; }

        // Esito finale dell'attacco (Successo, Critico, Schivato, Fallito)
        public EsitoTurno Esito { get; set; }

        // Log testuale dell'azione (utile per console)
        public string Log { get; set; }
    }
}