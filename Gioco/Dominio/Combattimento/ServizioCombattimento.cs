
using Gioco.Dominio.Modelli;

namespace Gioco.Dominio.Combattimento
{
    public class ServizioCombattimento
    {
        private readonly Random _rng = new Random();

        // ---------------------------------------------------------
        // SIMULA COMBATTIMENTO COMPLETO
        // ---------------------------------------------------------
        public EsitoCombattimento SimulaCombattimento(Personaggio p, Nemico n)
        {
            var log = new List<LogCombattimento>();

            int turno = 1;

            // Copie locali degli HP
            int hpGiocatore = p.Statistiche.SaluteAttuale;
            int hpNemico = n.Statistiche.SaluteAttuale;

            // Determina chi inizia
            bool turnoGiocatore = p.Statistiche.Velocita >= n.Statistiche.Velocita;

            while (hpGiocatore > 0 && hpNemico > 0)
            {
                if (turnoGiocatore)
                {
                    int danno = CalcolaDanno(p.Statistiche.Attacco, n.Statistiche.Difesa, out bool critico, out bool schivato);

                    if (schivato)
                    {
                        log.Add(new LogCombattimento($"Turno {turno}: Il nemico schiva il tuo attacco!"));
                    }
                    else
                    {
                        hpNemico -= danno;
                        log.Add(new LogCombattimento(
                            critico
                                ? $"Turno {turno}: Colpo critico! Infliggi {danno} danni al nemico."
                                : $"Turno {turno}: Infliggi {danno} danni al nemico."
                        ));
                    }
                }
                else
                {
                    int danno = CalcolaDanno(n.Statistiche.Attacco, p.Statistiche.Difesa, out bool critico, out bool schivato);

                    if (schivato)
                    {
                        log.Add(new LogCombattimento($"Turno {turno}: Schivi l'attacco del nemico!"));
                    }
                    else
                    {
                        hpGiocatore -= danno;
                        log.Add(new LogCombattimento(
                            critico
                                ? $"Turno {turno}: Il nemico ti colpisce con un critico! Subisci {danno} danni."
                                : $"Turno {turno}: Il nemico ti colpisce. Subisci {danno} danni."
                        ));
                    }
                }

                turnoGiocatore = !turnoGiocatore;
                turno++;
            }

            bool vittoria = hpGiocatore > 0;

            // Aggiorna HP reali del giocatore
            p.Statistiche.SaluteAttuale = Math.Max(0, hpGiocatore);

            return new EsitoCombattimento
            {
                GiocatoreVincitore = vittoria,
                Log = log,
                NemicoSconfitto = vittoria ? n : null
            };
        }

        // ---------------------------------------------------------
        // CALCOLO DANNI
        // ---------------------------------------------------------
        private int CalcolaDanno(int attacco, int difesa, out bool critico, out bool schivato)
        {
            critico = false;
            schivato = false;

            // Schivata 5%
            if (_rng.Next(0, 100) < 5)
            {
                schivato = true;
                return 0;
            }

            // Critico 10%
            if (_rng.Next(0, 100) < 10)
            {
                critico = true;
                attacco = (int)(attacco * 1.5);
            }

            int danno = attacco - difesa;
            if (danno < 1) danno = 1;

            return danno;
        }
    }
}