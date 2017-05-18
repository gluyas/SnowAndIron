using System;
using System.Net;

namespace Model
{
	public class World
	{
		public readonly int W, E;
		private Hex[,] _terrain;

		public World(int w, int e)
		{
			// TODO: implement properly
			W = w;
			E = e;
			_terrain = new Hex[w, e];
			for (var iw = 0; iw < w; iw++)
			{
				for (var ie = 0; ie < e; ie++)
				{
					_terrain[iw, ie] = new Hex((HexType) ((iw+ie)%3));
				}
			}
		}

		public Hex this[TileVector pos]
		{
			get { return this[pos.W, pos.E]; }
		}

		public Hex this[int w, int e]
		{
			get
			{
				if (IsInRange(w, e)) return _terrain[w, e];
				else return null;
			}
		}

		private bool IsInRange(int w, int e)
		{
			return w >= 0 && w < W &&
			       e >= 0 && e < E;
		}
	}

	public class Hex
	{
		/// <summary>
		/// The Unit that is currently in this Hex, null if there is none present.
		/// </summary>
		public Unit Occupant { get; set; }	// TODO: revise this design decision

		/// <summary>
		/// The type of terrain this represents.
		/// </summary>
		public HexType Type { get; private set; } 	// replace with a delegate to implement more complex hex behavior

		// public int Height { get; private set; }; // for if we want to implement height later

		public Hex(HexType type)
		{
			Type = type;
			Occupant = null;
		}
	}

	public enum HexType
	{
		Dirt,
		Stone,
		Snow
	}
}