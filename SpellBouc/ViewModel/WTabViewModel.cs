using SpellBouc.Model;
using SpellBouc.SpellBooks;
using SpellBouc.UISpells;
using SpellBouc.ViewModel.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace SpellBouc.ViewModel
{
    public class WTabViewModel : INotifyPropertyChanged
    {
        /* Event */
        public event PropertyChangedEventHandler PropertyChanged = (sender,e)=> { };

        private ObservableCollection<UiWizardSpellTab> _wizardSpellTabList;
        private int _selectedIndex;

        // Commands
        public WSimpleCommand AddSpellCommand { get; set; }
        public WSimpleCommand RemoveSpellCommand { get; set; }
        public WSimpleCommand IncrementSpellCountCommand { get; set; }
        public WSimpleCommand DecrementSpellCountCommand { get; set; }

        // ObservableCollections de UiWizardSpellTab lié à un evênement pour rafraichir l'IU à chaque changement du tableau
        public ObservableCollection<UiWizardSpellTab> WizardSpellTabList 
        { 
            get { 
                return _wizardSpellTabList; 
            }
            set
            {
                if (_wizardSpellTabList == value)
                    return;
                _wizardSpellTabList = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(WizardSpellTabList)));
            } 
        
        }
        public UiWizardSpellTab SelectedTab { get; set; }
        public int SelectedIndex 
        { 
            get { 
                return _selectedIndex; 
            }
            set
            {
                if (_selectedIndex == value)
                    return;
                _selectedIndex = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedIndex)));
            } 
        }

        public WTabViewModel()
        {     
            InitializeCommands();
            WizardSpellTabList = new ObservableCollection<UiWizardSpellTab>();
            InitializeWizardSpellTabList();
        }

        /* Initialise les commandes */
        public void InitializeCommands()
        {
            AddSpellCommand = new WSimpleCommand(this, WSimpleCommandType.AddSpell);
            RemoveSpellCommand = new WSimpleCommand(this, WSimpleCommandType.RemoveSpell);
            IncrementSpellCountCommand = new WSimpleCommand(this, WSimpleCommandType.IncrementSpellCount);
            DecrementSpellCountCommand = new WSimpleCommand(this, WSimpleCommandType.DecrementSpellCount);

        }

        /* Initialise tous les header des tabs */
        private void InitializeWizardSpellTabList()
        {
            // Retourne une liste de Tab
            WizardSpellTabList = UiWizardSpellTab.GetTabListFromWizardSpellBook();
        }

        /* Au click du boutton + on incrément le nombre d'utilisation journalière d'un sort spécifique */
        public void IncrementSpellCount(int id)
        {
            Globals.AppWizardSpellBook.IncrementWizardPlayerSpell(id);

            //TODO TO INMPLEMENTE PROPERLY AFTER TESTING
            foreach (var spellinList in WizardSpellTabList[SelectedIndex].SpellList)
            {
                foreach(UIWizardPlayerSpell uiSpell in Globals.AppWizardSpellBook.UIPlayerSpells)
                {
                    if(spellinList.Id == uiSpell.Id)
                    {
                        spellinList.PlayerSpellCount = uiSpell.PlayerSpellCount;
                        continue;
                    }
                }
            }
        }

        /* Au click du boutton - on décrémente le nombre d'utilisation journalière d'un sort spécifique */
        public void DecrementSpell(int id)
        {
            Globals.AppWizardSpellBook.DecrementWizardPlayerSpell(id);
        }

        /* Au click du boutton Ajouter de la page ADD/REMOVE SPELL on ajoute le sort spécifié au spell book */
        public void AddSpell(int id)
        {
            Globals.AppWizardSpellBook.AddSpellInSpellBook(id);
        }

        /* Au click du boutton Supprimer de la page ADD/REMOVE SPELL on ajoute le sort spécifié au spell book */
        public void RemoveSpell(int id)
        {
            Globals.AppWizardSpellBook.RemoveSpellInSpellBook(id);
        }


    }
}

