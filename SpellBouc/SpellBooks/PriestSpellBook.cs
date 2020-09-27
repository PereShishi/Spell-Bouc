using System;
using System.Collections.Generic;
using System.Text;

namespace SpellBouc
{
    /* Clase de livre de sort de mage (non utilisée en V1) */
    class PriestSpellBook : SpellBook
    {

        public override SpellContainer PlayerSpells { get; set; }

        public override SpellContainer CompleteClassSpell { get; set; }


        public PriestSpellBook()
        {
            PlayerSpells = new SpellContainer(ContainerType.PriestPlayerSpells);
            CompleteClassSpell = new SpellContainer(ContainerType.PriestCompleteSpells);
        }

        public override void AddSpellInPlayerBook(String name) { }
        public override void RemoveSpellInPlayerBook(String name) { }
    }
}
