using SpellBouc.UIClasses;

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

        internal override void AddSpellInSpellBook(string name) { }

        internal override void RemoveSpellInSpellBook(string name) { }

        internal override void AddSpellInSpellBook(int id) { }

        internal override void RemoveSpellInSpellBook(int id) { }

        internal override void AddSpellInUIList(Spell spell, UIContainerType uiContainerType) { }

        internal override void RemoveSpellInUIList(Spell spell) { }

        internal override void FillMissingUIInfosFromPlayerSpells() { }
    }
}
