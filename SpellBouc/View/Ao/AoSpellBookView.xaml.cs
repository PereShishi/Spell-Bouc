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

        private void b0_Click(object sender, RoutedEventArgs e)
        {
            AoVM.UpdateFilter(0);
        }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            AoVM.UpdateFilter(1);
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            AoVM.UpdateFilter(2);
        }

        private void b3_Click(object sender, RoutedEventArgs e)
        {
            AoVM.UpdateFilter(3);
        }


        private void b4_Click(object sender, RoutedEventArgs e)
        {
            AoVM.UpdateFilter(4);
        }

        private void b5_Click(object sender, RoutedEventArgs e)
        {
            AoVM.UpdateFilter(5);
        }

        private void b6_Click(object sender, RoutedEventArgs e)
        {
            AoVM.UpdateFilter(6);
        }

        private void b7_Click(object sender, RoutedEventArgs e)
        {
            AoVM.UpdateFilter(7);
        }

        private void b8_Click(object sender, RoutedEventArgs e)
        {
            AoVM.UpdateFilter(8);
        }

        private void b9_Click(object sender, RoutedEventArgs e)
        {
            AoVM.UpdateFilter(9);
        }

        private void b10_Click(object sender, RoutedEventArgs e)
        {
            AoVM.UpdateFilter(10);
        }

        private void textFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            AoVM.UpdateFilter(textFilter.Text);
        }
    }
}
