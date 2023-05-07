namespace MarchingSquares.ViewModel.Commands;

public class OpenFileDialogCommand : CommandBase
{
    public OpenFileDialogCommand(MainWindowViewModel customerListViewModel) : base(customerListViewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _customerListViewModel.OpenFileDialog();
    }
}