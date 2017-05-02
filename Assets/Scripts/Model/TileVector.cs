using System;
using System.Collections.Generic;
using UnityEditorInternal;

namespace Model
{
	/// <summary>
	/// Representation of (discrete) hexagonal space
	/// </summary>
    public struct TileVector
    {
	    public readonly int W;
	    public readonly int E;

	    /// <summary>
	    /// Construct a new TileVector
	    /// </summary>
	    /// <param name="w">the W or SOUTHWEST component</param>
	    /// <param name="e">the E or SOUTHEAST component</param>
	    public TileVector(int w, int e)
	    {
		    W = w;
		    E = e;
	    }

	    /// <summary>
	    /// Get distance
	    /// </summary>
	    /// <param name="a"></param>
	    /// <param name="b"></param>
	    /// <returns></returns>
	    public static int Distance(TileVector a, TileVector b)
	    {
		    TileVector diff = b - a;
		    return Math.Max(diff.W, diff.E);
	    }

	    public static TileVector operator +(TileVector a, CardinalDirection d)
	    {
		    return a + d.GetTileVector();
	    }

	    public static TileVector operator +(TileVector a, TileVector b)
	    {
		    return new TileVector(a.W + b.W, a.E + b.E);
	    }

	    public static TileVector operator -(TileVector a, TileVector b)
	    {
		    return new TileVector(a.W - b.W, a.E - b.E);
	    }

	    public static TileVector operator -(TileVector a)
	    {
		    return new TileVector(-a.W, -a.W);
	    }

	    public static bool operator ==(TileVector a, TileVector b)
	    {
		    return a.Equals(b);
	    }

	    public static bool operator !=(TileVector a, TileVector b)
	    {
		    return !a.Equals(b);
	    }

	    public override string ToString()
	    {
		    return String.Format("({0}, {1})", W, E);
	    }

	    public override bool Equals(object obj)
	    {
		    // Check for null values and compare run-time types.
		    if (obj == null || GetType() != obj.GetType())
			    return false;

		    TileVector t = (TileVector) obj;
		    return W == t.W && E == t.E;
	    }

	    public override int GetHashCode()
	    {
		    return W ^ E;
	    }
    }
}