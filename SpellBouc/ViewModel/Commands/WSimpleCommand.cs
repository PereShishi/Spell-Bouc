using System;
using System.Windows.Input;

namespace SpellBouc.ViewModel.Commands
{
    /* Classe qui définit les SimpleCommand qui héritent de ICommand, implémentation très basique de l'interface excépété que le execute est définit en fonction du WSimpleCommandType
     * Cela permet d'éviter de copier du code, et d'avoir une même classe pour des ICommands basiques.
     */
    public class WSimpleCommand : ICommand
    {
        public WTabViewModel TabVM { get; set; }
        private readonly WSimpleCommandType _commandType;

        internal WSimpleCommand(WTabViewModel wizardSpellTabViewModel, WSimpleCommandType simpleCommandType)
        {
            TabVM = wizardSpellTabViewModel;
            _commandType = simpleCommandType;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        /* Bind la WSimple commande a une methode dans le VM dépendament du WSimpleCommandType définit à sa création */
        public void Execute(object parameter)
        {
            int id = (int)parameter;

            switch (_commandType)
            {
                case WSimpleCommandType.AddSpell:
                    TabVM.AddSpell(id);
                    break;
                case WSimpleCommandType.RemoveSpell:
                    TabVM.RemoveSpell(id);
                    break;
                case WSimpleCommandType.IncrementSpellCount:
                    TabVM.IncrementSpellCount(id);
                    break;
                case WSimpleCommandType.DecrementSpellCount:
                    TabVM.DecrementSpell(id);
                    break;
                default:
                    Console.WriteLine("Une Icommand a été éxécutée sans binding !");
                    break; ;
            }
        }
    }
}
