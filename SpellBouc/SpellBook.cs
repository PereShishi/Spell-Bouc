using System;
using System.Collections.Generic;
using System.Text;

namespace SpellBouc
{
    class SpellBook
    {
        public virtual SpellContainer PlayerSpells { get; set; }

        public virtual SpellContainer CompleteClassSpell { get; set; }


    }
}
