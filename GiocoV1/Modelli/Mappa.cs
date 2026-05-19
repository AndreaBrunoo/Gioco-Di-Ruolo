namespace GiocoV1.Modelli
{
    public class Mappa
    {
        public int Id { get; set; }
        public int X { get; set; } = 1;
        public int Y { get; set; } = 1;
        public bool HaNemici { get; set; } = false;
        public List<Nemico> Nemici { get ; set; } = new();
    }
}