using SpellBouc.Model.Common;
using SpellBouc.Model.Divine;
using SpellBouc.UIContainers;
using SpellBouc.ViewModel.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace SpellBouc.ViewModel
{
    public class DTabViewModel : INotifyPropertyChanged
    {
        /* Event */
        public event PropertyChangedEventHandler PropertyChanged = (sender,e)=> { };
        
        private ObservableCollection<UiDivineSpellTab> _divineSpellTabList;
        private int _selectedIndex;
        private UiDivineSpellTab _selectedTab;

        public DTabViewModel()
        {
            InitializeCommands();
            DivineSpellTabList = new ObservableCollection<UiDivineSpellTab>();
            InitializeDivineSpellTabList();
        }

        /* Commandes */
        public DAddTabCommand TabPanelButtonClickCommand { get; set; }

        // ObservableCollections de UiWizardSpellTab lié à un evênement pour rafraichir l'IU à chaque changement du tableau
        public ObservableCollection<UiDivineSpellTab> DivineSpellTabList
        { 
            get { 
                return _divineSpellTabList; 
            }
            set
            {
                if (_divineSpellTabList == value)
                    return;
                _divineSpellTabList = value;

                PropertyChanged(this, new PropertyChangedEventArgs(nameof(DivineSpellTabList)));
            } 
        
        }
        public UiDivineSpellTab SelectedTab
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

        /* Initialise les commandes */
        public void InitializeCommands()
        {
            TabPanelButtonClickCommand = new DAddTabCommand(this);
        }

        /* Initialise tous les header des tabs */
        private void InitializeDivineSpellTabList()
        {
            // Retourne une liste de Tab
            DivineSpellTabList = UiDivineSpellTab.GetTabListFromDivineSpellBook();
        }

        /* Au click du boutton + on incrémente le nombre d'utilisation de sorts journaliers du lanceur de sort divin */
        public void IncrementSpellCount(int id)
        { 
            // TODO : on incrémente le nombre de sort journalier du lancer de sort divin
        }

        /* Au click du boutton - on décrémente le nombre d'utilisation de sorts journaliers du lanceur de sort divin */
        public void DecrementSpell(int id)
        {
            // TODO : on décrémente le nombre de sort journaliers du lanceur de sort divin
        }

        /* Au click du boutton Ajouter de la page ADD SPELL on ajoute le sort spécifié au spell book */
        public void AddSpell(int id)
        {
            UISpellContainer uiCompleteClassSpell;

            // Update le DivineSpellBook
            switch (Globals.SelectedSpellBook)
            {
                case ContainerType.PriestPlayerSpells:
                    Globals.AppPriestSpellBook.AddSpellInSpellBook(id);
                    uiCompleteClassSpell = Globals.AppPriestSpellBook.UICompleteClassSpells;
                    break;
                case ContainerType.DruidPlayerSpells:
                    Globals.AppDruidSpellBook.AddSpellInSpellBook(id);
                    uiCompleteClassSpell = Globals.AppDruidSpellBook.UICompleteClassSpells;
                    break;
                default:
                    uiCompleteClassSpell = null;
                    break;
            }
             
            // Check for UI duplication
            foreach (UiDivineSpellTab tab in DivineSpellTabList)
            {
                if(tab.SpellList == null)
                {
                    tab.SpellList = new ObservableCollection<UiSpell>();
                }
                foreach (UiSpell spellInTab in tab.SpellList)
                {
                    if (spellInTab.Id == id) return;
                }
            }

            // Update Ui 
            UiSpell spellToAdd = new UiSpell();
            foreach (UiSpell uiSpell in uiCompleteClassSpell)
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
                DivineSpellTabList[spellToAdd.Lvl].SpellList.Add(spellToAdd);
            }
        }

        /* Au click du boutton de supression on supprime le sort spécifié au spell book */
        public void RemoveSpell(int id)
        {
            switch (Globals.SelectedSpellBook)
            {
                case ContainerType.PriestPlayerSpells:
                    Globals.AppPriestSpellBook.RemoveSpellInSpellBook(id);
                    break;
                case ContainerType.DruidPlayerSpells:
                    Globals.AppDruidSpellBook.RemoveSpellInSpellBook(id);
                    break;
                default:
                    break;
            }

            // Update Ui 
            foreach(UiSpell spell in SelectedTab.SpellList)
            {
                if (id == spell.Id)
                {
                    SelectedTab.SpellList.Remove(spell);
                    return;
                }
            }
        }

        /* Ajoute un sort dans la Tab à partir du boutton Ajouter du menu d'ajout des sorts*/
        public void AddTab()
        {
            int lvl = 0;
            // Get le niveau max des table
            foreach(var tab in DivineSpellTabList)
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
            UiDivineSpellTab spellTabToAdd = new UiDivineSpellTab(lvl);
            DivineSpellTabList.Add(spellTabToAdd);
            SelectedIndex = lvl;
        }

        /* Update les sorts maxx par jour à partir du MaxSpellPerDayView */
        internal void UpdateMaxSpellPerDay(int[] maxSpellByLvl)
        {
            foreach(UiDivineSpellTab tab in DivineSpellTabList)
            {
                tab.MaxSpellsPerDay = maxSpellByLvl[tab.Lvl];
            }
        }

    }
}

