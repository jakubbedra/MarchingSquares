using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MarchingSquares.Model;
using MarchingSquares.Service;
using MarchingSquares.View;
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

    public MainWindowViewModel MainModel { get; set; } 
    public MarchingSquaresWindow Window { get; set; }

    private readonly BitmapService _bitmapService;

    public MarchingSquaresViewModel()
    {
        IsoLevelsCount = 5;
        StepSize = 10;
        DoMarchingSquaresCommand = new DoMarchingSquaresCommand(this);
        _bitmapService = new BitmapService();
    }

    public void ApplyMarchingSquares()
    {
        MainModel.Layers.Clear();
        Bitmap bitmap = new Bitmap(MainModel.ReadBitmap);
        float[,] noiseMap = _bitmapService.ImageToNoiseMap(bitmap);
        List<Tuple<PointF, PointF>> contours = _bitmapService.DoMarchingSquares(
            noiseMap, MainModel.ReadBitmap.Width, MainModel.ReadBitmap.Height, IsoLevelsCount, StepSize
        );

        if (MainModel.ColorVisible)
        {
            bitmap = _bitmapService.NoiseMapToColorImage(noiseMap);
        }

        MainModel.Layers.Add(new MarchingSquaresLayer() { Layer = contours });

        // Draw each line segment of the contour
        Pen pen = new Pen(Color.Red, 1);
        Graphics graphics = Graphics.FromImage(bitmap);
        foreach (Tuple<PointF, PointF> line in contours)
        {
            graphics.DrawLine(pen, line.Item1, line.Item2);
        }

        // todo: currently it applies the isolines to the map, we would like the lines to be separate later on
        BitmapImage img = new BitmapImage();
        using (var stream = new MemoryStream())
        {
            bitmap.Save(stream, ImageFormat.Bmp);
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