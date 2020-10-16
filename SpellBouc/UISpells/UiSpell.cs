using System;

/* Classe UISpells (classe mère). Elle définit les objets passés aux différents interfaces utilisateurs */
namespace SpellBouc.UISpells
{
     internal class UiSpell
    {
        internal int Id { get; set; }
        internal int Lvl { get; set; }
        internal string Name { get; set; }
        internal String School { get; set; }
        internal string Description { get; set; }
        internal bool IsAddable { get; set; }

        internal UiSpell()
        {
            this.IsAddable = true;
        }
    }
}
