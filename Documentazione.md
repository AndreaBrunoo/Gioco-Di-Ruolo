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

using Gioco.Dominio.Enum;
using Gioco.Dominio.Modelli;

namespace Gioco.Dominio.Classi
{
    public class ServizioClassi
    {
        public void ApplicaStatisticheBase(Personaggio personaggio)
        {
            var stats = personaggio.Statistiche;

            switch (personaggio.Classe)
            {
                case ClassePersonaggio.Guerriero:
                    stats.SaluteMassima = 120;
                    stats.Attacco = 20;
                    stats.Difesa = 15;
                    stats.Velocita = 10;
                    stats.ManaMassimo = 20;
                    break;

                case ClassePersonaggio.Mago:
                    stats.SaluteMassima = 80;
                    stats.Attacco = 10;
                    stats.Difesa = 8;
                    stats.Velocita = 12;
                    stats.ManaMassimo = 60;
                    break;

                case ClassePersonaggio.Ladro:
                    stats.SaluteMassima = 90;
                    stats.Attacco = 15;
                    stats.Difesa = 10;
                    stats.Velocita = 20;
                    stats.ManaMassimo = 25;
                    break;

                case ClassePersonaggio.Tank:
                    stats.SaluteMassima = 150;
                    stats.Attacco = 12;
                    stats.Difesa = 25;
                    stats.Velocita = 5;
                    stats.ManaMassimo = 15;
                    break;

                case ClassePersonaggio.Arcere:
                    stats.SaluteMassima = 100;
                    stats.Attacco = 18;
                    stats.Difesa = 10;
                    stats.Velocita = 18;
                    stats.ManaMassimo = 30;
                    break;
            }

            stats.SaluteAttuale = stats.SaluteMassima;
            stats.ManaAttuale = stats.ManaMassimo;
        }

        public List<Abilita> AbilitaIniziali(ClassePersonaggio classe)
        {
            return classe switch
            {
                ClassePersonaggio.Guerriero => new List<Abilita>
                {
                    new Abilita
                    {
                        Id = 100,
                        Nome = "Colpo Potente",
                        Tipo = TipoAbilita.Attiva,
                        Potenza = 35,
                        CostoMana = 5,
                        Descrizione = "Un colpo fisico devastante."
                    }
                },

                ClassePersonaggio.Mago => new List<Abilita>
                {
                    new Abilita
                    {
                        Id = 200,
                        Nome = "Dardo Magico",
                        Tipo = TipoAbilita.Attiva,
                        Potenza = 45,
                        CostoMana = 10,
                        Elemento = TipoElemento.Fuoco,
                        Descrizione = "Un proiettile magico che colpisce sempre."
                    }
                },

                ClassePersonaggio.Ladro => new List<Abilita>
                {
                    new Abilita
                    {
                        Id = 300,
                        Nome = "Pugnalata Rapida",
                        Tipo = TipoAbilita.Attiva,
                        Potenza = 25,
                        CostoMana = 4,
                        Descrizione = "Un attacco veloce con alta probabilità di critico."
                    }
                },

                ClassePersonaggio.Tank => new List<Abilita>
                {
                    new Abilita
                    {
                        Id = 400,
                        Nome = "Provocazione",
                        Tipo = TipoAbilita.Passiva,
                        BonusDifesa = 5,
                        Descrizione = "Aumenta la difesa e attira l'attenzione dei nemici."
                    }
                },

                ClassePersonaggio.Arcere => new List<Abilita>
                {
                    new Abilita
                    {
                        Id = 500,
                        Nome = "Freccia Penetrante",
                        Tipo = TipoAbilita.Attiva,
                        Potenza = 30,
                        CostoMana = 6,
                        Descrizione = "Una freccia che ignora parte della difesa."
                    }
                },

                _ => new List<Abilita>()
            };
        }

    }
}


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

namespace Gioco.Dominio.Enum
{
    public enum Rarita
    {
        Comune,
        NonComune,
        Raro,
        Epico,
        Leggendario
    }

    public enum TipoAbilita
    {
        Attiva,
        Passiva
    }

    public enum StatoQuest
    {
        NonIniziata,
        InCorso,
        Completata,
        Consegnata
    }

    public enum ClassePersonaggio
    {
        Guerriero,
        Mago,
        Ladro,
        Tank,
        Arcere,
    }

    public enum TipoElemento
    {
        Neutro,
        Fuoco,
        Ghiaccio,
        Veleno,
        Sacro,
        Ombra
    }

    public enum TipoAttacco
    {
        CorpoACorpo,
        Distanza,
        Magico
    }

    public enum TipoOggetto
    {
        Arma,
        Armatura,
        Consumabile,
        Varie
    }

    public enum SlotEquipaggiamento
    {
        Nessuno,
        Arma,
        Testa,
        Corpo,
        Gambe,
        Accessorio
    }

    public enum TipoStatus
    {
        Avvelenato,
        Scottato,
        Stordito,
        Sanguinamento,
        Rallentato
    }

    public enum TipoCella
    {
        // Mappa 1

        Piazza,
        Taverna,
        Mercante,
        Tempio,
        Uscita,

        // Mappa 2

        Strada,
        BoscoFitto,
        Radura,
        Grotta,
        Arena,

        // Mappa 3

        Campi,
        Accampamento,
        Rovine,

        // Mappa 4

        SentieroMontano,
        Caverna,
        Ponte,

        // Mappa 5

        Corridoio,
        Sala,
        Altare,

        // Per test

        Villaggio,
        Dungeon,
    }
}

