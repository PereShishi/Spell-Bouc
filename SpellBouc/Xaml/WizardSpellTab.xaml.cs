using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpellBouc.Xaml
{
    /// <summary>
    /// Logique d'interaction pour WizardSpellTab.xaml
    /// </summary>
    public partial class WizardSpellTab : UserControl
    {
        private WizardWindow wizardWindow = null;

        public WizardSpellTab()
        {
            InitializeComponent();
        }
        private void OnControlLoaded(object sender, RoutedEventArgs e)
        {
            wizardWindow = Window.GetWindow(this) as WizardWindow;
        }
    }
}
