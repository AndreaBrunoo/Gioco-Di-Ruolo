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