using System;
using System.Collections.Generic;
using System.Text;

namespace SpellBouc.Model
{
    public abstract class Tab
    {
        // Header: 
        public string Lvl { get; set; }

        public string SpellMaxPerDay { get; set; } = 10.ToString();

        public string SpellLeft { get; set; }

    }
}
