using System.Windows;
using System.Windows.Input;


namespace SpellBouc.View
{
    /// <summary>
    /// Logique d'interaction pour WizardWindow.xaml
    /// </summary>
    public partial class DWindowView : Window
    {
        private ContainerType _type;

        public DWindowView()
        {
            InitializeComponent();
            _type = ContainerType.PriestPlayerSpells;
        }

        public DWindowView(ContainerType type)
        {
            if (type != ContainerType.DruidPlayerSpells && type != ContainerType.PriestPlayerSpells)
                return;

            _type = type;
            InitializeComponent();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RefreshButton(object sender, RoutedEventArgs e)
        {
            DWindowView res = new DWindowView(_type);
            res.Top = this.Top;
            res.Left = this.Left;
            Application.Current.MainWindow = res;
            res.Show();
            this.Close();

        }

        private void CreateMaxSpellPerDay(object sender, RoutedEventArgs e)
        {
            MaxSpellPerDayView window = new MaxSpellPerDayView();
            window.Show();
        }

        private void CreateAoSpellBook(object sender, RoutedEventArgs e)
        {
            AoSpellBookView aoSpellBook = new AoSpellBookView(ContainerType.WizardCompleteSpells);
            aoSpellBook.Show();
        }
    }
}