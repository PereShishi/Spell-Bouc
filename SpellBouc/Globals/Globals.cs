using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SpellBouc
{
    class Globals
    {
        public static String PROJECT_DIRECTORY_PATH = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        public static String DB_WIZARD_SPELL_PATH = PROJECT_DIRECTORY_PATH + "\\BDD\\wizard_spells.db";
        public static String DB_PLAYER_WIZARD_SPELL_PATH = PROJECT_DIRECTORY_PATH + "\\BDD\\player_wizard_spells.db";
    }
}
