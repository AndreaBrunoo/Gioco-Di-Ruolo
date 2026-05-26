namespace GiocoV1.Modelli
{
    public class Nemico : Entita
    {
        public int Salute { get; set; } = 1;
        public int Attacco { get; set; } = 1;
        public int Difesa { get; set; } = 1;
        public int Velocita { get; set; } = 1;
        public int Livello { get; set; } = 1;
        public int ProbabilitaSpawn { get; set; } = 0;
        public List<Mossa> Mosse { get; set; } = new();
        public List<Oggetto> Inventario { get; } = new();
    }
}