using System.Collections.Generic;
using Gioco.Dominio.Modelli;
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Equip
{
    /// <summary>
    /// Gestisce l'equipaggiamento del personaggio (indossare e rimuovere oggetti).
    /// </summary>
    public class ServizioEquipaggiamento
    {
        /// <summary>
        /// Prova a equipaggiare un oggetto dal suo inventario.
        /// </summary>
        public List<string> Equipaggia(Personaggio personaggio, Oggetto oggetto)
        {
            var log = new List<string>();

            if (!personaggio.Inventario.Oggetti.Contains(oggetto))
            {
                log.Add("❌ L'oggetto non è nel tuo inventario.");
                return log;
            }

            if (oggetto.SlotEquip == SlotEquipaggiamento.Nessuno)
            {
                log.Add($"❌ {oggetto.Nome} non è equipaggiabile.");
                return log;
            }

            // Rimuovi eventuale oggetto già equipaggiato nello stesso slot
            var rimosso = RimuoviOggettoDaSlot(personaggio, oggetto.SlotEquip);
            if (rimosso != null)
            {
                personaggio.Inventario.ProvaAggiungi(rimosso);
                log.Add($"↩ Hai rimosso {rimosso.Nome}.");
            }

            // Equipaggia il nuovo oggetto
            AssegnaOggettoASlot(personaggio, oggetto);
            personaggio.Inventario.Rimuovi(oggetto);

            // Aggiorna statistiche
            ApplicaBonusStatistiche(personaggio);

            log.Add($"🛡️ Hai equipaggiato {oggetto.Nome}.");

            return log;
        }

        /// <summary>
        /// Rimuove un oggetto equipaggiato e lo rimette nell'inventario.
        /// </summary>
        public List<string> Rimuovi(Personaggio personaggio, SlotEquipaggiamento slot)
        {
            var log = new List<string>();

            var oggetto = RimuoviOggettoDaSlot(personaggio, slot);

            if (oggetto == null)
            {
                log.Add("❌ Nessun oggetto equipaggiato in questo slot.");
                return log;
            }

            if (personaggio.Inventario.Pieno)
            {
                log.Add($"⚠ Inventario pieno! {oggetto.Nome} è stato rimosso ma perso.");
                return log;
            }

            personaggio.Inventario.ProvaAggiungi(oggetto);
            ApplicaBonusStatistiche(personaggio);

            log.Add($"🗃️ Hai rimosso {oggetto.Nome}.");

            return log;
        }

        // ------------------------------
        // METODI PRIVATI
        // ------------------------------

        private Oggetto? RimuoviOggettoDaSlot(Personaggio personaggio, SlotEquipaggiamento slot)
        {
            Oggetto? oggetto = slot switch
            {
                SlotEquipaggiamento.Arma => personaggio.Equipaggiamento.Arma,
                SlotEquipaggiamento.Testa => personaggio.Equipaggiamento.Testa,
                SlotEquipaggiamento.Corpo => personaggio.Equipaggiamento.Corpo,
                SlotEquipaggiamento.Gambe => personaggio.Equipaggiamento.Gambe,
                SlotEquipaggiamento.Accessorio => personaggio.Equipaggiamento.Accessorio,
                _ => null
            };

            if (oggetto == null)
                return null;

            // Svuota lo slot
            switch (slot)
            {
                case SlotEquipaggiamento.Arma: personaggio.Equipaggiamento.Arma = null; break;
                case SlotEquipaggiamento.Testa: personaggio.Equipaggiamento.Testa = null; break;
                case SlotEquipaggiamento.Corpo: personaggio.Equipaggiamento.Corpo = null; break;
                case SlotEquipaggiamento.Gambe: personaggio.Equipaggiamento.Gambe = null; break;
                case SlotEquipaggiamento.Accessorio: personaggio.Equipaggiamento.Accessorio = null; break;
            }

            return oggetto;
        }

        private void AssegnaOggettoASlot(Personaggio personaggio, Oggetto oggetto)
        {
            switch (oggetto.SlotEquip)
            {
                case SlotEquipaggiamento.Arma: personaggio.Equipaggiamento.Arma = oggetto; break;
                case SlotEquipaggiamento.Testa: personaggio.Equipaggiamento.Testa = oggetto; break;
                case SlotEquipaggiamento.Corpo: personaggio.Equipaggiamento.Corpo = oggetto; break;
                case SlotEquipaggiamento.Gambe: personaggio.Equipaggiamento.Gambe = oggetto; break;
                case SlotEquipaggiamento.Accessorio: personaggio.Equipaggiamento.Accessorio = oggetto; break;
            }
        }

        /// <summary>
        /// Ricalcola le statistiche del personaggio in base all'equipaggiamento.
        /// </summary>
        private void ApplicaBonusStatistiche(Personaggio personaggio)
        {
            // Prima riportiamo le statistiche ai valori base (senza equip)
            var baseStats = personaggio.Statistiche;

            // Reset ai valori base (senza bonus)
            int saluteBase = baseStats.SaluteMassima - SommaBonus(personaggio, o => o.BonusSalute);
            int attaccoBase = baseStats.Attacco - SommaBonus(personaggio, o => o.BonusAttacco);
            int difesaBase = baseStats.Difesa - SommaBonus(personaggio, o => o.BonusDifesa);
            int velocitaBase = baseStats.Velocita - SommaBonus(personaggio, o => o.BonusVelocita);
            int manaBase = baseStats.ManaMassimo - SommaBonus(personaggio, o => o.BonusMana);

            // Ora applichiamo i bonus
            baseStats.SaluteMassima = saluteBase + SommaBonus(personaggio, o => o.BonusSalute);
            baseStats.Attacco = attaccoBase + SommaBonus(personaggio, o => o.BonusAttacco);
            baseStats.Difesa = difesaBase + SommaBonus(personaggio, o => o.BonusDifesa);
            baseStats.Velocita = velocitaBase + SommaBonus(personaggio, o => o.BonusVelocita);
            baseStats.ManaMassimo = manaBase + SommaBonus(personaggio, o => o.BonusMana);

            // Mantene salute/mana attuali entro i limiti
            baseStats.SaluteAttuale = Math.Min(baseStats.SaluteAttuale, baseStats.SaluteMassima);
            baseStats.ManaAttuale = Math.Min(baseStats.ManaAttuale, baseStats.ManaMassimo);
        }

        private int SommaBonus(Personaggio personaggio, System.Func<Oggetto, int> selettore)
        {
            int somma = 0;

            var equip = personaggio.Equipaggiamento;

            if (equip.Arma != null) somma += selettore(equip.Arma);
            if (equip.Testa != null) somma += selettore(equip.Testa);
            if (equip.Corpo != null) somma += selettore(equip.Corpo);
            if (equip.Gambe != null) somma += selettore(equip.Gambe);
            if (equip.Accessorio != null) somma += selettore(equip.Accessorio);

            return somma;
        }
    }
}


using Gioco.Dominio.Modelli;
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Loot
{
    public class ServizioLoot
    {
        private readonly Random _rng = new Random();

        // Probabilità base per rarità
        private readonly Dictionary<Rarita, int> ProbRarita = new()
        {
            { Rarita.Comune, 60 },
            { Rarita.NonComune, 25 },
            { Rarita.Raro, 10 },
            { Rarita.Epico, 4 },
            { Rarita.Leggendario, 1 }
        };

        // ---------------------------------------------------------
        // GENERA LOOT COMPLETO
        // ---------------------------------------------------------
        public List<LootItem> GeneraLoot(Nemico nemico, TabellaLoot tabella)
        {
            var lootFinale = new List<LootItem>();

            // 1) Monete
            int monete = _rng.Next(tabella.MoneteMin, tabella.MoneteMax + 1);
            lootFinale.Add(new LootItem
            {
                Nome = "Monete",
                Quantita = monete,
                Rarita = Rarita.Comune
            });

            // 2) Oggetti
            foreach (var item in tabella.PossibiliLoot)
            {
                int roll = _rng.Next(0, 100);
                int prob = ProbRarita[item.Rarita];

                if (roll < prob)
                {
                    lootFinale.Add(item);
                }
            }

            // 3) Bonus boss
            if (nemico.Boss)
            {
                lootFinale.Add(new LootItem
                {
                    Nome = "Essenza del Boss",
                    Rarita = Rarita.Leggendario,
                    Quantita = 1
                });
            }

            return lootFinale;
        }

        // ---------------------------------------------------------
        // AGGIUNGE IL LOOT ALL'INVENTARIO
        // ---------------------------------------------------------
        public void ApplicaLoot(Personaggio p, List<LootItem> loot)
        {
            foreach (var item in loot)
            {
                if (item.Nome == "Monete")
                {
                    p.Monete += item.Quantita;
                    continue;
                }

                p.Inventario.Oggetti.Add(new Oggetto
                {
                    Nome = item.Nome,
                    Quantita = item.Quantita
                });
            }
        }
    }
}

