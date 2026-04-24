using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    public class Attacco
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public TipoAttacco TipoAttacco { get; set; }
        public TipoElemento Elemento { get; set; }
        public int PotenzaBase { get; set; }
        public int CostoMana { get; set; }
        public int PrecisioneBase { get; set; }
        public int ProbabilitaCritico { get; set; }

        public TipoStatus? StatusApplicato { get; set; }
        public int ProbabilitaStatus { get; set; }
    }
}