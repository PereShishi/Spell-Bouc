using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SpellBouc
{
    class Globals
    {
        public static String PROJECT_DIRECTORY_PATH = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        public static String DB_PATH = PROJECT_DIRECTORY_PATH + "\\BDD\\wizard_spell.db";

    }
}
