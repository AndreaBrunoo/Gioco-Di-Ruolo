namespace Gioco.Dominio.Modelli
{
    public class TabellaLoot
    {
        public int IdNemico { get; set; }
        public List<LootItem> PossibiliLoot { get; set; } = new();
        public int MoneteMin { get; set; }
        public int MoneteMax { get; set; }
    }
}