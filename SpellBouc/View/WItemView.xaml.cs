using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SpellBouc.View
{
    /// <summary>
    /// Logique d'interaction pour WizardSpellItemView.xaml
    /// </summary>
    public partial class WItemView : UserControl
    {
        public WItemView()
        {
            InitializeComponent();
        }

        /* Fonction qui appele le VM WTabViewModel d'où a été crée la page, pour lui faire supprimer un sort */
        private void RemoveSpell(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).Tag;
            WWindowView mainWWindowView = Application.Current.Windows.OfType<WWindowView>().FirstOrDefault();
            if (mainWWindowView != null)
            {
                mainWWindowView.currentWizardSpellTab.currentTabVM.RemoveSpell(id);
            }
        }
    }
}
