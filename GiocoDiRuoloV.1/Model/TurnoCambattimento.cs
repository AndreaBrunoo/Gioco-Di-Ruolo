namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta un singolo turno di combattimento.
    /// Contiene informazioni su attaccante, bersaglio, attacco e risultato.
    /// </summary>
    public class TurnoCombattimento
    {
        // Numero progressivo del turno
        public int NumeroTurno { get; set; }

        // Id dell'entità che attacca (Personaggio o Nemico)
        public string AttaccanteId { get; set; }

        // Id dell'entità che subisce l'attacco
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

        // Stato del turno (Successo, Fallito, Schivato, Critico...)
        public EsitoTurno Esito { get; set; }

        // Log testuale del turno (utile per console)
        public string Log { get; set; }
    }
}