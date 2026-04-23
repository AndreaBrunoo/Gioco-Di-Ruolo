using GiocoRuolo.Models;

namespace GiocoRuolo.Helpers
{
    /// <summary>
    /// Gestisce la progressione del personaggio:
    /// esperienza, salite di livello e applicazione dei bonus.
    /// </summary>
    public static class ProgressioneHelper
    {
        /// <summary>
        /// Aggiunge esperienza al personaggio e gestisce eventuali salite di livello.
        /// Restituisce true se il personaggio sale di livello.
        /// </summary>
        public static bool AggiungiEsperienza(Personaggio personaggio, int expGuadagnata, List<Livello> livelli)
        {
            personaggio.Esperienza += expGuadagnata;

            bool salito = false;

            // Controlla se il personaggio può salire di livello
            while (true)
            {
                var livelloAttuale = livelli.First(l => l.Numero == personaggio.Livello);
                var prossimoLivello = livelli.FirstOrDefault(l => l.Numero == personaggio.Livello + 1);

                if (prossimoLivello == null || livelloAttuale.LivelloMassimo)
                    break;

                if (personaggio.Esperienza >= prossimoLivello.ExpRichiesta)
                {
                    SalitaDiLivello(personaggio, prossimoLivello);
                    salito = true;
                }
                else
                {
                    break;
                }
            }

            return salito;
        }

        /// <summary>
        /// Applica i bonus del nuovo livello al personaggio.
        /// </summary>
        private static void SalitaDiLivello(Personaggio personaggio, Livello nuovoLivello)
        {
            personaggio.Livello = nuovoLivello.Numero;

            // Applica bonus statistiche
            personaggio.Statistiche.Attacco += nuovoLivello.BonusStatistiche.Attacco;
            personaggio.Statistiche.Difesa += nuovoLivello.BonusStatistiche.Difesa;
            personaggio.Statistiche.VitaMassima += nuovoLivello.BonusStatistiche.VitaMassima;
            personaggio.Statistiche.ManaMassimo += nuovoLivello.BonusStatistiche.ManaMassimo;
            personaggio.Statistiche.Velocita += nuovoLivello.BonusStatistiche.Velocita;

            // Ripristina vita e mana al massimo
            personaggio.Statistiche.VitaAttuale = personaggio.Statistiche.VitaMassima;
            personaggio.Statistiche.ManaAttuale = personaggio.Statistiche.ManaMassimo;

            // Aggiunge punti abilità
            personaggio.PuntiAbilita += nuovoLivello.PuntiAbilita;
        }
    }
}