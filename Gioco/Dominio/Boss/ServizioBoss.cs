using System;
using System.Collections.Generic;
using Gioco.Dominio.Modelli;

namespace Gioco.Dominio.Boss
{
    /// <summary>
    /// Gestisce la logica avanzata dei boss: fasi, pattern e attacchi speciali.
    /// </summary>
    public class ServizioBoss
    {
        private readonly Random _random = new();

        /// <summary>
        /// Aggiorna la fase del boss in base alla percentuale di vita.
        /// </summary>
        public List<string> AggiornaFase(Nemico boss)
        {
            var log = new List<string>();

            double percentuale = (double)boss.Statistiche.SaluteAttuale / boss.Statistiche.SaluteMassima * 100;

            if (boss.Fase == 1 && percentuale <= boss.SogliaFase2)
            {
                boss.Fase = 2;
                log.Add($"🔥 Il boss entra nella FASE 2: {boss.NomeFase2}");
            }
            else if (boss.Fase == 2 && percentuale <= boss.SogliaFase3)
            {
                boss.Fase = 3;
                log.Add($"💀 Il boss entra nella FASE 3: {boss.NomeFase3}");
            }

            return log;
        }

        /// <summary>
        /// Restituisce l'attacco scelto dal boss in base alla fase.
        /// </summary>
        public Attacco ScegliAttacco(Nemico boss)
        {
            List<Attacco> lista = boss.Fase switch
            {
                1 => boss.Attacchi,
                2 => boss.AttacchiFase2.Count > 0 ? boss.AttacchiFase2 : boss.Attacchi,
                3 => boss.AttacchiFase3.Count > 0 ? boss.AttacchiFase3 : boss.AttacchiFase2.Count > 0 ? boss.AttacchiFase2 : boss.Attacchi,
                _ => boss.Attacchi
            };

            int index = _random.Next(lista.Count);
            return lista[index];
        }
    }
}