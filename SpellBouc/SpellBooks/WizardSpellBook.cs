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

        public override void AddSpellInPlayerBook(String name)
        {
            var spellToAdd = CompleteClassSpell.GetSpell(name);

            var status = PlayerSpells.AddSpellInBookAndBD(spellToAdd, ContainerType.WizardPlayerSpells);

            if(status == ErrorCode.ERROR)
            {
                //TODO: Erreur si l'upload n'a pas marché. msgbox ?
            }
        }

        public override void RemoveSpellInPlayerBook(String name)
        {
            var spellToRemove = CompleteClassSpell.GetSpell(name);

            var status = PlayerSpells.RemoveSpellInBookAndBD(spellToRemove, ContainerType.WizardPlayerSpells);

            if (status == ErrorCode.ERROR)
            {
                //TODO: Erreur si l'upload n'a pas marché. msgbox ?
            }
        }

    }
}
