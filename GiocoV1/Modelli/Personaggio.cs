namespace GiocoV1.Modelli
{
    public class Personaggio
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int SaluteMassima { get; set; }
        public int SaluteAttuale { get; set; }
        public int Attacco { get; set; }
        public int Difesa { get; set; }
        public int Velocita { get; set; }
        public int Livello { get; set; }
        public int Esperienza { get; set; }
        public List<Mossa> Mosse { get; set; } = new();

        public List<Oggetto> Inventario { get; } = new();
        public int CapacitaInventario { get; } = 10;
        
        public int Monete { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int IdMappa { get; set; }
    }
}