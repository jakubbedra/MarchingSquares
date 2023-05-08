using System;
using System.Windows.Input;

namespace MarchingSquares.ViewModel.Commands;

public abstract class CommandBase<T> : ICommand
{
    protected readonly T _viewModel;

    public CommandBase(T viewModel)
    {
        _viewModel = viewModel;
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