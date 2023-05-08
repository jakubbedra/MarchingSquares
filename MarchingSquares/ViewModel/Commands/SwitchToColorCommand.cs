namespace MarchingSquares.ViewModel.Commands;

public class SwitchToColorCommand : CommandBase<MainWindowViewModel>
{
    public SwitchToColorCommand(MainWindowViewModel viewModel) : base(viewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _viewModel.SwitchToColor();
    }
}