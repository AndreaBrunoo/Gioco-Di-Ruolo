namespace GiocoRuolo.DTO
{
    public class ClassePersonaggioDto
    {
        public string Nome { get; set; }
        public string Descrizione { get; set; }

        public StatisticheDto StatisticheBase { get; set; }

        public List<AttaccoDto> AttacchiDisponibili { get; set; } = new();

        public Dictionary<string, int> BonusStatistiche { get; set; } = new();

        public ArmaDto ArmaIniziale { get; set; }
        public ArmaturaDto ArmaturaIniziale { get; set; }
    }
}