using Gioco.Dominio.Modelli;
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Mappa
{
    public class ServizioIncontri
    {
        private readonly Random _rng = new Random();

        // Probabilità base di incontro
        private const int ProbBase = 30; // 30%

        // Probabilità incontro raro
        private const int ProbRaro = 5;  // 5%

        // Probabilità miniboss casuale
        private const int ProbMiniBoss = 1; // 1%

        // ---------------------------------------------------------
        // DETERMINA SE AVVIENE UN INCONTRO
        // ---------------------------------------------------------
        public bool AvvieneIncontro(CellaMappa cella)
        {
            if (!cella.HaNemici)
                return false;

            int probabilita = ProbBase;

            // Modificatori per tipo cella
            switch (cella.Tipo)
            {
                case TipoCella.Strada:
                    probabilita -= 10; // meno incontri
                    break;

                case TipoCella.BoscoFitto:
                    probabilita += 15; // più incontri
                    break;

                case TipoCella.Grotta:
                    probabilita += 25; // quasi garantito
                    break;

                case TipoCella.Arena:
                    return true; // incontro forzato
            }

            int roll = _rng.Next(0, 100);
            return roll < probabilita;
        }

        // ---------------------------------------------------------
        // GENERA NEMICO DAL POOL
        // ---------------------------------------------------------
        public Nemico GeneraNemicoDaPool(PoolNemici pool)
        {
            // Miniboss casuale?
            if (pool.Nemici.Any(n => n.Boss))
            {
                int rollBoss = _rng.Next(0, 100);
                if (rollBoss < ProbMiniBoss)
                {
                    return pool.Nemici.First(n => n.Boss);
                }
            }

            // Incontro raro?
            int rollRaro = _rng.Next(0, 100);
            if (rollRaro < ProbRaro)
            {
                var rari = pool.Nemici.Where(n => n.LivelloMinaccia >= 5).ToList();
                if (rari.Count > 0)
                    return rari[_rng.Next(rari.Count)];
            }

            // Nemico normale
            var normali = pool.Nemici.Where(n => !n.Boss).ToList();
            return normali[_rng.Next(normali.Count)];
        }

        // ---------------------------------------------------------
        // GENERA NEMICO DATO IL POOL ID
        // ---------------------------------------------------------
        public Nemico? GeneraNemicoDaPoolId(int poolId, List<PoolNemici> poolTotali)
        {
            var pool = poolTotali.FirstOrDefault(p => p.Id == poolId);
            if (pool == null)
                return null;

            return GeneraNemicoDaPool(pool);
        }
    }
}

using Gioco.Dominio.Modelli;

namespace Gioco.Dominio.Mappa
{
    public class ServizioMappa
    {
        private readonly Dictionary<int, List<CellaMappa>> _mappe;

        public ServizioMappa(
            List<CellaMappa> celleVillaggio,
            List<CellaMappa> celleBosco,
            List<CellaMappa> cellePianure,
            List<CellaMappa> celleMontagna,
            List<CellaMappa> celleRovine)
        {
            _mappe = new Dictionary<int, List<CellaMappa>>
            {
                { 1, celleVillaggio },
                { 2, celleBosco },
                { 3, cellePianure },
                { 4, celleMontagna },
                { 5, celleRovine }
            };
        }

        // ---------------------------------------------------------
        // OTTIENI CELLA CORRENTE
        // ---------------------------------------------------------
        public CellaMappa? GetCella(Personaggio p)
        {
            if (!_mappe.ContainsKey(p.IdMappa))
                return null;

            return _mappe[p.IdMappa]
                .FirstOrDefault(c => c.X == p.PosX && c.Y == p.PosY);
        }

        // ---------------------------------------------------------
        // MUOVI IL PERSONAGGIO
        // ---------------------------------------------------------
        public bool Muovi(Personaggio p, int dx, int dy)
        {
            int nuovoX = p.PosX + dx;
            int nuovoY = p.PosY + dy;

            if (!_mappe.ContainsKey(p.IdMappa))
                return false;

            var celle = _mappe[p.IdMappa];

            var nuovaCella = celle.FirstOrDefault(c => c.X == nuovoX && c.Y == nuovoY);

            if (nuovaCella == null)
                return false;

            // Movimento valido
            p.PosX = nuovoX;
            p.PosY = nuovoY;

            return true;
        }

        // ---------------------------------------------------------
        // CAMBIO MAPPA (es. uscita dal villaggio → bosco)
        // ---------------------------------------------------------
        public bool CambiaMappa(Personaggio p, int nuovaMappa, int x, int y)
        {
            if (!_mappe.ContainsKey(nuovaMappa))
                return false;

            p.IdMappa = nuovaMappa;
            p.PosX = x;
            p.PosY = y;

            return true;
        }

        // ---------------------------------------------------------
        // CONTROLLA SE LA CELLA HA NEMICI
        // ---------------------------------------------------------
        public bool CellaHaNemici(Personaggio p)
        {
            var cella = GetCella(p);
            return cella != null && cella.HaNemici;
        }

        // ---------------------------------------------------------
        // OTTIENI POOL NEMICI DELLA CELLA
        // ---------------------------------------------------------
        public int? GetPoolNemici(Personaggio p)
        {
            var cella = GetCella(p);
            return cella?.IdPoolNemici;
        }
    }
}

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

```c#
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    public class Abilita
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;

        public TipoAbilita Tipo { get; set; }

        // Costo mana per abilità attive
        public int CostoMana { get; set; }

        // Bonus per abilità passive
        public int BonusAttacco { get; set; }
        public int BonusDifesa { get; set; }
        public int BonusVelocita { get; set; }
        public int BonusSalute { get; set; }
        public int BonusMana { get; set; }

        // Danno base per abilità attive
        public int Potenza { get; set; }

        // Elemento dell’abilità
        public TipoElemento Elemento { get; set; } = TipoElemento.Neutro;
    }
}
```

