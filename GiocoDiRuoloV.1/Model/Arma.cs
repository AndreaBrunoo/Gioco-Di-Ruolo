namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta un'arma equipaggiabile dal personaggio o ottenibile dai nemici.
    /// Estende la classe Oggetto e aggiunge proprietà relative al danno e al tipo di attacco.
    /// </summary>
    public class Arma : Oggetto
    {
        // Danno base dell'arma
        public int Danno { get; set; }

        // Tipo di attacco associato all'arma (Vicino, Lontano, Magico)
        public TipoAttacco TipoAttacco { get; set; }

        // Bonus alla forza o altre statistiche (es. +2 Forza)
        public Dictionary<string, int> BonusStatistiche { get; set; } = new();

        // Rarità dell'arma (Comune, Raro, Epico, Leggendario)
        // Ereditata da Oggetto tramite la proprietà Rarita

        // Valore in monete dell'arma
        // Ereditato da Oggetto tramite la proprietà Valore
    }
}