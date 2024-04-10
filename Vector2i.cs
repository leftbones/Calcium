using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Numerics;

namespace Calcium;

/// <summary>
///	Implementation of System.Numerics.Vector2 that uses int instead of float, useful for when you are working in a coordinates system that always uses integers to avoid needing to cast constantly.
///	Can be initialized with a single value (assigned to both), two int values, two float values, or a Vector2 (values are casted to int)
/// </summary>

// Todo
// - (maybe) change the math functions to be MyVec2i.Add(n) instead of Vector2.Add(MyVec2i, n)

[System.Serializable]
public struct Vector2i : IEquatable<Vector2i> {
	public int X;
	public int Y;

	//
	// Constructors

	/// <summary>
	/// Creates a new Vector2i object whose two elements have the same value
	/// </summary>
	/// <param name="value">The value to assign to both elements</param>
	public Vector2i(int value) {
		X = value;
		Y = value;
	}

	/// <summary>
	/// Creates a new Vector2i object with the specified values assigned to its elements
	/// </summary>
	/// <param name="x">The value to assign to x</param>
	/// <param name="y">The value to assign to y</param>
	public Vector2i(int x, int y) {
		X = x;
		Y = y;
	}

	/// <summary>
	/// Creates a new Vector2i object with the specified values casted to `int` and assigned to its elements
	/// </summary>
	/// <param name="x">The value to assign to x</param>
	/// <param name="y">The value to assign to y</param>
	public Vector2i(float x, float y) {
		X = (int)x;
		Y = (int)y;
	}

	/// <summary>
	/// Creates a new Vector2i object with the specified values casted to `int` and assigned to its elements
	/// </summary>
	/// <param name="x">The value to assign to x</param>
	/// <param name="y">The value to assign to y</param>
	public Vector2i(double x, double y) {
		X = (int)x;
		Y = (int)y;
	}

	/// <summary>
	/// Creates a new Vector2i object whose elements have the value of the given Vector2, casted to `int`
	/// </summary>
	/// <param name="vec2">The Vector2 which the values are taken from</param>
	public Vector2i(Vector2 vec2) {
		X = (int)vec2.X;
		Y = (int)vec2.Y;
	}


	//
	// Access

	/// <summary>
	/// Get or Set values in the Vector2i as if it were an array
	/// </summary>
	/// <param name="index"></param>
	/// <returns>The value at index <paramref name="index"/></returns>
	/// <exception cref="IndexOutOfRangeException"><paramref name="index"/> must be 0-1</exception>
	public int this[int index] {
        readonly get {
			if (index == 0) return X;
			if (index == 1) return Y;

			throw new IndexOutOfRangeException("Tried to access Vector2i at index: " + index);
		}

		set {
			if (index == 0) {
				X = value;
			} else if (index == 1) {
				Y = value;
			} else {
				throw new IndexOutOfRangeException("Tried to set Vector2i at index: " + index);
			}
		}
	}


	//
	// Shorthand

	/// <summary>
	/// Creates a new Vector2i whose values are both zero
	/// </summary>
	public static readonly Vector2i Zero = new Vector2i(0, 0);

	/// <summary>
	/// Creates a new Vector2i whose values are both one
	/// </summary>
	public static readonly Vector2i One = new Vector2i(1, 1);


	//
	// Distance

	/// <summary>
	/// Calculate the manhattan distance between two Vector2i points
	/// </summary>
	public readonly int ManhattanDistance(Vector2i other) {
		return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
	}

	/// <summary>
	/// Calculate the euclidean distance between two Vector2i points
	/// </summary>
	public readonly int EuclideanDistance(Vector2i other) {
		return (int)Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
	}

	/// <summary>
	/// Shorthand for `EuclideanDistance(Vector2i.Zero)`, which is more commonly used over `ManhattanDistance`
	/// </summary>
	/// <param name="other"></param>
	/// <returns></returns>
	public readonly int Distance(Vector2i other){
		return EuclideanDistance(other);
	}


	//
	// Lerp

	/// <summary>
	/// Perform linear interpolation between two Vector2i objects
	/// </summary>
	/// <param name="a">The Vector2i to lerp from</param>
	/// <param name="b">The Vector2i to lerp towards</param>
	/// <param name="t">The amount to interpolate by</param>
	/// <returns><paramref name="a"/> interpolated towards <paramref name="b"/> at a step of <paramref name="t"/></returns>
	public static Vector2i Lerp(Vector2i a, Vector2i b, double t) {
		return new Vector2i(
			a.X + (b.X - a.X) * t,
			a.Y + (b.Y - a.Y) * t
		);
	}


