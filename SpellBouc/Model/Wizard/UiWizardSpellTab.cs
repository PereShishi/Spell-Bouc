using SpellBouc.Model.Common;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SpellBouc.Model.Wizard
{
    public class UiWizardSpellTab: UiTab
    {
        /* Event */
        private ObservableCollection<UIWizardPlayerSpell> _spellList;
        private UIWizardPlayerSpell _selectedSpell;

        /* Constructeur vide utilisé lorsqu'on créer une fenêtre vide */
        public UiWizardSpellTab(){}

        /* Créer une tab en fonction d'un niveau de sort */
        public UiWizardSpellTab(int lvl)
        {
            this.Lvl = lvl;
            SetSpellListFromWizardSpellBook();
            UpdateTotalSpellCount();
            MaxSpellsPerDay = Globals.AppWizardSpellBook.MaxSpellsByLvl[lvl];
        }

        public ObservableCollection<UIWizardPlayerSpell> SpellList
        {
            get
            {
                return _spellList;
            }
            set
            {
                if (_spellList == value)
                    return;
                _spellList = value;
                OnPropertyChanged(nameof(SpellList));

            }
        }
       
        /* Sort sélectionné */
        public UIWizardPlayerSpell SelectedSpell 
        {
            get
            {
                return _selectedSpell;
            }
            set
            {
                if (_selectedSpell == value)
                    return;
                _selectedSpell = value;
                if(_selectedSpell != null)
                {
                    IsVisible = true;
                }
                OnPropertyChanged(nameof(SelectedSpell));
            }

        }

        /* Génère une tab de sort (header + liste de sorts) à afficher dans l'interface utilisateur */
        public static ObservableCollection<UiWizardSpellTab> GetTabListFromWizardSpellBook()
        {
            ObservableCollection<UiWizardSpellTab> wizardSpellTabList = new ObservableCollection<UiWizardSpellTab>();

            // Initialise les headers
            for (int i = 0; i <= Globals.AppWizardSpellBook.SpellMaxFromPlayer; i++)
            {
                wizardSpellTabList.Add(new UiWizardSpellTab (i));
            }

            return wizardSpellTabList;
        }

        /* Génère la liste des item de la tab à partir d'un WizardSpellBook => appel du model Item */
        private void SetSpellListFromWizardSpellBook()
        {
            ObservableCollection<UIWizardPlayerSpell> returnedWizardItemList = new ObservableCollection<UIWizardPlayerSpell>();

            foreach (UiSpell uispell in Globals.AppWizardSpellBook.UIPlayerSpells)
            {
                if(uispell.Lvl == Lvl)
                {
                    returnedWizardItemList.Add((UIWizardPlayerSpell)uispell);
                }
            }

            SpellList = returnedWizardItemList;
        }

        /* Met à jour TotalSpellCountdépendament du PlayerSpellCount pour chaque sort de la SpellList */
        private void UpdateTotalSpellCount()
        {
            int totalSpellCount = 0;
            foreach (UIWizardPlayerSpell spell in SpellList)
            {
                totalSpellCount += spell.PlayerSpellCount;
            }
            TotalSpellCount = totalSpellCount;
        }

    }
}
