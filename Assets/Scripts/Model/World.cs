using System;
using System.IO;
using System.Net;

namespace Model
{
	public class World
	{
		public readonly int W, E;
		private Hex[,] _terrain;

        /*public World(int w, int e)
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
		}*/
        public World(int map, int mapsize)
        {
            W = mapsize;
            E = mapsize;
            _terrain = new Hex[mapsize, mapsize];
            String filename = "map" + map + ".txt";
            StreamReader reader = File.OpenText(filename);
            string line;
            int i = 0;
            while ((line = reader.ReadLine()) != null)
            {
                //just hextype
                string[] mapline = line.Split('\t');
                for(int j = 0; j < mapline.Length; j++)
                {
                        if(mapline[j] == "-")
                    {
                        //Do nothing
                    }
                    else
                    {
                        HexType type = (HexType)int.Parse(mapline[j]);
                        _terrain[i, j] = new Hex(type);
                    }
                }
                i++;
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