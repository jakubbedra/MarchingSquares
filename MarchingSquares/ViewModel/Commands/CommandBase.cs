using System;
using System.Windows.Input;

namespace MarchingSquares.ViewModel.Commands;

public abstract class CommandBase : ICommand
{
    protected readonly MainWindowViewModel _customerListViewModel;

    public CommandBase(MainWindowViewModel customerListViewModel)
    {
        _customerListViewModel = customerListViewModel;
    }

    event EventHandler ICommand.CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public virtual bool CanExecute(object? parameter)
    {
        return true;
    }
    
    public abstract void Execute(object? parameter);

    public event EventHandler? CanExecuteChanged;
    
}