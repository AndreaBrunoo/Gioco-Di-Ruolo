namespace Gioco.Dominio.Modelli
{
    public class LogCombattimento
    {
        public string Descrizione { get; set; }

        public LogCombattimento(string descrizione)
        {
            Descrizione = descrizione;
        }
    }
}