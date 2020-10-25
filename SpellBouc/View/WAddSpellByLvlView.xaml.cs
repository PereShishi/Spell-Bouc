using SpellBouc.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpellBouc.View
{
    /// <summary>
    /// Logique d'interaction pour WAddSpellByLvlView.xaml
    /// </summary>
    public partial class WAddSpellByLvlView : Window      
    {
        WAddSpellByLvlViewModel PageVM { get; set; }

        public WAddSpellByLvlView()
        {
            InitializeComponent();
            PageVM = new WAddSpellByLvlViewModel();
            this.DataContext = PageVM;
            this.testList.SelectedItem = 0;

        }
        public WAddSpellByLvlView(int lvl)
        {
            InitializeComponent();
            this.DataContext = new WAddSpellByLvlViewModel(lvl);
            this.testList.SelectedItem = 0;
        }
    }
}
