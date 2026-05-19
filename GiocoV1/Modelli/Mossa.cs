namespace GiocoV1.Modelli
{
    public class Mossa
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int PotenzaBase { get; set; }
        public int PrecisioneBase { get; set; }
        public int ProbabilitaCritico { get; set; }
    }
}