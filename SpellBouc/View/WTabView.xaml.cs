﻿using System;
using System.Windows;
using System.Windows.Controls;
using SpellBouc.Model;
using SpellBouc.UISpells;
using SpellBouc.ViewModel;

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
            WAddSpellByLvlView addSpellByLvlView = new WAddSpellByLvlView(currentLvl);

            addSpellByLvlView.Show();
        }
    }
}
