using SpellBouc.Model.Common;
using SpellBouc.UIContainers;
using System.Collections.ObjectModel;

namespace SpellBouc.SpellBooks
{
    /* Classe abstraite de livre de sort */
    public class SpellBook
    {
        internal virtual SpellContainer PlayerSpells { get; set; }

        internal virtual SpellContainer CompleteClassSpells { get; set; }

        internal virtual UISpellContainer UIPlayerSpells { get; set; }

        internal virtual  UISpellContainer UICompleteClassSpells { get; set; }

        internal int SpellMaxFromPlayer { get; set; }

        internal int[] PlayerSpellsByLvl { get; set; }
        internal int[] MaxSpellsByLvl { get; set; }

        internal virtual void AddSpellInSpellBook(string name) { }

        internal virtual void RemoveSpellInSpellBook(string name) { }

        internal virtual void AddSpellInSpellBook(int id) { }

        internal virtual void RemoveSpellInSpellBook(int id) { }

        internal virtual void AddSpellInUIList(Spell spell, UIContainerType uiContainerType) { }

        internal virtual void RemoveSpellInUIList(Spell spell) { }

        internal virtual void FillMissingUIInfosFromPlayerSpells() { }

        internal virtual void UpdateSpellNumberByLvl() {}

        internal virtual void SetMaxSpellNumberByLvl() { }

        internal virtual void UpdateMaxSpellNumberByLvl(int[] maxSpellToUpdate) { }

        /* Récupère le niveau le plus haut du sort du joueur */
        internal void UpdateMaxLvlSpell()
        {
            var maxSpell = 0;
            foreach( Spell spell in PlayerSpells)
            {
                if (spell.Lvl > maxSpell) maxSpell = spell.Lvl;
            }
            SpellMaxFromPlayer = maxSpell;
        }

        /* Créer une liste de UISpells à partir de la liste complète des sorts de la classe du joueur */
        internal void InitUICompleteClassSpells(UIContainerType uIContainerType)
        {
            foreach (Spell spell in CompleteClassSpells)
            {
                var spellToAdd = UICompleteClassSpells.CreateUISpellFromSpell(spell, uIContainerType);
                UICompleteClassSpells.AddUiSpell(spellToAdd);
            }
        }

        /* Met à jour les membres isAdable de UICompleteClassSpells en fonctions des sorts présents */
        internal void UpdateUICompleteClassSpell()
        {
            foreach (Spell playerSpell in PlayerSpells)
            {
                foreach (UiSpell finalSpell in UICompleteClassSpells)
                {
                    if (playerSpell.Id == finalSpell.Id)
                    {
                        finalSpell.IsAddable = false;
                        continue;
                    }
                    else
                    {
                        finalSpell.IsAddable = true;
                        continue;
                    }
                }
            }
        }

        /* Retourne une liste de ObservableCollection<UIWizardPlayerSpell> qui sera affichée les pages d'ajout de sort */
        internal ObservableCollection<UiSpell> GetUiSpellListByLvl(int pageLvl)
        {
            ObservableCollection<UiSpell> returnList = new ObservableCollection<UiSpell>();
            foreach (UiSpell dUiSpell in UICompleteClassSpells)
            {
                if (dUiSpell.Lvl == pageLvl)
                {
                    returnList.Add(dUiSpell);
                }
            }
            return returnList;
        }

        /* Retourne une liste de ObservableCollection<UIWizardPlayerSpell> qui sera affichée dans le livre de sort d'AO */
        internal ObservableCollection<UiSpell> GetCompleteUiSpellList()
        {
            ObservableCollection<UiSpell> returnList = new ObservableCollection<UiSpell>();
            foreach (UiSpell wUiSpell in UICompleteClassSpells)
            {

                returnList.Add(wUiSpell);

            }
            return returnList;
        }
    }
}