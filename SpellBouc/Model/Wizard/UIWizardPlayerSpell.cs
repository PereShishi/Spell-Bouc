namespace SpellBouc.UISpells
{
    /* Classe qui store toutes les informations à afficher dans les UI: Elles contiennent les sorts de mage du joueur et leur nombre d'utilisation */
    public class UIWizardPlayerSpell : UiSpell
    {
        public int PlayerSpellCount { get; set; }
        internal UIWizardPlayerSpell()
        {
            this.IsAddable = true;
        }
    }
}
