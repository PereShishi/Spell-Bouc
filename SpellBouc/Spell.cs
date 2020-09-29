using System;
using System.Collections.Generic;
using System.Text;

namespace SpellBouc
{
    // Classe qui contient les sorts et leur attributs
    class Spell
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Lvl { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Composante { get; set; }
        public string IncTime { get; set; }
        public string Range { get; set; }
        public string AreaEffect { get; set; }
        public string Duration { get; set; }
        public string SaveDice { get; set; }
        public bool MagicResist { get; set; }
        public string Description { get; set; }
        public string Comp { get; set; }
    }
}
