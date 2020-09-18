using System;
using System.Collections.Generic;
using System.Text;

namespace SpellBouc
{
    // Classe qui contient les sorts et leur attributs
    class Spell
    {
        private String Id { get; set; }
        private String Name { get; set; }
        private int Lvl { get; set; }
        private int Type { get; set; }
        private String Source { get; set; }
        private String Composante { get; set; }
        private String IncTime { get; set; }
        private String Range { get; set; }
        private String AreaEffect { get; set; }
        private String Duration { get; set; }
        private String SaveDice { get; set; }
        private bool MagicResist { get; set; }
        private String Description { get; set; }
        private String Comp { get; set; }
    }
}
