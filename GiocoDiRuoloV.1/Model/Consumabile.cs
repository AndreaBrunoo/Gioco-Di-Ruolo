namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta un oggetto consumabile utilizzabile dal giocatore.
    /// Produce un effetto immediato (cura, buff, mana, ecc.).
    /// </summary>
    public class Consumabile : Oggetto
    {
        // Quantità di vita curata (0 se non cura)
        public int Cura { get; set; }

        // Quantità di mana ripristinata (0 se non ripristina mana)
        public int Mana { get; set; }

        // Durata del buff in turni (0 se effetto istantaneo)
        public int DurataBuff { get; set; }

        // Tipo di buff applicato (attacco, difesa, velocità, ecc.)
        public TipoBuff TipoBuff { get; set; }

        // True se l'oggetto viene consumato all'uso
        public bool Monouso { get; set; } = true;
    }
}