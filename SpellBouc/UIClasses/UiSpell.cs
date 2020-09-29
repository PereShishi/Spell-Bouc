using System;

/* Classe UISpells (classe mère). Elle définit les objets passés aux différents interfaces utilisateurs */
namespace SpellBouc.UIClasses
{
     internal class UiSpell
    {
        public int Id { get; set; }
        public int Lvl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
