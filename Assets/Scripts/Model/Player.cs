using System;
using System.Collections.Generic;
using Model;

namespace Model
{
	public sealed class Player
	{
		public List<String> resources = new List<String>();
		public List<Unit> units = new List<Unit>();
		public int num;

		public Player (int number)
		{
			num = number;
		}

		public void AddUnit(Unit unit)
		{
			units.Add (unit);
		}

		public void AddResource(String resource)
		{
			resources.Add (resource);
		}
	}
}

