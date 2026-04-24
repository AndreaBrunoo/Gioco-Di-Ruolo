using System;
using System.Collections.Generic;
using Gioco.Dominio.Loot;
using Gioco.Dominio.Enum;
using Gioco.Dominio.Progressione;
using Gioco.Dominio.Modelli;

namespace Gioco.Dominio.Combattimento
{
    /// <summary>
    /// Gestisce la logica di combattimento tra un personaggio e un nemico.
    /// </summary>
    public class ServizioCombattimento
    {
        private readonly Random _random = new();

        /// <summary>
        /// Simula un combattimento completo tra un personaggio e un nemico.
        /// </summary>
        public RisultatoCombattimento SimulaCombattimento(Personaggio personaggio, Nemico nemico)
        {
            var risultato = new RisultatoCombattimento();

            // Clono le statistiche per non modificare gli oggetti originali.
            var statsGiocatore = personaggio.Statistiche.Clona();
            var statsNemico = nemico.Statistiche.Clona();

            var statusGiocatore = new List<EffettoStatus>(personaggio.StatusAttivi);
            var statusNemico = new List<EffettoStatus>(nemico.StatusAttivi);

            bool combattimentoTerminato = false;

            while (!combattimentoTerminato)
            {
                // 1. Determina ordine dei turni in base alla velocità
                bool turnoGiocatorePerPrimo = DeterminaChiAgiscePerPrimo(statsGiocatore, statsNemico);

                if (turnoGiocatorePerPrimo)
                {
                    EseguiTurnoGiocatore(personaggio, nemico, statsGiocatore, statsNemico, statusGiocatore, statusNemico, risultato);

                    if (statsNemico.SaluteAttuale <= 0)
                    {
                        risultato.Esito = EsitoCombattimento.VittoriaGiocatore;
                        GestisciFineCombattimentoVittoria(personaggio, nemico, risultato);
                        combattimentoTerminato = true;
                        break;
                    }

                    EseguiTurnoNemico(personaggio, nemico, statsGiocatore, statsNemico, statusGiocatore, statusNemico, risultato);

                    if (statsGiocatore.SaluteAttuale <= 0)
                    {
                        risultato.Esito = EsitoCombattimento.SconfittaGiocatore;
                        combattimentoTerminato = true;
                        break;
                    }
                }
                else
                {
                    EseguiTurnoNemico(personaggio, nemico, statsGiocatore, statsNemico, statusGiocatore, statusNemico, risultato);

                    if (statsGiocatore.SaluteAttuale <= 0)
                    {
                        risultato.Esito = EsitoCombattimento.SconfittaGiocatore;
                        combattimentoTerminato = true;
                        break;
                    }

                    EseguiTurnoGiocatore(personaggio, nemico, statsGiocatore, statsNemico, statusGiocatore, statusNemico, risultato);

                    if (statsNemico.SaluteAttuale <= 0)
                    {
                        risultato.Esito = EsitoCombattimento.VittoriaGiocatore;
                        GestisciFineCombattimentoVittoria(personaggio, nemico, risultato);
                        combattimentoTerminato = true;
                        break;
                    }
                }

                // 2. Applica effetti per turno degli status
                ApplicaStatusPerTurno(statsGiocatore, statusGiocatore, "Giocatore", risultato);
                ApplicaStatusPerTurno(statsNemico, statusNemico, "Nemico", risultato);

                if (statsGiocatore.SaluteAttuale <= 0 && statsNemico.SaluteAttuale <= 0)
                {
                    risultato.Esito = EsitoCombattimento.Pareggio;
                    combattimentoTerminato = true;
                }
                else if (statsGiocatore.SaluteAttuale <= 0)
                {
                    risultato.Esito = EsitoCombattimento.SconfittaGiocatore;
                    combattimentoTerminato = true;
                }
                else if (statsNemico.SaluteAttuale <= 0)
                {
                    risultato.Esito = EsitoCombattimento.VittoriaGiocatore;
                    GestisciFineCombattimentoVittoria(personaggio, nemico, risultato);
                    combattimentoTerminato = true;
                }
            }

            return risultato;
        }

