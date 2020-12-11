using SpellBouc.ViewModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SpellBouc.View
{
    /// <summary>
    /// Logique d'interaction pour AddSpellByLvlView.xaml
    /// </summary>
    public partial class AddSpellByLvlView : Window      
    {
        dynamic PageVM { get; set; }

        /* Constructeur par défaut qui set le lvl à 0 */
        public AddSpellByLvlView()
        {
            InitializeComponent();

            if (Globals.SelectedSpellBook == ContainerType.WizardPlayerSpells)
            {
                PageVM = new WAddSpellByLvlViewModel();
            }
            else
            {
                PageVM = new DAddSpellByLvlViewModel();
            }

            this.DataContext = PageVM;
            this.addSpellList.SelectedItem = 0;

        }

        /* Constructeur avec le level max en paramètre */
        public AddSpellByLvlView(int lvl)
        {
            InitializeComponent();

            if (Globals.SelectedSpellBook == ContainerType.WizardPlayerSpells)
            {
                this.DataContext = new WAddSpellByLvlViewModel(lvl);
            }
            else
            {
                this.DataContext = new DAddSpellByLvlViewModel(lvl);
            }

            this.addSpellList.SelectedItem = 0;
        }


        /* Fonction qui appele le VM WTabViewModel d'où a été crée la page, pour lui faire ajouter un sort */
        private void AddSpell(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).Tag;

            if (Globals.SelectedSpellBook == ContainerType.WizardPlayerSpells)
            {
                WWindowView mainWWindowView = Application.Current.Windows.OfType<WWindowView>().FirstOrDefault();
                if (mainWWindowView != null)
                {
                    mainWWindowView.currentWizardSpellTab.currentTabVM.AddSpell(id);
                }
            }
            else
            {
                DWindowView mainDWindowView = Application.Current.Windows.OfType<DWindowView>().FirstOrDefault();
                if (mainDWindowView != null)
                {
                    mainDWindowView.currentDivineSpellTab.currentTabVM.AddSpell(id);
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CommandBinding_CanExecute_1(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void CommandBinding_Executed_3(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }
    }
}
