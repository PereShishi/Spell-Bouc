using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SpellBouc.AccessLayer;
using SpellBouc.Logs;

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
         *      * Les CompleteSpells appellent l'entièreté de la BDD des sorts éxistants pour une classe spécifique 
         */
        internal SpellContainer(ContainerType containerType)
        {
            switch (containerType)
            {
                // Mage
                case ContainerType.WizardPlayerSpells:
                    Spells = Access.GetWizardSpellsFromDB(Globals.DB_PLAYER_WIZARD_SPELL_PATH);
                    IEnumerable<Spell> queryw1 = Spells.OrderBy(spell => spell.Id);
                    break;

                case ContainerType.WizardCompleteSpells:
                    Spells = Access.GetWizardSpellsFromDB(Globals.DB_WIZARD_SPELL_PATH);
                    IEnumerable<Spell> queryw2 = Spells.OrderBy(spell => spell.Id);
                    break;

                // Prêtre
                case ContainerType.PriestPlayerSpells:
                    Spells = Access.GetDivineSpellsFromDB(Globals.DB_PLAYER_PRIEST_SPELL_PATH);
                    IEnumerable<Spell> queryp1 = Spells.OrderBy(spell => spell.Id);
                    break;

                case ContainerType.PriestCompleteSpells:
                    Spells = Access.GetDivineSpellsFromDB(Globals.DB_PRIEST_SPELL_PATH);
                    IEnumerable<Spell> queryp2 = Spells.OrderBy(spell => spell.Id);
                    break;

                // Druide
                case ContainerType.DruidPlayerSpells:
                    Spells = Access.GetDivineSpellsFromDB(Globals.DB_PLAYER_DRUID_SPELL_PATH);
                    IEnumerable<Spell> queryd1 = Spells.OrderBy(spell => spell.Id);
                    break;

                case ContainerType.DruidCompleteSpells:
                    Spells = Access.GetDivineSpellsFromDB(Globals.DB_DRUID_SPELL_PATH);
                    IEnumerable<Spell> queryd2 = Spells.OrderBy(spell => spell.Id);
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
                if (spellToAdd.Id == spell.Id)
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
                if (spellToRemove.Id == spell.Id)
                {
                    Spells.Remove(spell);
                    return;
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
                    break;

                case ContainerType.PriestPlayerSpells:
                    status = Access.AddDivineSpellInPlayerDB(spell, Globals.DB_PLAYER_PRIEST_SPELL_PATH);
                    break;

                case ContainerType.DruidPlayerSpells:
                    status = Access.AddDivineSpellInPlayerDB(spell, Globals.DB_PLAYER_DRUID_SPELL_PATH);
                    break;

                // Default case: ne peut pas rentrer dans cette étape théoriquement
                default:
                    status = ErrorCode.ERROR;
                    break;
            }

            if (status == ErrorCode.SUCCESS)
            {
                AddSpell(spell);
            }
  
            return status;
        }

        /*
         * Retire un spell dans la liste ET dans la BDD
         */
        internal ErrorCode RemoveSpellInBookAndBD(Spell spell, ContainerType containerType)
        {
            ErrorCode status = ErrorCode.ERROR;
            switch (containerType)
            {
                // Mise à jour sort de Mage
                case ContainerType.WizardPlayerSpells:
                    status = Access.RemoveSpellInPlayerDB(spell.Id, Globals.DB_PLAYER_WIZARD_SPELL_PATH);
                    break;

                case ContainerType.PriestPlayerSpells:
                    status = Access.RemoveSpellInPlayerDB(spell.Id, Globals.DB_PLAYER_PRIEST_SPELL_PATH);
                    break;

                case ContainerType.DruidPlayerSpells:
                    status = Access.RemoveSpellInPlayerDB(spell.Id, Globals.DB_PLAYER_DRUID_SPELL_PATH);
                    break;

                // Default case: ne peut pas rentrer dans cette étape théoriquement
                default:
                    status = ErrorCode.ERROR;
                    break;
            }

            if (status == ErrorCode.SUCCESS)
            {
                RemoveSpell(spell);        
            }

            return status;
        }

        /*
         * Check les doublons 
         */
        internal bool IsInContainer(int spellId)
        {
            foreach (var spell in Spells)
            {
                if (spellId == spell.Id)
                {
                    return true;
                }
            }
            return false;
        }

        /*
         * Check si le sort est présent ou non dans le container 
         */
        internal bool IsNotInContainer(int spellId)
        {
            foreach (var spell in Spells)
            {
                if (spellId == spell.Id)
                {
                    return false;
                }
            }
            return true;
        }
        

        public IEnumerator GetEnumerator()
        {
            return Spells.GetEnumerator();
        }
    }
}
