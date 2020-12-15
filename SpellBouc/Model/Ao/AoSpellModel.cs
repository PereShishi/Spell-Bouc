using SpellBouc.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SpellBouc.Model.Ao
{
    public class AoSpellModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        private UiSpell _uiSpell;
        private bool _isVisible;

        public AoSpellModel(UiSpell uispell)
        {
            _uiSpell = uispell;
            _isVisible = false;
        }

        /* Sort */
        public UiSpell UiSpell
        {
            get
            {
                return _uiSpell;
            }
            set
            {
                if (_uiSpell == value)
                    return;
                _uiSpell = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(UiSpell)));
            }
        }

        /* Visibilité du sort */
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                if (_isVisible == value)
                    return;
                _isVisible = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(_isVisible)));
            }
        }
    }
}
