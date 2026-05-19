namespace GiocoV1.Modelli
{
    public class Mappa
    {
        public int Id { get; set; }
        public bool HaNemici { get; set; } = false;
        public List<Nemico> Nemici { get ; set; } = new();
    }
}