	//
	// Rotate

	/// <summary>
	/// Rotate a Vector2i around an origin point by a given amount
	/// </summary>
	/// <param name="v">Vector2i to rotate</param>
	/// <param name="o">Origin point to rotate around</param>
	/// <param name="a">Amount to rotate</param>
	/// <returns><paramref name="v"/> rotated around <paramref name="o"/> by <paramref name="a"/></returns>
	public static Vector2i Rotate(Vector2i v, Vector2i o, float a) {
		var S = (float)Math.Sin(a);
		var C = (float)Math.Cos(a);

		var TX = v.X - o.X;
		var TY = v.Y - o.Y;

		var RX = TX * C - TY * S;
		var RY = TX * S + TY * C;

		return new Vector2i(Math.Round(RX + o.X), Math.Round(RY + o.Y));
	}


	//
	// Basic Math

	/// <summary>
	/// Add the values of Vector2i <paramref name="b"/> to <paramref name="a"/>
	/// </summary>
	/// <param name="a">First Vector2i</param>
	/// <param name="b">Second Vector2i</param>
	/// <returns>Vector2i with added values of <paramref name="a"/> and <paramref name="b"/></returns>
	public static Vector2i Add(Vector2i a, Vector2i b) {
		a.X += b.X;
		a.Y += b.Y;
		return a;
	}

	/// <summary>
	/// Add the value <paramref name="n"/> to both values of <paramref name="vec"/>
	/// </summary>
	/// <param name="vec">Vector2i to add to</param>
	/// <param name="n">Value to add</param>
	/// <returns><paramref name="vec"/> with <paramref name="n"/> added to both values</returns>
	public static Vector2i Add(Vector2i vec, int n) {
		vec.X += n;
		vec.Y += n;
		return vec;
	}

	/// <summary>
	/// Subtract the values of Vector2i <paramref name="b"/> from <paramref name="a"/>
	/// </summary>
	/// <param name="a">First Vector2i</param>
	/// <param name="b">Second Vector2i</param>
	/// <returns>Vector2i with values of <paramref name="b"/> subtracted from <paramref name="a"/></returns>
	public static Vector2i Subtract(Vector2i a, Vector2i b) {
		a.X -= b.X;
		a.Y -= b.Y;
		return a;
	}

	/// <summary>
	/// Subtract the value <paramref name="n"/> from both values of <paramref name="vec"/>
	/// </summary>
	/// <param name="vec">Vector2i to subtract from</param>
	/// <param name="n">Value to subtract</param>
	/// <returns><paramref name="vec"/> with <paramref name="n"/> subtracted from both values</returns>
	public static Vector2i Subtract(Vector2i vec, int n) {
		vec.X -= n;
		vec.Y -= n;
		return vec;
	}

	/// <summary>
	/// Multiply the values of two Vector2i objects
	/// </summary>
	/// <param name="a">First Vector2i</param>
	/// <param name="b">Second Vector2i</param>
	/// <returns><paramref name="a"/> with its values multiplied by the values of <paramref name="b"/></returns>
	public static Vector2i Multiply(Vector2i a, Vector2i b) {
		a.X *= b.X;
		a.Y *= b.Y;
		return a;
	}

	/// <summary>
	/// Multiply the values of a Vector2i by a value
	/// </summary>
	/// <param name="vec">Vector2i to multiply</param>
	/// <param name="n">Value to multiply by</param>
	/// <returns><paramref name="vec"/> with its values multiplied by <paramref name="n"/></returns>
	public static Vector2i Multiply(Vector2i vec, int n) {
		vec.X *= n;
		vec.Y *= n;
		return vec;
	}

	/// <summary>
	/// Divide the values of a Vector2i by the values of another Vector2i
	/// </summary>
	/// <param name="a">First Vector2i</param>
	/// <param name="b">Second Vector2i</param>
	/// <returns><paramref name="a"/> with its values divided by the values of <paramref name="b"/></returns>
	public static Vector2i Divide(Vector2i a, Vector2i b) {
		a.X /= b.X;
		a.Y /= b.Y;
		return a;
	}

