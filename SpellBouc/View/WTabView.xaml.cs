using System.Windows.Controls;
using SpellBouc.Model;
using SpellBouc.UISpells;
using SpellBouc.ViewModel;

namespace SpellBouc.View
{
    /// <summary>
    /// Logique d'interaction pour WizardSpellTab.xaml
    /// </summary>
    public partial class WTabView : UserControl
    {
        
        public WTabView()
        {
            InitializeComponent();
            this.DataContext = new WTabViewModel().WizardSpellTabList;
            this.wizardTab.SelectedIndex = 0;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
