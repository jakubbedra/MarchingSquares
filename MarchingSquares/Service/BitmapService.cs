using System;
using System.Collections.Generic;
using System.Drawing;

namespace MarchingSquares.Service;

public class BitmapService
{
    private const float ScaleMin = 0.1f;
    
    public BitmapService()
    {
        
    }

    public List<Tuple<PointF, PointF>> DoMarchingSquares(float[,] noiseMap, int width, int height)
    {
        List<Tuple<PointF, PointF>> contours = new List<Tuple<PointF, PointF>>();
        int isoLevelsCount = 5;
        for (int j = 0; j < isoLevelsCount; j++)
        {
            List<Tuple<PointF, PointF>> contour1 = Algorithms.MarchingSquares.GetContour(noiseMap, 1/ (float)isoLevelsCount * j + ScaleMin, 10);
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
    /*
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
    
    private float GetHeight(Color color, float minValue, float maxValue)
    {
        float grayValue = (color.R + color.G + color.B) / 3f; // calculate grayscale value
        return minValue + (maxValue - minValue) * grayValue / 255f; // calculate height
    }
*/
}