using System.Windows;
using System.Windows.Input;
using SpellBouc.Xaml;


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

        private void TestMainButtonWindow(object sender, RoutedEventArgs e)
        {
            var wizardSpellBook = new WizardSpellBook();
            var test = new WizardWindow(wizardSpellBook);
            SystemCommands.CloseWindow(this);
            test.Show();
        }


        //private void TestFunction(object sender, RoutedEventArgs e)
        //{
        //    var wizardSpellBook = new WizardSpellBook();

        //    var test = Access.GetWizardSpellsFromDB(Globals.DB_PLAYER_WIZARD_SPELL_PATH);
        //    var test2 = Access.GetUIWizardSpellsFromDB();

        //    return;
        //}

        //private void Test2Function(object sender, RoutedEventArgs e)
        //{
        //    var wizardSpellBook = new WizardSpellBook();
        //    var inputID = Int32.Parse(textBox1.Text);

        //    wizardSpellBook.AddSpellInSpellBook(inputID);
        //    var test = Access.GetWizardSpellsFromDB(Globals.DB_PLAYER_WIZARD_SPELL_PATH);

        //    wizardSpellBook.IncrementWizardPlayerSpell(inputID);
        //    var test2 = Access.GetUIWizardSpellsFromDB();

        //    wizardSpellBook.UpdateMaxLvlSpell();

        //    test = Access.GetWizardSpellsFromDB(Globals.DB_PLAYER_WIZARD_SPELL_PATH);
        //    test2 = Access.GetUIWizardSpellsFromDB();


        //    return;
        //}

        //private void Test3Function(object sender, RoutedEventArgs e)
        //{
        //    var wizardSpellBook = new WizardSpellBook();
        //    var inputID = Int32.Parse(textBox1.Text);

        //    wizardSpellBook.RemoveSpellInSpellBook(inputID);

        //    var test = Access.GetWizardSpellsFromDB(Globals.DB_PLAYER_WIZARD_SPELL_PATH);
        //    var test2 = Access.GetUIWizardSpellsFromDB();

        //    return;
        //}
    }
}
