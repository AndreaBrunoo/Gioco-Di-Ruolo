namespace GiocoRuolo.Models
{
    /// <summary>
    /// Identifica il ruolo e il comportamento dell'NPC.
    /// Utilizzato per determinare dialoghi, interazioni e funzionalità.
    /// </summary>
    public enum TipoNPC
    {
        Neutrale = 0,      // NPC narrativo o ambientale
        Mercante = 1,      // Vende e compra oggetti
        QuestGiver = 2,    // Assegna quest al giocatore
        Ostile = 3,        // NPC che può attaccare (non un vero nemico)
        Guida = 4,         // Fornisce informazioni utili
        Speciale = 5       // NPC unici legati alla storia o eventi
    }
}