namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta il portafoglio del personaggio.
    /// Gestisce la quantità di monete possedute.
    /// </summary>
    public class Portafoglio
    {
        // Monete attualmente possedute
        public int Monete { get; set; }

        /// <summary>
        /// Restituisce true se il personaggio ha almeno la quantità richiesta.
        /// </summary>
        public bool HaMonete(int quantitaRichiesta)
        {
            return Monete >= quantitaRichiesta;
        }
    }
}