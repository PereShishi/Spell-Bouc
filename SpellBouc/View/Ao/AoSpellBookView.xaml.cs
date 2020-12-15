using SpellBouc.ViewModel;
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

namespace SpellBouc.View
{
    /// <summary>
    /// Logique d'interaction pour AoSpellBookView.xaml
    /// </summary>
    public partial class AoSpellBookView : Window
    {
        AoSpellBookViewModel AoVM { get; set; }

        public AoSpellBookView(ContainerType type)
        {
          
            InitializeComponent();
            AoVM = new AoSpellBookViewModel(type);
            this.DataContext = AoVM;
            this.addSpellList.SelectedItem = 0;
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
