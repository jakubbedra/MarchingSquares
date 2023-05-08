using System.Windows;
using MarchingSquares.ViewModel;

namespace MarchingSquares.View;

public partial class GeneratorWindow : Window
{
    public GeneratorWindow(MainWindowViewModel mainWindowViewModel)
    {
        InitializeComponent();
        
        GeneratorViewModel viewModel = (GeneratorViewModel)MainGrid.DataContext;
        viewModel.MainModel = mainWindowViewModel;
        viewModel.Window = this;
    }
}