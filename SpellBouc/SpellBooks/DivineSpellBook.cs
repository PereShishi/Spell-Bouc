using SpellBouc.AccessLayer;
using SpellBouc.Logs;
using SpellBouc.Model.Common;
using SpellBouc.UIContainers;
using System.Collections.Generic;
using System.Linq;

namespace SpellBouc.SpellBooks
{
    /* Clase de livre de sort divin */
    public class DivineSpellBook : SpellBook
    {

        internal override SpellContainer PlayerSpells { get; set; }

        internal override SpellContainer CompleteClassSpells { get; set; }

        internal override UISpellContainer UIPlayerSpells { get; set; }

        internal override UISpellContainer UICompleteClassSpells { get; set; }

        private readonly ContainerType _playerSpellsType = ContainerType.PriestPlayerSpells;

        private readonly ContainerType _completeSpellsType = ContainerType.PriestCompleteSpells;

        private readonly UIContainerType _uiplayerSpellsType = UIContainerType.UIPriestSpell;

        private readonly UIContainerType _uicompleteSpellsType = UIContainerType.UIDruidSpell;


        /* Initialisation des membres */
        public DivineSpellBook(ContainerType type)
        {
            if (type != ContainerType.DruidPlayerSpells && type != ContainerType.PriestPlayerSpells)
                return;
            _playerSpellsType = type;

            switch (_playerSpellsType)
            {
                case ContainerType.PriestPlayerSpells:
                    _completeSpellsType = ContainerType.PriestCompleteSpells;
                    _uiplayerSpellsType = UIContainerType.UIPriestSpell;
                    break;

                case ContainerType.DruidPlayerSpells:
                    _completeSpellsType   = ContainerType.DruidCompleteSpells;
                    _uicompleteSpellsType = UIContainerType.UIDruidSpell;
                    break;

                default:
                    break;
            }

            // Récupère tous les sorts que le joueur a dans son grimoire 
            PlayerSpells = new SpellContainer(_playerSpellsType);
            // Récupère tous les sorts de mages
            CompleteClassSpells = new SpellContainer(_completeSpellsType);
            // Récupère toutes les utilisations quotidiennes des sorts du joueurs, et les infos à afficher dans les UIs
            UIPlayerSpells = new UISpellContainer();
            FillMissingUIInfosFromPlayerSpells();
            // Set le nombre d'onglets (niveau max des sorts du joueur).
            UpdateMaxLvlSpell();
            // Set le nombre max de sorts par jour pour chaque niveau
            SetMaxSpellNumberByLvl();

            // Partie CompleteClassSpells
            UICompleteClassSpells = new UISpellContainer();
            // Initialise CompleteClassSpells & update les sorts qui sont ajoutables
            InitUICompleteClassSpells(UIContainerType.UIPriestSpell);
            UpdateUICompleteClassSpell();
            UpdateSpellNumberByLvl();
        }


        /********************************************* IMPLEMENTATIONS *********************************************/

        /* Ajoute un spell dans le livre de sort (implémentation à partir de la classe mère SpellBook) input: id */
        internal override void AddSpellInSpellBook(int id)
        {
            if (PlayerSpells.IsInContainer(id) == true) return;
            var spellToAdd = CompleteClassSpells.GetSpell(id);

            // Met à jour les BDD et le PlayerSpells
            var status = PlayerSpells.AddSpellInBookAndBD(spellToAdd, _playerSpellsType);

            if (status == ErrorCode.SUCCESS)
            {
                if (UIPlayerSpells.IsDuplicated(spellToAdd) == true) return;
                // Met à jour les UIs
                status = UIPlayerSpells.AddUISpellInBookAndBD(spellToAdd, _uiplayerSpellsType);
                if (status != ErrorCode.SUCCESS)
                {
                    Log.GenerateLog(status, "Erreur lors de la mise à jour des UIs dans AddUISpellInBookAndBD");
                }
                UpdateMaxLvlSpell();
                // Update adable spell from CompleteClassSpell
                UpdateUICompleteClassSpell();
                UpdateSpellNumberByLvl();
            }
            else
            {
                Log.GenerateLog(status, "Erreur lors de la mise à jour des sorts dans AddSpellInBookAndBD");
            }
        }

