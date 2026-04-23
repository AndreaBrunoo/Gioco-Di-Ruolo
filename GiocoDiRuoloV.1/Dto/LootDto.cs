namespace GiocoRuolo.DTO
{
    public class LootDto
    {
        public int MoneteMin { get; set; }
        public int MoneteMax { get; set; }

        public List<OggettoDto> OggettiPossibili { get; set; } = new();

        public int ProbabilitaDropOggetto { get; set; }
        public int ProbabilitaDropRaro { get; set; }
        public int ProbabilitaDropMultiplo { get; set; }
    }
}