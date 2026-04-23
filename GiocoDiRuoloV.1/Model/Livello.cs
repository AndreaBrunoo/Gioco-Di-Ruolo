namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta un livello del personaggio.
    /// Contiene l'esperienza richiesta e i bonus ottenuti al passaggio di livello.
    /// </summary>
    public class Livello
    {
        // Numero del livello (es. 1, 2, 3...)
        public int Numero { get; set; }

        // Esperienza totale richiesta per raggiungere questo livello
        public int ExpRichiesta { get; set; }

        // Bonus alle statistiche ottenuti salendo di livello
        public Statistiche BonusStatistiche { get; set; }

        // Punti abilità ottenuti al passaggio di livello
        public int PuntiAbilita { get; set; }

        // True se questo è il livello massimo raggiungibile
        public bool LivelloMassimo { get; set; }
    }
}