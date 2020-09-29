using System;
using System.Collections.Generic;
using System.Text;

namespace SpellBouc
{
    /* Clase de livre de sort de mage (non utilisée en V1) */
    class PriestSpellBook : SpellBook
    {

        internal override SpellContainer PlayerSpells { get; set; }

        internal override SpellContainer CompleteClassSpell { get; set; }


        internal PriestSpellBook()
        {
            PlayerSpells = new SpellContainer(ContainerType.PriestPlayerSpells);
            CompleteClassSpell = new SpellContainer(ContainerType.PriestCompleteSpells);
        }

        internal override void AddSpellInPlayerBook(String name) { }
        internal override void RemoveSpellInPlayerBook(String name) { }
    }
}
