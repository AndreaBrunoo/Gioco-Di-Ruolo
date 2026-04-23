namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta un mercante nel gioco.
    /// Gestisce un inventario di oggetti acquistabili e vendibili.
    /// </summary>
    public class Mercante
    {
        // Identificatore univoco del mercante
        public string Id { get; set; }

        // Nome del mercante
        public string Nome { get; set; }

        // Descrizione o dialogo introduttivo
        public string Descrizione { get; set; }

        // Inventario degli oggetti che il mercante vende
        public List<Oggetto> Inventario { get; set; } = new();

        // Moltiplicatore dei prezzi (es. 1.0 = prezzo base, 1.2 = +20%)
        public decimal MoltiplicatorePrezzo { get; set; } = 1.0m;

        // True se il mercante può acquistare oggetti dal giocatore
        public bool CompraDalGiocatore { get; set; } = true;

        // Prezzo di acquisto (percentuale del valore dell'oggetto)
        public decimal PercentualeAcquisto { get; set; } = 0.5m;

        // Aggiorna l'inventario (es. ogni tot tempo o evento)
        public void AggiornaInventario(List<Oggetto> nuoviOggetti)
        {
            Inventario = nuoviOggetti;
        }

        // Calcola il prezzo finale di un oggetto
        public decimal CalcolaPrezzo(Oggetto oggetto)
        {
            return oggetto.ValoreBase * MoltiplicatorePrezzo;
        }
    }
}