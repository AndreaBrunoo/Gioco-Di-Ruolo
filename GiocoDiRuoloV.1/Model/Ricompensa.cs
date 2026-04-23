namespace GiocoRuolo.Models
{
    /// <summary>
    /// Rappresenta la ricompensa ottenuta al completamento di una quest.
    /// Può includere monete, esperienza e oggetti.
    /// </summary>
    public class Ricompensa
    {
        // Monete ottenute completando la quest
        public int Monete { get; set; }

        // Esperienza ottenuta
        public int Esperienza { get; set; }

        // Oggetti ottenuti (armi, armature, consumabili, ecc.)
        public List<Oggetto> Oggetti { get; set; } = new();

        // True se la ricompensa contiene almeno un oggetto
        public bool HaOggetti => Oggetti.Any();
    }
}