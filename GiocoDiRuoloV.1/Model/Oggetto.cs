namespace GiocoRuolo.Models
{
    /// <summary>
    /// Classe base per tutti gli oggetti del gioco.
    /// Armi, armature e altri oggetti derivano da questa classe.
    /// </summary>
    public class Oggetto
    {
        // Nome dell'oggetto (es. "Pozione Minore", "Spada di Ferro")
        public string Nome { get; set; }

        // Descrizione dell'oggetto
        public string Descrizione { get; set; }

        // Valore in monete dell'oggetto (per vendita/acquisto)
        public int Valore { get; set; }

        // Rarità dell'oggetto (Comune, Raro, Epico, Leggendario)
        public Rarita Rarita { get; set; }
    }
}