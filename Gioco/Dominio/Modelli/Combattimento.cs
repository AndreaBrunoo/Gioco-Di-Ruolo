namespace Gioco.Dominio.Modelli
{
    public class EsitoCombattimento
    {
        public bool GiocatoreVincitore { get; set; }
        public Nemico? NemicoSconfitto { get; set; }
        public List<LogCombattimento> Log { get; set; } = new();
    }

    public class LogCombattimento
    {
        public string Descrizione { get; set; }

        public LogCombattimento(string descrizione)
        {
            Descrizione = descrizione;
        }
    }
}