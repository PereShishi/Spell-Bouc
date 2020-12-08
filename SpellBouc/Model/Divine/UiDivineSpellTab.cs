using SpellBouc.Model.Common;
using SpellBouc.UIContainers;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SpellBouc.Model.Divine
{
    public class UiDivineSpellTab: UiTab
    {
        private ObservableCollection<UiSpell> _spellList;
        private UiSpell _selectedSpell;

        /* Constructeur vide utilisé lorsqu'on créer une fenêtre vide */
        public UiDivineSpellTab(){}

        /* Créer une tab en fonction d'un niveau de sort */
        public UiDivineSpellTab(int lvl)
        {
            this.Lvl = lvl;
            SetSpellListFromDivineSpellBook();
            switch (Globals.SelectedSpellBook)
            {
                case ContainerType.PriestPlayerSpells:
                    MaxSpellsPerDay = Globals.AppPriestSpellBook.MaxSpellsByLvl[lvl];
                    break;
                case ContainerType.DruidPlayerSpells:
                    MaxSpellsPerDay = Globals.AppDruidSpellBook.MaxSpellsByLvl[lvl];
                    break;
            }
        }

        public ObservableCollection<UiSpell> SpellList
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
        public UiSpell SelectedSpell 
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
        public static ObservableCollection<UiDivineSpellTab> GetTabListFromDivineSpellBook()
        {
            ObservableCollection<UiDivineSpellTab> divineSpellTabList = new ObservableCollection<UiDivineSpellTab>();

            // Initialise les headers
            switch (Globals.SelectedSpellBook)
            {
                case ContainerType.PriestPlayerSpells:
                    for (int i = 0; i <= Globals.AppPriestSpellBook.SpellMaxFromPlayer; i++)
                    {
                        divineSpellTabList.Add(new UiDivineSpellTab { Lvl = i, MaxSpellsPerDay = Globals.AppPriestSpellBook.MaxSpellsByLvl[i] });
                    }
                    break;

                case ContainerType.DruidPlayerSpells:
                    for (int i = 0; i <= Globals.AppDruidSpellBook.SpellMaxFromPlayer; i++)
                    {
                        divineSpellTabList.Add(new UiDivineSpellTab { Lvl = i, MaxSpellsPerDay = Globals.AppDruidSpellBook.MaxSpellsByLvl[i] });
                    }
                    break;
            }

            // Initialise les Contents
            foreach (UiDivineSpellTab divineSpellTab in divineSpellTabList)
            {
                divineSpellTab.SetSpellListFromDivineSpellBook();
            }

            return divineSpellTabList;
        }

        /* Génère la liste des items de la tab à partir d'un DivineSpellBook => appel du model Item */
        private void SetSpellListFromDivineSpellBook()
        {
            ObservableCollection<UiSpell> returnedDivineItemList = new ObservableCollection<UiSpell>();
            UISpellContainer uiPlayerSpells;
            switch (Globals.SelectedSpellBook)
            {
                case ContainerType.PriestPlayerSpells:
                    uiPlayerSpells = Globals.AppPriestSpellBook.UIPlayerSpells;
                    break;
                case ContainerType.DruidPlayerSpells:
                    uiPlayerSpells = Globals.AppDruidSpellBook.UIPlayerSpells;
                    break;

                default:
                    uiPlayerSpells = null;
                    break;
            }

            foreach (UiSpell uispell in uiPlayerSpells)
            {
                if(uispell.Lvl == Lvl)
                {
                    returnedDivineItemList.Add((UiSpell)uispell);
                }
            }

            SpellList = returnedDivineItemList;
        }
    }
}
