using System;
using System.Collections.Generic;
using System.Text;

namespace SpellBouc
{
    /* Classe abstraite de livre de sort */
    class SpellBook
    {
        public virtual SpellContainer PlayerSpells { get; set; }

        public virtual SpellContainer CompleteClassSpell { get; set; }

        public virtual void AddSpellInPlayerBook(String name) { }

        public virtual void RemoveSpellInPlayerBook(String name) { }
    }

}
