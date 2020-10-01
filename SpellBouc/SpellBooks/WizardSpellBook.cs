using SpellBouc.UIClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpellBouc
{
    /* Clase de livre de sort de mage */
    class WizardSpellBook : SpellBook
    {

        internal override SpellContainer PlayerSpells { get; set; }

        internal override SpellContainer CompleteClassSpell { get; set; }

        internal UISpellContainer UIPlayerSpells { get; set;}

        /* Initialisation des membres */
        internal WizardSpellBook()
        {
            // Récupère tous les sorts que le joueur a dans son grimoire 
            PlayerSpells = new SpellContainer(ContainerType.WizardPlayerSpells);
            // Récupère tous les sorts de mages
            CompleteClassSpell = new SpellContainer(ContainerType.WizardCompleteSpells);
            // Récupère toutes les utilisations quotidiennes des sorts du joueurs, et les infos à afficher dans les UIs
            UIPlayerSpells = new UISpellContainer(UIContainerType.UIWizardSpell);
            FillMissingUIInfosFromPlayerSpells();
        }

        /********************************************* IMPLEMENTATIONS *********************************************/

        /* Ajoute un spell dans le livre de sort (implémentation à partir de la classe mère SpellBook) input: id */
        internal override void AddSpellInSpellBook(int id)
        {
            var spellToAdd = CompleteClassSpell.GetSpell(id);

            // Met à jour les BDD et le PlayerSpells
            var status = PlayerSpells.AddSpellInBookAndBD(spellToAdd, ContainerType.WizardPlayerSpells);

            if (status == ErrorCode.SUCCESS)
            {
                // Met à jour les UIs
                status = UIPlayerSpells.AddUISpellInBookAndBD(spellToAdd, UIContainerType.UIWizardSpell);
                if(status != ErrorCode.SUCCESS)
                {
                    //TODO: Erreur si l'upload n'a pas marché. msgbox ?
                }
            }
            else
            {
                //TODO: Erreur si l'upload n'a pas marché. msgbox ?
            }
        }

        /* Ajoute un spell dans le livre de sort (implémentation à partir de la classe mère SpellBook) input: name */
        internal override void AddSpellInSpellBook(string name)
        {
            var spellToAdd = CompleteClassSpell.GetSpell(name);

            // Met à jour les BDD et le PlayerSpells
            var status = PlayerSpells.AddSpellInBookAndBD(spellToAdd, ContainerType.WizardPlayerSpells);

            if (status == ErrorCode.SUCCESS)
            {
                // Met à jour les UIs
                status = UIPlayerSpells.AddUISpellInBookAndBD(spellToAdd, UIContainerType.UIWizardSpell);
                if (status != ErrorCode.SUCCESS)
                {
                    //TODO: Erreur si l'upload n'a pas marché. msgbox ?
                }
            }
            else
            {
                //TODO: Erreur si l'upload n'a pas marché. msgbox ?
            }
        }

        /* Enlève un sort dans le livre du sort du joueur (implémentation à partir de la classe mère SpellBook) input: name */
        internal override void RemoveSpellInSpellBook(string name)
        {
            var spellToRemove = CompleteClassSpell.GetSpell(name);

            // Met à jour les BDD et le PlayerSpells
            var status = PlayerSpells.RemoveSpellInBookAndBD(spellToRemove, ContainerType.WizardPlayerSpells);
            if (status == ErrorCode.SUCCESS)
            {
                // Met à jour les UIs
                status = UIPlayerSpells.RemoveUISpellInBookAndBD(spellToRemove, UIContainerType.UIWizardSpell);
                if (status != ErrorCode.SUCCESS)
                {
                    //TODO: Erreur si l'upload n'a pas marché. msgbox ?
                }
            }
            else
            {
                //TODO: Erreur si l'upload n'a pas marché. msgbox ?
            }
        }

        /* Enlève un sort dans le livre du sort du joueur (implémentation à partir de la classe mère SpellBook) input: id */
        internal override void RemoveSpellInSpellBook(int id)
        {
            var spellToRemove = CompleteClassSpell.GetSpell(id);

            // Met à jour les BDD et le PlayerSpells
            var status = PlayerSpells.RemoveSpellInBookAndBD(spellToRemove, ContainerType.WizardPlayerSpells);
            if (status == ErrorCode.SUCCESS)
            {
                // Met à jour les UIs
                status = UIPlayerSpells.RemoveUISpellInBookAndBD(spellToRemove, UIContainerType.UIWizardSpell);
                if (status != ErrorCode.SUCCESS)
                {
                    //TODO: Erreur si l'upload n'a pas marché. msgbox ?
                }
            }
            else
            {
                //TODO: Erreur si l'upload n'a pas marché. msgbox ?
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



        /********************************************* SPECIFIQUE A WIZARDSPELLBOOK *********************************************/

        /* Ajoute un sort quotidien */
        internal void IncrementWizardPlayerSpell(int id)
        {
            var spellToIncrement = CompleteClassSpell.GetSpell(id);
            UIPlayerSpells.IncrementWizardSpellPlayerCount(spellToIncrement);

        }

        /* Retire un sort quotidien */
        internal void DecrementWizardPlayerSpell(int id)
        {
            var spellToIncrement = CompleteClassSpell.GetSpell(id);
            UIPlayerSpells.DecrementWizardSpellPlayerCount(spellToIncrement);

        }

    }
}
 