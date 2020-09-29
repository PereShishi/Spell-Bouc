using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SpellBouc.AccessLayer;

namespace SpellBouc
{
    /*
     * Classe qui sert de container aux sorts et leur contenu. Cette classe est l'interface avec la couche Acess.
     * Elle est composée d'une liste de sorts et toutes les fonctions qui permetront de lister, accéder, trier, afficher ect...
     */
    class SpellContainer: IEnumerable 
    {
        private List<Spell> Spells { get; set; }

        /* 
         * Constructeur qui construit le containeur de sort dépendament de son type
         * Chaque type appelera un appel différent de la BDD: 
         *      * Les PlayerSpells appellent l'entièreté de la BDD du jouer pour une classe spécifique 
         *      * Les CompleteSpells appellent l'entièreté  de la BDD des sorts éxistants pour une classe spécifique 
         */
        internal SpellContainer(ContainerType containerType)
        {
            switch (containerType)
            {
                case ContainerType.WizardPlayerSpells:
                    Spells = Access.GetWizardSpellsFromDB(Globals.DB_PLAYER_WIZARD_SPELL_PATH);
                    IEnumerable<Spell> query1 = Spells.OrderBy(spell => spell.Id);
                    break;

                case ContainerType.WizardCompleteSpells:
                    Spells = Access.GetWizardSpellsFromDB(Globals.DB_WIZARD_SPELL_PATH);
                    IEnumerable<Spell> query2 = Spells.OrderBy(spell => spell.Id);
                    break;

                case ContainerType.PriestPlayerSpells:
                    break;

                case ContainerType.PriestCompleteSpells:
                    break;

                default:
                    break;
            }
        }

        /*
         * Getter de spell de la liste du container. input : ID
         */
        public Spell GetSpell(int id)
        {
            foreach (Spell spell in Spells)
            {
                if (spell.Id == id)
                {
                    return spell;
                }
            }
            return new Spell();
        }

        /*
         * Getter de spell de la liste du container. input : Name
         */
        public Spell GetSpell(String name)
        {
            foreach (Spell spell in Spells)
            {
                if (spell.Name == name)
                {
                    return spell;
                }
            }
            return new Spell();
        }


        /*
         * Getter de liste de spells à partir de la liste du container. input : Lvl
         */
        public List<Spell> GetSpells(int lvl)
        {
            var returnSpells = new List<Spell>();

            foreach (Spell spell in Spells)
            {
                if (spell.Lvl == lvl)
                {
                    returnSpells.Add(spell);
                }
            }
            return returnSpells;
        }

        /*
         * Getter de liste de spells à partir de la liste du container. input : Type
         */
        public List<Spell> GetSpells(String type)
        {
            var returnSpells = new List<Spell>();

            foreach (Spell spell in Spells)
            {
                if (spell.Type == type)
                {
                    returnSpells.Add(spell);
                }
            }
            return returnSpells;
        }

        /*
         * Ajoute un spell dans la liste
         */
        private void AddSpell(Spell spellToAdd)
        {
            foreach (var spell in Spells)
            {
                if (spellToAdd == spell)
                {
                    return;
                }
            }
            Spells.Add(spellToAdd);
        }

        /*
         * Supprime un spell dans la liste
         */
        private void RemoveSpell(Spell spellToRemove)
        {
            foreach (var spell in Spells)
            {
                if (spellToRemove == spell)
                {
                    Spells.Remove(spell);
                }
            }   
        }

        /*
         * Ajoute un spell dans la liste ET dans la BDD
         */
        internal ErrorCode AddSpellInBookAndBD(Spell spell, ContainerType containerType)
        {
            ErrorCode status;
            switch (containerType)
            {
                // Mise à jour sort de Mage
                case ContainerType.WizardPlayerSpells:
                    status = Access.AddWizardSpellInPlayerDB(spell);
                    if (status != ErrorCode.ERROR)
                    {
                        AddSpell(spell);
                    }
                    else
                    {
                        status = ErrorCode.ERROR;
                    }
                    break;

                // Default case: ne peut pas rentrer dans cette étape théoriquement
                default:
                    status = ErrorCode.ERROR;
                    break;
            }

            return status;
        }

        /*
         * Retire un spell dans la liste ET dans la BDD
         */
        internal ErrorCode RemoveSpellInBookAndBD(Spell spell, ContainerType containerType)
        {
            ErrorCode status;
            switch (containerType)
            {
                // Mise à jour sort de Mage
                case ContainerType.WizardPlayerSpells:
                    status = Access.RemoveWizardSpellInPlayerDB(spell.Id);

                    if (status != ErrorCode.ERROR)
                    {
                        status = Access.RemoveSpellInUiDB(spell.Id);

                        if (status != ErrorCode.ERROR)
                        {
                            RemoveSpell(spell);
                            return ErrorCode.SUCCESS;
                        }                            
                    }
                    else
                    {
                        status = ErrorCode.ERROR;
                    }
                    break;

                // Default case: ne peut pas rentrer dans cette étape théoriquement
                default:
                    status = ErrorCode.ERROR;
                    break;
            }

            return status;
        }

        public IEnumerator GetEnumerator()
        {
            return Spells.GetEnumerator();
        }
    }
}
