using System;
using System.Collections.Generic;
using System.Text;

namespace SpellBouc
{
    class PriestSpellBook : SpellBook
    {

        public override SpellContainer PlayerSpells { get; set; }

        public override SpellContainer CompleteClassSpell { get; set; }


        public PriestSpellBook()
        {
            PlayerSpells = new SpellContainer(ContainerType.PriestPlayerSpells);
            CompleteClassSpell = new SpellContainer(ContainerType.PriestCompleteSpells);
        }

    }
}