```c#
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    public class Attacco
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public TipoAttacco TipoAttacco { get; set; }
        public TipoElemento Elemento { get; set; }
        public int PotenzaBase { get; set; }
        public int CostoMana { get; set; }
        public int PrecisioneBase { get; set; }
        public int ProbabilitaCritico { get; set; }

        public TipoStatus? StatusApplicato { get; set; }
        public int ProbabilitaStatus { get; set; }
    }
}
```

```c#
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    public class CellaMappa
    {
        public int IdMappa { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public TipoCella Tipo { get; set; }

        public bool HaNemici { get; set; }
        public bool HaMercante { get; set; }
        public bool HaNpcQuest { get; set; }

        public int? IdPoolNemici { get; set; }
        public int? IdBoss { get; set; }
        public int? IdMinigioco { get; set; }
    }
}
```

```c#
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    public class EffettoStatus
    {
        public TipoStatus Tipo { get; set; }
        public int TurniRimanenti { get; set; }
        public int Intensita { get; set; }
    }
}
```

```c#
namespace Gioco.Dominio.Modelli
{ 
    public class Equipaggiamento
    {
        public Oggetto? Arma { get; set; }
        public Oggetto? Testa { get; set; }
        public Oggetto? Corpo { get; set; }
        public Oggetto? Gambe { get; set; }
        public Oggetto? Accessorio { get; set; }
    }
}
```

```c#
namespace Gioco.Dominio.Modelli
{
    public class EsitoCombattimento
    {
        public bool GiocatoreVincitore { get; set; }
        public Nemico? NemicoSconfitto { get; set; }
        public List<LogCombattimento> Log { get; set; } = new();
    }
}
```

```c#
using System.Collections.Generic;

namespace Gioco.Dominio.Modelli
{
    public class Inventario
    {
        public int CapacitaMassima { get; } = 10;
        public List<Oggetto> Oggetti { get; } = new();

        public bool Pieno => Oggetti.Count >= CapacitaMassima;

        public bool Aggiungi(Oggetto oggetto)
        {
            if (Pieno)
                return false;

            Oggetti.Add(oggetto);
            return true;
        }

        public bool Rimuovi(Oggetto oggetto)
        {
            return Oggetti.Remove(oggetto);
        }
    }
}
```

```c#
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
```

```c#
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    public class LootItem
    {
        public string Nome { get; set; } = "";
        public Rarita Rarita { get; set; }
        public int Quantita { get; set; } = 1;
    }
}
```

```c#
using System.Collections.Generic;

namespace Gioco.Dominio.Modelli
{
    /// <summary>
    /// Rappresenta un mercante che vende oggetti.
    /// </summary>
    public class Mercante
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Lista degli oggetti che il mercante vende.
        /// </summary>
        public List<Oggetto> InventarioVendita { get; set; } = new();
    }
}
```

```c#
namespace Gioco.Dominio.Modelli
{
    /// <summary>
    /// Rappresenta un minigioco presente in una cella della mappa.
    /// </summary>
    public class Minigioco
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;

        /// <summary>
        /// XP ottenuta completando il minigioco.
        /// </summary>
        public int RicompensaXp { get; set; }

        /// <summary>
        /// Monete ottenute completando il minigioco.
        /// </summary>
        public int RicompensaMonete { get; set; }

        /// <summary>
        /// Oggetto opzionale come ricompensa.
        /// </summary>
        public Oggetto? RicompensaOggetto { get; set; }
    }
}
```

```c#
using System.Collections.Generic;
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    public class Nemico
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public Statistiche Statistiche { get; set; } = new();

        public Dictionary<TipoElemento, double> MoltiplicatoriElementali { get; set; } = new();

        public List<EffettoStatus> StatusAttivi { get; set; } = new();

        public List<Attacco> Attacchi { get; set; } = new();

        public int LivelloMinaccia { get; set; }

        public TabellaLoot TabellaLoot { get; set; } = new();

        public bool Boss { get; set; }
        public int Fase { get; set; } = 1;

        public List<Attacco> AttacchiFase2 { get; set; } = new();
        public List<Attacco> AttacchiFase3 { get; set; } = new();

        public int SogliaFase2 { get; set; } = 50; // % di vita
        public int SogliaFase3 { get; set; } = 20; // % di vita

        public string? NomeFase2 { get; set; }
        public string? NomeFase3 { get; set; }
    }
}
```

```c#
using System.Collections.Generic;

namespace Gioco.Dominio.Modelli
{
    /// <summary>
    /// NPC che può dare quest o dialoghi.
    /// </summary>
    public class Npc
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public string DialogoIniziale { get; set; } = string.Empty;
        public string DialogoCompletamento { get; set; } = string.Empty;

        public Quest? QuestDaDare { get; set; }
    }
}
```

```c#
namespace Gioco.Dominio.Modelli
{
    /// <summary>
    /// Obiettivo singolo della quest (es. uccidi 3 goblin).
    /// </summary>
    public class ObiettivoQuest
    {
        public string Descrizione { get; set; } = string.Empty;
        public int QuantitaRichiesta { get; set; }
        public int QuantitaAttuale { get; set; }

        public bool Completato => QuantitaAttuale >= QuantitaRichiesta;
    }
}
```

```c#
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    public class Oggetto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public TipoOggetto TipoOggetto { get; set; }
        public SlotEquipaggiamento SlotEquip { get; set; }

        public int BonusAttacco { get; set; }
        public int BonusDifesa { get; set; }
        public int BonusVelocita { get; set; }
        public int BonusSalute { get; set; }
        public int BonusMana { get; set; }

        public TipoElemento? Elemento { get; set; }

        public int Valore { get; set; }
        public Rarita Rarita { get; set; }
        public int Quantita { get; set; }
    }
}
```

```c#
using System.Collections.Generic;
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    public class Personaggio
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public ClassePersonaggio Classe { get; set; }

        public int Livello { get; set; }
        public int Esperienza { get; set; }

        public Statistiche Statistiche { get; set; } = new();

        public Dictionary<TipoElemento, double> MoltiplicatoriElementali { get; set; } = new();

        public List<EffettoStatus> StatusAttivi { get; set; } = new();

        public List<Attacco> Attacchi { get; set; } = new();

        public Inventario Inventario { get; set; } = new();
        public Equipaggiamento Equipaggiamento { get; set; } = new();

        public int Monete { get; set; }

        public int PosX { get; set; }
        public int PosY { get; set; }
        public int IdMappa { get; set; }
        public List<Quest>? QuestAttive { get; set; }
        public List<Quest> QuestCompletate { get; set; } = new();

        public List<Abilita> AbilitaSbloccate { get; set; } = new();
        public List<Abilita> AbilitaDisponibili { get; set; } = new();
    }
}
```

