namespace Calcium;

/// <summary>
/// Collection of various random number generation and random chance methods. Previously, this was a static class, but it has now been modified to be instanced with an optional seed.
/// The various `Roll` methods can be invoked with `Roll(n)` using either float, double, or int values, or specified (for readability) with `Rollf(n)`, `Rolld(n)`, and `Rolli(n)`
/// </summary>

public class RNG {
    public Random Random { get; private set; }

    public RNG(int? seed=null) {
        if (seed is null) {
            Random = new Random();
        } else {
            Random = new Random((int)seed);
        }
    }

    /// <summary>
    /// Return true if <paramref name="n"/> >= a random double from 0.0 to 1.0
    /// </summary>
    /// <param name="n"></param>
    /// <returns>Result of the roll</returns>
    public bool Roll(float n) {
        return Random.NextDouble() <= n;
    }

    /// <summary>
    /// Return true if <paramref name="n"/> >= a random double from 0.0 to 1.0
    /// </summary>
    /// <param name="n"></param>
    /// <returns>Result of the roll</returns>
	public bool Rollf(float n) { return Roll(n); }

    /// <summary>
    /// Return true if <paramref name="n"/> >= a random double from 0.0 to 1.0
    /// </summary>
    /// <param name="n"></param>
    /// <returns>Result of the roll</returns>
    public bool Roll(double n) {
        return Random.NextDouble() <= n;
    }

    /// <summary>
    /// Return true if <paramref name="n"/> >= a random double from 0.0 to 1.0
    /// </summary>
    /// <param name="n"></param>
    /// <returns>Result of the roll</returns>
	public bool Rolld(double n) { return Roll(n); }

    /// <summary>
    /// Return true if <paramref name="n"/> >= a random integer between 1 and 100
    /// </summary>
    /// <param name="n"></param>
    /// <returns>Result of the roll</returns>
    public bool Roll(int n) {
        return Random.Next(1, 100) <= n;
    }

    /// <summary>
    /// Return true if <paramref name="n"/> >= a random integer between 1 and 100
    /// </summary>
    /// <param name="n"></param>
    /// <returns>Result of the roll</returns>
	public bool Rolli(int n) { return Roll(n); }

    // Return a random int from an inclusive range
    /// <summary>
    /// Returns a random integer from an inclusive range
    /// </summary>
    /// <param name="min">Lowest possible value</param>
    /// <param name="max">Highest possible value</param>
    /// <returns>Random int from <paramref name="min"/> to <paramref name="max"/> (inclusive)</returns>
    public int Range(int min, int max) {
        return Random.Next(min, max+1);
    }

    // Return true or false based on 1:n odds
    /// <summary>
    /// Return true if the result of a <paramref name="n"/>-sided die equals 1
    /// Shorthand for `Range(1, n) == 1`
    /// </summary>
    /// <param name="n">Number of "sides"</param>
    /// <returns>Result of the roll</returns>
    public bool Odds(int n) {
        return Range(1, n) == 1;
    }

    /// <summary>
    /// Returns the result of a coin flip
    /// Shorthand for `Roll(50)`
    /// </summary>
    /// <returns></returns>
    public bool CoinFlip() {
        return Roll(50);
    }

    /// <summary>
    /// Returns <paramref name="e"/> with it's contents in random order
    /// </summary>
    /// <param name="e">IEnumerable<> to shuffle</param>
    /// <returns><paramref name="e"/> shuffled</returns>
    public IEnumerable<object> Shuffle(IEnumerable<object> e) {
        return e.OrderBy(a => Random.Next()).ToList();
    }
}