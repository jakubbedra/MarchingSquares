using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MarchingSquares.ViewModel.Commands;

namespace MarchingSquares.ViewModel;

public class MarchingSquaresViewModel : INotifyPropertyChanged
{
    private int _isoLevelsCount;

    public int IsoLevelsCount
    {
        get => _isoLevelsCount;
        set
        {
            _isoLevelsCount = value;
            OnPropertyChanged(nameof(IsoLevelsCount));
        }
    }

    private int _stepSize;
    public int StepSize
    {
        get => _stepSize;
        set
        {
            _stepSize = value;
            OnPropertyChanged(nameof(StepSize));
        }
    }

    public ICommand DoMarchingSquaresCommand { get; set; }
    
    public MainWindowViewModel MainModel { get; set; }//todo
    
    public MarchingSquaresViewModel()
    {
        IsoLevelsCount = 5;
        StepSize = 10;
        DoMarchingSquaresCommand = new DoMarchingSquaresCommand(this);
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}