        /// <summary>
        /// Determina chi agisce per primo in base alla velocità (in caso di parità, random).
        /// </summary>
        private bool DeterminaChiAgiscePerPrimo(Statistiche statsGiocatore, Statistiche statsNemico)
        {
            if (statsGiocatore.Velocita > statsNemico.Velocita)
                return true;

            if (statsNemico.Velocita > statsGiocatore.Velocita)
                return false;

            // Velocità uguale → coin flip
            return _random.Next(0, 2) == 0;
        }

        /// <summary>
        /// Esegue il turno del giocatore: scelta attacco, calcolo danno, applicazione status.
        /// </summary>
        private void EseguiTurnoGiocatore(
            Personaggio personaggio,
            Nemico nemico,
            Statistiche statsGiocatore,
            Statistiche statsNemico,
            List<EffettoStatus> statusGiocatore,
            List<EffettoStatus> statusNemico,
            RisultatoCombattimento risultato)
        {
            // Se il giocatore è stordito, salta il turno.
            if (HaStatus(statusGiocatore, TipoStatus.Stordito))
            {
                risultato.Log.Add(new AzioneCombattimento
                {
                    Descrizione = "Il giocatore è stordito e non può agire!"
                });
                return;
            }

            // Per ora: scelgo il primo attacco disponibile.
            // In futuro: passo l'attacco scelto dal giocatore come parametro.
            var attacco = personaggio.Attacchi.Count > 0 ? personaggio.Attacchi[0] : null;

            if (attacco == null)
            {
                risultato.Log.Add(new AzioneCombattimento
                {
                    Descrizione = "Il giocatore non ha attacchi disponibili!"
                });
                return;
            }

            // Controllo mana
            if (statsGiocatore.ManaAttuale < attacco.CostoMana)
            {
                risultato.Log.Add(new AzioneCombattimento
                {
                    Descrizione = $"Il giocatore non ha abbastanza mana per usare {attacco.Nome}!"
                });
                return;
            }

            statsGiocatore.ManaAttuale -= attacco.CostoMana;

            // Tiro per colpire
            if (!TiroPerColpire(attacco, "Giocatore", risultato))
                return;

            // Calcolo danno
            int danno = CalcolaDanno(
                attacco,
                statsGiocatore,
                statsNemico,
                personaggio.MoltiplicatoriElementali,
                nemico.MoltiplicatoriElementali,
                out bool critico);

            statsNemico.SaluteAttuale = Math.Max(0, statsNemico.SaluteAttuale - danno);

            var descrizione = $"Il giocatore usa {attacco.Nome} e infligge {danno} danni al nemico.";
            if (critico)
                descrizione += " Colpo critico!";

            risultato.Log.Add(new AzioneCombattimento { Descrizione = descrizione });

            // Applicazione status
            ProvaApplicaStatus(attacco, statusNemico, "Nemico", risultato);
        }