	/// <summary>
	/// Divide the values of a Vector2i by a value
	/// </summary>
	/// <param name="vec">Vector2i to divide</param>
	/// <param name="n">Value to divide by</param>
	/// <returns><paramref name="vec"/> with its values divided by <paramref name="n"/></returns>
	public static Vector2i Divide(Vector2i vec, int n) {
		vec.X /= n;
		vec.Y /= n;
		return vec;
	}

	/// <summary>
	/// Clamp the values of a Vector2i between two values
	/// </summary>
	/// <param name="vec">Vector2i to clamp</param>
	/// <param name="min">Minimum value</param>
	/// <param name="max">Maximum value</param>
	/// <returns><paramref name="vec"/> with its values clamped between <paramref name="min"/> and <paramref name="max"/></returns>
	public static Vector2i Clamp(Vector2i vec, int min, int max) {
		vec.X = Math.Clamp(vec.X, min, max);
		vec.Y = Math.Clamp(vec.Y, min, max);
		return vec;
	}

	/// <summary>
	/// Clamp the X value of a Vector2i between two values
	/// </summary>
	/// <param name="vec">Vector2i to clamp the X value of</param>
	/// <param name="min">Minimum value</param>
	/// <param name="max">Maximum value</param>
	/// <returns><paramref name="vec"/> with its X value clamped between <paramref name="min"/> and <paramref name="max"/></returns>
	public static Vector2i ClampX(Vector2i vec, int min, int max) {
		vec.X = Math.Clamp(vec.X, min, max);
		return vec;
	}

	/// <summary>
	/// Clamp the Y value of a Vector2i between two values
	/// </summary>
	/// <param name="vec">Vector2i to clamp the Y value of</param>
	/// <param name="min">Minimum value</param>
	/// <param name="max">Maximum value</param>
	/// <returns><paramref name="vec"/> with its Y value clamped between <paramref name="min"/> and <paramref name="max"/></returns>
	public static Vector2i ClampY(Vector2i vec, int min, int max) {
		vec.Y = Math.Clamp(vec.Y, min, max);
		return vec;
	}


	//
	// Operator Math

	[Pure]
	public static Vector2i operator +(Vector2i left, Vector2i right) {
		left.X += right.X;
		left.Y += right.Y;
		return left;
	}

	[Pure]
	public static Vector2i operator -(Vector2i left, Vector2i right) {
		left.X -= right.X;
		left.Y -= right.Y;
		return left;
	}

	[Pure]
	public static Vector2i operator *(Vector2i left, Vector2i right) {
		left.X *= right.X;
		left.Y *= right.Y;
		return left;
	}

	[Pure]
	public static Vector2i operator *(Vector2i vec, int scale) {
		vec.X *= scale;
		vec.Y *= scale;
		return vec;
	}

	[Pure]
	public static Vector2i operator /(Vector2i left, Vector2i right) {
		left.X /= right.X;
		left.Y /= right.Y;
		return left;
	}

	[Pure]
	public static Vector2i operator /(Vector2i vec, int scale) {
		vec.X /= scale;
		vec.Y /= scale;
		return vec;
	}

	[Pure]
	public static Vector2i operator -(Vector2i vec) {
		vec.X = -vec.X;
		vec.Y = -vec.Y;
		return vec;
	}

	public static bool operator ==(Vector2i left, Vector2i right) {
		return left.Equals(right);
	}

	public static bool operator !=(Vector2i left, Vector2i right) {
		return !(left == right);
	}


	//
	// Other

	/// <summary>
	/// Creates a new System.Numerics.Vector2 object whos values are the same as this Vector2i
	/// </summary>
	/// <returns>A new Vector2 matching the values of this Vector2i</returns>
	public readonly Vector2 ToVector2() {
		return new Vector2(X, Y);
	}

	/// <summary>
	/// Shorthand for `ToVector2()`
	/// </summary>
	/// <returns>A new Vector2 matching the values of this Vector2i</returns>
	public readonly Vector2 ToVec2() {
		return ToVector2();
	}

	public override readonly string ToString() {
		return string.Format("({0}, {1})", X, Y);
	}

	public override readonly bool Equals([NotNullWhen(true)] object? obj) {
		return obj is Vector2i && base.Equals(obj);
	}

	public readonly bool Equals(Vector2i other) {
		return X == other.X && Y == other.Y;
	}

	public override readonly int GetHashCode() {
		return HashCode.Combine(X, Y);
	}
}