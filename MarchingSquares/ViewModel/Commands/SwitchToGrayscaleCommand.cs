namespace MarchingSquares.ViewModel.Commands;

public class SwitchToGrayscaleCommand : CommandBase<MainWindowViewModel>
{
    public SwitchToGrayscaleCommand(MainWindowViewModel viewModel) : base(viewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _viewModel.SwitchToGrayscale();
    }
}