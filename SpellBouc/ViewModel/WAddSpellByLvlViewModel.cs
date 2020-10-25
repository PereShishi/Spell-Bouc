using SpellBouc.Model;
using SpellBouc.UISpells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text;

namespace SpellBouc.ViewModel
{
    public class WAddSpellByLvlViewModel
    {
        // ObservableCollections
        public ObservableCollection<UIWizardPlayerSpell> AddSpellList { get; set; } = new ObservableCollection<UIWizardPlayerSpell>();
        public UIWizardPlayerSpell SelectedSpell { get; set; }

        private int PageLvl {get; set;}

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
