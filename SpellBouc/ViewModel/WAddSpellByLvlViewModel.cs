using SpellBouc.UISpells;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace SpellBouc.ViewModel
{
    public class WAddSpellByLvlViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<UIWizardPlayerSpell> AddSpellList { get; set; } = new ObservableCollection<UIWizardPlayerSpell>();
        private UIWizardPlayerSpell _selectedSpell;
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

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
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedSpell)));
            }

        }

        public int PageLvl {get; set;}

        public WAddSpellByLvlViewModel()
        {
            PageLvl = 0;
            GenerateSPellList();
            SelectedSpell = (UIWizardPlayerSpell)AddSpellList[0]; 
        }

        public WAddSpellByLvlViewModel(int lvl)
        {
            PageLvl = lvl;
            GenerateSPellList();
            SelectedSpell = (UIWizardPlayerSpell)AddSpellList[0];
        }

        private void GenerateSPellList()
        {
            AddSpellList = Globals.AppWizardSpellBook.GetUiSpellListByLvl(PageLvl);
            Console.WriteLine("break");
        }
    }


}
