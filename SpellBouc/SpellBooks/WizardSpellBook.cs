using SpellBouc.AccessLayer;
using SpellBouc.Logs;
using SpellBouc.Model.Wizard;
using SpellBouc.UIContainers;
using System.Collections.Generic;
using System.Linq;

namespace SpellBouc.SpellBooks
{
    /* Clase de livre de sort de mage */
    public class WizardSpellBook : SpellBook
    {

        internal override SpellContainer PlayerSpells { get; set; }

        internal override SpellContainer CompleteClassSpells { get; set; }

        internal override UISpellContainer UIPlayerSpells { get; set; }

        internal override UISpellContainer UICompleteClassSpells { get; set; }


        /* Initialisation des membres */
        public WizardSpellBook()
        {
            // Récupère tous les sorts que le joueur a dans son grimoire 
            PlayerSpells = new SpellContainer(ContainerType.WizardPlayerSpells);
            // Récupère tous les sorts de mages
            CompleteClassSpells = new SpellContainer(ContainerType.WizardCompleteSpells);
            // Récupère toutes les utilisations quotidiennes des sorts du joueurs, et les infos à afficher dans les UIs
            UIPlayerSpells = new UISpellContainer(UIContainerType.UIWizardSpell);
            FillMissingUIInfosFromPlayerSpells();
            // Set le nombre d'onglets (niveau max des sorts du joueur).
            UpdateMaxLvlSpell();
            // Set le nombre max de sorts par jour pour chaque niveau
            SetMaxSpellNumberByLvl();

            // Partie CompleteClassSpells
            UICompleteClassSpells = new UISpellContainer(UIContainerType.UIWizardCompletSpell);
            // Initialise CompleteClassSpells & update les sorts qui sont ajoutables
            InitUICompleteClassSpells();
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
            var status = PlayerSpells.AddSpellInBookAndBD(spellToAdd, ContainerType.WizardPlayerSpells);

            if (status == ErrorCode.SUCCESS)
            {
                if (UIPlayerSpells.IsDuplicated(spellToAdd) == true) return;
                // Met à jour les UIs
                status = UIPlayerSpells.AddUISpellInBookAndBD(spellToAdd, UIContainerType.UIWizardSpell);
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
            var status = PlayerSpells.AddSpellInBookAndBD(spellToAdd, ContainerType.WizardPlayerSpells);

            if (status == ErrorCode.SUCCESS)
            {
                if (UIPlayerSpells.IsDuplicated(spellToAdd) == true) return;
                // Met à jour les UIs
                status = UIPlayerSpells.AddUISpellInBookAndBD(spellToAdd, UIContainerType.UIWizardSpell);
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
            var status = PlayerSpells.RemoveSpellInBookAndBD(spellToRemove, ContainerType.WizardPlayerSpells);
            if (status == ErrorCode.SUCCESS)
            {
                // Met à jour les UIs
                status = UIPlayerSpells.RemoveUISpellInBookAndBD(spellToRemove, UIContainerType.UIWizardSpell);
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
            var status = PlayerSpells.RemoveSpellInBookAndBD(spellToRemove, ContainerType.WizardPlayerSpells);
            if (status == ErrorCode.SUCCESS)
            {
                // Met à jour les UIs
                status = UIPlayerSpells.RemoveUISpellInBookAndBD(spellToRemove, UIContainerType.UIWizardSpell);
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

        /* Remplir les données manquantes de UIPlayerSpells après son initialisation */
        internal override void FillMissingUIInfosFromPlayerSpells()
        {
            var tempUIPlayerSpells = new List<UIWizardPlayerSpell>();
            foreach (Spell playerSpell in PlayerSpells)
            {
                foreach (UIWizardPlayerSpell uiWizardPlayerSpell in UIPlayerSpells)
                {
                    if (playerSpell.Id == uiWizardPlayerSpell.Id)
                    {
                        uiWizardPlayerSpell.Lvl         = playerSpell.Lvl;
                        uiWizardPlayerSpell.Name        = playerSpell.Name;
                        uiWizardPlayerSpell.School      = playerSpell.Type;
                        uiWizardPlayerSpell.Description = playerSpell.Description;
                        uiWizardPlayerSpell.Source      = playerSpell.Source;
                        uiWizardPlayerSpell.Composante  = playerSpell.Composante;
                        uiWizardPlayerSpell.IncTime     = playerSpell.IncTime;
                        uiWizardPlayerSpell.Range       = playerSpell.Range;
                        uiWizardPlayerSpell.AreaEffect  = playerSpell.AreaEffect;
                        uiWizardPlayerSpell.Duration    = playerSpell.Duration;
                        uiWizardPlayerSpell.SaveDice    = playerSpell.SaveDice;
                        uiWizardPlayerSpell.MagicResist = playerSpell.MagicResist;
                        uiWizardPlayerSpell.Comp        = playerSpell.Comp;

                        tempUIPlayerSpells.Add(uiWizardPlayerSpell);
                    }
                }
            }

            UIPlayerSpells.AddRange(tempUIPlayerSpells.Select(x => (dynamic)x).ToList());
        }

        /* Récupère le nombre de sorts par jour par niveau restants */
        internal override void UpdateSpellNumberByLvl()
        {
            PlayerSpellsByLvl = new int[SpellMaxFromPlayer + 1];
            // Set le tableur:
            for (int i = 0; i < SpellMaxFromPlayer + 1; i++)
            {
                PlayerSpellsByLvl[i] = 0;
            }

            // Compte  
            foreach (UIWizardPlayerSpell spell in UIPlayerSpells)
            {
                PlayerSpellsByLvl[spell.Lvl] = PlayerSpellsByLvl[spell.Lvl] + spell.PlayerSpellCount;
            }
        }

        /* Set le nombre max de sorts par jour pour chaque niveau */
        internal override void SetMaxSpellNumberByLvl()
        {
            // Init ( valeur settée à 5 soit le niveau maximum que le spellbouc peut afficher pour l'instant) 
            // A remplacer quand on pourra le faire dynamiquement 
            MaxSpellsByLvl = new int[5];

            // Récupère les valeurs et les set dans la propriété
            MaxSpellsByLvl = Access.GetMaxNumberByLvl(MaxSpellsByLvl.Length, Globals.DB_PLAYER_WIZARD_SPELL_PATH);
        }

        /* Set le nombre max de sorts par jour pour chaque niveau */
        internal override void UpdateMaxSpellNumberByLvl(int[] maxSpellToUpdate)
        {
            ErrorCode status = Access.UpdateMaxNumberByLvl(maxSpellToUpdate, Globals.DB_PLAYER_WIZARD_SPELL_PATH);
            if (status == ErrorCode.SUCCESS)
            {
                MaxSpellsByLvl = maxSpellToUpdate;
            }
        }

        /********************************************* SPECIFIQUE A WIZARDSPELLBOOK *********************************************/

        /* Ajoute un sort quotidien */
        internal void IncrementWizardPlayerSpell(int id)
        {
            var spellToIncrement = CompleteClassSpells.GetSpell(id);
            UIPlayerSpells.IncrementWizardSpellPlayerCount(spellToIncrement);
            UpdateSpellNumberByLvl();

        }

        /* Retire un sort quotidien */
        internal void DecrementWizardPlayerSpell(int id)
        {
            var spellToIncrement = CompleteClassSpells.GetSpell(id);
            UIPlayerSpells.DecrementWizardSpellPlayerCount(spellToIncrement);
            UpdateSpellNumberByLvl();
        }
    }
}
