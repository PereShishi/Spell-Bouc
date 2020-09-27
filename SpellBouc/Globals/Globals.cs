using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SpellBouc
{
    /* Variables globales */
    class Globals
    {
        // Path du project directory 
        public static String PROJECT_DIRECTORY_PATH = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        // Path de la BDD des sorts de mage
        public static String DB_WIZARD_SPELL_PATH = PROJECT_DIRECTORY_PATH + "\\BDD\\wizard_spells.db";

        // Path de la BDD des sorts dans le livre de sort du joueur
        public static String DB_PLAYER_WIZARD_SPELL_PATH = PROJECT_DIRECTORY_PATH + "\\BDD\\player_wizard_spells.db";
    }
}
