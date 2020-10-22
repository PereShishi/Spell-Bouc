using System;
using System.Windows.Input;

namespace SpellBouc.ViewModel.Commands
{
    public class SimpleCommand : ICommand
    {
        public WTabViewModel TabVM { get; set; }

        public SimpleCommand(WTabViewModel wizardSpellTabViewModel)
        {
            TabVM = wizardSpellTabViewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            TabVM.SimpleMethod();
        }
    }
}
