namespace MarchingSquares.ViewModel.Commands;

public class OpenFileDialogCommand : CommandBase<MainWindowViewModel>
{
    public OpenFileDialogCommand(MainWindowViewModel customerListViewModel) : base(customerListViewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _viewModel.OpenFileDialog();
    }
}