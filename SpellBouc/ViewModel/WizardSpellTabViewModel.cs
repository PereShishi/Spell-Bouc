using SpellBouc.Model;
using SpellBouc.UIClasses;
using System.Collections.ObjectModel;


namespace SpellBouc.ViewModel
{
    internal class WizardSpellTabViewModel : ViewModel
    {
        public ObservableCollection<WizardSpellTab> WizardSpellTabList { get; set; } = new ObservableCollection<WizardSpellTab>();


        private readonly WizardSpellBook _wizardSpellBook;

        public WizardSpellTabViewModel(WizardSpellBook wizardSpellBook)
        {
            this._wizardSpellBook = wizardSpellBook;
            InitializeWizardSpellTabList();
        }

        /* Initialise tous les header des tabs */
        private void InitializeWizardSpellTabList()
        {
            // Retourne une liste 
            WizardSpellTabList = WizardSpellTab.GetTabListFromWizardSpellBook(_wizardSpellBook);
        }
    }


}