        /* Ajoute un spell dans le livre de sort (implémentation à partir de la classe mère SpellBook) input: name */
        internal override void AddSpellInSpellBook(string name)
        {
            var spellToAdd = CompleteClassSpells.GetSpell(name);
            if (PlayerSpells.IsInContainer(spellToAdd.Id) == true) return;

            // Met à jour les BDD et le PlayerSpells
            var status = PlayerSpells.AddSpellInBookAndBD(spellToAdd, _playerSpellsType);

            if (status == ErrorCode.SUCCESS)
            {
                if (UIPlayerSpells.IsDuplicated(spellToAdd) == true) return;
                // Met à jour les UIs
                status = UIPlayerSpells.AddUISpellInBookAndBD(spellToAdd, _uiplayerSpellsType);
                if (status != ErrorCode.SUCCESS)
                {
                    Log.GenerateLog(status, "Erreur lors de la mise à jour des UIs dans AddUISpellInBookAndBD");
                }
                UpdateMaxLvlSpell();
                // Update adable spell from CompleteClassSpell
                UpdateUICompleteClassSpell();
                UpdateSpellNumberByLvl();
            }
            else
            {
                Log.GenerateLog(status, "Erreur lors de la mise à jour des sorts dans AddSpellInBookAndBD");
            }
        }

        /* Enlève un sort dans le livre du sort du joueur (implémentation à partir de la classe mère SpellBook) input: name */
        internal override void RemoveSpellInSpellBook(string name)
        {
            var spellToRemove = CompleteClassSpells.GetSpell(name);
            if (PlayerSpells.IsNotInContainer(spellToRemove.Id)) return;

            // Met à jour les BDD et le PlayerSpells
            var status = PlayerSpells.RemoveSpellInBookAndBD(spellToRemove, _playerSpellsType);
            if (status == ErrorCode.SUCCESS)
            {
                // Met à jour les UIs
                status = UIPlayerSpells.RemoveUISpellInBookAndBD(spellToRemove, _uiplayerSpellsType);
                if (status != ErrorCode.SUCCESS)
                {
                    Log.GenerateLog(status, "Erreur lors de la mise à jour des UIs dans RemoveUISpellInBookAndBD");
                }
                UpdateMaxLvlSpell();
                // Update adable spell from CompleteClassSpell
                UpdateUICompleteClassSpell();
                UpdateSpellNumberByLvl();
            }
            else
            {
                Log.GenerateLog(status, "Erreur lors de la mise à jour des sorts dans RemoveSpellInBookAndBD");
            }
        }

        /* Enlève un sort dans le livre du sort du joueur (implémentation à partir de la classe mère SpellBook) input: id */
        internal override void RemoveSpellInSpellBook(int id)
        {
            if (PlayerSpells.IsNotInContainer(id)) return;
            var spellToRemove = CompleteClassSpells.GetSpell(id);

            // Met à jour les BDD et le PlayerSpells
            var status = PlayerSpells.RemoveSpellInBookAndBD(spellToRemove, _playerSpellsType);
            if (status == ErrorCode.SUCCESS)
            {
                // Met à jour les UIs
                status = UIPlayerSpells.RemoveUISpellInBookAndBD(spellToRemove, _uiplayerSpellsType);
                if (status != ErrorCode.SUCCESS)
                {
                    Log.GenerateLog(status, "Erreur lors de la mise à jour des UIs dans RemoveUISpellInBookAndBD");
                }
                UpdateMaxLvlSpell();
                // Update adable spell from CompleteClassSpell
                UpdateUICompleteClassSpell();
                UpdateSpellNumberByLvl();
            }
            else
            {
                Log.GenerateLog(status, "Erreur lors de la mise à jour des sorts dans RemoveSpellInBookAndBD");
            }
        }

        /* Ajoute un spell dans la liste des UI */
        internal override void AddSpellInUIList(Spell spell, UIContainerType uiContainerType)
        {
            UIPlayerSpells.AddUiSpell(spell, uiContainerType);
        }

        /* Retire un spell dans la liste des UI */
        internal override void RemoveSpellInUIList(Spell spell)
        {
            UIPlayerSpells.RemoveUiSpell(spell);
        }

        /* Remplir les données manquantes de UIPlayerSpells après son instantiation */
        internal override void FillMissingUIInfosFromPlayerSpells()
        {
            foreach (Spell playerSpell in PlayerSpells)
            {
                UiSpell uiPlayerSpell = new UiSpell
                {
                    Id          = playerSpell.Id,
                    Lvl         = playerSpell.Lvl,
                    Name        = playerSpell.Name,
                    School      = playerSpell.Type,
                    Description = playerSpell.Description,
                    Source      = playerSpell.Source,
                    Composante  = playerSpell.Composante,
                    IncTime     = playerSpell.IncTime,
                    Range       = playerSpell.Range,
                    AreaEffect  = playerSpell.AreaEffect,
                    Duration    = playerSpell.Duration,
                    SaveDice    = playerSpell.SaveDice,
                    MagicResist = playerSpell.MagicResist,
                    Comp        = playerSpell.Comp,
                    Alignement  = playerSpell.Alignement,
                    EffetType   = playerSpell.EffetType,
                    Domaine     = playerSpell.Domaine
                };

                UIPlayerSpells.AddUiSpell(uiPlayerSpell);
            }   
        }

