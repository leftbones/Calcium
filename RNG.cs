namespace Calcium;

/// <summary>
/// Collection of various random number generation and random chance methods. Previously, this was a static class, but it has now been modified to be instanced with an optional seed.
/// The various `Roll` methods can be invoked with `Roll(n)` using either float, double, or int values, or specified (for readability) with `Rollf(n)`, `Rolld(n)`, and `Rolli(n)`
/// </summary>

public static class RNG {
    private static Random Random = new();

    public static void SetSeed(int seed) {
        Random = new Random(seed);
    }

    /// <summary>
    /// Alias for `Random.Next()` with an optional maximum value
    /// </summary>
    /// <param name="max">The maximum value of the integer returned</param>
    /// <returns>Random int between 0 and <paramref name="max"/></returns>
    public static int Next(int max=int.MaxValue) {
        return Random.Next(max);
    }

    /// <summary>
    /// Alias for `Random.NextDouble()`
    /// </summary>
    /// <returns>Random double</returns>
    public static double NextDouble() {
        return Random.NextDouble();
    }

    /// <summary>
    /// Return the result of an <paramref name="n"/>-sided dice roll (Shorthand for `Range(1, n)`)
    /// </summary>
    /// <param name="n">Highest number on the die to roll</param>
    /// <returns>Result of the roll</returns>
    public static int Roll(int n) {
        return Range(1, n);
    }

    /// <summary>
    /// Return a random double between 0 and n (inclusive)
    /// </summary>
    /// <param name="n">Maximum return value</param>
    /// <returns>Double between 0 and n</returns>
    public static double Roll(double n) {
        return Random.NextDouble() * n;
    }

    /// <summary>
    /// Return a random float between 0 and n (inclusive)
    /// </summary>
    /// <param name="n">Maximum return value</param>
    /// <returns>Float between 0 and n</returns>
    public static float Roll(float n) {
        return (float)Roll((double)n);
    }

    // Return a random int from an inclusive range
    /// <summary>
    /// Returns a random integer from an inclusive range
    /// </summary>
    /// <param name="min">Lowest possible value</param>
    /// <param name="max">Highest possible value</param>
    /// <returns>Random int from <paramref name="min"/> to <paramref name="max"/> (inclusive)</returns>
    public static int Range(int min, int max) {
        return Random.Next(min, max+1);
    }

    /// <summary>
    /// Return true if the result of a <paramref name="n"/>-sided die equals 1
    /// Shorthand for `Range(1, n) == 1` or `Roll(n) == 1`
    /// </summary>
    /// <param name="n">Number of "sides"</param>
    /// <returns>Result of the roll</returns>
    public static bool Odds(int n) {
        return Range(1, n) == 1;
    }

    /// <summary>
    /// Return true if the result of a 100-sided die is less than or equal equal to <paramref name="n"/>
    /// </summary>
    /// <param name="n">% chance of sucess</param>
    /// <returns>Boolean result of the % chance</returns>
    public static bool Chance(int n) {
        return Range(1, 100) <= n;
    }

    /// <summary>
    /// Returns the result of a coin flip
    /// Shorthand for `Roll(50)`
    /// </summary>
    /// <returns></returns>
    public static bool CoinFlip() {
        return Roll(2) == 1;
    }

    /// <summary>
    /// Returns <paramref name="e"/> with it's contents in random order
    /// </summary>
    /// <param name="e">IEnumerable<> to shuffle</param>
    /// <returns><paramref name="e"/> shuffled</returns>
    public static IEnumerable<object> Shuffle(IEnumerable<object> e) {
        return e.OrderBy(a => Random.Next()).ToList();
    }
}