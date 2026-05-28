using GiocoV1.Modelli;
using GiocoV1.Configurazioni;
using GiocoV1.Dtos;

namespace GiocoV1.Servizi;
public class ServizioMovimento
{
    private readonly Cella?[,] _griglia;

    public ServizioMovimento(Cella?[,] griglia) { _griglia = griglia; }

    public RisultatoMovimento Muovi(Personaggio personaggio, char direzione, Sezione sezione, ConfigNemici configNemici)
    {
        var cellaAttuale = _griglia[personaggio.PosY, personaggio.PosX]!;

        // 🔹 Se il tasto non è valido → non muovere e mostra la cella attuale
        if (!"wasdWASD".Contains(direzione))
        {
            return new RisultatoMovimento
            {
                Messaggio = $"Ti trovi in: {cellaAttuale.Nome}. {cellaAttuale.Descrizione}",
                NemicoTrovato = null
            };
        }

        var risultato = new RisultatoMovimento();
        int nuovoX = personaggio.PosX;
        int nuovoY = personaggio.PosY;

        if (direzione == 'w' || direzione == 'W') nuovoY--;
        if (direzione == 's' || direzione == 'S') nuovoY++;
        if (direzione == 'd' || direzione == 'D') nuovoX++;
        if (direzione == 'a' || direzione == 'A') nuovoX--;

        if (nuovoX < 0 || nuovoX >= _griglia.GetLength(1) ||
            nuovoY < 0 || nuovoY >= _griglia.GetLength(0))
        {
            return new RisultatoMovimento
            {
                Messaggio = $"Ti trovi in: {cellaAttuale.Nome}. {cellaAttuale.Descrizione}",
                NemicoTrovato = null
            };
        }

        if (_griglia[nuovoY, nuovoX] == null)
        {
            return new RisultatoMovimento
            {
                Messaggio = $"Ti trovi in: {cellaAttuale.Nome}. {cellaAttuale.Descrizione}",
                NemicoTrovato = null
            };
        }

        personaggio.PosX = nuovoX;
        personaggio.PosY = nuovoY;

        var cella = _griglia[nuovoY, nuovoX]!;

        var cellaPosizionata = new CellaPosizionata
        {
            X = nuovoX,
            Y = nuovoY,
            Nome = cella.Nome,
            Descrizione = cella.Descrizione
        };

        // ⭐ TENTA LO SPAWN ⭐
        var entita = ServizioNemici.TentaSpawnNemico(sezione, cellaPosizionata, configNemici);

        if (entita != null)
        {
            risultato.NemicoTrovato = entita;
            return risultato;
        }

        risultato.Messaggio = $"Ti trovi in: {cella.Nome}. {cella.Descrizione}";
        return risultato;
    }
}