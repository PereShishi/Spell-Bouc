using SpellBouc.UISpells;

namespace SpellBouc
{
    /* Clase de livre de sort de mage (non utilisée en V1) */
    class PriestSpellBook : SpellBook
    {

        internal override SpellContainer PlayerSpells { get; set; }

        internal override SpellContainer CompleteClassSpells { get; set; }


        internal PriestSpellBook()
        {
            PlayerSpells = new SpellContainer(ContainerType.PriestPlayerSpells);
            CompleteClassSpells = new SpellContainer(ContainerType.PriestCompleteSpells);
        }

        internal override void AddSpellInSpellBook(string name) { }

        internal override void RemoveSpellInSpellBook(string name) { }

        internal override void AddSpellInSpellBook(int id) { }

        internal override void RemoveSpellInSpellBook(int id) { }

        internal override void AddSpellInUIList(Spell spell, UIContainerType uiContainerType) { }

        internal override void RemoveSpellInUIList(Spell spell) { }

        internal override void FillMissingUIInfosFromPlayerSpells() { }

        /* Récupère le nombre de sorts par niveau */
        internal override void UpdateSpellNumberByLvl()
        {

            // Set le tableur:
            for (int i = 0; i < MaxLvlSpell + 1; i++)
            {
                SpellNumberByLvl[i] = 0;
            }

            // Compte  
            foreach (Spell spell in PlayerSpells)
            {
                SpellNumberByLvl[spell.Lvl]++;
            }
        }
    }
}
