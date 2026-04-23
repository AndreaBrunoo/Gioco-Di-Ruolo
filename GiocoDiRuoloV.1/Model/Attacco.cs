namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta un attacco eseguibile da un personaggio o nemico.
    /// Contiene informazioni su danno base, tipo di attacco e probabilità di successo.
    /// </summary>
    public class Attacco
    {
        public string Id { get; set; }

        // Nome dell'attacco (es. "Colpo Pesante", "Freccia Magica")
        public string Nome { get; set; }

        // Descrizione dell'attacco
        public string Descrizione { get; set; }

        // Danno base dell'attacco (prima dei calcoli su forza/resistenza)
        public int DannoBase { get; set; }

        // Tipo dell'attacco: Vicino, Lontano, Magico, Speciale
        public TipoAttacco Tipo { get; set; }

        // Costo in mana (solo per attacchi magici)
        public int CostoMana { get; set; }

        // Probabilità di successo (0-100)
        public int ProbabilitaSuccesso { get; set; }

        // Probabilità di colpo critico (0-100)
        public int ProbabilitaCritico { get; set; }

        // Moltiplicatore del danno critico (es. 2.0 = doppio danno)
        public double MoltiplicatoreCritico { get; set; }

        public Elemento Elemento { get; set; } = Elemento.Nessuno;

        // Effetto di stato applicato al bersaglio (se diverso da Nessuno)
        public TipoEffetto EffettoApplicato { get; set; } = TipoEffetto.Nessuno;
    }
}