using System;
using System.Windows.Input;

namespace MarchingSquares.ViewModel.Commands;

public class DoMarchingSquaresCommand : CommandBase<MainWindowViewModel>
{
    public DoMarchingSquaresCommand(MainWindowViewModel viewModel) : base(viewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _viewModel.ApplyMarchingSquares();
    }
    
    public override bool CanExecute(object? parameter)
    {
        return _viewModel.ReadBitmap != null;
    }
    
}