        /// <summary>
        /// Esegue il turno del nemico.
        /// </summary>
        private void EseguiTurnoNemico(
            Personaggio personaggio,
            Nemico nemico,
            Statistiche statsGiocatore,
            Statistiche statsNemico,
            List<EffettoStatus> statusGiocatore,
            List<EffettoStatus> statusNemico,
            RisultatoCombattimento risultato)
        {
            if (HaStatus(statusNemico, TipoStatus.Stordito))
            {
                risultato.Log.Add(new AzioneCombattimento
                {
                    Descrizione = "Il nemico è stordito e non può agire!"
                });
                return;
            }

            // Per ora: il nemico usa il primo attacco disponibile.
            var attacco = nemico.Attacchi.Count > 0 ? nemico.Attacchi[0] : null;

            if (attacco == null)
            {
                risultato.Log.Add(new AzioneCombattimento
                {
                    Descrizione = "Il nemico non ha attacchi disponibili!"
                });
                return;
            }

            if (statsNemico.ManaAttuale < attacco.CostoMana)
            {
                risultato.Log.Add(new AzioneCombattimento
                {
                    Descrizione = $"Il nemico non ha abbastanza mana per usare {attacco.Nome}!"
                });
                return;
            }

            statsNemico.ManaAttuale -= attacco.CostoMana;

            if (!TiroPerColpire(attacco, "Nemico", risultato))
                return;

            int danno = CalcolaDanno(
                attacco,
                statsNemico,
                statsGiocatore,
                nemico.MoltiplicatoriElementali,
                personaggio.MoltiplicatoriElementali,
                out bool critico);

            statsGiocatore.SaluteAttuale = Math.Max(0, statsGiocatore.SaluteAttuale - danno);

            var descrizione = $"Il nemico usa {attacco.Nome} e infligge {danno} danni al giocatore.";
            if (critico)
                descrizione += " Colpo critico!";

            risultato.Log.Add(new AzioneCombattimento { Descrizione = descrizione });

            ProvaApplicaStatus(attacco, statusGiocatore, "Giocatore", risultato);
        }

        /// <summary>
        /// Tiro per colpire basato su precisione e random.
        /// </summary>
        private bool TiroPerColpire(Attacco attacco, string chiAttacca, RisultatoCombattimento risultato)
        {
            int tiro = _random.Next(1, 101); // 1-100
            if (tiro > attacco.PrecisioneBase)
            {
                risultato.Log.Add(new AzioneCombattimento
                {
                    Descrizione = $"{chiAttacca} usa {attacco.Nome} ma manca il bersaglio!"
                });
                return false;
            }

            return true;
        }

        /// <summary>
        /// Calcola il danno finale con: attacco, difesa, elemento, critico, random factor.
        /// </summary>
        private int CalcolaDanno(
            Attacco attacco,
            Statistiche statsAttaccante,
            Statistiche statsDifensore,
            Dictionary<TipoElemento, double> moltiplicatoriAttaccante,
            Dictionary<TipoElemento, double> moltiplicatoriDifensore,
            out bool critico)
        {
            // 1. Base grezza
            // K è una costante di bilanciamento (es. 10)
            const double K = 10.0;
            double baseGrezza = (statsAttaccante.Attacco * attacco.PotenzaBase) / K;
            double difesa = statsDifensore.Difesa;

            double dannoGrezzo = baseGrezza - difesa;
            if (dannoGrezzo < 1)
                dannoGrezzo = 1;

            // 2. Critico
            int tiroCritico = _random.Next(1, 101);
            critico = tiroCritico <= attacco.ProbabilitaCritico;
            double moltiplicatoreCritico = critico ? 1.5 : 1.0;

            // 3. Elementi
            double moltiplicatoreElemento = 1.0;

            // Se il difensore ha un moltiplicatore per l'elemento dell'attacco, lo usiamo.
            if (moltiplicatoriDifensore.TryGetValue(attacco.Elemento, out double multDifensore))
            {
                moltiplicatoreElemento = multDifensore;
            }

            // 4. Random factor
            double randomFactor = _random.NextDouble() * (1.0 - 0.85) + 0.85; // 0.85 - 1.0

            double dannoFinale = dannoGrezzo * moltiplicatoreCritico * moltiplicatoreElemento * randomFactor;

            int dannoInt = (int)Math.Floor(dannoFinale);
            if (dannoInt < 1)
                dannoInt = 1;

            return dannoInt;
        }

