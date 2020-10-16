using SpellBouc.Model;
using SpellBouc.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;


namespace SpellBouc.View
{
    /// <summary>
    /// Logique d'interaction pour WizardWindow.xaml
    /// </summary>
    public partial class WizardWindowView : Window
    {
        WizardSpellBook _wizardSpellBook;

        public WizardWindowView(WizardSpellBook wizardSpellBook)
        {
            InitializeComponent();
            this._wizardSpellBook = wizardSpellBook;
            InitializeCurrentWizardSpellTab();
        }

        /* Initialise la première wizardSpellTab en utilisant le VM */
        private void InitializeCurrentWizardSpellTab()
        {
            WizardSpellTabViewModel test = new WizardSpellTabViewModel(_wizardSpellBook);
            ObservableCollection<UiWizardSpellTab> list = test.WizardSpellTabList;

            currentWizardSpellTab.wizardTab.DataContext = list;
            currentWizardSpellTab.wizardTab.SelectedIndex = 0;
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
