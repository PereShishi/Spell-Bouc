using SpellBouc.Model;
using SpellBouc.SpellBooks;
using SpellBouc.ViewModel.Commands;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SpellBouc.ViewModel
{
    public class WTabViewModel
    {
        // Commands
        public SimpleCommand AddSpellCommand { get; set; }

        // ObservableCollections
        public ObservableCollection<UiWizardSpellTab> WizardSpellTabList { get; set; } = new ObservableCollection<UiWizardSpellTab>();

        // Properties
        private readonly WizardSpellBook _wizardSpellBook;

        public WTabViewModel()
        {
            this._wizardSpellBook = new WizardSpellBook();
            InitializeCommands();
            InitializeWizardSpellTabList();
            Debug.WriteLine("TAB INITAILIZED !");

            //WizardSpellTabViewModel test = new WizardSpellTabViewModel(_wizardSpellBook);
            //ObservableCollection<UiWizardSpellTab> list = test.WizardSpellTabList;

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
            AddSpellCommand = new SimpleCommand(this);
        }
        public void test_cmd()
        {
            Debug.WriteLine("Binding");
        }

        public void SimpleMethod()
        {
            Debug.WriteLine("Hello Word");
        }
    }
}

