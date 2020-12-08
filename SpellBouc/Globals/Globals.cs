using SpellBouc.SpellBooks;
using System;
using System.IO;

namespace SpellBouc
{
    /* Variables globales */
    public static class Globals
    {
        // Path du project directory 
        public static String PROJECT_DIRECTORY_PATH = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        // Paths de la BDD de chaque movre de sorts complet 
        public static String DB_WIZARD_SPELL_PATH = PROJECT_DIRECTORY_PATH + "\\BDD\\wizard_spells.db";
        public static String DB_PRIEST_SPELL_PATH = PROJECT_DIRECTORY_PATH + "\\BDD\\priest_spells.db";
        public static String DB_DRUID_SPELL_PATH  = PROJECT_DIRECTORY_PATH + "\\BDD\\druid_spells.db";

        // Paths de la BDD de chaque livre de sort du joueur
        public static String DB_PLAYER_WIZARD_SPELL_PATH = PROJECT_DIRECTORY_PATH + "\\BDD\\player_wizard_spells.db";
        public static String DB_PLAYER_PRIEST_SPELL_PATH = PROJECT_DIRECTORY_PATH + "\\BDD\\player_priest_spells.db";
        public static String DB_PLAYER_DRUID_SPELL_PATH  = PROJECT_DIRECTORY_PATH + "\\BDD\\player_druid_spells.db";

        // Livre de sorts de mage
        public static WizardSpellBook AppWizardSpellBook { get; set; } = new WizardSpellBook();

        // Livres de sorts divins
        public static DivineSpellBook AppPriestSpellBook { get; set; } = new DivineSpellBook(ContainerType.PriestPlayerSpells);
        public static DivineSpellBook AppDruidSpellBook { get; set; } = new DivineSpellBook(ContainerType.DruidPlayerSpells);

        // Type de livre séléctionné 
        public static ContainerType SelectedSpellBook;
    }
}
