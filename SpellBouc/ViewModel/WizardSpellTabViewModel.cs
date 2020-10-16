using SpellBouc.Model;
using SpellBouc.UISpells;
using System.Collections.ObjectModel;


namespace SpellBouc.ViewModel
{
    internal class WizardSpellTabViewModel
    {
        public ObservableCollection<UiWizardSpellTab> WizardSpellTabList { get; set; } = new ObservableCollection<UiWizardSpellTab>();


        private readonly WizardSpellBook _wizardSpellBook;

        public WizardSpellTabViewModel(WizardSpellBook wizardSpellBook)
        {
            this._wizardSpellBook = wizardSpellBook;
            InitializeWizardSpellTabList();
        }

        /* Initialise tous les header des tabs */
        private void InitializeWizardSpellTabList()
        {
            // Retourne une liste de Tab
            WizardSpellTabList = UiWizardSpellTab.GetTabListFromWizardSpellBook(_wizardSpellBook);
        }
    }
}

