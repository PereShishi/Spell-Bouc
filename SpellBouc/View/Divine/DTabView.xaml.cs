using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SpellBouc.View
{
    /// <summary>
    /// Logique d'interaction pour WizardSpellTab.xaml
    /// </summary>
    public partial class DTabView : UserControl
    {

        public DTabView()
        {
            InitializeComponent();
            divineTab.SelectedIndex = 0;
        }
        
        private void GenerateAddSpellPage(object sender, System.Windows.RoutedEventArgs e)
        {
            int currentLvl = this.divineTab.SelectedIndex;
            AddSpellByLvlView addSpellByLvlView = new AddSpellByLvlView(currentLvl);

            addSpellByLvlView.Show();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Window parentWindow = Window.GetWindow(this);
                parentWindow.DragMove();
            }
        }

        // Incrémente le nombre de sorts du lanceur de sort pour un niveau particulier
        private void IncrementSpell(object sender, RoutedEventArgs e)
        {
            currentTabVM.IncrementSpellCount();
        }

        private void DecrementSpell(object sender, RoutedEventArgs e)
        {
            currentTabVM.DecrementSpell();
        }
    }

}
