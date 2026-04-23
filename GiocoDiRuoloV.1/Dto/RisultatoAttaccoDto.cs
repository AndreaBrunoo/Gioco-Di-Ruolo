using GiocoRuolo.Models;

namespace GiocoRuolo.DTO
{
    public class RisultatoAttaccoDto
    {
        public string AttaccanteId { get; set; }
        public string BersaglioId { get; set; }

        public AttaccoDto AttaccoUsato { get; set; }

        public int DanniInflitti { get; set; }

        public bool ColpoCritico { get; set; }
        public bool Schivato { get; set; }
        public bool BersaglioSconfitto { get; set; }

        public EsitoTurno Esito { get; set; }

        public string Log { get; set; }
    }
}