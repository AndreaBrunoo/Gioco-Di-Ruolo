using System.Collections.Generic;
using Gioco.Dominio.Modelli;
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Abilità
{
    public class ServizioAbilita
    {
        /// <summary>
        /// Sblocca un’abilità dal skill tree.
        /// </summary>
        public List<string> SbloccaAbilita(Personaggio personaggio, Abilita abilita)
        {
            var log = new List<string>();

            if (!personaggio.AbilitaDisponibili.Contains(abilita))
            {
                log.Add("❌ Questa abilità non è nel tuo skill tree.");
                return log;
            }

            if (personaggio.AbilitaSbloccate.Contains(abilita))
            {
                log.Add("❌ Hai già sbloccato questa abilità.");
                return log;
            }

            personaggio.AbilitaSbloccate.Add(abilita);
            log.Add($"✨ Abilità sbloccata: {abilita.Nome}");

            // Se è passiva → applica bonus
            if (abilita.Tipo == TipoAbilita.Passiva)
            {
                ApplicaBonusPassivi(personaggio, abilita);
                log.Add("📈 Bonus passivi applicati.");
            }

            return log;
        }

        /// <summary>
        /// Applica i bonus delle abilità passive.
        /// </summary>
        private void ApplicaBonusPassivi(Personaggio personaggio, Abilita abilita)
        {
            var stats = personaggio.Statistiche;

            stats.Attacco += abilita.BonusAttacco;
            stats.Difesa += abilita.BonusDifesa;
            stats.Velocita += abilita.BonusVelocita;
            stats.SaluteMassima += abilita.BonusSalute;
            stats.ManaMassimo += abilita.BonusMana;

            stats.SaluteAttuale = stats.SaluteMassima;
            stats.ManaAttuale = stats.ManaMassimo;
        }
    }
}