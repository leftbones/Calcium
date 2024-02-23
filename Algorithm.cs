namespace Calcium;

/// <summary>
/// Collection of some algorithm implementations I've made which should be useful in a variety of situations, mostly game development.
/// Currently only one algorithm, but I may add some more in the future, as I find that I need them.
/// </summary>

static class Algorithm {

    /// <summary>
    /// Generate a list of points between two points on a grid, with an optional size parameter to generate a line of a specific width.
    /// This is a modified version of the Bresenham's line algorithm, which is a line drawing algorithm that determines the points of an n-dimensional raster that should be selected in order to form a close approximation to a straight line between two points.
    /// </summary>
    /// <param name="from">Start point of the line</param>
    /// <param name="to">End point of the line</param>
    /// <param name="size">Size (or thickness) of the line</param>
    /// <returns>A List<Vector2i> of points between the <paramref name="from"/> and <paramref name="to"/> points.</returns>
    public static List<Vector2i> GenerateLine(Vector2i from, Vector2i to, int size=1) {
        var Points = new List<Vector2i>();

        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                Vector2i a = new Vector2i(from.X - size / 2 + x, from.Y - size / 2 + y);
                Vector2i b = new Vector2i(to.X - size / 2 + x, to.Y - size / 2 + y);

                int w = b.X - a.X;
                int h = b.Y - a.Y;
                Vector2i d1 = Vector2i.Zero;
                Vector2i d2 = Vector2i.Zero;

                if (w < 0) d1.X = -1; else if (w > 0) d1.X = 1;
                if (h < 0) d1.Y = -1; else if (h > 0) d1.Y = 1;
                if (w < 0) d2.X = -1; else if (w > 0) d2.X = 1;

                int longest  = Math.Abs(w);
                int shortest = Math.Abs(h);
                if (!(longest > shortest)) {
                    longest = Math.Abs(h);
                    shortest = Math.Abs(w);
                    if (h < 0) d2.Y = -1; else if (h > 0) d2.Y = 1;
                    d2.X = 0;
                }

                int numerator = longest >> 1;
                for (int i = 0; i <= longest; i++) {
                    Points.Add(new Vector2i(a.X, a.Y));
                    numerator += shortest;
                    if (!(numerator < longest)) {
                        numerator -= longest;
                        a.X += d1.X;
                        a.Y += d1.Y;
                    } else {
                        a.X += d2.X;
                        a.Y += d2.Y;
                    }
                }
            }
        }

        return Points.Distinct().ToList();
    }
}