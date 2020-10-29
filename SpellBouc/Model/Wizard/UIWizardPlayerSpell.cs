using System.ComponentModel;

namespace SpellBouc.UISpells
{
    /* Classe qui store toutes les informations à afficher dans les UI: Elles contiennent les sorts de mage du joueur et leur nombre d'utilisation */
    public class UIWizardPlayerSpell : UiSpell,INotifyPropertyChanged
    {
        private int _playerSpellCount;

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public int PlayerSpellCount 
        {
            get
            {
                return _playerSpellCount;
            }
            set
            {
                if (_playerSpellCount == value)
                    return;
                _playerSpellCount = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(PlayerSpellCount)));
            }
        }
        internal UIWizardPlayerSpell()
        {
            this.IsAddable = true;
        }
    }
}
