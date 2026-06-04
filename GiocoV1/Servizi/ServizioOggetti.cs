using System.Text.Json;
using GiocoV1.Modelli;
using GiocoV1.Configurazioni;
using GiocoV1.Enum;

namespace GiocoV1.Servizi;

public static class ServizioOggetti
{
    private static List<Oggetto> _tutti = new();
    public static ConfigOggetti Config { get; private set; } = new();

    public static void CaricaOggettiPerCategoria(string percorso)
    {
        if (!File.Exists(percorso))
            throw new FileNotFoundException($"File oggetti non trovato: {percorso}");

        string json = File.ReadAllText(percorso);

        var opzioni = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        Config = JsonSerializer.Deserialize<ConfigOggetti>(json, opzioni)
            ?? throw new Exception("Errore nel parsing del JSON degli oggetti!");
    }

    public static void CaricaTuttiOggetti()
    {
        var c = Config;

        _tutti = new List<Oggetto>();

        // Pozioni
        foreach (var o in c.Pozioni)
        {
            o.Categoria = CategoriaOggetto.Consumabile;
            _tutti.Add(o);
        }

        // Offensivi
        foreach (var o in c.Offensivi)
        {
            o.Categoria = CategoriaOggetto.Offensivo;
            _tutti.Add(o);
        }

        // Materiali
        foreach (var o in c.Materiali)
        {
            o.Categoria = CategoriaOggetto.Materiale;
            _tutti.Add(o);
        }

        // Equipaggiamenti
        foreach (var o in c.Equipaggiamenti.Elmi)
        {
            o.Categoria = CategoriaOggetto.Elmo;
            _tutti.Add(o);
        }

        foreach (var o in c.Equipaggiamenti.Corazze)
        {
            o.Categoria = CategoriaOggetto.Corazza;
            _tutti.Add(o);
        }

        foreach (var o in c.Equipaggiamenti.Gambali)
        {
            o.Categoria = CategoriaOggetto.Gambali;
            _tutti.Add(o);
        }

        foreach (var o in c.Equipaggiamenti.Stivali)
        {
            o.Categoria = CategoriaOggetto.Stivali;
            _tutti.Add(o);
        }

        foreach (var o in c.Equipaggiamenti.Armi)
        {
            o.Categoria = CategoriaOggetto.Arma;
            _tutti.Add(o);
        }
    }

    public static void AggiungiOggettoAlPersonaggio(Personaggio personaggio, OggettoInventario nuovoOggetto)
    {
        var esistente = personaggio.Inventario
            .FirstOrDefault(o => o.Oggetto.Nome.Equals(
                nuovoOggetto.Oggetto.Nome, StringComparison.OrdinalIgnoreCase));

        if (esistente != null) esistente.Quantita += nuovoOggetto.Quantita;
        else personaggio.Inventario.Add(nuovoOggetto);
    }

    public static void EquipaggiaOggetto(Personaggio personaggio, OggettoInventario nuovoOggetto)
    {
        var categoria = nuovoOggetto.Oggetto.Categoria;
        var esistente = personaggio.Inventario
            .FirstOrDefault(o => o.Oggetto.Nome.Equals(
                nuovoOggetto.Oggetto.Nome, StringComparison.OrdinalIgnoreCase));

        if (esistente == null) return;
        var equipaggiamentoVecchio = personaggio.Equipaggiamenti.Get(categoria);

        if (equipaggiamentoVecchio == null)
        {
            personaggio.Equipaggiamenti.Set(categoria, nuovoOggetto);

            if (esistente.Quantita > 1) esistente.Quantita -= 1;
            else personaggio.Inventario.Remove(esistente);
            return;
        }

        var inInventario = personaggio.Inventario
            .FirstOrDefault(o => o.Oggetto.Nome.Equals(
                equipaggiamentoVecchio.Oggetto.Nome, StringComparison.OrdinalIgnoreCase));

        if (inInventario != null) inInventario.Quantita += 1;
        else personaggio.Inventario.Add(new OggettoInventario
        {
            Oggetto = equipaggiamentoVecchio.Oggetto,
            Quantita = 1
        });
        personaggio.Equipaggiamenti.Set(categoria, nuovoOggetto);

        if (esistente.Quantita > 1) esistente.Quantita -= 1;
        else personaggio.Inventario.Remove(esistente);
    }

    public static void EquipaggiaOggettoDaNome(Personaggio personaggio, string nomeOggetto)
    {
        var oggetto = personaggio.Inventario
            .FirstOrDefault(o => o.Oggetto.Nome.Equals(
                nomeOggetto, StringComparison.OrdinalIgnoreCase));

        if (oggetto == null) return;
        EquipaggiaOggetto(personaggio, oggetto);
    }

    public static void RimuoviOggettoEquipaggiato(Personaggio personaggio, OggettoInventario oggettoDaRimuovere)
    {
        var categoria = oggettoDaRimuovere.Oggetto.Categoria;

        var equipaggiamentoVecchio = personaggio.Equipaggiamenti.Get(categoria);
        if (equipaggiamentoVecchio == null) return;

        var inInventario = personaggio.Inventario
            .FirstOrDefault(o => o.Oggetto.Nome.Equals(
                equipaggiamentoVecchio.Oggetto.Nome, StringComparison.OrdinalIgnoreCase));

        if (inInventario != null) inInventario.Quantita += 1;
        else personaggio.Inventario.Add(new OggettoInventario
        {
            Oggetto = equipaggiamentoVecchio.Oggetto,
            Quantita = 1
        });

        personaggio.Equipaggiamenti.Set(categoria, null);
    }
}