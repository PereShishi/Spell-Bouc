using SpellBouc.Logs;
using SpellBouc.UIClasses;
using System.Collections.Generic;
using System.Linq;

namespace SpellBouc
{
    /* Clase de livre de sort de mage */
    class WizardSpellBook : SpellBook
    {

        internal override SpellContainer PlayerSpells { get; set; }

        internal override SpellContainer CompleteClassSpells { get; set; }

        internal override UISpellContainer UIPlayerSpells { get; set;}

        internal override UISpellContainer UICompleteClassSpells { get; set; }

        
        /* Initialisation des membres */
        internal WizardSpellBook()
        {
            // Récupère tous les sorts que le joueur a dans son grimoire 
            PlayerSpells = new SpellContainer(ContainerType.WizardPlayerSpells);
            // Récupère tous les sorts de mages
            CompleteClassSpells = new SpellContainer(ContainerType.WizardCompleteSpells);
            // Récupère toutes les utilisations quotidiennes des sorts du joueurs, et les infos à afficher dans les UIs
            UIPlayerSpells = new UISpellContainer(UIContainerType.UIWizardSpell);
            FillMissingUIInfosFromPlayerSpells();
            // Set le nombre d'onglets.
            UpdateMaxLvlSpell();

            UICompleteClassSpells = new UISpellContainer();
            // Initialise CompleteClassSpells & update les sorts qui sont ajoutables
            InitUICompleteClassSpells();
            UpdateUICompleteClassSpell();
            UpdateSpellNumberByLvl();
        }

        /********************************************* IMPLEMENTATIONS *********************************************/

        /* Ajoute un spell dans le livre de sort (implémentation à partir de la classe mère SpellBook) input: id */
        internal override void AddSpellInSpellBook(int id)
        {
            if (PlayerSpells.IsDuplicated(id) == true) return;
            var spellToAdd = CompleteClassSpells.GetSpell(id);

            // Met à jour les BDD et le PlayerSpells
            var status = PlayerSpells.AddSpellInBookAndBD(spellToAdd, ContainerType.WizardPlayerSpells);

            if (status == ErrorCode.SUCCESS)
            {
                if (UIPlayerSpells.IsDuplicated(spellToAdd) == true) return;
                // Met à jour les UIs
                status = UIPlayerSpells.AddUISpellInBookAndBD(spellToAdd, UIContainerType.UIWizardSpell);
                if(status != ErrorCode.SUCCESS)
                {
                    Log.GenerateLog(status, "Erreur lors de la mise à jour des UIs dans AddUISpellInBookAndBD");
                }
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
            if (PlayerSpells.IsDuplicated(spellToAdd.Id) == true) return;

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

        /* Remplir les données manquantes de UIPlayerSpells après son initialisation (Lvl, Name, Description) */
        internal override void FillMissingUIInfosFromPlayerSpells()
        {
            var tempUIPlayerSpells = new List<UIWizardPlayerSpell>();
            foreach (Spell playerSpell in PlayerSpells)
            {
                foreach (UIWizardPlayerSpell uiWizardPlayerSpell in UIPlayerSpells)
                {
                    if (playerSpell.Id == uiWizardPlayerSpell.Id)
                    {
                        uiWizardPlayerSpell.Lvl = playerSpell.Lvl;
                        uiWizardPlayerSpell.Name = playerSpell.Name;
                        uiWizardPlayerSpell.Name = "Description";
                        // TODO: Description 

                        tempUIPlayerSpells.Add(uiWizardPlayerSpell);
                    }
                }
            }

            UIPlayerSpells.AddRange(tempUIPlayerSpells.Select(x => (dynamic)x).ToList());
        }

        /* Récupère le nombre de sorts par niveau */
        internal override void UpdateSpellNumberByLvl()
        {
            // Set le tableur:
            for (int i = 0; i < MaxLvlSpell + 1; i++)
            {
                SpellNumberByLvl[i] = 0;
            }

            // Compte  
            foreach (UIWizardPlayerSpell spell in UIPlayerSpells)
            {
                SpellNumberByLvl[spell.Lvl] = SpellNumberByLvl[spell.Lvl] + spell.PlayerSpellCount;
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
 