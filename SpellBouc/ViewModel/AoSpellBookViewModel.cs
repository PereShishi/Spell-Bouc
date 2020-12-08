﻿using SpellBouc.Model.Common;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SpellBouc.ViewModel
{
    public class AoSpellBookViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<UiSpell> SpellList { get; set; } = new ObservableCollection<UiSpell>();
        private UiSpell _selectedSpell;
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

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
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedSpell)));
            }

        }

        public AoSpellBookViewModel(ContainerType type = ContainerType.WizardCompleteSpells)
        {
            switch (type)
            {
                case ContainerType.WizardCompleteSpells:
                    SpellList = Globals.AppWizardSpellBook.GetCompleteUiSpellList();
                    SelectedSpell = (UiSpell)SpellList[0];
                    break;
                case ContainerType.PriestCompleteSpells:
                    break;
                case ContainerType.DruidCompleteSpells:
                    break;
                default:
                    break;
            }
        }
    }
}