        /* Récupère le nombre de sorts par jour par niveau restants */
        internal override void UpdateSpellNumberByLvl()
        {
            switch (_playerSpellsType)
            {
                case ContainerType.PriestPlayerSpells:
                    // Récupère les valeurs et les set dans la propriété
                    PlayerSpellsByLvl = Access.GetSpellPerDayPerLvl(SpellMaxFromPlayer + 1, Globals.DB_PLAYER_PRIEST_SPELL_PATH);
                    break;

                case ContainerType.DruidCompleteSpells:
                    // Récupère les valeurs et les set dans la propriété
                    PlayerSpellsByLvl = Access.GetSpellPerDayPerLvl(MaxSpellsByLvl.Length, Globals.DB_PLAYER_DRUID_SPELL_PATH);
                    break;

                default:
                    // Récupère les valeurs et les set dans la propriété
                    PlayerSpellsByLvl = Access.GetSpellPerDayPerLvl(MaxSpellsByLvl.Length, Globals.DB_PLAYER_PRIEST_SPELL_PATH);
                    break;
            }
        }

        /* Set le nombre max de sorts par jour pour chaque niveau */
        internal override void SetMaxSpellNumberByLvl()
        {
            // Init ( valeur settée à 5 soit le niveau maximum que le spellbouc peut afficher pour l'instant) 
            // A remplacer quand on pourra le faire dynamiquement 
            MaxSpellsByLvl = new int[5];

            switch (_playerSpellsType)
            {
                case ContainerType.PriestPlayerSpells:
                    // Récupère les valeurs et les set dans la propriété
                    MaxSpellsByLvl = Access.GetMaxNumberByLvl(MaxSpellsByLvl.Length, Globals.DB_PLAYER_PRIEST_SPELL_PATH);
                    break;

                case ContainerType.DruidCompleteSpells:
                    // Récupère les valeurs et les set dans la propriété
                    MaxSpellsByLvl = Access.GetMaxNumberByLvl(MaxSpellsByLvl.Length, Globals.DB_PLAYER_DRUID_SPELL_PATH);
                    break;

                default:
                    // Récupère les valeurs et les set dans la propriété
                    MaxSpellsByLvl = Access.GetMaxNumberByLvl(MaxSpellsByLvl.Length, Globals.DB_PLAYER_PRIEST_SPELL_PATH);
                    break;
            }

        }

        /* Set le nombre max de sorts par jour pour chaque niveau */
        internal override void UpdateMaxSpellNumberByLvl(int[] maxSpellToUpdate)
        {
            string db = "";

            switch(_playerSpellsType)
            {
                case ContainerType.PriestPlayerSpells:
                    db = Globals.DB_PLAYER_PRIEST_SPELL_PATH;
                    break;
                case ContainerType.DruidPlayerSpells:
                    db = Globals.DB_PLAYER_DRUID_SPELL_PATH;
                    break;
            }

            ErrorCode status = Access.UpdateMaxNumberByLvl(maxSpellToUpdate, db);
            if (status == ErrorCode.SUCCESS)
            {
                MaxSpellsByLvl = maxSpellToUpdate;
            }
        }

        /********************************************* SPECIFIQUE A DIVINESPELBOOK *********************************************/
        /* Ajoute un sort quotidien */
        internal void IncrementDivinePlayerSpell(int lvl)
        {
            int spellValue = PlayerSpellsByLvl[lvl]++;
            string db = "";
            switch(_playerSpellsType)
            {
                case ContainerType.PriestPlayerSpells:
                    db = Globals.DB_PLAYER_PRIEST_SPELL_PATH;
                    break;
                case ContainerType.DruidPlayerSpells:
                    db = Globals.DB_PLAYER_DRUID_SPELL_PATH;
                    break;
            }
            Access.UpdateDivineSpellPlayerCount(lvl, spellValue, db);
            UpdateSpellNumberByLvl();

        }

        /* Retire un sort quotidien */
        internal void DecrementDivinePlayerSpell(int lvl)
        {
            int spellValue = PlayerSpellsByLvl[lvl]--;
            string db = "";
            switch (_playerSpellsType)
            {
                case ContainerType.PriestPlayerSpells:
                    db = Globals.DB_PLAYER_PRIEST_SPELL_PATH;
                    break;
                case ContainerType.DruidPlayerSpells:
                    db = Globals.DB_PLAYER_DRUID_SPELL_PATH;
                    break;
            }
            Access.UpdateDivineSpellPlayerCount(lvl, spellValue, db);
            UpdateSpellNumberByLvl();
        }
    }
}
