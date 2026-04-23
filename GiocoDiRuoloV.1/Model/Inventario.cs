namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta l'inventario del personaggio.
    /// Contiene fino a un massimo di 10 oggetti.
    /// </summary>
    public class Inventario
    {
        // Numero massimo di slot disponibili
        public int CapacitaMassima { get; set; } = 10;

        // Lista degli oggetti contenuti nell'inventario
        public List<Oggetto> Oggetti { get; set; } = new();

        /// <summary>
        /// Restituisce true se l'inventario è pieno.
        /// </summary>
        public bool IsPieno => Oggetti.Count >= CapacitaMassima;
    }
}