using SpellBouc.UIClasses;
namespace SpellBouc
{
    /* Classe abstraite de livre de sort */
    class SpellBook
    {
        internal virtual SpellContainer PlayerSpells { get; set; }

        internal virtual SpellContainer CompleteClassSpell { get; set; }

        internal virtual void AddSpellInSpellBook(string name) { }

        internal virtual void RemoveSpellInSpellBook(string name) { }

        internal virtual void AddSpellInSpellBook(int id) { }

        internal virtual void RemoveSpellInSpellBook(int id) { }

        internal virtual void AddSpellInUIList(Spell spell, UIContainerType uiContainerType) { }

        internal virtual void RemoveSpellInUIList(Spell spell) { }

        internal virtual void FillMissingUIInfosFromPlayerSpells() { }

    }

}
