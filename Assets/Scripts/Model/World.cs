using System;
using System.Net;

namespace Model
{
	public class World
	{
		private Hex[,] _terrain;

		public World(int w, int e)
		{
			// TODO: implement properly
			_terrain = new Hex[w, e];
			for (var iw = 0; iw < w; iw++)
			{
				for (var ie = 0; ie < e; ie++)
				{
					_terrain[iw, ie] = new Hex(0);
				}
			}
		}

		public Hex this[TileVector pos]
		{
			get { return this[pos.W, pos.E]; }
		}

		public Hex this[int w, int e]
		{
			get { return _terrain[w, e]; }
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