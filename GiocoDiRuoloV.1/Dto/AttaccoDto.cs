using GiocoRuolo.Models;

namespace GiocoRuolo.DTO
{
    public class AttaccoDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }

        public int DannoBase { get; set; }
        public int ProbabilitaSuccesso { get; set; }

        public TipoAttacco Tipo { get; set; }
        public Elemento Elemento { get; set; }

        public TipoEffetto EffettoApplicato { get; set; }
    }
}