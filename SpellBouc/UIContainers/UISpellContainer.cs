using SpellBouc.AccessLayer;
using SpellBouc.Logs;
using SpellBouc.UISpells;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/* Conteneur de UISPells, fonctionne de la même façon que le SpellContainer mais pour les UISpells */
namespace SpellBouc.UIContainers
{
    class UISpellContainer: IEnumerable
    { 
        internal string Type { get; set; }

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

                /* Dans le cas d'un UIWizardCompletSpell UiSpells devient une List<UIWizardPlayerSpell> (empty qui sera remplie ulterieurement)*/
                case UIContainerType.UIWizardCompletSpell:
                    UiSpells = new List<UIWizardPlayerSpell>().Select(x => (dynamic)x).ToList();
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
                        AddUiSpell(uiSpell);
                    }
                    else
                    {
                        Log.GenerateLog(status, "Erreur lors de l'ajout des UIs dans la fonction CreateUISpellFromSpell");
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
                        RemoveUiSpell(spell);
                    }
                    else
                    {
                        Log.GenerateLog(status, "Erreur lors de la supression des UIs dans la fonction CreateUISpellFromSpell");
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
        internal dynamic CreateUISpellFromSpell(Spell spell, UIContainerType uiContainerType = default)
        {
            switch (uiContainerType)
            {
                case UIContainerType.UIWizardSpell:
                    var uiWizardPlayerSpell = new UIWizardPlayerSpell
                    {
                        Id = spell.Id,
                        Lvl = spell.Lvl,
                        Name = spell.Name,
                        School = spell.Type,
                        PlayerSpellCount = 0,
                        Description = spell.Description
                    };
                    return uiWizardPlayerSpell;

            // Default sera utilisé pour des UISpells classiques
                default:
                    var returnedSpell = new UiSpell
                    {
                        Id = spell.Id,
                        Lvl = spell.Lvl,
                        Name = spell.Name,
                        School = spell.Type,
                        Description = spell.Description
                    };
                    return returnedSpell;
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
                var returnUiSpell = CreateUISpellFromSpell(InputuiSpellToAdd, uiContainerType);
                UiSpells.Add(InputuiSpellToAdd);
            }
            catch
            {
                Log.GenerateLog(ErrorCode.ERROR, "Erreur lors de l'ajout d'un UISpell dans le container ");
            }
        }

        /*
         * Ajoute un UIspell dans la liste l'input peut être un spell ou un UISpell traité dans un Try/Catch.
         */
        internal void AddUiSpell(dynamic InputuiSpellToAdd)
        {
            foreach (var uiSpell in UiSpells)
            {
                // Dans le cas où le sort existe déjà on abort l'ajout.
                if (InputuiSpellToAdd.Id == uiSpell.Id)
                {
                    return;
                }
            }
            UiSpells.Add(InputuiSpellToAdd);     
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
                    UiSpells.Remove(UiSpells);
                    return;
                }
            }
        }

        /*
         * Incrémente un sort de mage journalier dans la BDD et dans le UIPlayerSpells
         */
        internal void IncrementWizardSpellPlayerCount(Spell spell)
        {
            try
            {
                foreach (UIWizardPlayerSpell iuSpell in UiSpells)
                {
                    if (iuSpell.Id == spell.Id)
                    {
                        Access.ChangeWizardSpellPlayerCount(spell.Id, iuSpell.PlayerSpellCount +1);
                        iuSpell.PlayerSpellCount++;
                        return;
                    } 
                }
            }
            catch 
            {
                Log.GenerateLog(ErrorCode.ERROR, "Erreur lors l'incrémentation du sort ");
            }
        }

        /*
         * Incrémente un sort de mage journalier dans la BDD et dans le UIPlayerSpells
         */
        internal void DecrementWizardSpellPlayerCount(Spell spell)
        {
            try
            {
                foreach (UIWizardPlayerSpell iuSpell in UiSpells)
                {
                    if (iuSpell.Id == spell.Id)
                    {
                        if(iuSpell.PlayerSpellCount > 0)
                        {
                            Access.ChangeWizardSpellPlayerCount(spell.Id, iuSpell.PlayerSpellCount - 1);
                            iuSpell.PlayerSpellCount--;
                            return;
                        }
                    }
                }
            }
            catch
            {
                Log.GenerateLog(ErrorCode.ERROR, "Erreur lors la décrémentation du sort ");
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

        /*
         * Check les doublons 
         */
        internal bool IsDuplicated(dynamic spellToAdd)
        {
            foreach (var spell in UiSpells)
            {
                if (spellToAdd.Id == spell.Id)
                {
                    return true;
                }
            }
            return false;
        }


        public IEnumerator GetEnumerator()
        {
            return UiSpells.GetEnumerator();
        }
    }
}
