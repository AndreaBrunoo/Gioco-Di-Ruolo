namespace GiocoRuolo.Models
{
    /// <summary>
    /// Identifica la categoria del minigioco.
    /// </summary>
    public enum TipoMinigioco
    {
        Abilita = 0,     // Richiede riflessi o precisione
        Fortuna = 1,     // Basato sul caso (dadi, carte...)
        Puzzle = 2,      // Enigmi, logica, combinazioni
        Conoscenza = 3,  // Domande, quiz, indovinelli
        Allenamento = 4  // Migliora statistiche o abilità
    }
}