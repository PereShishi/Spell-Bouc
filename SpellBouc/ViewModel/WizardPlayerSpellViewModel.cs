using SpellBouc.Model;
using SpellBouc.UISpells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SpellBouc.ViewModel
{
    internal class WizardPlayerSpellViewModel
    {
        public ObservableCollection<UIWizardPlayerSpell> WizardSpellList { get; set; } = new ObservableCollection<UIWizardPlayerSpell>();


        private readonly WizardSpellBook _wizardSpellBook;

        public WizardPlayerSpellViewModel(WizardSpellBook wizardSpellBook)
        {
            this._wizardSpellBook = wizardSpellBook;
            InitializeWizardSpellTabList();
        }

        /* Initialise tous les header des tabs */
        private void InitializeWizardSpellTabList()
        {
            // Retourne une liste de Sorts à afficher
            ObservableCollection<UIWizardPlayerSpell> test = new ObservableCollection<UIWizardPlayerSpell>();
            foreach (var UiSpell in _wizardSpellBook.UIPlayerSpells)
            {
                test.Add((UIWizardPlayerSpell)UiSpell);
            }
            WizardSpellList = test;
            //WizardSpellList = UIWizardPlayerSpell.GetSpellListFromWizardSpellBook(_wizardSpellBook);
        }
    }
}
