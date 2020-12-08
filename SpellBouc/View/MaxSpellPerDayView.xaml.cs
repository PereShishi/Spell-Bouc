using SpellBouc.SpellBooks;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Logique d'interaction pour MaxSpellPerDayView.xaml
    /// </summary>
    public partial class MaxSpellPerDayView : Window
    {
        public MaxSpellPerDayView()
        {
            InitializeComponent();
        }

        private void ApplyChanges(object sender, RoutedEventArgs e)
        {
            int lvl0 = 0;
            int lvl1 = 0;
            int lvl2 = 0;
            int lvl3 = 0;
            int lvl4 = 0;

            try
            {
                 lvl0 = int.Parse(lvl0maxSpell.Text);
                 lvl1 = int.Parse(lvl1maxSpell.Text);
                 lvl2 = int.Parse(lvl2maxSpell.Text);
                 lvl3 = int.Parse(lvl3maxSpell.Text);
                 lvl4 = int.Parse(lvl4maxSpell.Text);
            }
            catch{}

            // Assignation rentrée en dur si lvl max = 4 
            int[] maxSpellByLvl = new int[]
            {
                lvl0,
                lvl1,
                lvl2,
                lvl3,
                lvl4
            };
            DWindowView mainDWindowView;
            switch (Globals.SelectedSpellBook)
            {
                case ContainerType.WizardPlayerSpells:
                    Globals.AppWizardSpellBook.UpdateMaxSpellNumberByLvl(maxSpellByLvl);
                    WWindowView mainWWindowView = Application.Current.Windows.OfType<WWindowView>().FirstOrDefault();
                    if (mainWWindowView != null)
                    {
                        mainWWindowView.currentWizardSpellTab.currentTabVM.UpdateMaxSpellPerDay(maxSpellByLvl);
                    }
                    break;

                case ContainerType.PriestPlayerSpells:
                    Globals.AppPriestSpellBook.UpdateMaxSpellNumberByLvl(maxSpellByLvl);
                    mainDWindowView = Application.Current.Windows.OfType<DWindowView>().FirstOrDefault();
                    if (mainDWindowView != null)
                    {
                        mainDWindowView.currentDivineSpellTab.currentTabVM.UpdateMaxSpellPerDay(maxSpellByLvl);
                    }
                    break;
                case ContainerType.DruidPlayerSpells:
                    Globals.AppDruidSpellBook.UpdateMaxSpellNumberByLvl(maxSpellByLvl);
                    mainDWindowView = Application.Current.Windows.OfType<DWindowView>().FirstOrDefault();
                    if (mainDWindowView != null)
                    {
                        mainDWindowView.currentDivineSpellTab.currentTabVM.UpdateMaxSpellPerDay(maxSpellByLvl);
                    }
                    break;
            }
        }
    }
}
