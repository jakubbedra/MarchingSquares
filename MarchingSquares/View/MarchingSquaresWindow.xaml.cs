using System.Windows;
using MarchingSquares.ViewModel;

namespace MarchingSquares.View;

public partial class MarchingSquaresWindow : Window
{
    public MarchingSquaresWindow(MainWindowViewModel mainWindowViewModel)
    {
        InitializeComponent();
        
        MarchingSquaresViewModel viewModel = (MarchingSquaresViewModel)MainGrid.DataContext;
        viewModel.MainModel = mainWindowViewModel;
        viewModel.Window = this;
    }
}