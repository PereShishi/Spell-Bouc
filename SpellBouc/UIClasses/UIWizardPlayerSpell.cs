using System;
using System.Collections.Generic;
using System.Text;

namespace SpellBouc
{
    /* Classe qui store toutes les informations à afficher dans les UI: Elles contiennent les sorts de mage du joueur et leur nombre d'utilisation */
    class UIWizardPlayerSpell
    {
        public int Id { get; set; }
        public int Lvl { get; set; }
        public int PlayerSpellCount { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

    }
}
