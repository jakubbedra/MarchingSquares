using System;
using System.Windows.Input;

namespace MarchingSquares.ViewModel.Commands;

public class DoMarchingSquaresCommand : CommandBase<MarchingSquaresViewModel>
{
    public DoMarchingSquaresCommand(MarchingSquaresViewModel viewModel) : base(viewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _viewModel.ApplyMarchingSquares();
    }
    
    public override bool CanExecute(object? parameter)
    {
        return _viewModel.MainModel != null && _viewModel.MainModel.ReadBitmap != null;
    }
    
}