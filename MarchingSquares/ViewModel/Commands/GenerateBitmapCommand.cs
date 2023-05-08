using System;

namespace MarchingSquares.ViewModel.Commands;

public class GenerateBitmapCommand : CommandBase<GeneratorViewModel>
{
    public GenerateBitmapCommand(GeneratorViewModel viewModel) : base(viewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _viewModel.GenerateBitmap();
    }
}