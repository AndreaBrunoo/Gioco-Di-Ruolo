namespace Gioco.Dominio.Modelli
{
    /// <summary>
    /// Contiene tutti i dati necessari per salvare una partita.
    /// </summary>
    public class SalvataggioGioco
    {
        public Personaggio Personaggio { get; set; } = null!;

        public int IdMappa { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }

        public List<Quest> QuestAttive { get; set; } = new();
        public List<Quest> QuestCompletate { get; set; } = new();
    }
}