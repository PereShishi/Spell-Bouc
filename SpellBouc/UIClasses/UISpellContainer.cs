using SpellBouc.AccessLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

/* Conteneur de UISPells, fonctionne de la même façon que le SpellContainer mais pour les UISpells */
namespace SpellBouc.UIClasses
{
    class UISpellContainer: IEnumerable
    { 
        private List<dynamic> UiSpells { get; set; }

        /* 
         * Constructeur qui construit le containeur de UIsort dépendament de son type.
         * Chaque type appelera un appel différent de la BDD: 
         *      * Les UIWizardSpell appellent la BDD qui stock le nombre d'utilisations journalière des sorts de mages du joueur.
         *      * Les UIPriestSpell appellent la BDD qui stock le nombre d'utilisations journalière des sorts de prêtre du joueur.
         */

        internal UISpellContainer(UIContainerType uiContainerType)
        {
            switch (uiContainerType)
            {
                /* Dans le cas d'un UIWizardSpell UiSpells devient une List<UIWizardPlayerSpell> */
                case UIContainerType.UIWizardSpell:
                    UiSpells = Access.GetUIWizardSpellsFromDB().Select(x => (dynamic)x).ToList();
                    //IEnumerable<UIWizardSpell> query1 = (IEnumerable<UIWizardSpell>)UiSpells.OrderBy(spell => spell.Id);
                    break;
                    
                /* Dans le cas d'un UIPriestSpell UiSpells devient une List<UIPriestPlayerSpell> */
                case UIContainerType.UIPriestSpell:
                    // TODO: Implementer les sorts de prêtre.
                    break;

                default:
                    break;
            }
        }

        /*
         * Ajoute un UIspell dans la liste ET dans la BDD.
         */
        internal ErrorCode AddUISpellInBookAndBD(Spell spell, UIContainerType uiContainerType)
        {
            ErrorCode status;
            
            switch (uiContainerType)
            {
                // Mise à jour sort de Mage
                case UIContainerType.UIWizardSpell:
                    UiSpell uiSpell = CreateUISpellFromSpell(spell, uiContainerType);
                    status = Access.AddSpellInUiDB(spell.Id);
                    if (status != ErrorCode.ERROR)
                    {
                        AddUiSpell(uiSpell, uiContainerType);
                    }
                    else
                    {
                        // TODO: Implementation de l'erreur
                        status = ErrorCode.ERROR;
                    }
                    break;

                case UIContainerType.UIPriestSpell:
                    status = ErrorCode.ERROR;
                    //TODO: implémenter les UIPrêtre
                    break;

                // Default case: ne peut pas rentrer dans cette étape théoriquement
                default:
                    status = ErrorCode.ERROR;
                    break;
            }

            return status;
        }

        /*
         * Retire un UIspell dans la liste ET dans la BDD.
         */
        internal ErrorCode RemoveUISpellInBookAndBD(Spell spell, UIContainerType uiContainerType)
        {
            ErrorCode status;

            switch (uiContainerType)
            {
                // Mise à jour sort de Mage
                case UIContainerType.UIWizardSpell:
                    status = Access.RemoveSpellInUiDB(spell.Id);
                    if (status != ErrorCode.ERROR)
                    {
                        RemoveUiSpell(spell.Id);
                    }
                    else
                    {
                        // TODO: Implementation de l'erreur
                        status = ErrorCode.ERROR;
                    }
                    break;

                case UIContainerType.UIPriestSpell:
                    status = ErrorCode.ERROR;
                    //TODO: implémenter les UIPrêtre
                    break;

                // Default case: ne peut pas rentrer dans cette étape théoriquement
                default:
                    status = ErrorCode.ERROR;
                    break;
            }

            return status;
        }

        /*
         * Créer un UISpell à partir d'un Spell.
         */
        private dynamic CreateUISpellFromSpell(Spell spell, UIContainerType uiContainerType)
        {
            switch (uiContainerType)
            {
                case UIContainerType.UIWizardSpell:
                    var uiWizardPlayerSpell = new UIWizardPlayerSpell();

                    uiWizardPlayerSpell.Id = spell.Id;
                    uiWizardPlayerSpell.Lvl = spell.Lvl;
                    uiWizardPlayerSpell.Name = spell.Name;
                    uiWizardPlayerSpell.PlayerSpellCount = 0;
                    uiWizardPlayerSpell.Description = "Description";
                    return uiWizardPlayerSpell;

                case UIContainerType.UIPriestSpell:
                    //TODO: implémenter les UIPrêtre
                    return 0;

                default:
                    return 0;
            }
                
        }

        /*
         * Ajoute un UIspell dans la liste l'input peut être un spell ou un UISpell traité dans un Try/Catch.
         */
        internal void AddUiSpell(dynamic InputuiSpellToAdd, UIContainerType uiContainerType)
        {
            // Si l'input est un Spell 
            try
            {
                var returnUiSpell = CreateUISpellFromSpell((Spell)InputuiSpellToAdd, uiContainerType);
                UiSpells.Add(InputuiSpellToAdd);
            }
            // Sinon c'est c'est le même type que UiSpells, qu'on ajoute dans la liste.
            catch
            {
                try
                {
                    foreach (var uiSpell in UiSpells)
                    {
                        // Dans le cas où le sort existe déjà on abort l'ajout.
                        if (InputuiSpellToAdd == uiSpell)
                        {
                            return;
                        }
                    }
                    UiSpells.Add(InputuiSpellToAdd);
                }
                catch
                {
                    // TODO: gerrer l'erreur.
                }
            }
        }

        /*
         * Supprime un UIspell dans la liste. input Ui
         */
        internal void RemoveUiSpell(dynamic uiSpellToRemove)
        {
            foreach (var uiSpell in UiSpells)
            {
                if (uiSpellToRemove.Id == uiSpell.Id)
                {
                    UiSpells.Remove(uiSpell);
                }
            }
        }

        /*
         * Supprime un UIspell dans la liste. Input Id
         */
        internal void RemoveUiSpell(string id)
        {
            foreach (var uiSpell in UiSpells)
            {
                if (id == uiSpell.Id)
                {
                    UiSpells.Remove(uiSpell);
                }
            }
        }

        /*
         * Ajoute une liste de UISpells à la liste éxistante (exclue les doublons).
         */
        internal void AddRange(List<dynamic> inputUiSpells)
        {
            bool isDuplicated;
            foreach (var inputUiSpell in inputUiSpells)
            {
                isDuplicated = false;
                foreach (var uiSpell in UiSpells)
                {
                    if(uiSpell.Id == inputUiSpell.Id)
                    {
                        isDuplicated = true;
                        continue;
                    }
 
                }
                if (isDuplicated == false) UiSpells.Add(inputUiSpell);
            }
        }


        public IEnumerator GetEnumerator()
        {
            return UiSpells.GetEnumerator();
        }
    }
}
