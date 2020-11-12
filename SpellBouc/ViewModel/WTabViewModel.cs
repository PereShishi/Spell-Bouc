using SpellBouc.Model;
using SpellBouc.UISpells;
using SpellBouc.View;
using SpellBouc.ViewModel.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace SpellBouc.ViewModel
{
    public class WTabViewModel : INotifyPropertyChanged
    {
        /* Event */
        public event PropertyChangedEventHandler PropertyChanged = (sender,e)=> { };

        private ObservableCollection<UiWizardSpellTab> _wizardSpellTabList;
        private int _selectedIndex;
        private UiWizardSpellTab _selectedTab;

        /* Commandes */
        public WSimpleCommand IncrementSpellCountCommand { get; set; }
        public WSimpleCommand DecrementSpellCountCommand { get; set; }
        public WAddTabCommand TabPanelButtonClickCommand { get; set; }

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
        public UiWizardSpellTab SelectedTab
        {
            get
            {
                return _selectedTab;
            }
            set
            {
                if (_selectedTab == value)
                    return;
                _selectedTab = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedTab)));
            }
        }

        public int SelectedIndex
        {
            get
            {
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
            IncrementSpellCountCommand = new WSimpleCommand(this, WSimpleCommandType.IncrementSpellCount);
            DecrementSpellCountCommand = new WSimpleCommand(this, WSimpleCommandType.DecrementSpellCount);
            TabPanelButtonClickCommand = new WAddTabCommand(this);
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

            int totalSpellCount = 0;
            foreach (var spellInList in SelectedTab.SpellList)
            {
                foreach(UIWizardPlayerSpell uiSpell in Globals.AppWizardSpellBook.UIPlayerSpells)
                {
                    if(spellInList.Id == uiSpell.Id)
                    {
                        spellInList.PlayerSpellCount = uiSpell.PlayerSpellCount;
                        totalSpellCount += spellInList.PlayerSpellCount;
                        continue;
                    }
                }
            }
            // Update le total des sorts dans le header 
            SelectedTab.TotalSpellCount = totalSpellCount;
        }

        /* Au click du boutton - on décrémente le nombre d'utilisation journalière d'un sort spécifique */
        public void DecrementSpell(int id)
        {
            Globals.AppWizardSpellBook.DecrementWizardPlayerSpell(id);

            int totalSpellCount = 0;
            foreach (var spellInList in SelectedTab.SpellList)
            {
                foreach (UIWizardPlayerSpell uiSpell in Globals.AppWizardSpellBook.UIPlayerSpells)
                {
                    if (spellInList.Id == uiSpell.Id)
                    {
                        spellInList.PlayerSpellCount = uiSpell.PlayerSpellCount;
                        totalSpellCount += spellInList.PlayerSpellCount;
                        continue;
                    }
                }
            }
            // Update le total des sorts dans le header 
            SelectedTab.TotalSpellCount = totalSpellCount;
        }

        /* Au click du boutton Ajouter de la page ADD SPELL on ajoute le sort spécifié au spell book */
        public void AddSpell(int id)
        {
            // Update WizardSpellBook
            Globals.AppWizardSpellBook.AddSpellInSpellBook(id);


            // Check for UI duplication
            foreach (UiWizardSpellTab tab in WizardSpellTabList)
            {
                if(tab.SpellList == null)
                {
                    tab.SpellList = new ObservableCollection<UIWizardPlayerSpell>();
                }
                foreach (UIWizardPlayerSpell spellInTab in tab.SpellList)
                {
                    if (spellInTab.Id == id) return;
                }
            }

            // Update Ui 
            UIWizardPlayerSpell spellToAdd = new UIWizardPlayerSpell();
            foreach (UIWizardPlayerSpell uiSpell in Globals.AppWizardSpellBook.UICompleteClassSpells)
            {
                if (uiSpell.Id == id)
                {
                    spellToAdd = uiSpell;
                    break;
                }
            }
            if (spellToAdd.Lvl == SelectedTab.Lvl)
                SelectedTab.SpellList.Add(spellToAdd);

            else
            {
                WizardSpellTabList[spellToAdd.Lvl].SpellList.Add(spellToAdd);
            }
        }

        /* Au click du boutton de supression on ajoute le sort spécifié au spell book */
        public void RemoveSpell(int id)
        {
            Globals.AppWizardSpellBook.RemoveSpellInSpellBook(id);

            // Update Ui 
            foreach(UIWizardPlayerSpell spell in SelectedTab.SpellList)
            {
                if (id == spell.Id)
                {
                    SelectedTab.SpellList.Remove(spell);
                    SelectedTab.TotalSpellCount -= spell.PlayerSpellCount;
                    return;
                }
            }
        }

        /* Ajoute un sort dans la Tab à partir du boutton Ajouter du menu d'ajout des sorts*/
        public void AddTab()
        {
            int lvl = 0;
            // Get le niveau max des table
            foreach(var tab in WizardSpellTabList)
            {
                if (tab.Lvl > lvl)
                {
                    lvl = tab.Lvl;
                }
            }
            lvl++;

            // Bloque le nombre d'onglets maximums à 5
            if (lvl == 5)
            {
                return;
            }

            // Ajoute une nouvelle Tabe et la selectionne
            UiWizardSpellTab spellTabToAdd = new UiWizardSpellTab(lvl);
            WizardSpellTabList.Add(spellTabToAdd);
            SelectedIndex = lvl;
        }

        /* Update les sorts maxx par jour à partir du MaxSpellPerDayView */
        internal void UpdateMaxSpellPerDay(int[] maxSpellByLvl)
        {
            foreach(UiWizardSpellTab tab in WizardSpellTabList)
            {
                tab.MaxSpellsPerDay = maxSpellByLvl[tab.Lvl];
            }
        }

    }
}

