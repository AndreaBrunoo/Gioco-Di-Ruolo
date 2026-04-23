using System.Reflection;

namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta l'equipaggiamento attualmente indossato dal personaggio.
    /// Ogni slot può contenere un'armatura o un'arma specifica.
    /// </summary>
    public class Equipaggiamento
    {
        // Arma equipaggiata
        public Arma Arma { get; set; }

        // Armatura per la testa
        public Armatura Testa { get; set; }

        // Armatura per il corpo
        public Armatura Corpo { get; set; }

        // Armatura per le gambe
        public Armatura Gambe { get; set; }

        // Armatura per i piedi
        public Armatura Gambali { get; set; }

        /// <summary>
        /// Restituisce tutti gli oggetti equipaggiati come lista.
        /// Utile per calcolare bonus cumulativi.
        /// </summary>
        public List<Oggetto> TuttiGliOggetti =>
            new List<Oggetto>()
            {
                Arma,
                Testa,
                Corpo,
                Gambe,
                Gambali
            }.Where(o => o != null).ToList();
    }
}