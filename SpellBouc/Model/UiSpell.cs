using System;
using System.ComponentModel;

/* Classe UISpells (classe mère). Elle définit les objets passés aux différents interfaces utilisateurs */
namespace SpellBouc.UISpells
{
     public class UiSpell
    {
        public int Id { get; set; }
        public int Lvl { get; set; }
        public string Name { get; set; }
        public string School { get; set; }
        public string Description { get; set; }
        public bool IsAddable { get; set; }

        public UiSpell()
        {
            this.IsAddable = true;
        }
    }
}
