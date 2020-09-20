using System;
using System.Collections.Generic;
using System.Text;

namespace SpellBouc
{
    // Classe qui contient les sorts et leur attributs
    class Spell
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Lvl { get; set; }
        public String Type { get; set; }
        public String Source { get; set; }
        public String Composante { get; set; }
        public String IncTime { get; set; }
        public String Range { get; set; }
        public String AreaEffect { get; set; }
        public String Duration { get; set; }
        public String SaveDice { get; set; }
        public bool MagicResist { get; set; }
        public String Description { get; set; }
        public String Comp { get; set; }
    }
}
