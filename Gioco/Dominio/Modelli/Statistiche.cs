namespace Gioco.Dominio.Modelli
{
    public class Statistiche
    {
        public int SaluteMassima { get; set; }
        public int SaluteAttuale { get; set; }
        public int Attacco { get; set; }
        public int Difesa { get; set; }
        public int Velocita { get; set; }
        public int ManaMassimo { get; set; }
        public int ManaAttuale { get; set; }

        public Statistiche Clona()
        {
            return new Statistiche
            {
                SaluteMassima = SaluteMassima,
                SaluteAttuale = SaluteAttuale,
                Attacco = Attacco,
                Difesa = Difesa,
                Velocita = Velocita,
                ManaMassimo = ManaMassimo,
                ManaAttuale = ManaAttuale
            };
        }
    }
}