using SpellBouc.UISpells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SpellBouc.SpellBooks;

namespace SpellBouc.Model
{
    public class UiWizardSpellTab: UiTab
    {
        public List<UIWizardPlayerSpell> SpellList { get; set; }
        public UIWizardPlayerSpell SelectedSpell { get; set; }

        /* Génère une tab de sort (header + liste de sorts) à afficher dans l'interface utilisateur */
        public static ObservableCollection<UiWizardSpellTab> GetTabListFromWizardSpellBook()
        {
            ObservableCollection<UiWizardSpellTab> WizardSpellTabList = new ObservableCollection<UiWizardSpellTab>();

            // Initialise les headers
            for (int i = 0; i <= Globals.AppWizardSpellBook.MaxLvlSpell; i++)
            {
                WizardSpellTabList.Add(new UiWizardSpellTab { Lvl = i });
            }

            // Initialise les Contents
            foreach (UiWizardSpellTab wizardSpellTab in WizardSpellTabList)
            {
                wizardSpellTab.SetSpellListFromWizardSpellBook();
            }

            return WizardSpellTabList;
        }

        /* Génère la liste des item de la tab à partir d'un WizardSpellBook => appel du model Item */
        private void SetSpellListFromWizardSpellBook()
        {
            List<UIWizardPlayerSpell> returnedWizardItemList = new List<UIWizardPlayerSpell>();

            foreach (UiSpell uispell in Globals.AppWizardSpellBook.UIPlayerSpells)
            {
                if(uispell.Lvl == Lvl)
                {
                    returnedWizardItemList.Add((UIWizardPlayerSpell)uispell);
                }
            }

            SpellList = returnedWizardItemList;
        }

    }
}
