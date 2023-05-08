namespace MarchingSquares.ViewModel.Commands;

public class OpenSaveFileDialogCommand : CommandBase<MainWindowViewModel>
{
    public OpenSaveFileDialogCommand(MainWindowViewModel viewModel) : base(viewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _viewModel.OpenSaveFileDialog();
    }
    
}