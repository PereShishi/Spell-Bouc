using SpellBouc.Model.Ao;
using SpellBouc.Model.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace SpellBouc.ViewModel
{
    public class AoSpellBookViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<AoSpellModel> AoSpellList { get; set; } = new ObservableCollection<AoSpellModel>();
        private AoSpellModel _selectedSpell;
        private Filter _spellFilter;

        public ICollectionView AoSpellListCollectionView
        {
            get { return CollectionViewSource.GetDefaultView(AoSpellList); }
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => {};

        public AoSpellBookViewModel(ContainerType type)
        {
            switch (type)
            {
                case ContainerType.WizardPlayerSpells:
                    foreach (UiSpell uiSpell in Globals.AppWizardSpellBook.UICompleteClassSpells)
                    {
                        AoSpellModel aoSpell = new AoSpellModel(uiSpell);
                        AoSpellList.Add(aoSpell);
                    }
                    SelectedSpell = (AoSpellModel)AoSpellList[0];
                    break;
                case ContainerType.PriestPlayerSpells:
                    foreach (UiSpell uiSpell in Globals.AppPriestSpellBook.UICompleteClassSpells)
                    {
                        AoSpellModel aoSpell = new AoSpellModel(uiSpell);
                        AoSpellList.Add(aoSpell);
                    }
                    SelectedSpell = (AoSpellModel)AoSpellList[0];
                    break;
                case ContainerType.DruidPlayerSpells:
                    foreach (UiSpell uiSpell in Globals.AppDruidSpellBook.UICompleteClassSpells)
                    {
                        AoSpellModel aoSpell = new AoSpellModel(uiSpell);
                        AoSpellList.Add(aoSpell);
                    }
                    SelectedSpell = (AoSpellModel)AoSpellList[0];
                    break;
                default:
                    break;
            }
            SpellFilter = new Filter();

            foreach(AoSpellModel spell in AoSpellList)
            {
                if (spell.UiSpell.Alignement == null)
                {
                    spell.UiSpell.Alignement = "";
                }
            }
                
        }

        /* Sort sélectionné */
        public AoSpellModel SelectedSpell
        {
            get
            {
                return _selectedSpell;
            }
            set
            {
                if (_selectedSpell == value)
                    return;
                _selectedSpell = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedSpell)));
            }

        }

        /* Filtre */
        public Filter SpellFilter
        {
            get
            {
                return _spellFilter;
            }
            set
            {
                if (_spellFilter == value)
                    return;
                _spellFilter = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SpellFilter)));
            }

        }

        /* Update the filter from a button */
        public void UpdateFilter(int lvl)
        {
            if (SpellFilter.LvlFilter[lvl] == true)
                SpellFilter.LvlFilter[lvl] = false;
            else
                SpellFilter.LvlFilter[lvl] = true;

            UpdateVisibility();
        }

        /* Update the filter from the textBox */
        public void UpdateFilter(string text)
        {
            SpellFilter.TextFilter = text;
            UpdateVisibility();
        }

        /* Update visibility */
        public void UpdateVisibility()
        {
            foreach(AoSpellModel aospell in AoSpellList)
            {
                // Applique le filtre de niveau
                aospell.IsVisible = SpellFilter.LvlFilter[aospell.UiSpell.Lvl]; 
                // Si le sorts est toujours afficher, on filtre ensuite avec le texte
                if(aospell.IsVisible == true && SpellFilter.TextFilter != null && Regex.Replace(SpellFilter.TextFilter, @"\s+", "") != "")
                {

                    // Filtre
                    if (!aospell.UiSpell.Name.ToLower().Contains(SpellFilter.TextFilter.ToLower()) && !aospell.UiSpell.School.ToLower().Contains(SpellFilter.TextFilter.ToLower())
                                                                                    && !aospell.UiSpell.Alignement.ToLower().Contains(SpellFilter.TextFilter.ToLower()))
                        aospell.IsVisible = false;
                }
            }

            // Applique le filtre sur l'item
            AoSpellListCollectionView.Filter = o =>
            {
                AoSpellModel item = (AoSpellModel)o;
                return item.IsVisible;
            };
            // Si le sort séléctionné a été filtré on séléctionne le premier de la liste
            if (SelectedSpell == null)
                foreach (AoSpellModel aospell in AoSpellList)
                {
                    if (aospell.IsVisible == true)
                    {
                        SelectedSpell = aospell;
                        break;
                    }  
                }
        }


        public class Filter
        {
        public List<bool> LvlFilter { get; set; }
        public string TextFilter { get; set; }

            /* Constructeur par défaut */
            public Filter()
            {
                LvlFilter = new List<bool>();
                // Initialise la visibilité des sorts
                for (int i = 0; i < 11; i++)
                    LvlFilter.Add(true);

                TextFilter = "";
            }
        }
        
    }
}
