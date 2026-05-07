using System.Collections.Generic;
using Gioco.Dominio.Modelli;
using Gioco.Dominio.Progressione;
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Quest
{
    /// <summary>
    /// Gestisce l'avanzamento e il completamento delle quest.
    /// </summary>
    public class ServizioQuest
    {
        private readonly ServizioProgressione _progressione = new();

        /// <summary>
        /// Il giocatore accetta una quest da un NPC.
        /// </summary>
        public List<string> AccettaQuest(Personaggio personaggio, Npc npc)
        {
            var log = new List<string>();

            if (npc.QuestDaDare == null)
            {
                log.Add("❌ Questo NPC non ha quest da dare.");
                return log;
            }

            if (npc.QuestDaDare.Stato != StatoQuest.NonIniziata)
            {
                log.Add("❌ Hai già accettato questa quest.");
                return log;
            }

            npc.QuestDaDare.Stato = StatoQuest.InCorso;
            personaggio.QuestAttiva = npc.QuestDaDare;

            log.Add($"📜 Hai accettato la quest: {npc.QuestDaDare.Titolo}");
            log.Add(npc.DialogoIniziale);

            return log;
        }

        /// <summary>
        /// Aggiorna gli obiettivi della quest (es. uccisione nemici).
        /// </summary>
        public void AggiornaObiettivo(Personaggio personaggio, string descrizioneObiettivo)
        {
            var quest = personaggio.QuestAttiva;
            if (quest == null || quest.Stato != StatoQuest.InCorso)
                return;

            foreach (var ob in quest.Obiettivi)
            {
                if (ob.Descrizione == descrizioneObiettivo)
                {
                    ob.QuantitaAttuale++;
                }
            }

            // Se tutti gli obiettivi sono completati → quest completata
            bool tuttiCompletati = true;
            foreach (var ob in quest.Obiettivi)
            {
                if (!ob.Completato)
                {
                    tuttiCompletati = false;
                    break;
                }
            }

            if (tuttiCompletati)
            {
                quest.Stato = StatoQuest.Completata;
            }
        }

        /// <summary>
        /// Il giocatore consegna la quest all'NPC.
        /// </summary>
        public List<string> ConsegnaQuest(Personaggio personaggio, Npc npc)
        {
            var log = new List<string>();

            var quest = personaggio.QuestAttiva;

            if (quest == null)
            {
                log.Add("❌ Non hai quest attive.");
                return log;
            }

            if (quest.Stato != StatoQuest.Completata)
            {
                log.Add("❌ Non hai ancora completato gli obiettivi.");
                return log;
            }

            // Ricompense
            log.Add(npc.DialogoCompletamento);

            var logXp = _progressione.AggiungiEsperienza(personaggio, quest.RicompensaXp);
            foreach (var l in logXp) log.Add(l);

            personaggio.Monete += quest.RicompensaMonete;
            log.Add($"💰 Hai ricevuto {quest.RicompensaMonete} monete.");

            foreach (var oggetto in quest.RicompensaOggetti)
            {
                if (!personaggio.Inventario.Pieno)
                {
                    personaggio.Inventario.ProvaAggiungi(oggetto);
                    log.Add($"📦 Hai ricevuto: {oggetto.Nome}");
                }
                else
                {
                    log.Add($"⚠ Inventario pieno! L'oggetto {oggetto.Nome} è stato perso.");
                }
            }

            quest.Stato = StatoQuest.Consegnata;
            personaggio.QuestAttiva = null;

            return log;
        }
    }
}