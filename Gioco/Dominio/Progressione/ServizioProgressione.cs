using System;
using System.Collections.Generic;
using Gioco.Dominio.Modelli;
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Progressione
{
    /// <summary>
    /// Gestisce esperienza, livelli e aumento statistiche del personaggio.
    /// </summary>
    public class ServizioProgressione
    {
        /// <summary>
        /// Calcola quanta esperienza serve per raggiungere il prossimo livello.
        /// Curva semplice: 100 * livello attuale.
        /// </summary>
        public int CalcolaXpPerLivello(int livello)
        {
            return 100 * livello;
        }

        /// <summary>
        /// Aggiunge esperienza al personaggio e gestisce eventuali level-up multipli.
        /// </summary>
        public List<string> AggiungiEsperienza(Personaggio personaggio, int xp)
        {
            var log = new List<string>();

            personaggio.Esperienza += xp;
            log.Add($"Il personaggio guadagna {xp} XP.");

            bool salito = true;

            while (salito)
            {
                int xpNecessaria = CalcolaXpPerLivello(personaggio.Livello);

                if (personaggio.Esperienza >= xpNecessaria)
                {
                    personaggio.Esperienza -= xpNecessaria;
                    personaggio.Livello++;
                    log.Add($"🎉 Il personaggio sale al livello {personaggio.Livello}!");

                    ApplicaAumentoStatistiche(personaggio, log);
                }
                else
                {
                    salito = false;
                }
            }

            return log;
        }

        /// <summary>
        /// Aumenta le statistiche del personaggio quando sale di livello.
        /// </summary>
        private void ApplicaAumentoStatistiche(Personaggio personaggio, List<string> log)
        {
            // Aumenti base (si possono personalizzare per classe)
            int aumentoSalute = 10;
            int aumentoAttacco = 3;
            int aumentoDifesa = 2;
            int aumentoVelocita = 1;
            int aumentoMana = 5;

            personaggio.Statistiche.SaluteMassima += aumentoSalute;
            personaggio.Statistiche.Attacco += aumentoAttacco;
            personaggio.Statistiche.Difesa += aumentoDifesa;
            personaggio.Statistiche.Velocita += aumentoVelocita;
            personaggio.Statistiche.ManaMassimo += aumentoMana;

            // Ripristino parziale
            personaggio.Statistiche.SaluteAttuale = personaggio.Statistiche.SaluteMassima;
            personaggio.Statistiche.ManaAttuale = personaggio.Statistiche.ManaMassimo;

            log.Add($"Statistiche aumentate:");
            log.Add($" +{aumentoSalute} Salute");
            log.Add($" +{aumentoAttacco} Attacco");
            log.Add($" +{aumentoDifesa} Difesa");
            log.Add($" +{aumentoVelocita} Velocità");
            log.Add($" +{aumentoMana} Mana");
        }
    }
}