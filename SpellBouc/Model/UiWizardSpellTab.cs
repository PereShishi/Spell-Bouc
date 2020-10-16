using SpellBouc.UISpells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SpellBouc.Model
{
    public class UiWizardSpellTab: UiTab
    {
        internal List<UIWizardPlayerSpell> SpellList { get; set; }

        /* Génère une tab de sort (header + liste de sorts) à afficher dans l'interface utilisateur */
        public static ObservableCollection<UiWizardSpellTab> GetTabListFromWizardSpellBook(WizardSpellBook wizardSpellBook)
        {
            ObservableCollection<UiWizardSpellTab> WizardSpellTabList = new ObservableCollection<UiWizardSpellTab>();

            // Initialise les headers
            for (int i = 0; i <= wizardSpellBook.MaxLvlSpell; i++)
            {
                WizardSpellTabList.Add(new UiWizardSpellTab { Lvl = i });
            }

            // Initialise les Contents
            foreach (UiWizardSpellTab wizardSpellTab in WizardSpellTabList)
            {
                wizardSpellTab.SetSpellListFromWizardSpellBook(wizardSpellBook);
            }

            return WizardSpellTabList;
        }

        /* Génère la liste des item de la tab à partir d'un WizardSpellBook => appel du model Item */
        private void SetSpellListFromWizardSpellBook(WizardSpellBook wizardSpellBook)
        {
            List<UIWizardPlayerSpell> returnedWizardItemList = new List<UIWizardPlayerSpell>();

            foreach (UiSpell uispell in wizardSpellBook.UIPlayerSpells)
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
