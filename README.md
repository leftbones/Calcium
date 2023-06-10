# Calcium

This is a (growing) collection of utility classes I use in my .NET projects, not intended for use by anyone but me, but anyone is welcome to use and contribute to it.

You can get the package on NuGet here: https://www.nuget.org/packages/CalciumLib/

## Vector2i
An implementation of `System.Numerics.Vector2` that uses `int` instead of `float`, because when I'm working on something that *always* uses `int`, I don't want to have to cast to `float` all the time. It includes all of the basic methods you'd expect, and you can instance it with `Vector2i(int x, int y)`, `Vector2i(float x, float y)`, or `Vector2i(Vector2 vec)` for convenience.

## RNG
Various extension methods using `System.Random`. It's a static class that contains an instance of `System.Random` that's seeded with `Guid.NewGuid().GetHashCode()` which I find to give really good pseudo-random results.

Here's a quick rundown of the methods available:
* `bool Roll(float n)` - returns `Random.NextDouble() <= n`, can be called with `float`, `double`, or `int` (which works differently), also has overloads in the form of `Rollf`, `Rolld`, and `Rolli` for verbosity.
* `bool Roll(int n)` - Slightly different than the `float`/`double` methods, essentially returns the result of an n% chance.
* `bool Odds(int n)` - Returns the result of 1:n odds
* `bool CoinFlip()` - Returns the result of a coin flip (shorthand for `Roll(0.5f)` or `Roll(50)`)
* `int Range(int min, int max)` - Returns a random `int` from an *inclusive* range (from min to max + 1)
* `IEnumerable<object> Shuffle(IEnumerable<object> e)` - Returns the given `IEnumerable`, but shuffled in a random order
