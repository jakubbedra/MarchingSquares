using System;
using System.Collections.Generic;
using System.Drawing;

namespace MarchingSquares.Model;

public class MarchingSquaresLayer
{
    public List<Tuple<PointF, PointF>> Layer { get; set; }
}