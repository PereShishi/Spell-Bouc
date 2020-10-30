using SpellBouc.ViewModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SpellBouc.View
{
    /// <summary>
    /// Logique d'interaction pour WAddSpellByLvlView.xaml
    /// </summary>
    public partial class WAddSpellByLvlView : Window      
    {
        WAddSpellByLvlViewModel PageVM { get; set; }

        /* Constructeur par défaut qui set le lvl à 0 */
        public WAddSpellByLvlView()
        {
            InitializeComponent();
            PageVM = new WAddSpellByLvlViewModel();
            this.DataContext = PageVM;
            this.testList.SelectedItem = 0;

        }

        /* Constructeur avec le level max en paramètre */
        public WAddSpellByLvlView(int lvl)
        {
            InitializeComponent();
            this.DataContext = new WAddSpellByLvlViewModel(lvl);
            this.testList.SelectedItem = 0;
        }


        /* Fonction qui appele le VM WTabViewModel d'où a été crée la page, pour lui faire ajouter un sort */
        private void AddSpell(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).Tag;
            WWindowView mainWWindowView = Application.Current.Windows.OfType<WWindowView>().FirstOrDefault();
            if (mainWWindowView != null)
            {
                mainWWindowView.currentWizardSpellTab.currentTabVM.AddSpell(id);
            }
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
