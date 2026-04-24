using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    public class EffettoStatus
    {
        public TipoStatus Tipo { get; set; }
        public int TurniRimanenti { get; set; }
        public int Intensita { get; set; }
    }
}