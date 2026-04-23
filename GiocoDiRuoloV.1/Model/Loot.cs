namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta il loot ottenibile da un nemico o boss.
    /// Include monete, oggetti e probabilità di drop.
    /// </summary>
    public class Loot
    {
        // Monete minime che il nemico può droppare
        public int MoneteMin { get; set; }

        // Monete massime che il nemico può droppare
        public int MoneteMax { get; set; }

        // Lista di oggetti che il nemico può droppare (armi, armature, ecc.)
        public List<Oggetto> OggettiPossibili { get; set; } = new();

        // Probabilità (0-100) che il nemico droppi un oggetto
        public int ProbabilitaDropOggetto { get; set; }

        // Probabilità (0-100) che il nemico droppi un oggetto raro/epico/leggendario
        public int ProbabilitaDropRaro { get; set; }

        // Probabilità (0-100) che droppi più di un oggetto
        public int ProbabilitaDropMultiplo { get; set; }
    }
}