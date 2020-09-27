using SpellBouc.AccessLayer;
using System;
using System.Collections.Generic;
using System.Printing.IndexedProperties;
using System.Text;
using System.Windows.Media;

namespace SpellBouc
{
    /* Clase de livre de sort de mage */
    class WizardSpellBook : SpellBook
    {

        public override SpellContainer PlayerSpells { get; set; }

        public override SpellContainer CompleteClassSpell { get; set; }

        public List<UIWizardPlayerSpell> UIPlayerSpells{ get; set;}

        /* Initialisation des membres */
        public WizardSpellBook()
        {
            PlayerSpells = new SpellContainer(ContainerType.WizardPlayerSpells);
            CompleteClassSpell = new SpellContainer(ContainerType.WizardCompleteSpells);
            getPlayerSpellsUI();
        }

        /* Ajoute un spelle dans le livre de sort (implémentation de la classe mère)  */
        public override void AddSpellInPlayerBook(String name)
        {
            var spellToAdd = CompleteClassSpell.GetSpell(name);

            var status = PlayerSpells.AddSpellInBookAndBD(spellToAdd, ContainerType.WizardPlayerSpells);

            if(status == ErrorCode.ERROR)
            {
                //TODO: Erreur si l'upload n'a pas marché. msgbox ?
            }
        }

        /* Enlève un sort dans le livre du sort du joueur */
        public override void RemoveSpellInPlayerBook(String name)
        {
            var spellToRemove = CompleteClassSpell.GetSpell(name);

            var status = PlayerSpells.RemoveSpellInBookAndBD(spellToRemove, ContainerType.WizardPlayerSpells);

            if (status == ErrorCode.ERROR)
            {
                //TODO: Erreur si l'upload n'a pas marché. msgbox ?
            }
        }

        /* Récupère les infos du nomre d'utilisation quotidiennes des sorts du joueur */
        private void getPlayerSpellsUI()
        {
            // Récupère juste les ID/Use de chaque sorts du joueurs
            UIPlayerSpells = Access.GetAllPlayerCount();

            // Init les conteurs de spells à zero
            InitUIPlayerSpells();

            // Fait le lien avec la PlayerSpells pour remplir les autres champs de UIPlayerSpells:
            FillMissingUIInfosFromPlayerSpells();

        }

        /* Initialise les PlayerSpellCount des UIPlayerSpells à 0 (évite les NULL) */
        private void InitUIPlayerSpells()
        {
            foreach (var UISpell in UIPlayerSpells)
            {
                UISpell.PlayerSpellCount = 0;
            }
        }

        /* Remplir les données manquantes de UIPlayerSpells après son initialisation (Lvl, Name, Description) */
        private void FillMissingUIInfosFromPlayerSpells()
        {
            var tempUIPlayerSpells = new List<UIWizardPlayerSpell>();
            foreach (Spell playerSpell in PlayerSpells)
            {
                foreach (var uiWizardPlayerSpell in UIPlayerSpells)
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
            UIPlayerSpells.AddRange(tempUIPlayerSpells);
        }

        /* Ajoute un sort quotidien */
        private void IncrementWizardPlayerSpell(String name)
        {
            var status = ErrorCode.SUCCESS;
            var spellToIncrement = CompleteClassSpell.GetSpell(name);

            if (status == ErrorCode.ERROR)
            {  
                //TODO: Erreur si l'upload n'a pas marché. msgbox ?
            }
        }

    }
}
 