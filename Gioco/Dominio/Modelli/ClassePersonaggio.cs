using System.Collections.Generic;
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    public class ClassePersonaggio
    {
        public TipoClassePersonaggio TipoClasse { get; set; }
        public string Nome { get; set; } = string.Empty;

        public Statistiche StatisticheBase { get; set; } = new();

        public List<TipoElemento> ElementiAffini { get; set; } = new();

        public List<Attacco> AttacchiBase { get; set; } = new();
    }
}