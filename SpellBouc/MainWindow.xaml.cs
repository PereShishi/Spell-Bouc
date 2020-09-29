using SpellBouc.AccessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void TestFunction(object sender, RoutedEventArgs e)
        {
            var wizardSpellBook = new WizardSpellBook();                                 
        }

        private void Test2Function(object sender, RoutedEventArgs e)
        {
            var wizardSpellBook = new WizardSpellBook();
            var inputID = Int32.Parse(textBox1.Text);
            
            wizardSpellBook.AddSpellInSpellBook(inputID);
            var test = Access.GetWizardSpellsFromDB(Globals.DB_PLAYER_WIZARD_SPELL_PATH);

            wizardSpellBook.AddSpellInSpellBook(inputID);
            test = Access.GetWizardSpellsFromDB(Globals.DB_PLAYER_WIZARD_SPELL_PATH);

        }
    }
}
