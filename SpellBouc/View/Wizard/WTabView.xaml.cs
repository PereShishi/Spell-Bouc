using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SpellBouc.View
{
    /// <summary>
    /// Logique d'interaction pour WizardSpellTab.xaml
    /// </summary>
    public partial class WTabView : UserControl
    {

        public WTabView()
        {
            InitializeComponent();
            wizardTab.SelectedIndex = 0;
        }

        private void GenerateAddSpellPage(object sender, System.Windows.RoutedEventArgs e)
        {
            int currentLvl = this.wizardTab.SelectedIndex;
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
    }

}
