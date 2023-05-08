using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using MarchingSquares.Algorithms.MapGeneration;

namespace MarchingSquares.Service;

public class BitmapService
{
    private const float ScaleMin = 0.1f;
    private const float ScaleMax = 5.0f;
    private const float RadiusMin = 1.0f;
    private const float RadiusMax = 50.0f;

    private readonly Random _r;

    private List<TerrainType> _regions;

    public BitmapService()
    {
        _r = new Random();
        _regions = new List<TerrainType>();

        _regions.Add(new TerrainType()
        {
            Name = "deep water",
            Height = 0.1f,
            Color = Color.DarkBlue
        });
        _regions.Add(new TerrainType()
        {
            Name = "water1",
            Height = 0.2f,
            Color = Color.MediumBlue
        });
        _regions.Add(new TerrainType()
        {
            Name = "water",
            Height = 0.3f,
            Color = Color.Blue
        });
        _regions.Add(new TerrainType()
        {
            Name = "water",
            Height = 0.3f,
            Color = Color.RoyalBlue
        });
        _regions.Add(new TerrainType()
        {
            Name = "water",
            Height = 0.36f,
            Color = Color.Khaki
        });
        _regions.Add(new TerrainType()
        {
            Name = "land1",
            Height = 0.5f,
            Color = Color.Lime
        });
        _regions.Add(new TerrainType()
        {
            Name = "land2",
            Height = 0.7f,
            Color = Color.ForestGreen
        });
        _regions.Add(new TerrainType()
        {
            Name = "land2",
            Height = 0.76f,
            Color = Color.Green
        });
        _regions.Add(new TerrainType()
        {
            Name = "mountains",
            Height = 0.8f,
            Color = Color.LightSlateGray
        });
        _regions.Add(new TerrainType()
        {
            Name = "mountains2",
            Height = 0.86f,
            Color = Color.DimGray
        });
        _regions.Add(new TerrainType()
        {
            Name = "mountains2",
            Height = 0.94f,
            Color = Color.DarkSlateGray
        });
        _regions.Add(new TerrainType()
        {
            Name = "mountains3",
            Height = 1.0f,
            Color = Color.White
        });
    }

    public Bitmap GenerateImagesPerlin(int width, int height, bool useColor, int octaves = 4, float persistance = 0.5f, float lacunarity = 2.0f)
    {
        PerlinNoiseGenerator perlinNoiseGenerator = new PerlinNoiseGenerator(height, width);

        float scale = _r.NextSingle() * (ScaleMax - ScaleMin) + ScaleMin;
        float[,] noiseMap = perlinNoiseGenerator.GenerateNoiseMap(_r.Next(), scale, octaves, persistance, lacunarity);
        return NoiseMapToImage(noiseMap, useColor ? _regions : new List<TerrainType>());
    }

    public Bitmap GenerateImagesGaussian(int width, int height, bool useColor)
    {
        GaussianNoiseGenerator gaussianNoiseGenerator = new GaussianNoiseGenerator(height, width);

        float radius = _r.NextSingle() * (RadiusMax - RadiusMin) + RadiusMin;
        float scale = _r.NextSingle() * (ScaleMax - ScaleMin) + ScaleMin;
        float[,] noiseMap = gaussianNoiseGenerator.GenerateNoiseMap(_r.Next(10, 24));
        return NoiseMapToImage(noiseMap, useColor ? _regions : new List<TerrainType>());
    }

    public List<Tuple<PointF, PointF>> DoMarchingSquares(float[,] noiseMap, int width, int height)
    {
        List<Tuple<PointF, PointF>> contours = new List<Tuple<PointF, PointF>>();
        int isoLevelsCount = 5;
        for (int j = 0; j < isoLevelsCount; j++)
        {
            List<Tuple<PointF, PointF>> contour1 =
                Algorithms.MarchingSquares.GetContour(noiseMap, 1 / (float)isoLevelsCount * j + ScaleMin, 10);
            contours.AddRange(contour1);
        }

        return contours;
    }

    public float[,] ImageToNoiseMap(Bitmap bitmap)
    {
        float[,] result = new float[bitmap.Width, bitmap.Height];

        for (int y = 0; y < bitmap.Height; y++)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                Color color = bitmap.GetPixel(x, y);
                float grayValue = color.R / 255f;
                result[x, y] = grayValue;
            }
        }

        return result;
    }

    public Bitmap NoiseMapToColorImage(float[,] noiseMap)
    {
        return NoiseMapToImage(noiseMap, _regions);
    }
    
    public Bitmap NoiseMapToImage(float[,] noiseMap, List<TerrainType> regions)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        float minValue = float.MaxValue;
        float maxValue = float.MinValue;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float value = noiseMap[x, y];
                minValue = Math.Min(minValue, value);
                maxValue = Math.Max(maxValue, value);
            }
        }

        Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float value = noiseMap[x, y];
                //int grayValue = (int)(255 * (value - minValue) / (maxValue - minValue));
                //Color pixelColor = Color.FromArgb(grayValue, grayValue, grayValue);
                Color pixelColor = GetAssignedColor(value, regions, minValue, maxValue);
                bitmap.SetPixel(x, y, pixelColor);
            }
        }

        return bitmap;
    }

    private Color GetAssignedColor(float height, List<TerrainType> regions, float minValue, float maxValue)
    {
        foreach (TerrainType region in regions)
        {
            if (height < region.Height)
            {
                return region.Color;
            }
        }

        int grayValue = (int)(255 * (height - minValue) / (maxValue - minValue));
        return Color.FromArgb(grayValue, grayValue, grayValue);
    }
}