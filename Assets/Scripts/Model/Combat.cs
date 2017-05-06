using System.Collections.Generic;

namespace Model
{
	public struct Combat
	{
		public readonly Unit Unit1;
		public readonly Unit Unit2;
		private static readonly Comparer<Combat> PriorityComparerInstance = new CombatPriorityComparer();

		public Combat(Unit unit1, Unit unit2)
		{
			Unit1 = unit1;
			Unit2 = unit2;
		}

		private sealed class CombatPriorityComparer : Comparer<Combat>
		{
			public override int Compare(Combat x, Combat y)
			{
				// this implementation puts lower energy combats at higher priority
				return (x.Unit1.Energy + x.Unit2.Energy) - (y.Unit1.Energy + y.Unit2.Energy);
			}
		}

		public static Comparer<Combat> PriorityComparer
		{
			get { return PriorityComparerInstance; }
		}

		public void Apply()
		{
			// kind of a weird implementation, however both units must be notified of the combat for animation purposes
			// it's also worth noting that solving combat in two halves like this guarantees symmetry
			Unit1.ApplyCombat(this);
			Unit2.ApplyCombat(this);
		}

		public override bool Equals(object obj)
		{
			if (obj is Combat)
			{
				var other = (Combat) obj;
				return this.Unit1 == other.Unit1 && this.Unit2 == other.Unit2
				    || this.Unit1 == other.Unit2 && this.Unit2 == other.Unit1;
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return Unit1.GetHashCode() ^ Unit2.GetHashCode();
		}
	}
}