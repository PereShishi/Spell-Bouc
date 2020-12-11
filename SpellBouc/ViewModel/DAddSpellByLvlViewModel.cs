using SpellBouc.Model.Common;
using SpellBouc.Model.Wizard;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace SpellBouc.ViewModel
{
    public class DAddSpellByLvlViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<UiSpell> AddSpellList { get; set; } = new ObservableCollection<UiSpell>();
        private UiSpell _selectedSpell;
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        public int PageLvl { get; set; }

        public DAddSpellByLvlViewModel()
        {
            PageLvl = 0;
            GenerateSPellList();
            SelectedSpell = (UiSpell)AddSpellList[0];
        }

        public DAddSpellByLvlViewModel(int lvl)
        {
            PageLvl = lvl;
            GenerateSPellList();
            SelectedSpell = (UiSpell)AddSpellList[0];
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
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedSpell)));
            }
        }

        private void GenerateSPellList()
        {
            switch (Globals.SelectedSpellBook)
            {
                case ContainerType.PriestPlayerSpells:
                    AddSpellList = Globals.AppPriestSpellBook.GetUiSpellListByLvl(PageLvl);
                    break;
                case ContainerType.DruidPlayerSpells:
                    AddSpellList = Globals.AppDruidSpellBook.GetUiSpellListByLvl(PageLvl);
                    break;
                default:
                    break;
            }
        }
    }
}
