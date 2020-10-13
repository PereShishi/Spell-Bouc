using SpellBouc.UIClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SpellBouc.Model
{
    public class WizardSpellTab: Tab
    {
        public List<WizardItem> SpellList { get; set; }

        public static ObservableCollection<WizardSpellTab> GetTabListFromWizardSpellBook(WizardSpellBook wizardSpellBook)
        {
            ObservableCollection<WizardSpellTab> WizardSpellTabList = new ObservableCollection<WizardSpellTab>();

            // Initialise les headers
            for (int i = 0; i <= wizardSpellBook.MaxLvlSpell; i++)
            {
                WizardSpellTabList.Add(new WizardSpellTab { Lvl = "Niveau " + i });
            }

            // Initialise les Contents
            foreach (WizardSpellTab wizardSpellTab in WizardSpellTabList)
            {
                wizardSpellTab.GenerateSpellListFromWizardSpellBook(wizardSpellBook);
            }

            return WizardSpellTabList;
        }

        private void GenerateSpellListFromWizardSpellBook(WizardSpellBook wizardSpellBook)
        {
            //TODO: pour chaque sort du niveau de la WizardSpellTab on lui renvoie tous les sorts associés 
        }

    }
}
