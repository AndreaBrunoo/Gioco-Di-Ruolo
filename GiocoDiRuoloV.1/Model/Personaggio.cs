namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta il personaggio controllato dal giocatore.
    /// Contiene statistiche, inventario, equipaggiamento, classe, livello e monete.
    /// </summary>
    public class Personaggio
    {
        public string Nome { get; set; }

        // Classe del personaggio (Guerriero, Mago, Ladro…)
        public ClassePersonaggio Classe { get; set; }

        // Statistiche base e attuali
        public Statistiche Statistiche { get; set; }

        // Attacchi disponibili (dipendono dalla classe)
        public List<Attacco> Attacchi { get; set; } = new();

        // Inventario limitato a 10 slot
        public Inventario Inventario { get; set; }

        // Equipaggiamento attualmente indossato (arma, armatura, ecc.)
        public Equipaggiamento Equipaggiamento { get; set; }

        // Monete possedute
        public Portafoglio Portafoglio { get; set; }

        // Livello ed esperienza
        public int Livello { get; set; }
        public int PuntiAbilita { get; set; }

        public int Esperienza { get; set; }

        // Posizione attuale nella mappa
        public string PosizioneCorrenteId { get; set; } // id di Area/Villaggio
    }
}