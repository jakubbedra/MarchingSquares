﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MarchingSquares.Service;
using MarchingSquares.ViewModel.Commands;
using Microsoft.Win32;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;

namespace MarchingSquares.ViewModel;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private BitmapImage _visibleImage;

    public BitmapImage VisibleImage
    {
        get { return _visibleImage; }
        set
        {
            _visibleImage = value;
            OnPropertyChanged(nameof(VisibleImage));
        }
    }

    public ICommand OpenFileDialogCommand { get; set; }
    public ICommand DoMarchingSquaresCommand { get; set; }

    private Bitmap? _readBitmap;

    public Bitmap? ReadBitmap
    {
        get { return _readBitmap; }
        set
        {
            _readBitmap = value; 
            OnPropertyChanged(nameof(ReadBitmap));
            CommandManager.InvalidateRequerySuggested();
        }
    }

    private readonly BitmapService _bitmapService;

    public MainWindowViewModel()
    {
        _bitmapService = new BitmapService();
        OpenFileDialogCommand = new OpenFileDialogCommand(this);
        DoMarchingSquaresCommand = new DoMarchingSquaresCommand(this);
        VisibleImage = new BitmapImage();
    }

    public void OpenFileDialog()
    {
        var dialog = new OpenFileDialog();
        dialog.Filter = "Bitmap files (*.bmp)|*.bmp|All files (*.*)|*.*";
        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        string path = "";
        bool? result = dialog.ShowDialog();
        if (result == true)
        {
            path = dialog.FileName;
            // generate bitmap
            var bitmap = new Bitmap(path);
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

            VisibleImage = img;
            ReadBitmap = bitmap;
            // todo: also clear any layers other than the main one
        }
    }

    public void ApplyMarchingSquares()
    {
        Bitmap bitmap = new Bitmap(ReadBitmap);
        float[,] noiseMap = _bitmapService.ImageToNoiseMap(bitmap);
        List<Tuple<PointF,PointF>> contours = _bitmapService.DoMarchingSquares(noiseMap, ReadBitmap.Width, ReadBitmap.Height);
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
        
        VisibleImage = img;
        
        //VisibleImage = img;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}