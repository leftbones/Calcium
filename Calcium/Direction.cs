namespace Calcium;

/// <summary>
/// Utility class that holds various Vector2i directions, various sets of directions, and some helper classes
/// </summary>

public static class Direction {
    public static readonly Vector2i None = new Vector2i(0, 0);

    // Direction
    public static readonly Vector2i Up = new Vector2i(0, -1);
    public static readonly Vector2i Down = new Vector2i(0, 1);
    public static readonly Vector2i Left = new Vector2i(-1, 0);
    public static readonly Vector2i Right = new Vector2i(1, 0);
    public static readonly Vector2i UpLeft = new Vector2i(-1, -1);
    public static readonly Vector2i UpRight = new Vector2i(1, -1);
    public static readonly Vector2i DownLeft = new Vector2i(-1, 1);
    public static readonly Vector2i DownRight = new Vector2i(1, 1);

    // Direction Sets
    public static readonly List<Vector2i> Vertical = new List<Vector2i>() { Direction.Up, Direction.Down };
    public static readonly List<Vector2i> Horizontal = new List<Vector2i>() { Direction.Left, Direction.Right };

    public static readonly List<Vector2i> Upward = new List<Vector2i>() { Direction.Up, Direction.UpLeft, Direction.UpRight };
    public static readonly List<Vector2i> Downward = new List<Vector2i>() { Direction.Down, Direction.DownLeft, Direction.DownRight };

    public static readonly List<Vector2i> DiagonalUp = new List<Vector2i>() { Direction.UpLeft, Direction.UpRight };
    public static readonly List<Vector2i> DiagonalDown = new List<Vector2i>() { Direction.DownLeft, Direction.DownRight };

    public static readonly List<Vector2i> UpperHalf = new List<Vector2i>() { Direction.Left, Direction.UpLeft, Direction.Up, Direction.UpRight, Direction.Right };
    public static readonly List<Vector2i> LowerHalf = new List<Vector2i>() { Direction.Left, Direction.DownLeft, Direction.Down, Direction.DownRight, Direction.Right };

    public static readonly List<Vector2i> CardinalUp = new List<Vector2i>() { Direction.Left, Direction.Up, Direction.Right };
    public static readonly List<Vector2i> CardinalDown = new List<Vector2i>() { Direction.Left, Direction.Down, Direction.Right };

    public static readonly List<Vector2i> Cardinal = new List<Vector2i>() { Direction.Left, Direction.Right, Direction.Up, Direction.Down };
    public static readonly List<Vector2i> Diagonal = new List<Vector2i>() { Direction.UpLeft, Direction.UpRight, Direction.DownLeft, Direction.DownRight };

    public static readonly List<Vector2i> Full = new List<Vector2i>() { Direction.Up, Direction.Down, Direction.Left, Direction.Right, Direction.UpLeft, Direction.UpRight, Direction.DownLeft, Direction.DownRight };

    /// <summary>
    /// Shuffle a set of directions
    /// </summary>
    /// <param name="rng">RNG instance</param>
    /// <param name="directions">Direction set to shuffle</param>
    /// <returns><paramref name="directions"/> shuffled in random order</returns>
    public static List<Vector2i> Shuffled(RNG rng, List<Vector2i> directions) {
        return directions.OrderBy(a => rng.Random.Next()).ToList();
    }

    /// <summary>
    /// Choose a random value from a direction set
    /// </summary>
    /// <param name="rng">RNG instance</param>
    /// <param name="directions">Direction set to choose from</param>
    /// <returns>Random value from <paramref name="directions"/></returns>
    public static Vector2i Random(RNG rng, List<Vector2i> directions) {
        return directions[rng.Range(0, directions.Count - 1)];
    }

    /// <summary>
    /// Flip a direction vertically
    /// </summary>
    /// <param name="direction">Direction to flip</param>
    /// <returns><paramref name="direction"/> flipped vertically</returns>
    public static Vector2i FlipVertical(Vector2i direction) {
        if (direction == Up) return Down;
        if (direction == Down) return Up;
        if (direction == UpLeft) return DownLeft;
        if (direction == UpRight) return DownRight;
        if (direction == DownLeft) return UpLeft;
        if (direction == DownRight) return UpRight;
        return direction;
    }

    /// <summary>
    /// Flip a direction vertically
    /// Shorthand for `FlipVertical`
    /// </summary>
    /// <param name="direction">Direction to flip</param>
    /// <returns><paramref name="direction"/> flipped vertically</returns>
    public static Vector2i FlipV(Vector2i direction) {
        return FlipVertical(direction);
    }

    /// <summary>
    /// Flip a direction horizontally
    /// </summary>
    /// <param name="direction">Direction to flip</param>
    /// <returns><paramref name="direction"/> flipped horizontally</returns>
    public static Vector2i FlipHorizontal(Vector2i direction) {
        if (direction == Left) return Right;
        if (direction == Right) return Left;
        if (direction == UpLeft) return UpRight;
        if (direction == UpRight) return UpLeft;
        if (direction == DownLeft) return DownRight;
        if (direction == DownRight) return DownLeft;
        return direction;
    }

    /// <summary>
    /// Flip a direction horizontally
    /// Shorthand for `FlipHorizontal`
    /// </summary>
    /// <param name="direction">Direction to flip</param>
    /// <returns><paramref name="direction"/> flipped horizontally</returns>
    public static Vector2i FlipH(Vector2i direction) {
        return FlipHorizontal(direction);
    }

    /// <summary>
    /// Reverse a direction (UpLeft becomes DownRight, etc)
    /// </summary>
    /// <param name="direction">Direction to reverse</param>
    /// <returns><paramref name="direction"/> reversed</returns>
    public static Vector2i Reverse(Vector2i direction) {
        if (direction == Up) return Down;
        if (direction == Down) return Up;
        if (direction == Left) return Right;
        if (direction == Right) return Left;
        if (direction == UpLeft) return DownRight;
        if (direction == UpRight) return DownLeft;
        if (direction == DownLeft) return UpRight;
        if (direction == DownRight) return UpLeft;
        return None;
    }

    /// <summary>
    /// Find the direciton of movement between two Vector2i points
    /// </summary>
    /// <param name="a">First Vector2i</param>
    /// <param name="b">Second Vector2i</param>
    /// <returns>The direction of movement from <paramref name="a"/> to <paramref name="b"/></returns>
    public static Vector2i GetMovementDirection(Vector2i a, Vector2i b) {
        return new Vector2i(Math.Sign(b.X - a.X), Math.Sign(b.Y - a.Y));
    }
}