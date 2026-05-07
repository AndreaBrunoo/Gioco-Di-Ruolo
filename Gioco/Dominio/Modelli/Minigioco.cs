namespace Gioco.Dominio.Modelli
{
    /// <summary>
    /// Rappresenta un minigioco presente in una cella della mappa.
    /// </summary>
    public class Minigioco
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;

        /// <summary>
        /// XP ottenuta completando il minigioco.
        /// </summary>
        public int RicompensaXp { get; set; }

        /// <summary>
        /// Monete ottenute completando il minigioco.
        /// </summary>
        public int RicompensaMonete { get; set; }

        /// <summary>
        /// Oggetto opzionale come ricompensa.
        /// </summary>
        public Oggetto? RicompensaOggetto { get; set; }
    }
}