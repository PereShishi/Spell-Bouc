using SpellBouc.Model.Common;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SpellBouc.View
{
    /// <summary>
    /// Logique d'interaction pour WizardSpellItemView.xaml
    /// </summary>
    public partial class DItemView : UserControl
    {
        public DItemView()
        {
            InitializeComponent();
        }

        /* Fonction qui appele le VM DTabViewModel d'où a été crée la page, pour lui faire supprimer un sort */
        private void RemoveSpell(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).Tag;
            DWindowView mainDWindowView = Application.Current.Windows.OfType<DWindowView>().FirstOrDefault();
            if (mainDWindowView != null)
            {
                mainDWindowView.currentDivineSpellTab.currentTabVM.RemoveSpell(id);
            }
        }

        private void collapseEmptyItem(object sender, DependencyPropertyChangedEventArgs e)
        {
            UiSpell dtc = new UiSpell();
            var dataContext = DataContext;
            if (dataContext == null)
                return;

            if (dataContext.GetType() == dtc.GetType())
            {
                dtc = (UiSpell)DataContext;
                if (dtc.Domaine == "" || dtc.Domaine == null)
                    domainValue.Visibility = Visibility.Collapsed;

                if (dtc.Alignement == "" || dtc.Alignement == null)
                    alignementValue.Visibility = Visibility.Collapsed;
            }

        }
    }
}