```c#
using System.Collections.Generic;

namespace Gioco.Dominio.Modelli
{
    public class PoolNemici
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public List<Nemico> Nemici { get; set; } = new();
    }
}
```

```c#
using System.Collections.Generic;
using Gioco.Dominio.Enum;

namespace Gioco.Dominio.Modelli
{
    public class Quest
    {
        public int Id { get; set; }
        public string Titolo { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;

        public StatoQuest Stato { get; set; } = StatoQuest.NonIniziata;

        public List<ObiettivoQuest> Obiettivi { get; set; } = new();

        public int RicompensaXp { get; set; }
        public int RicompensaMonete { get; set; }
        public List<Oggetto> RicompensaOggetti { get; set; } = new();
    }
}
```

```c#
namespace Gioco.Dominio.Modelli
{
    /// <summary>
    /// Contiene tutti i dati necessari per salvare una partita.
    /// </summary>
    public class SalvataggioGioco
    {
        public Personaggio Personaggio { get; set; } = null!;

        public int IdMappa { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }

        public List<Quest> QuestAttive { get; set; } = new();
        public List<Quest> QuestCompletate { get; set; } = new();
    }
}
```

```c#
namespace Gioco.Dominio.Modelli
{
    public class Statistiche
    {
        public int SaluteMassima { get; set; }
        public int SaluteAttuale { get; set; }
        public int Attacco { get; set; }
        public int Difesa { get; set; }
        public int Velocita { get; set; }
        public int ManaMassimo { get; set; }
        public int ManaAttuale { get; set; }

        public Statistiche(int hp, int atk, int def, int vel, int mana)
        {
            SaluteMassima = hp;
            SaluteAttuale = hp;
            Attacco = atk;
            Difesa = def;
            Velocita = vel;
            ManaMassimo = mana;
            ManaAttuale = mana;
        }

        public Statistiche() : this(1, 1, 1, 1, 1) { }
    }
}
```

```c#
namespace Gioco.Dominio.Modelli
{
    public class TabellaLoot
    {
        public int IdNemico { get; set; }
        public List<LootItem> PossibiliLoot { get; set; } = new();
        public int MoneteMin { get; set; }
        public int MoneteMax { get; set; }
    }
}
```

```c#

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
```

// Questo servizio non è funzionante
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
            personaggio.QuestAttive.Add(npc.QuestDaDare);

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

// Questo servizio non è funzionante
using System.IO;
using System.Text.Json;
using Gioco.Dominio.Modelli;

namespace Gioco.Dominio.Salvataggi
{
    /// <summary>
    /// Gestisce salvataggio e caricamento del gioco in JSON.
    /// </summary>
    public class ServizioSalvataggio
    {
        private readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true,
            IncludeFields = true
        };

        public void Salva(string percorso, Personaggio personaggio)
        {
            var save = new SalvataggioGioco
            {
                Personaggio = personaggio,
                IdMappa = personaggio.IdMappa,
                PosX = personaggio.PosX,
                PosY = personaggio.PosY,
                QuestAttiva = personaggio.QuestAttiva
            };

            var json = JsonSerializer.Serialize(save, _options);
            File.WriteAllText(percorso, json);
        }

        public Personaggio Carica(string percorso)
        {
            var json = File.ReadAllText(percorso);
            var save = JsonSerializer.Deserialize<SalvataggioGioco>(json, _options);

            if (save == null)
                throw new Exception("Errore nel caricamento del salvataggio.");

            var personaggio = save.Personaggio;
            personaggio.IdMappa = save.IdMappa;
            personaggio.PosX = save.PosX;
            personaggio.PosY = save.PosY;
            personaggio.QuestAttiva = save.QuestAttiva;

            return personaggio;
        }
    }
}

using System.Collections.Generic;
using Gioco.Dominio.Modelli;

namespace Gioco.Dominio.Shop
{
    /// <summary>
    /// Gestisce acquisti e vendite tra giocatore e mercante.
    /// </summary>
    public class ServizioShop
    {
        /// <summary>
        /// Il giocatore compra un oggetto dal mercante.
        /// </summary>
        public List<string> CompraOggetto(Personaggio personaggio, Mercante mercante, Oggetto oggetto)
        {
            var log = new List<string>();

            if (!mercante.InventarioVendita.Contains(oggetto))
            {
                log.Add("❌ Il mercante non vende questo oggetto.");
                return log;
            }

            if (personaggio.Monete < oggetto.Valore)
            {
                log.Add($"❌ Non hai abbastanza monete per comprare {oggetto.Nome}.");
                return log;
            }

            if (personaggio.Inventario.Pieno)
            {
                log.Add("❌ Inventario pieno! Non puoi acquistare altri oggetti.");
                return log;
            }

            personaggio.Monete -= oggetto.Valore;
            personaggio.Inventario.ProvaAggiungi(oggetto);

            log.Add($"🛒 Hai acquistato {oggetto.Nome} per {oggetto.Valore} monete.");

            return log;
        }

        /// <summary>
        /// Il giocatore vende un oggetto al mercante.
        /// </summary>
        public List<string> VendiOggetto(Personaggio personaggio, Mercante mercante, Oggetto oggetto)
        {
            var log = new List<string>();

            if (!personaggio.Inventario.Oggetti.Contains(oggetto))
            {
                log.Add("❌ Non possiedi questo oggetto.");
                return log;
            }

            int valoreVendita = oggetto.Valore / 2; // 50% del valore

            personaggio.Monete += valoreVendita;
            personaggio.Inventario.Rimuovi(oggetto);

            log.Add($"💰 Hai venduto {oggetto.Nome} per {valoreVendita} monete.");

            return log;
        }
    }
}
using Gioco.Dominio.Modelli;
using Gioco.Dominio.Enum;
using Gioco.Dominio.Mappa;
using Gioco.Dominio.Combattimento;

