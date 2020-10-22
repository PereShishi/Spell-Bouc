using SpellBouc.Model;
using SpellBouc.SpellBooks;
using SpellBouc.ViewModel.Commands;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SpellBouc.ViewModel
{
    public class WizardSpellTabViewModel
    {
        // Commands
        public SimpleCommand SimpleCommand { get; set; }

        // ObservableCollections
        public ObservableCollection<UiWizardSpellTab> WizardSpellTabList { get; set; } = new ObservableCollection<UiWizardSpellTab>();

        // Properties
        private readonly WizardSpellBook _wizardSpellBook;

        public WizardSpellTabViewModel()
        {
            this._wizardSpellBook = new WizardSpellBook();
            InitializeCommands();
            InitializeWizardSpellTabList();

            WizardSpellTabViewModel test = new WizardSpellTabViewModel(_wizardSpellBook);
            ObservableCollection<UiWizardSpellTab> list = test.WizardSpellTabList;

        }
    
        public WizardSpellTabViewModel(WizardSpellBook wizardSpellBook)
        {
            this._wizardSpellBook = wizardSpellBook;
            InitializeCommands();
            InitializeWizardSpellTabList();
        }

        /* Initialise tous les header des tabs */
        private void InitializeWizardSpellTabList()
        {
            // Retourne une liste de Tab
            WizardSpellTabList = UiWizardSpellTab.GetTabListFromWizardSpellBook(_wizardSpellBook);
        }

        /* Initialise les commandes */
        public void InitializeCommands()
        {
            this.SimpleCommand = new SimpleCommand(this);
        }
        public void SimpleMethod()
        {
            Debug.WriteLine("Hello Word");
        }
    }
}

