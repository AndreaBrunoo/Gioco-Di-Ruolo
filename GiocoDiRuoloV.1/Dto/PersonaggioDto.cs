namespace GiocoRuolo.DTO
{
    public class PersonaggioDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }

        public int Livello { get; set; }
        public int Esperienza { get; set; }

        public int VitaAttuale { get; set; }
        public int ManaAttuale { get; set; }

        public StatisticheDto Statistiche { get; set; }

        public ClassePersonaggioDto Classe { get; set; }

        public List<AttaccoDto> Attacchi { get; set; } = new();
        public List<OggettoDto> Inventario { get; set; } = new();

        public string PosizioneCorrenteId { get; set; }
    }
}