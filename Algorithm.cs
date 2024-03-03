namespace Calcium;

/// <summary>
/// Collection of some algorithm implementations I've made which should be useful in a variety of situations, mostly game development.
/// Currently only one algorithm, but I may add some more in the future, as I find that I need them.
/// </summary>

public static class Algorithm {

    /// <summary>
    /// Generate a list of points between two points on a grid, with an optional size parameter to generate a line of a specific width.
    /// This is a modified version of the Bresenham's line algorithm, which is a line drawing algorithm that determines the points of an n-dimensional raster that should be selected in order to form a close approximation to a straight line between two points.
    /// </summary>
    /// <param name="from">Start point of the line</param>
    /// <param name="to">End point of the line</param>
    /// <param name="size">Size (or thickness) of the line</param>
    /// <returns>A List<Vector2i> of points between the <paramref name="from"/> and <paramref name="to"/> points.</returns>
    public static List<Vector2i> GetLinePoints(Vector2i from, Vector2i to, int size=1) {
        var Points = new List<Vector2i>();

        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                Vector2i A = new Vector2i(from.X - size / 2 + x, from.Y - size / 2 + y);
                Vector2i B = new Vector2i(to.X - size / 2 + x, to.Y - size / 2 + y);

                int W = B.X - A.X;
                int H = B.Y - A.Y;
                Vector2i D1 = Vector2i.Zero;
                Vector2i D2 = Vector2i.Zero;

                if (W < 0) D1.X = -1; else if (W > 0) D1.X = 1;
                if (H < 0) D1.Y = -1; else if (H > 0) D1.Y = 1;
                if (W < 0) D2.X = -1; else if (W > 0) D2.X = 1;

                int longest  = Math.Abs(W);
                int shortest = Math.Abs(H);
                if (!(longest > shortest)) {
                    longest = Math.Abs(H);
                    shortest = Math.Abs(W);
                    if (H < 0) D2.Y = -1; else if (H > 0) D2.Y = 1;
                    D2.X = 0;
                }

                int numerator = longest >> 1;
                for (int i = 0; i <= longest; i++) {
                    Points.Add(new Vector2i(A.X, A.Y));
                    numerator += shortest;
                    if (!(numerator < longest)) {
                        numerator -= longest;
                        A.X += D1.X;
                        A.Y += D1.Y;
                    } else {
                        A.X += D2.X;
                        A.Y += D2.Y;
                    }
                }
            }
        }

        return Points.Distinct().ToList();
    }


    /// <summary>
    /// Generate a list of points in a circle around a center point, with a specified radius.
    /// </summary>
    /// <param name="center">Center point of the circle</param>
    /// <param name="radius">Radius of the circle</param>
    /// <returns>A List<Vector2i> of points on the outline of the generated circle</returns>
    public static List<Vector2i> GetCirclePoints(Vector2i center, int radius) {
        var Points = new List<Vector2i>();

        int X = radius;
        int Y = 0;
        int ERR = 0;

        while (X >= Y) {
            Points.Add(new Vector2i(center.X + X, center.Y + Y));
            Points.Add(new Vector2i(center.X + Y, center.Y + X));
            Points.Add(new Vector2i(center.X - Y, center.Y + X));
            Points.Add(new Vector2i(center.X - X, center.Y + Y));
            Points.Add(new Vector2i(center.X - X, center.Y - Y));
            Points.Add(new Vector2i(center.X - Y, center.Y - X));
            Points.Add(new Vector2i(center.X + Y, center.Y - X));
            Points.Add(new Vector2i(center.X + X, center.Y - Y));

            if (ERR <= 0) {
                Y += 1;
                ERR += 2 * Y + 1;
            }

            if (ERR > 0) {
                X -= 1;
                ERR -= 2 * X + 1;
            }
        }

        return Points;
    }
}