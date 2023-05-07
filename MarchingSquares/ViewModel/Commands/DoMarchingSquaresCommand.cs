using System;
using System.Windows.Input;

namespace MarchingSquares.ViewModel.Commands;

public class DoMarchingSquaresCommand : CommandBase
{
    public DoMarchingSquaresCommand(MainWindowViewModel customerListViewModel) : base(customerListViewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _customerListViewModel.ApplyMarchingSquares();
    }
    
    public override bool CanExecute(object? parameter)
    {
        return _customerListViewModel.ReadBitmap != null;
    }
    
}