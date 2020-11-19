using SpellBouc.UISpells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

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
                case ContainerType.DruidPCompleteSpells:
                    break;
                default:
                    break;
            }
        }
    }
}
