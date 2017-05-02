namespace Model
{
	public class Map
	{
		private Hex[][] _terrain;
	}

	public struct Hex
	{
		public readonly int Type;

		public Hex(int type)
		{
			Type = type;
		}
	}
}