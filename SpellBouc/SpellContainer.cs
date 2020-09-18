using System;
using System.Collections.Generic;
using System.Text;

namespace SpellBouc
{
    /*
     * Classe qui sert de container aux sorts et leur contenu. Cette classe est l'interface avec la couche Acess.
     * Elle est composée d'une liste de sorts et toutes les fonctions qui permetront de lister, accéder, trier, afficher ect...
     */

    class SpellContainer
    {
        private List<Spell> spells { get; set; }

        /* 
         * Constructeur qui construit le containeur de sort dépendament de son type
         * Chaque type appelera un appel différent de la BDD: 
                * Les PlayerSpells appellent l'entièreté de la BDD du jouer pour une classe spécifique 
                * Les CompleteSpells appellent l'entièreté  de la BDD des sorts éxistants pour une classe spécifique 
         */

        public SpellContainer(ContainerType containerType)
        {
            switch (containerType)
            {
                case ContainerType.WizardPlayerSpells:
                    break; 

                case ContainerType.WizardCompleteSpells:
                    Access.GetWizardCompleteSpells();
                    break;

                case ContainerType.PriestPlayerSpells:
                    break;

                case ContainerType.PriestCompleteSpells:
                    break;

                default:
                    break;
            }  
        }
    }
}
