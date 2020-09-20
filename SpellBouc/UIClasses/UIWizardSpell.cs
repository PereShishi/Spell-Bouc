using System;
using System.Collections.Generic;
using System.Text;

namespace SpellBouc.UIClasses
{
    class UIWizardSpell
    {
        public int Id { get; set; }
        public bool IsAddable { get; set; }
        public int Lvl { get; set; }
        public String Name { get; set; }
        public String School { get; set; }
        public String Description { get; set; }
    }
}
