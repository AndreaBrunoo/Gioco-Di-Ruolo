namespace GiocoRuolo.Models
{
    /// <summary>
    /// Identifica la tipologia dell'attacco.
    /// Determina la logica di danno e gli effetti applicati.
    /// </summary>
    public enum TipoAttacco
    {
        Fisico = 0,       // Attacco basato sulla forza fisica
        Magico = 1,       // Attacco basato sulla magia
        Distanza = 2,     // Arco, frecce, proiettili, ecc.
        Elementale = 3,   // Fuoco, ghiaccio, fulmine, veleno...
        Critico = 4,      // Colpo critico con moltiplicatore
        Debuff = 5        // Riduce statistiche o applica effetti negativi
    }
}