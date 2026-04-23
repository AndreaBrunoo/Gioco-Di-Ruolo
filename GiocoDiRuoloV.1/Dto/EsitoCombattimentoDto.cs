using GiocoRuolo.Models;

namespace GiocoRuolo.DTO
{
    public class EsitoCombattimentoDto
    {
        public bool GiocatoreVincitore { get; set; }

        public string VincitoreId { get; set; }
        public string SconfittoId { get; set; }

        public List<TurnoCombattimentoDto> Turni { get; set; }

        public RicompensaDto Ricompensa { get; set; }

        public string LogFinale { get; set; }
    }
}
