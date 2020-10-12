using System.Windows;
using System.Windows.Input;


namespace SpellBouc.Xaml
{
    /// <summary>
    /// Logique d'interaction pour WizardWindow.xaml
    /// </summary>
    public partial class WizardWindow : Window
    {
        public WizardWindow(WizardSpellBook wizardSpellBook)
        {
            InitializeComponent();
            this._wizardSpellBook = wizardSpellBook;
        }

        WizardSpellBook _wizardSpellBook;

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