class Program
{
    static void Main()
    {
        Console.Title = "RPG – Esplorazione";

        // -------------------------
        // CREAZIONE PERSONAGGIO
        // -------------------------
        var personaggio = CreaPersonaggio();

        // -------------------------
        // INIZIALIZZAZIONE MONDO
        // -------------------------
        var celleVillaggio = InizializzaVillaggioDiArvendale();
        var celleBosco = InizializzaBoscoDelleOmbre();
        var cellePianure = InizializzaPianureContese();
        var celleMontagna = InizializzaMontagnaDelDrago();
        var celleRovine = InizializzaRovineAntiche();

        var servizioMappa = new ServizioMappa(
            celleVillaggio,
            celleBosco,
            cellePianure,
            celleMontagna,
            celleRovine
        );

        var poolNemici = InizializzaPoolNemici();
        var servizioIncontri = new ServizioIncontri();
        var servizioCombattimento = new ServizioCombattimento();

        // Posizione iniziale
        personaggio.IdMappa = 1;
        personaggio.PosX = 2;
        personaggio.PosY = 0;

        // -------------------------
        // LOOP DI ESPLORAZIONE
        // -------------------------

        while (true)
        {
            Console.Clear();
            StampaStato(personaggio, servizioMappa);

            Console.WriteLine("\nComandi: W A S D per muoverti | M per Menu | Q per uscire");

            Console.Write("> ");
            var input = Console.ReadKey(true).Key;

            if (input == ConsoleKey.M)
            {
                ApriMenuGioco(personaggio, servizioMappa);
                continue;
            }

            if (input == ConsoleKey.Q)
                break;

            int dx = 0, dy = 0;

            switch (input)
            {
                case ConsoleKey.W: dy = -1; break;
                case ConsoleKey.S: dy = 1; break;
                case ConsoleKey.A: dx = -1; break;
                case ConsoleKey.D: dx = 1; break;
                default:
                    continue;
            }

            // Movimento
            if (!servizioMappa.Muovi(personaggio, dx, dy))
            {
                Console.WriteLine("\nNon puoi andare in quella direzione.");
                Console.ReadKey();
                continue;
            }

            var cella = servizioMappa.GetCella(personaggio);

            // Cambio mappa automatico
            if (cella.Tipo == TipoCella.Uscita && personaggio.IdMappa == 1)
            {
                servizioMappa.CambiaMappa(personaggio, 2, 3, 6);
                continue;
            }

            // Incontro?
            if (servizioIncontri.AvvieneIncontro(cella))
            {
                Console.Clear();
                Console.WriteLine("⚔ INCONTRO!");

                var poolId = cella.IdPoolNemici;
                var nemico = servizioIncontri.GeneraNemicoDaPoolId(poolId.Value, poolNemici);

                Console.WriteLine($"Hai incontrato: {nemico.Nome}");

                var risultato = servizioCombattimento.SimulaCombattimento(personaggio, nemico);

                Console.WriteLine("\n--- COMBATTIMENTO ---");
                foreach (var log in risultato.Log)
                    Console.WriteLine(log.Descrizione);

                Console.WriteLine("\nPremi un tasto per continuare...");
                Console.ReadKey();
            }
        }
    }

    // -------------------------
    // MENU DI GIOCO
    // -------------------------

    static void ApriMenuGioco(Personaggio p, ServizioMappa servizioMappa)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== MENU DI GIOCO =====\n");

            Console.WriteLine("1) Inventario");
            Console.WriteLine("2) Equipaggiamento");
            Console.WriteLine("3) Quest");
            Console.WriteLine("4) Mappa");
            Console.WriteLine("5) Riposa");
            Console.WriteLine("0) Torna al gioco");

            Console.Write("\nScelta: ");
            var scelta = Console.ReadKey(true).Key;

