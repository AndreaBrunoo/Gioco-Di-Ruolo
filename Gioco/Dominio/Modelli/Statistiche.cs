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

        public Statistiche(int hp, int atk, int def, int vel, int mana)
        {
            SaluteMassima = hp;
            SaluteAttuale = hp;
            Attacco = atk;
            Difesa = def;
            Velocita = vel;
            ManaMassimo = mana;
            ManaAttuale = mana;
        }

        public Statistiche() : this(1, 1, 1, 1, 1) { }
    }
}