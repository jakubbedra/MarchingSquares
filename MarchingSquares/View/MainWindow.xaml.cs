using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MarchingSquares.View;
using MarchingSquares.ViewModel;

namespace MarchingSquares
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void OnAdd(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel viewModel = (MainWindowViewModel)MainGrid.DataContext;
            GeneratorWindow window = new GeneratorWindow(viewModel);
            window.Show();
        }

        private void OnMarchingSquares(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel viewModel = (MainWindowViewModel)MainGrid.DataContext;
            MarchingSquaresWindow window = new MarchingSquaresWindow(viewModel);
            window.Show();
        }
    }
}