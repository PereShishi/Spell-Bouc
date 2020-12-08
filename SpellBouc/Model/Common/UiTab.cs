using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace SpellBouc.Model.Common
{
    public abstract class UiTab: INotifyPropertyChanged
    {        
        /* Event */
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> raiser)
        {
            var propName = ((MemberExpression)raiser.Body).Member.Name;
            OnPropertyChanged(propName);
        }

        protected bool Set<T>(ref T field, T value, [CallerMemberName] string name = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(name);
                return true;
            }
            return false;
        }

        // Header: 
        public int Lvl { get; set; }
        public string SpellLeft { get; set; }

        // Propriétés liés au PropertyChangedEventHandler
        private int _totalSpellCount = 0;
        private bool _isVisible = false;
        private int _maxSpellsPerDay;

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
                OnPropertyChanged(nameof(MaxSpellsPerDay));
            }
        }


        /* Total de sorts réstants au joueur par niveau (affiché dans les headers) */
        public int TotalSpellCount
        {
            get
            {
                return _totalSpellCount;
            }
            set
            {
                if (_totalSpellCount == value)
                    return;
                _totalSpellCount = value;
                OnPropertyChanged(nameof(TotalSpellCount));
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
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(IsVisible)));
            }

        }
    }
}
