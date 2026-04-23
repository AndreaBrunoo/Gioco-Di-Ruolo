namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta l'intera mappa di gioco, composta da più aree esplorabili.
    /// </summary>
    public class Mappa
    {
        // Nome della mappa (es. "Regno di Eldoria")
        public string Nome { get; set; }

        // Lista delle aree che compongono la mappa
        public List<Area> Aree { get; set; } = new();

        // Restituisce un'area tramite il suo Id
        public Area GetAreaById(string id)
        {
            return Aree.FirstOrDefault(a => a.Id == id);
        }
    }
}