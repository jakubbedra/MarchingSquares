using DotnetNoise;

namespace MarchingSquares.Algorithms.MapGeneration;

public class PerlinNoiseGenerator
{
    private readonly int _mapHeight;
    private readonly int _mapWidth;

    public PerlinNoiseGenerator(int mapHeight, int mapWidth)
    {
        _mapHeight = mapHeight;
        _mapWidth = mapWidth;
    }

    public float[,] GenerateNoiseMap(int seed, float scale, int octaves, float persistance, float lacunarity)
    {
        scale = (float)(scale > 0.0 ? scale : 0.0001);
        float[,] noiseMap = new float[_mapWidth, _mapHeight];
        FastNoise noise = new FastNoise(seed);

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        for (int y = 0; y < _mapHeight; y++)
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = x / scale * frequency;
                    float sampleY = y / scale * frequency;
                    float perlinValue = noise.GetPerlin(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < _mapHeight; y++)
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                noiseMap[x, y] = (noiseMap[x, y] - minNoiseHeight) / (maxNoiseHeight - minNoiseHeight);
            }
        }

        return noiseMap;
    }
}