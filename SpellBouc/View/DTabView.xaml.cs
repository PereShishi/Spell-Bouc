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
        
        // TODO: Aremplacer par quelque chose de générique
        private void GenerateAddSpellPage(object sender, System.Windows.RoutedEventArgs e)
        {
            int currentLvl = this.divineTab.SelectedIndex;
            WAddSpellByLvlView addSpellByLvlView = new WAddSpellByLvlView(currentLvl);

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
