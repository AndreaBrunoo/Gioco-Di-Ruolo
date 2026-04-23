namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta un minigioco accessibile dal giocatore.
    /// Può essere un puzzle, un gioco d'azzardo, una prova di abilità, ecc.
    /// </summary>
    public class Minigioco
    {
        // Identificatore univoco del minigioco
        public string Id { get; set; }

        // Nome del minigioco (es. "Pesca", "Indovinello", "Dadi del Re")
        public string Nome { get; set; }

        // Descrizione narrativa o istruzioni
        public string Descrizione { get; set; }

        // Tipo del minigioco (abilità, fortuna, puzzle, ecc.)
        public TipoMinigioco Tipo { get; set; }

        // Difficoltà del minigioco
        public DifficoltaMinigioco Difficolta { get; set; }

        // Ricompensa ottenibile completando il minigioco
        public Ricompensa Ricompensa { get; set; }

        // True se il minigioco è ripetibile
        public bool Ripetibile { get; set; } = true;

        // Area o villaggio in cui si trova il minigioco
        public string PosizioneId { get; set; }
    }
}