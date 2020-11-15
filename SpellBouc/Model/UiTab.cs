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

        // Header: 
        public int Lvl { get; set; }

        public string SpellLeft { get; set; }

    }
}
