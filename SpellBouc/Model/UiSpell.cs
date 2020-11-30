using System;
using System.ComponentModel;

/* Classe UISpells (classe mère). Elle définit les objets passés aux différents interfaces utilisateurs */
namespace SpellBouc.UISpells
{
     public class UiSpell
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Lvl { get; set; }
        public string School { get; set; }
        public string Source { get; set; }
        public string Composante { get; set; }
        public string IncTime { get; set; }
        public string Range { get; set; }
        public string AreaEffect { get; set; }
        public string Duration { get; set; }
        public string SaveDice { get; set; }
        public bool MagicResist { get; set; }
        public string Description { get; set; }
        public string Comp { get; set; }
        public bool IsAddable { get; set; }

        // Spécifique aux sorts divins
        public string Alignement { get; set; }
        public string EffetType { get; set; }
        public string Domaine { get; set; }

        public UiSpell()
        {
            this.IsAddable = true;
        }
    }
}
