using SpellBouc.Model.Ao;
using SpellBouc.Model.Common;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SpellBouc.ViewModel
{
    public class AoSpellBookViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<AoSpellModel> AoSpellList { get; set; } = new ObservableCollection<AoSpellModel>();
        private AoSpellModel _selectedSpell;
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /* Sort sélectionné */
        public AoSpellModel SelectedSpell
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

        public AoSpellBookViewModel(ContainerType type)
        {
            switch (type)
            {
                case ContainerType.WizardPlayerSpells:
                    foreach (UiSpell uiSpell in Globals.AppWizardSpellBook.UICompleteClassSpells)
                    {
                        AoSpellModel aoSpell = new AoSpellModel(uiSpell);
                        AoSpellList.Add(aoSpell);
                    }
                    SelectedSpell = (AoSpellModel)AoSpellList[0];
                    break;
                case ContainerType.PriestPlayerSpells:
                    foreach (UiSpell uiSpell in Globals.AppPriestSpellBook.UICompleteClassSpells)
                    {
                        AoSpellModel aoSpell = new AoSpellModel(uiSpell);
                        AoSpellList.Add(aoSpell);
                    }
                    SelectedSpell = (AoSpellModel)AoSpellList[0];
                    break;
                case ContainerType.DruidPlayerSpells:
                    foreach (UiSpell uiSpell in Globals.AppDruidSpellBook.UICompleteClassSpells)
                    {
                        AoSpellModel aoSpell = new AoSpellModel(uiSpell);
                        AoSpellList.Add(aoSpell);
                    }
                    SelectedSpell = (AoSpellModel)AoSpellList[0];
                    break;
                default:
                    break;
            }
        }
    }
}