            switch (scelta)
            {
                case ConsoleKey.D1:
                    MostraInventario(p);
                    break;

                case ConsoleKey.D2:
                    MostraEquipaggiamento(p);
                    break;

                case ConsoleKey.D3:
                    MostraQuest(p);
                    break;

                case ConsoleKey.D4:
                    MostraMappa(p, servizioMappa);
                    break;

                case ConsoleKey.D5:
                    Riposa(p);
                    break;

                case ConsoleKey.D0:
                    return;
            }
        }
    }

    // -------------------------
    // MOSTRA INVENTARIO
    // -------------------------

    static void MostraInventario(Personaggio p)
    {
        Console.Clear();
        Console.WriteLine("===== INVENTARIO =====\n");

        if (p.Inventario.Oggetti.Count == 0)
        {
            Console.WriteLine("Inventario vuoto.");
        }
        else
        {
            foreach (var o in p.Inventario.Oggetti)
                Console.WriteLine($"• {o.Nome}");
        }

        Console.WriteLine("\nPremi un tasto per tornare...");
        Console.ReadKey();
    }

    // -------------------------
    // MOSTRA EQUIPPAGGIAMENTO
    // -------------------------

    static void MostraEquipaggiamento(Personaggio p)
    {
        Console.Clear();
        Console.WriteLine("===== EQUIPAGGIAMENTO =====\n");

        Console.WriteLine($"Testa: {p.Equipaggiamento.Testa?.Nome ?? "Nessuno"}");
        Console.WriteLine($"Corpo: {p.Equipaggiamento.Corpo?.Nome ?? "Nessuno"}");
        Console.WriteLine($"Gambe: {p.Equipaggiamento.Gambe?.Nome ?? "Nessuno"}");
        Console.WriteLine($"Arma: {p.Equipaggiamento.Arma?.Nome ?? "Nessuna"}");

        Console.WriteLine("\nPremi un tasto per tornare...");
        Console.ReadKey();
    }

    // -------------------------
    // MOSTRA QUEST
    // -------------------------

    static void MostraQuest(Personaggio p)
    {
        Console.Clear();
        Console.WriteLine("===== QUEST =====\n");

        if (p.QuestAttive.Count == 0)
        {
            Console.WriteLine("Nessuna quest attiva.");
        }
        else
        {
            foreach (var q in p.QuestAttive)
            {
                Console.WriteLine($"• {q.Titolo}");
                foreach (var o in q.Obiettivi)
                    Console.WriteLine($"   - {o.Descrizione}: {o.QuantitaAttuale}/{o.QuantitaRichiesta}");
            }
        }

        Console.WriteLine("\nPremi un tasto per tornare...");
        Console.ReadKey();
    }

    // -------------------------
    // MOSTRA MAPPA
    // -------------------------

    static void MostraMappa(Personaggio p, ServizioMappa servizioMappa)
    {
        Console.Clear();
        Console.WriteLine("===== MAPPA =====\n");

        var cella = servizioMappa.GetCella(p);

        Console.WriteLine($"Mappa attuale: {p.IdMappa}");
        Console.WriteLine($"Posizione: ({p.PosX}, {p.PosY})");
        Console.WriteLine($"Tipo cella: {cella.Tipo}");

        Console.WriteLine("\nPremi un tasto per tornare...");
        Console.ReadKey();
    }

    // -------------------------
    // RIPOSA
    // -------------------------

    static void Riposa(Personaggio p)
    {
        Console.Clear();
        Console.WriteLine("Ti riposi...");

        p.Statistiche.SaluteAttuale = p.Statistiche.SaluteMassima;
        p.Statistiche.ManaAttuale = p.Statistiche.ManaMassimo;

        Console.WriteLine("HP e Mana completamente recuperati!");
        Console.ReadKey();
    }

    // ---------------------------------------------------------
    // STAMPA STATO GIOCATORE + CELLA
    // ---------------------------------------------------------

    static void StampaStato(Personaggio p, ServizioMappa servizioMappa)
    {
        var cella = servizioMappa.GetCella(p);

        Console.WriteLine($"Mappa: {p.IdMappa}");
        Console.WriteLine($"Posizione: ({p.PosX}, {p.PosY})");
        Console.WriteLine($"Cella: {cella.Tipo}");
        Console.WriteLine($"HP: {p.Statistiche.SaluteAttuale}/{p.Statistiche.SaluteMassima}");
        Console.WriteLine($"Mana: {p.Statistiche.ManaAttuale}/{p.Statistiche.ManaMassimo}");
        Console.WriteLine($"XP: {p.Esperienza} | Monete: {p.Monete}");
    }

    // ---------------------------------------------------------
    // PERSONAGGIO
    // ---------------------------------------------------------

    static Personaggio CreaPersonaggio()
    {
        return new Personaggio
        {
            Id = 1,
            Nome = "Eroe",
            Livello = 1,
            Esperienza = 0,
            Monete = 50,
            Statistiche = new Statistiche
            {
                SaluteMassima = 100,
                SaluteAttuale = 100,
                Attacco = 20,
                Difesa = 10,
                Velocita = 10,
                ManaMassimo = 20,
                ManaAttuale = 20
            }
        };
    }

    // --------------------------------
    // MAPPA 1: VILLAGGIO DI ARVENDALE
    // --------------------------------

    static List<CellaMappa> InizializzaVillaggioDiArvendale()
    {
        return new List<CellaMappa>
        {
            new CellaMappa { IdMappa = 1, X = 2, Y = 0, Tipo = TipoCella.Piazza },
            new CellaMappa { IdMappa = 1, X = 2, Y = 1, Tipo = TipoCella.Taverna },
            new CellaMappa { IdMappa = 1, X = 2, Y = 2, Tipo = TipoCella.Mercante },
            new CellaMappa { IdMappa = 1, X = 2, Y = 3, Tipo = TipoCella.Tempio },
            new CellaMappa { IdMappa = 1, X = 2, Y = 4, Tipo = TipoCella.Uscita }
        };
    }

    // ---------------------------
    // MAPPA 2: BOSCO DELLE OMBRE
    // ---------------------------
    
    static List<CellaMappa> InizializzaBoscoDelleOmbre()
    {
        return new List<CellaMappa>
        {
            new CellaMappa { IdMappa = 2, X = 3, Y = 6, Tipo = TipoCella.Strada, HaNemici = false },
            new CellaMappa { IdMappa = 2, X = 3, Y = 5, Tipo = TipoCella.Strada, HaNemici = true, IdPoolNemici = 1 },
            new CellaMappa { IdMappa = 2, X = 3, Y = 4, Tipo = TipoCella.Strada, HaNemici = true, IdPoolNemici = 1 },
            new CellaMappa { IdMappa = 2, X = 2, Y = 4, Tipo = TipoCella.BoscoFitto, HaNemici = true, IdPoolNemici = 2 },
            new CellaMappa { IdMappa = 2, X = 4, Y = 4, Tipo = TipoCella.BoscoFitto, HaNemici = true, IdPoolNemici = 2 },
            new CellaMappa { IdMappa = 2, X = 3, Y = 3, Tipo = TipoCella.Radura, HaNemici = false },
            new CellaMappa { IdMappa = 2, X = 3, Y = 2, Tipo = TipoCella.Grotta, HaNemici = true, IdPoolNemici = 3 },
            new CellaMappa { IdMappa = 2, X = 3, Y = 1, Tipo = TipoCella.Arena, HaNemici = true, IdPoolNemici = 4 }
        };
    }

    // -------------------------
    // MAPPA 3: PIANURE CONTESE
    // -------------------------

    static List<CellaMappa> InizializzaPianureContese()
    {
        return new List<CellaMappa>
        {
            // Entrata dal Bosco
            new CellaMappa { IdMappa = 3, X = 4, Y = 7, Tipo = TipoCella.Strada, HaNemici = false },
            // Strade principali
            new CellaMappa { IdMappa = 3, X = 4, Y = 6, Tipo = TipoCella.Strada, HaNemici = true, IdPoolNemici = 5 },
            new CellaMappa { IdMappa = 3, X = 4, Y = 5, Tipo = TipoCella.Strada, HaNemici = true, IdPoolNemici = 5 },
            // Campi aperti (nemici medi)
            new CellaMappa { IdMappa = 3, X = 3, Y = 5, Tipo = TipoCella.Campi, HaNemici = true, IdPoolNemici = 6 },
            new CellaMappa { IdMappa = 3, X = 5, Y = 5, Tipo = TipoCella.Campi, HaNemici = true, IdPoolNemici = 6 },
            // Accampamento banditi
            new CellaMappa { IdMappa = 3, X = 2, Y = 4, Tipo = TipoCella.Accampamento, HaNemici = true, IdPoolNemici = 7 },
            // Rovine antiche minori
            new CellaMappa { IdMappa = 3, X = 6, Y = 4, Tipo = TipoCella.Rovine, HaNemici = true, IdPoolNemici = 6 },
            // Mini-boss: Capitano dei Banditi
            new CellaMappa { IdMappa = 3, X = 2, Y = 3, Tipo = TipoCella.Arena, HaNemici = true, IdPoolNemici = 8 }
        };
    }

    // ----------------------------
    // MAPPA 4: MANTAGNA DEL DRAGO
    // ----------------------------

    static List<CellaMappa> InizializzaMontagnaDelDrago()
    {
        return new List<CellaMappa>
        {
            // Entrata dalle Pianure
            new CellaMappa { IdMappa = 4, X = 4, Y = 7, Tipo = TipoCella.SentieroMontano, HaNemici = true, IdPoolNemici = 9 },
            // Sentieri
            new CellaMappa { IdMappa = 4, X = 4, Y = 6, Tipo = TipoCella.SentieroMontano, HaNemici = true, IdPoolNemici = 9 },
            new CellaMappa { IdMappa = 4, X = 4, Y = 5, Tipo = TipoCella.SentieroMontano, HaNemici = true, IdPoolNemici = 9 },
            // Caverne
            new CellaMappa { IdMappa = 4, X = 3, Y = 5, Tipo = TipoCella.Caverna, HaNemici = true, IdPoolNemici = 10 },
            new CellaMappa { IdMappa = 4, X = 5, Y = 5, Tipo = TipoCella.Caverna, HaNemici = true, IdPoolNemici = 10 },
            // Ponte sospeso
            new CellaMappa { IdMappa = 4, X = 4, Y = 4, Tipo = TipoCella.Ponte, HaNemici = true, IdPoolNemici = 11 },
            // Nido del Drago (boss finale)
            new CellaMappa { IdMappa = 4, X = 4, Y = 3, Tipo = TipoCella.Arena, HaNemici = true, IdPoolNemici = 12 }
        };
    }

    // ----------------------------
    // MAPPA 5: ROVINE ANTICHE
    // ----------------------------

    static List<CellaMappa> InizializzaRovineAntiche()
    {
        return new List<CellaMappa>
        {
            // Entrata dalla Montagna
            new CellaMappa { IdMappa = 5, X = 3, Y = 6, Tipo = TipoCella.Corridoio, HaNemici = true, IdPoolNemici = 13 },
            // Corridoi
            new CellaMappa { IdMappa = 5, X = 3, Y = 5, Tipo = TipoCella.Corridoio, HaNemici = true, IdPoolNemici = 13 },
            new CellaMappa { IdMappa = 5, X = 3, Y = 4, Tipo = TipoCella.Corridoio, HaNemici = true, IdPoolNemici = 13 },
            // Sale
            new CellaMappa { IdMappa = 5, X = 2, Y = 4, Tipo = TipoCella.Sala, HaNemici = true, IdPoolNemici = 14 },
            new CellaMappa { IdMappa = 5, X = 4, Y = 4, Tipo = TipoCella.Sala, HaNemici = true, IdPoolNemici = 14 },
            // Altare
            new CellaMappa { IdMappa = 5, X = 3, Y = 3, Tipo = TipoCella.Altare, HaNemici = true, IdPoolNemici = 14 },
            // Boss opzionale
            new CellaMappa { IdMappa = 5, X = 3, Y = 2, Tipo = TipoCella.Arena, HaNemici = true, IdPoolNemici = 15 }
        };
    }

    // -------------------------
    // POOL NEMICI
    // -------------------------
    
    static List<PoolNemici> InizializzaPoolNemici()
    {
        return new List<PoolNemici>
        {
            new PoolNemici
            {
                Id = 1,
                Nome = "Bosco – Low",
                Nemici = new List<Nemico>
                {
                    new Nemico { Nome = "Lupo", LivelloMinaccia = 1 },
                    new Nemico { Nome = "Pipistrello", LivelloMinaccia = 1 }
                }
            },
            new PoolNemici
            {
                Id = 2,
                Nome = "Bosco – Mid",
                Nemici = new List<Nemico>
                {
                    new Nemico { Nome = "Goblin", LivelloMinaccia = 2 },
                    new Nemico { Nome = "Lupo Alfa", LivelloMinaccia = 3 }
                }
            },
            new PoolNemici
            {
                Id = 3,
                Nome = "Bosco – Grotta",
                Nemici = new List<Nemico>
                {
                    new Nemico { Nome = "Ragno Gigante", LivelloMinaccia = 3 }
                }
            },
            new PoolNemici
            {
                Id = 4,
                Nome = "Bosco – MiniBoss",
                Nemici = new List<Nemico>
                {
                    new Nemico { Nome = "Goblin Capo", Boss = true, LivelloMinaccia = 5 }
                }
            },
            new PoolNemici
            {
                Id = 5,
                Nome = "Pianure – Strade",
                Nemici = new List<Nemico>
                {
                    new Nemico { Nome = "Bandito", LivelloMinaccia = 2 },
                    new Nemico { Nome = "Cane Randagio", LivelloMinaccia = 2 }
                }
            },
            new PoolNemici
            {
                Id = 6,
                Nome = "Pianure – Campi",
                Nemici = new List<Nemico>
                {
                    new Nemico { Nome = "Bestia delle Pianure", LivelloMinaccia = 3 },
                    new Nemico { Nome = "Soldato Corrotto", LivelloMinaccia = 3 }
                }
            },
            new PoolNemici
            {
                Id = 7,
                Nome = "Pianure – Accampamento",
                Nemici = new List<Nemico>
                {
                    new Nemico { Nome = "Bandito", LivelloMinaccia = 2 },
                    new Nemico { Nome = "Arciere Bandito", LivelloMinaccia = 3 },
                    new Nemico { Nome = "Ladro", LivelloMinaccia = 3 }
                }
            },
            new PoolNemici
            {
                Id = 8,
                Nome = "Pianure – MiniBoss",
                Nemici = new List<Nemico>
                {
                    new Nemico { Nome = "Capitano dei Banditi", Boss = true, LivelloMinaccia = 6 }
                }
            },
            new PoolNemici
            {
                Id = 9,
                Nome = "Montagna – Sentieri",
                Nemici = new List<Nemico>
                {
                    new Nemico { Nome = "Draghetto", LivelloMinaccia = 4 },
                    new Nemico { Nome = "Cultista", LivelloMinaccia = 4 }
                }
            },
            new PoolNemici
            {
                Id = 10,
                Nome = "Montagna – Caverne",
                Nemici = new List<Nemico>
                {
                    new Nemico { Nome = "Elementale di Fuoco", LivelloMinaccia = 5 },
                    new Nemico { Nome = "Drago Giovane", LivelloMinaccia = 6 }
                }
            },
            new PoolNemici
            {
                Id = 11,
                Nome = "Montagna – Ponte",
                Nemici = new List<Nemico>
                {
                    new Nemico { Nome = "Gargoyle", LivelloMinaccia = 6 },
                    new Nemico { Nome = "Elementale dell'Aria", LivelloMinaccia = 6 }
                }
            },
            new PoolNemici
            {
                Id = 12,
                Nome = "Montagna – Boss",
                Nemici = new List<Nemico>
                {
                    new Nemico { Nome = "Drago Antico", Boss = true, LivelloMinaccia = 10 }
                }
            },
            new PoolNemici
            {
                Id = 13,
                Nome = "Rovine – Corridoi",
                Nemici = new List<Nemico>
                {
                    new Nemico { Nome = "Scheletro", LivelloMinaccia = 5 },
                    new Nemico { Nome = "Spettro", LivelloMinaccia = 6 }
                }
            },
            new PoolNemici
            {
                Id = 14,
                Nome = "Rovine – Sale",
                Nemici = new List<Nemico>
                {
                    new Nemico { Nome = "Golem di Pietra", LivelloMinaccia = 7 },
                    new Nemico { Nome = "Mago Spettrale", LivelloMinaccia = 7 }
                }
            },
            new PoolNemici
            {
                Id = 15,
                Nome = "Rovine – Boss",
                Nemici = new List<Nemico>
                {
                    new Nemico { Nome = "Guardiano delle Rovine", Boss = true, LivelloMinaccia = 12 }
                }
            }
        };
    }
}