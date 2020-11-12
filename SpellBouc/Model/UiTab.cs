using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SpellBouc.Model
{
    public abstract class UiTab: INotifyPropertyChanged
    {        
        /* Event */
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        private int _maxSpellsPerDay;
        // Header: 
        public int Lvl { get; set; }

        /* Max de sorts par niveau (affiché dans les headers) */
        public int MaxSpellsPerDay
        {
            get
            {
                return _maxSpellsPerDay;
            }
            set
            {
                if (_maxSpellsPerDay == value)
                    return;
                _maxSpellsPerDay = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(MaxSpellsPerDay)));
            }
        }


        public string SpellLeft { get; set; }

    }
}
