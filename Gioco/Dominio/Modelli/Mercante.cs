using System.Collections.Generic;

namespace Gioco.Dominio.Modelli
{
    /// <summary>
    /// Rappresenta un mercante che vende oggetti.
    /// </summary>
    public class Mercante
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Lista degli oggetti che il mercante vende.
        /// </summary>
        public List<Oggetto> InventarioVendita { get; set; } = new();
    }
}