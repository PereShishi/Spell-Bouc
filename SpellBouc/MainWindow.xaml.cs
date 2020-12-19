using System.Windows;
using System.Windows.Input;
using SpellBouc.View;

namespace SpellBouc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /* Permet le dragMove de la page */
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

        /* Commande le bouton de fermeture de la page */
        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        /* Commande le bouton de minimize de la page */
        private void CommandBinding_Executed_3(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        /* Bascule sur un livre de sort du Mage */
        private void ChoseWizardSpellBook(object sender, RoutedEventArgs e)
        {
            Globals.SelectedSpellBook = ContainerType.WizardPlayerSpells;
            WWindowView wizardWindowView = new WWindowView
            {
                Top = this.Top,
                Left = this.Left
            };
            SystemCommands.CloseWindow(this);
            wizardWindowView.Show();
        }

        private void ChosePriestSpellBook(object sender, RoutedEventArgs e)
        {
            Globals.SelectedSpellBook = ContainerType.PriestPlayerSpells;
            DWindowView divineWindowView = new DWindowView()
            {
                Top = this.Top,
                Left = this.Left
            };
            SystemCommands.CloseWindow(this);
            divineWindowView.Show();
        }

        private void ChoseDruidSpellBook(object sender, RoutedEventArgs e)
        {
            Globals.SelectedSpellBook = ContainerType.DruidPlayerSpells;
            DWindowView divineWindowView = new DWindowView()
            {
                Top = this.Top,
                Left = this.Left
            };
            SystemCommands.CloseWindow(this);
            divineWindowView.Show();
        }
    }
}
