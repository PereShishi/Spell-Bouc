﻿using SpellBouc.Model;
using SpellBouc.SpellBooks;
using SpellBouc.UISpells;
using SpellBouc.ViewModel.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace SpellBouc.ViewModel
{
    public class WTabViewModel : INotifyPropertyChanged
    {
        /* Event */
        public event PropertyChangedEventHandler PropertyChanged = (sender,e)=> { };

        private ObservableCollection<UiWizardSpellTab> _wizardSpellTabList;
        private int _selectedIndex;
        private UiWizardSpellTab _selectedTab;

        // Commands

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

            //TODO TO INMPLEMENT PROPERLY AFTER TESTING
            foreach (var spellInList in SelectedTab.SpellList)
            {
                foreach(UIWizardPlayerSpell uiSpell in Globals.AppWizardSpellBook.UIPlayerSpells)
                {
                    if(spellInList.Id == uiSpell.Id)
                    {
                        spellInList.PlayerSpellCount = uiSpell.PlayerSpellCount;
                        continue;
                    }
                }
            }
        }

        /* Au click du boutton - on décrémente le nombre d'utilisation journalière d'un sort spécifique */
        public void DecrementSpell(int id)
        {
            Globals.AppWizardSpellBook.DecrementWizardPlayerSpell(id);

            //TODO TO INMPLEMENT PROPERLY AFTER TESTING
            foreach (var spellInList in SelectedTab.SpellList)
            {
                foreach (UIWizardPlayerSpell uiSpell in Globals.AppWizardSpellBook.UIPlayerSpells)
                {
                    if (spellInList.Id == uiSpell.Id)
                    {
                        spellInList.PlayerSpellCount = uiSpell.PlayerSpellCount;
                        continue;
                    }
                }
            }
        }

        /* Au click du boutton Ajouter de la page ADD/REMOVE SPELL on ajoute le sort spécifié au spell book */
        public void AddSpell(int id)
        {
            //TODO TO INMPLEMENT PROPERLY AFTER TESTING
            // Update WizardSpellBook
            Globals.AppWizardSpellBook.AddSpellInSpellBook(id);

            // Check for UI duplication
            foreach (UiWizardSpellTab tab in WizardSpellTabList)
            {
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

        /* Au click du boutton Supprimer de la page ADD/REMOVE SPELL on ajoute le sort spécifié au spell book */
        public void RemoveSpell(int id)
        {
            Globals.AppWizardSpellBook.RemoveSpellInSpellBook(id);

            //TODO TO INMPLEMENT PROPERLY AFTER TESTING

            // Update Ui 
            int lvlOfSpellToDelete = 0;
            foreach (UIWizardPlayerSpell uiSpell in Globals.AppWizardSpellBook.UICompleteClassSpells)
            {
                if (uiSpell.Id == id)
                {
                    lvlOfSpellToDelete = uiSpell.Lvl;
                    break;
                }

            }
            if (lvlOfSpellToDelete == SelectedTab.Lvl)
            {
                foreach(UIWizardPlayerSpell spell in SelectedTab.SpellList)
                {
                    if (id == spell.Id)
                    {
                        SelectedTab.SpellList.Remove(spell);
                        return;
                    }
                }
               
            }      
            else
            {
                foreach (UIWizardPlayerSpell spell in WizardSpellTabList[lvlOfSpellToDelete].SpellList)
                {
                    if (id == spell.Id)
                    {
                        SelectedTab.SpellList.Remove(spell);
                        return;
                    }
                }
            }

        }
    }
}

