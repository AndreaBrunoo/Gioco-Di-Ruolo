namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta una classe giocabile (es. Guerriero, Mago, Ladro).
    /// Definisce statistiche base, attacchi disponibili e bonus iniziali.
    /// </summary>
    public class ClassePersonaggio
    {
        // Nome della classe (Guerriero, Mago, Ladro…)
        public string Nome { get; set; }

        // Descrizione breve della classe
        public string Descrizione { get; set; }

        // Statistiche base della classe
        public Statistiche StatisticheBase { get; set; }

        // Attacchi disponibili per questa classe
        public List<Attacco> AttacchiDisponibili { get; set; } = new();

        // Bonus iniziali (es. +2 forza, +1 velocità)
        public Dictionary<string, int> BonusStatistiche { get; set; } = new();

        // Arma iniziale della classe
        public Arma ArmaIniziale { get; set; }

        // Armatura iniziale (se prevista)
        public Armatura ArmaturaIniziale { get; set; }
    }
}