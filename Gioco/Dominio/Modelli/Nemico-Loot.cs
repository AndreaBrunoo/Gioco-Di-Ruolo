using System.Collections.Generic;
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    public class Nemico
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public bool Boss { get; set; }

        public Statistiche Statistiche { get; set; } = new();

        public Dictionary<TipoElemento, double> MoltiplicatoriElementali { get; set; } = new();

        public List<EffettoStatus> StatusAttivi { get; set; } = new();

        public List<Attacco> Attacchi { get; set; } = new();

        public int LivelloMinaccia { get; set; }

        public TabellaLoot TabellaLoot { get; set; } = new();
    }

    public class TabellaLoot
    {
        public int MoneteMin { get; set; }
        public int MoneteMax { get; set; }

        public List<VoceLoot> Oggetti { get; set; } = new();
    }

    public class VoceLoot
    {
        public Oggetto Oggetto { get; set; } = null!;
        public int ProbabilitaDrop { get; set; }
    }
}