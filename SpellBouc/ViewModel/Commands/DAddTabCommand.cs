using System;
using System.Windows.Input;

namespace SpellBouc.ViewModel.Commands
{
    public class DAddTabCommand : ICommand
    {
        public DTabViewModel TabVM { get; set; }

        internal DAddTabCommand(DTabViewModel divineSpellTabViewModel)
        {
            TabVM = divineSpellTabViewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //You could place your own logic here that would enable or disable the
        //button by returning true/false.
        public Boolean CanExecute(Object parameter)
        {
            return true;
        }

        //This gets fired when the command is executed (i.e. the button has been clicked).
        public void Execute(Object parameter)
        {
            TabVM.AddTab();
        }
    }
}
