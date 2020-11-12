using System.Windows;
using System.Windows.Input;


namespace SpellBouc.View
{
    /// <summary>
    /// Logique d'interaction pour WizardWindow.xaml
    /// </summary>
    public partial class WWindowView : Window
    {
        public WWindowView()
        {
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
            WWindowView res = new WWindowView();
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
    }
}
