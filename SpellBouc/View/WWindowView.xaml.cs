using SpellBouc.Model;
using SpellBouc.SpellBooks;
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
    public partial class WWindowView : Window
    {
        public WWindowView()
        {
            InitializeComponent();
            //currentWizardSpellTab.wizardTab.DataContext = new WizardSpellTabViewModel().WizardSpellTabList;
            //currentWizardSpellTab.wizardTab.SelectedIndex = 0;
            //this._wizardSpellBook = wizardSpellBook;
            //InitializeCurrentWizardSpellTab();
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
