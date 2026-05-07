using System.Collections.Generic;
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    public class Nemico
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public Statistiche Statistiche { get; set; } = new();

        public Dictionary<TipoElemento, double> MoltiplicatoriElementali { get; set; } = new();

        public List<EffettoStatus> StatusAttivi { get; set; } = new();

        public List<Attacco> Attacchi { get; set; } = new();

        public int LivelloMinaccia { get; set; }

        public TabellaLoot TabellaLoot { get; set; } = new();

        public bool Boss { get; set; }
        public int Fase { get; set; } = 1;

        public List<Attacco> AttacchiFase2 { get; set; } = new();
        public List<Attacco> AttacchiFase3 { get; set; } = new();

        public int SogliaFase2 { get; set; } = 50; // % di vita
        public int SogliaFase3 { get; set; } = 20; // % di vita

        public string? NomeFase2 { get; set; }
        public string? NomeFase3 { get; set; }
    }

    public class TabellaLoot
    {
        public int IdNemico { get; set; }
        public List<LootItem> PossibiliLoot { get; set; } = new();
        public int MoneteMin { get; set; }
        public int MoneteMax { get; set; }
    }

    public class LootItem
    {
        public string Nome { get; set; } = "";
        public Rarita Rarita { get; set; }
        public int Quantita { get; set; } = 1;
    }

}