using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace SpellBouc.ViewModel.Commands
{
    public class WAddTabCommand : ICommand
    {
        public WTabViewModel TabVM { get; set; }

        internal WAddTabCommand(WTabViewModel wizardSpellTabViewModel)
        {
            TabVM = wizardSpellTabViewModel;
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
