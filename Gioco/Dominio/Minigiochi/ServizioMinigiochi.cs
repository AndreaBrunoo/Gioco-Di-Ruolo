using System.Collections.Generic;
using Gioco.Dominio.Modelli;
using Gioco.Dominio.Progressione;

namespace Gioco.Dominio.Minigiochi
{
    /// <summary>
    /// Gestisce l'esecuzione dei minigiochi e le ricompense.
    /// </summary>
    public class ServizioMinigiochi
    {
        private readonly ServizioProgressione _progressione = new();

        /// <summary>
        /// Simula il completamento di un minigioco.
        /// </summary>
        public List<string> CompletaMinigioco(Personaggio personaggio, Minigioco minigioco)
        {
            var log = new List<string>();

            log.Add($"🎮 Minigioco completato: {minigioco.Nome}");
            log.Add(minigioco.Descrizione);

            // XP
            var logXp = _progressione.AggiungiEsperienza(personaggio, minigioco.RicompensaXp);
            foreach (var l in logXp) log.Add(l);

            // Monete
            personaggio.Monete += minigioco.RicompensaMonete;
            log.Add($"💰 Hai ottenuto {minigioco.RicompensaMonete} monete.");

            // Oggetto
            if (minigioco.RicompensaOggetto != null)
            {
                if (!personaggio.Inventario.Pieno)
                {
                    personaggio.Inventario.ProvaAggiungi(minigioco.RicompensaOggetto);
                    log.Add($"📦 Hai ottenuto: {minigioco.RicompensaOggetto.Nome}");
                }
                else
                {
                    log.Add($"⚠ Inventario pieno! L'oggetto {minigioco.RicompensaOggetto.Nome} è stato perso.");
                }
            }

            return log;
        }
    }
}