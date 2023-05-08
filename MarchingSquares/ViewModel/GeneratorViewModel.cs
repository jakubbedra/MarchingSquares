using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MarchingSquares.Service;
using MarchingSquares.View;
using MarchingSquares.ViewModel.Commands;

namespace MarchingSquares.ViewModel;

public class GeneratorViewModel : INotifyPropertyChanged
{
    public bool GenerateComplicatedSpot { get; set; }

    private GeneratorType _type;

    public GeneratorType Type
    {
        get => _type;
        set
        {
            _type = value;
            OnPropertyChanged(nameof(Type));
        }
    }

    private int _width;

    public int Width
    {
        get => _width;
        set
        {
            _width = value;
            OnPropertyChanged(nameof(Width));
        }
    }

    private int _height;
    public int Height
    {
        get => _height;
        set
        {
            _height = value;
            OnPropertyChanged(nameof(Height));
        } 
    }

    public MainWindowViewModel MainModel { get; set; }
    public GeneratorWindow Window { get; set; }
    
    public ICommand GenerateBitmapCommand { set; get; }

    private readonly BitmapService _service;
    
    public GeneratorViewModel()
    {
        GenerateBitmapCommand = new GenerateBitmapCommand(this);
        GenerateComplicatedSpot = false;
        Type = GeneratorType.PerlinNoise;
        Width = 1000;
        Height = 1000;
        _service = new BitmapService();
    }

    public void GenerateBitmap()
    {
        Bitmap result;
        if (Type == GeneratorType.Gaussian)
        {
            result = _service.GenerateImagesGaussian(Width, Height, false);
        }
        else
        {
            result = _service.GenerateImagesPerlin(Width, Height, false);
        }

        MainModel.ReadBitmap = result;
        
        if (MainModel.ColorVisible)
        {
            float[,] noiseMap = _service.ImageToNoiseMap(result);
            result = _service.NoiseMapToColorImage(noiseMap);
        }
        
        BitmapImage img = new BitmapImage();
        using (var stream = new MemoryStream())
        {
            result.Save(stream, ImageFormat.Bmp);
            stream.Position = 0;
            img.BeginInit();
            img.StreamSource = stream;
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.EndInit();
        }
        
        MainModel.VisibleImage = img;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
}