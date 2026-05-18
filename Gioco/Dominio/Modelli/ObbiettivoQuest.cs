namespace Gioco.Dominio.Modelli
{
    /// <summary>
    /// Obiettivo singolo della quest (es. uccidi 3 goblin).
    /// </summary>
    public class ObiettivoQuest
    {
        public string Descrizione { get; set; } = string.Empty;
        public int QuantitaRichiesta { get; set; }
        public int QuantitaAttuale { get; set; }

        public bool Completato => QuantitaAttuale >= QuantitaRichiesta;
    }
}