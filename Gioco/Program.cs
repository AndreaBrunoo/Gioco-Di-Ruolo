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