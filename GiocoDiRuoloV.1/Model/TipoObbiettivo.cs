namespace GiocoRuolo.Models
{
    /// <summary>
    /// Identifica il tipo di obiettivo richiesto dalla quest.
    /// Determina la logica da eseguire per verificarne il completamento.
    /// </summary>
    public enum TipoObiettivo
    {
        Uccidi = 0,     // Sconfiggere uno o più nemici
        Raccogli = 1,   // Ottenere oggetti specifici
        Parla = 2,      // Parlare con un NPC
        Esplora = 3,    // Raggiungere una posizione o area
        Usa = 4,        // Utilizzare un oggetto o interagire con un elemento
        Difendi = 5     // Proteggere un NPC o un punto per un certo tempo
    }
}