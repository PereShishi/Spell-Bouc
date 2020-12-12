using SpellBouc.Model.Common;
using SpellBouc.Model.Wizard;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpellBouc.View
{
    /// <summary>
    /// Logique d'interaction pour SpellDescriptor.xaml
    /// </summary>
    public partial class SpellDescriptor : UserControl
    {
        public SpellDescriptor()
        {
            InitializeComponent();
        }

        private void displayElement(object sender, DependencyPropertyChangedEventArgs e)
        {
            UiSpell dtc = new UiSpell();
            UIWizardPlayerSpell uiWizardSpell = new UIWizardPlayerSpell();

            var dataContext = DataContext;

            if (dataContext == null)
                return;

            if (dataContext.GetType() == dtc.GetType() || dataContext.GetType() == uiWizardSpell.GetType())
            {
                dtc = (UiSpell)DataContext;

                // School:
                if (dtc.School == "" || dtc.School == null)
                {
                    schoolTitle.Text = string.Empty;
                    schoolSpace.Text = string.Empty;
                }
                else
                {
                    schoolTitle.Text = "Ecole: ";
                    schoolSpace.Text = "\n";
                }
               

                // Comp:
                if (dtc.Composante == "" || dtc.Composante == null)
                {
                    compTitle.Text = string.Empty;
                    compSpace.Text = string.Empty;
                }
                else
                {
                    compTitle.Text = "Composantes: ";
                    compSpace.Text = "\n";
                }
                

                // IncTime:
                if (dtc.IncTime == "" || dtc.IncTime == null )
                {
                    incTitle.Text = string.Empty;
                    incSpace.Text = string.Empty;
                }
                    
                else
                {
                    incTitle.Text = "Temps d'incantation: ";
                    incSpace.Text = "\n";
                }
                
                // RangeTitle:
                if (dtc.Range == "" || dtc.Range == null)
                {
                    rangeTitle.Text = string.Empty;
                    rangeSpace.Text = string.Empty;
                }
                else
                {
                    rangeTitle.Text = "Portée: ";
                    rangeSpace.Text = "\n";
                }
                

                // AreaEffect:
                if (dtc.AreaEffect == "" ||  dtc.AreaEffect == null)
                {
                    areaTitle.Text = string.Empty;
                    areaSpace.Text = string.Empty;
                } 
                else
                {
                    areaTitle.Text = "Zone d'effet: ";
                    areaSpace.Text = "\n";
                }
                

                // Duration:
                if (dtc.Duration == "" ||  dtc.Duration == null)
                {
                    durationTitle.Text = string.Empty;
                    durationSpace.Text = string.Empty;
                }
                    
                else
                {
                    durationTitle.Text = "Durée: ";
                    durationSpace.Text = "\n";
                }
               
                // Sauvegarde:
                if (dtc.SaveDice == "" || dtc.SaveDice ==  null)
                {
                    saveDiceTitle.Text = string.Empty;
                    saveDiceSpace.Text = string.Empty;
                }
                else
                {
                    saveDiceTitle.Text = "Sauvegarde: ";
                    saveDiceSpace.Text = "\n";
                }
                
                // Type:
                if (dtc.EffetType == "" || dtc.EffetType == null)
                {
                    typeTitle.Text = string.Empty;
                    typeSpace.Text = string.Empty;

                }
                else
                {
                    typeTitle.Text = "Type: ";
                    typeSpace.Text = "\n";
                }

                //Resistance magie
                if (dtc.MagicResist == true)
                {
                    magicResistValue.Text = "Oui";
                }
                else if(dtc.MagicResist == false)
                {
                    magicResistValue.Text = "Non";
                }

            } 
        }
    }
}