        /// <summary>
        /// Prova ad applicare uno status in base alla probabilità dell'attacco.
        /// </summary>
        private void ProvaApplicaStatus(
            Attacco attacco,
            List<EffettoStatus> listaStatus,
            string bersaglio,
            RisultatoCombattimento risultato)
        {
            if (attacco.StatusApplicato == null || attacco.ProbabilitaStatus <= 0)
                return;

            int tiro = _random.Next(1, 101);
            if (tiro > attacco.ProbabilitaStatus)
                return;

            listaStatus.Add(new EffettoStatus
            {
                Tipo = attacco.StatusApplicato.Value,
                TurniRimanenti = 3, // valore di default, potrai parametrizzarlo
                Intensita = 5       // valore di default, interpretato in ApplicaStatusPerTurno
            });

            risultato.Log.Add(new AzioneCombattimento
            {
                Descrizione = $"{bersaglio} è ora {attacco.StatusApplicato.Value}!"
            });
        }

        /// <summary>
        /// Applica gli effetti per turno degli status (danni nel tempo, rallentamenti, ecc.).
        /// </summary>
        private void ApplicaStatusPerTurno(
            Statistiche stats,
            List<EffettoStatus> listaStatus,
            string chi,
            RisultatoCombattimento risultato)
        {
            var daRimuovere = new List<EffettoStatus>();

            foreach (var status in listaStatus)
            {
                switch (status.Tipo)
                {
                    case TipoStatus.Avvelenato:
                    case TipoStatus.Sanguinamento:
                    case TipoStatus.Scottato:
                        int danno = status.Intensita;
                        stats.SaluteAttuale = Math.Max(0, stats.SaluteAttuale - danno);
                        risultato.Log.Add(new AzioneCombattimento
                        {
                            Descrizione = $"{chi} subisce {danno} danni da {status.Tipo}."
                        });
                        break;

                    case TipoStatus.Rallentato:
                        // Qui potrei ridurre temporaneamente la velocità.
                        break;

                    case TipoStatus.Stordito:
                        // Effetto gestito nel turno (salta azione).
                        break;
                }

                status.TurniRimanenti--;

                if (status.TurniRimanenti <= 0)
                    daRimuovere.Add(status);
            }

            foreach (var s in daRimuovere)
            {
                listaStatus.Remove(s);
                risultato.Log.Add(new AzioneCombattimento
                {
                    Descrizione = $"{chi} non è più {s.Tipo}."
                });
            }
        }

        /// <summary>
        /// Controlla se nella lista è presente un certo status.
        /// </summary>
        private bool HaStatus(List<EffettoStatus> listaStatus, TipoStatus tipo)
        {
            foreach (var s in listaStatus)
            {
                if (s.Tipo == tipo)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Gestisce XP e loot alla vittoria (per ora solo XP e monete base).
        /// </summary>
        private void GestisciFineCombattimentoVittoria(Personaggio personaggio, Nemico nemico, RisultatoCombattimento risultato)
        {
            var servizioProgressione = new ServizioProgressione();
            var servizioLoot = new ServizioLoot();

            // XP
            int xp = nemico.LivelloMinaccia * 10;
            var logXp = servizioProgressione.AggiungiEsperienza(personaggio, xp);
            risultato.EsperienzaGuadagnata = xp;

            foreach (var voce in logXp)
                risultato.Log.Add(new AzioneCombattimento { Descrizione = voce });

            // Monete
            int monete = servizioLoot.CalcolaMonete(nemico.TabellaLoot);
            personaggio.Monete += monete;
            risultato.MoneteGuadagnate = monete;

            risultato.Log.Add(new AzioneCombattimento
            {
                Descrizione = $"💰 Il nemico droppa {monete} monete."
            });

            // Oggetti
            var oggettiDroppati = servizioLoot.GeneraOggettiDroppati(nemico.TabellaLoot);

            if (oggettiDroppati.Count == 0)
            {
                risultato.Log.Add(new AzioneCombattimento
                {
                    Descrizione = "Nessun oggetto droppato."
                });
            }
            else
            {
                var logOggetti = servizioLoot.AggiungiOggettiAInventario(personaggio, oggettiDroppati);

                foreach (var voce in logOggetti)
                    risultato.Log.Add(new AzioneCombattimento { Descrizione = voce });
            }
        }

    }
}