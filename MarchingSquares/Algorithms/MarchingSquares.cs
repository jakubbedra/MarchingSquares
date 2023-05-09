using System;
using System.Collections.Generic;
using System.Drawing;

namespace MarchingSquares.Algorithms;

public static class MarchingSquares
{
    public static List<Tuple<PointF, PointF>> GetContour(float[,] data, float isovalue, int stepSize)
    {
        List<Tuple<PointF, PointF>> contours = new List<Tuple<PointF, PointF>>();

        int width = data.GetLength(0);
        int height = data.GetLength(1);

        for (int i = 0; i < width - stepSize; i += stepSize)
        {
            for (int j = 0; j < height - stepSize; j += stepSize)
            {
                float val1 = data[i, j];

                float val2 = data[i + stepSize, j];
                float val3 = data[i + stepSize, j + stepSize];
                float val4 = data[i, j + stepSize];
                float middleVal = (val1 + val2 + val3 + val4) / 4;

                int bin1 = val1 > isovalue ? 1 : 0;
                int bin2 = val2 > isovalue ? 2 : 0;
                int bin3 = val3 > isovalue ? 4 : 0;
                int bin4 = val4 > isovalue ? 8 : 0;
                int bin = bin1 + bin2 + bin3 + bin4;

                float x1 = i;
                float y1 = j;
                float x2 = i + stepSize;
                float y2 = j + stepSize;

                float factor = 0.5f;
                Tuple<PointF, PointF> line;
                PointF startPoint;
                PointF endPoint;
                switch (bin)
                {
                    case 1:
                        factor = (isovalue - val4) / (val1 - val4);
                        startPoint = new PointF(x1, y2 - factor * stepSize);
                        factor = (isovalue - val2) / (val1 - val2);
                        endPoint = new PointF(x2 - factor * stepSize, y1);
                        line = new Tuple<PointF, PointF>(startPoint, endPoint);
                        contours.Add(line);
                        break;
                    case 2:
                        factor = (isovalue - val1) / (val2 - val1);
                        startPoint = new PointF(x1 + factor * stepSize, y1);
                        factor = (isovalue - val3) / (val2 - val3);
                        endPoint = new PointF(x2, y2 - factor * stepSize);
                        line = new Tuple<PointF, PointF>(startPoint, endPoint);
                        contours.Add(line);
                        break;
                    case 3:
                        factor = (isovalue - val4) / (val1 - val4);
                        startPoint = new PointF(x1, y2 - factor * stepSize);

                        factor = (isovalue - val3) / (val2 - val3);
                        endPoint = new PointF(x2, y2 - factor * stepSize);
                        line = new Tuple<PointF, PointF>(startPoint, endPoint);
                        contours.Add(line);
                        break;
                    case 4:
                        factor = (isovalue - val2) / (val3 - val2);
                        startPoint = new PointF(x2, y1 + factor * stepSize);
                        factor = (isovalue - val4) / (val3 - val4);
                        endPoint = new PointF(x1 + factor * stepSize, y2);
                        line = new Tuple<PointF, PointF>(startPoint, endPoint);
                        contours.Add(line);
                        break;
                    case 5:
                        if (middleVal < isovalue)
                        {
                            factor = (isovalue - val4) / (val1 - val4);
                            startPoint = new PointF(x1, y2 - factor * stepSize);
                            factor = (isovalue - val2) / (val1 - val2);
                            endPoint = new PointF(x2 - factor * stepSize, y1);
                            line = new Tuple<PointF, PointF>(startPoint, endPoint);
                            contours.Add(line);

                            factor = (isovalue - val2) / (val3 - val2);
                            startPoint = new PointF(x2, y1 + factor * stepSize);
                            factor = (isovalue - val4) / (val3 - val4);
                            endPoint = new PointF(x1 + factor * stepSize, y2);
                            line = new Tuple<PointF, PointF>(startPoint, endPoint);
                            contours.Add(line);
                        }
                        else
                        {
                            factor = (isovalue - val4) / (val1 - val4);
                            startPoint = new PointF(x1, y2 - factor * stepSize);
                            factor = (isovalue - val4) / (val3 - val4);
                            endPoint = new PointF(x1 + factor * stepSize, y2);
                            line = new Tuple<PointF, PointF>(startPoint, endPoint);
                            contours.Add(line);

                            factor = (isovalue - val2) / (val3 - val2);
                            startPoint = new PointF(x2, y1 + factor * stepSize);
                            factor = (isovalue - val2) / (val1 - val2);
                            endPoint = new PointF(x2 - factor * stepSize, y1);
                            line = new Tuple<PointF, PointF>(startPoint, endPoint);
                            contours.Add(line);
                        }

                        break;
                    case 6:
                        factor = (isovalue - val1) / (val2 - val1);
                        startPoint = new PointF(x1 + factor * stepSize, y1);
                        factor = (isovalue - val4) / (val3 - val4);
                        endPoint = new PointF(x1 + factor * stepSize, y2);
                        line = new Tuple<PointF, PointF>(startPoint, endPoint);
                        contours.Add(line);
                        break;
                    case 7:
                        factor = (isovalue - val4) / (val1 - val4);
                        startPoint = new PointF(x1, y2 - factor * stepSize);
                        factor = (isovalue - val4) / (val3 - val4);
                        endPoint = new PointF(x1 + factor * stepSize, y2);
                        line = new Tuple<PointF, PointF>(startPoint, endPoint);
                        contours.Add(line);
                        break;
                    case 8:
                        factor = (isovalue - val3) / (val4 - val3);
                        startPoint = new PointF(x2 - factor * stepSize, y2);
                        factor = (isovalue - val1) / (val4 - val1);
                        endPoint = new PointF(x1, y1 + factor * stepSize);
                        line = new Tuple<PointF, PointF>(startPoint, endPoint);
                        contours.Add(line);
                        break;
                    case 9:
                        factor = (isovalue - val3) / (val4 - val3);
                        startPoint = new PointF(x2 - factor * stepSize, y2);
                        factor = (isovalue - val2) / (val1 - val2);
                        endPoint = new PointF(x2 - factor * stepSize, y1);
                        line = new Tuple<PointF, PointF>(startPoint, endPoint);
                        contours.Add(line);
                        break;
                    case 10:

                        if (middleVal > isovalue)
                        {
                            factor = (isovalue - val1) / (val2 - val1);
                            startPoint = new PointF(x1 + factor * stepSize, y1);
                            factor = (isovalue - val1) / (val4 - val1);
                            endPoint = new PointF(x1, y1 + factor * stepSize);
                            line = new Tuple<PointF, PointF>(startPoint, endPoint);
                            contours.Add(line);

                            factor = (isovalue - val3) / (val4 - val3);
                            startPoint = new PointF(x2 - factor * stepSize, y2);
                            factor = (isovalue - val3) / (val2 - val3);
                            endPoint = new PointF(x2, y2 - factor * stepSize);
                            line = new Tuple<PointF, PointF>(startPoint, endPoint);
                            contours.Add(line);
                        }
                        else
                        {
                            factor = (isovalue - val3) / (val4 - val3);
                            startPoint = new PointF(x2 - factor * stepSize, y2);
                            factor = (isovalue - val1) / (val4 - val1);
                            endPoint = new PointF(x1, y1 + factor * stepSize);
                            line = new Tuple<PointF, PointF>(startPoint, endPoint);
                            contours.Add(line);

                            factor = (isovalue - val1) / (val2 - val1);
                            startPoint = new PointF(x1 + factor * stepSize, y1);
                            factor = (isovalue - val3) / (val2 - val3);
                            endPoint = new PointF(x2, y2 - factor * stepSize);
                            line = new Tuple<PointF, PointF>(startPoint, endPoint);
                            contours.Add(line);
                        }

                        break;
                    case 11:
                        factor = (isovalue - val3) / (val4 - val3);
                        startPoint = new PointF(x2 - factor * stepSize, y2);
                        factor = (isovalue - val3) / (val2 - val3);
                        endPoint = new PointF(x2, y2 - factor * stepSize);
                        line = new Tuple<PointF, PointF>(startPoint, endPoint);
                        contours.Add(line);

                        break;
                    case 12:
                        factor = (isovalue - val2) / (val3 - val2);
                        startPoint = new PointF(x2, y1 + factor * stepSize);
                        factor = (isovalue - val1) / (val4 - val1);
                        endPoint = new PointF(x1, y1 + factor * stepSize);
                        line = new Tuple<PointF, PointF>(startPoint, endPoint);
                        contours.Add(line);
                        break;
                    case 13:
                        factor = (isovalue - val2) / (val3 - val2);
                        startPoint = new PointF(x2, y1 + factor * stepSize);
                        factor = (isovalue - val2) / (val1 - val2);
                        endPoint = new PointF(x2 - factor * stepSize, y1);
                        line = new Tuple<PointF, PointF>(startPoint, endPoint);
                        contours.Add(line);
                        break;
                    case 14:
                        factor = (isovalue - val1) / (val2 - val1);
                        startPoint = new PointF(x1 + factor * stepSize, y1);
                        factor = (isovalue - val1) / (val4 - val1);
                        endPoint = new PointF(x1, y1 + factor * stepSize);
                        line = new Tuple<PointF, PointF>(startPoint, endPoint);
                        contours.Add(line);
                        break;
                }
            }
        }

        return contours;
    }
}