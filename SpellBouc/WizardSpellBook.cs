using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace SpellBouc
{
    class WizardSpellBook : SpellBook
    {

        public override SpellContainer PlayerSpells { get; set; }

        public override SpellContainer CompleteClassSpell { get; set; }


        public WizardSpellBook()
        {
            PlayerSpells = new SpellContainer(ContainerType.WizardPlayerSpells);
            CompleteClassSpell = new SpellContainer(ContainerType.WizardCompleteSpells);
        }

    }
}
