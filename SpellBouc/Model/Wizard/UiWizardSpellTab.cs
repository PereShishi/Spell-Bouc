using SpellBouc.UISpells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SpellBouc.SpellBooks;
using System.ComponentModel;
using SQLitePCL;

namespace SpellBouc.Model
{
    public class UiWizardSpellTab: UiTab,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        private ObservableCollection<UIWizardPlayerSpell> _spellList;
        private UIWizardPlayerSpell _selectedSpell;
        private int _totalSpellCount = 0;
        private bool _isVisible = false;

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
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SpellList)));


            }
        }
        private int _maxSpellsPerDay;
        /* Max de sorts par niveau (affiché dans les headers) */
        public int MaxSpellsPerDay
        {
            get
            {
                return _maxSpellsPerDay;
            }
            set
            {
                if (_maxSpellsPerDay == value)
                    return;
                _maxSpellsPerDay = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(MaxSpellsPerDay)));
            }
        }


        /* Total de sorts réstant au joueur par niveau (affiché dans les headers) */
        public int TotalSpellCount
        {
            get
            {
                return _totalSpellCount;
            }
            set
            {
                if (_totalSpellCount == value)
                    return;
                _totalSpellCount = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(TotalSpellCount)));
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
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedSpell)));
            }

        }

        /* Sort sélectionné */
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                if (_isVisible == value)
                    return;
                _isVisible = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(IsVisible)));
            }

        }

        /* Créer une tab en fonction d'un niveau de sort */
        public UiWizardSpellTab(int lvl)
        {
            this.Lvl = lvl;
            SetSpellListFromWizardSpellBook();
            UpdateTotalSpellCount();
            MaxSpellsPerDay = Globals.AppWizardSpellBook.MaxSpellsByLvl[lvl];
        }

        /* Constructeur vide utilisé lorsqu'on créer une fenêtre vide */
        public UiWizardSpellTab()
        {
        }

        /* Génère une tab de sort (header + liste de sorts) à afficher dans l'interface utilisateur */
        public static ObservableCollection<UiWizardSpellTab> GetTabListFromWizardSpellBook()
        {
            ObservableCollection<UiWizardSpellTab> WizardSpellTabList = new ObservableCollection<UiWizardSpellTab>();

            // Initialise les headers
            for (int i = 0; i <= Globals.AppWizardSpellBook.SpellMaxFromPlayer; i++)
            {
                WizardSpellTabList.Add(new UiWizardSpellTab { Lvl = i , MaxSpellsPerDay  = Globals.AppWizardSpellBook.MaxSpellsByLvl[i] });
            }

            // Initialise les Contents
            foreach (UiWizardSpellTab wizardSpellTab in WizardSpellTabList)
            {
                wizardSpellTab.SetSpellListFromWizardSpellBook();
                wizardSpellTab.UpdateTotalSpellCount();
            }

            return WizardSpellTabList;
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

        /* Update TotalSpellCount depending on the PlayerSpellCount for each spell in SpellList*/
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
