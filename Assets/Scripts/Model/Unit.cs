using System;

namespace Model
{
	public sealed class Unit
	{
		public delegate Move AiMoveMethod(Unit unit, World world);

		public const int UnitBadMovesTimeout = 5;

		// Accessors
		public TileVector Position { get; private set; }
		public CardinalDirection Facing { get; private set; }

		public MovementPlan LastMove { get; private set; }

		public AiMoveMethod GetMove; // TODO: replace with new MovementPlan method instead once complete

		// Unit stats and behaviour
		public readonly int Owner = 0; // TODO: update with better semantics

		public readonly int MaxHealth;
		public int Health { get; private set; }

		public readonly int MaxEnergy;
		public int Energy { get; private set; }

		// AI Hooks
		private readonly AiMoveMethod _moveMethod;
		// we might need more, for example, combat modifiers

		// Private fields
		private readonly UnitAvatar _avatar;	// Unity representation
		private int _timeout = UnitBadMovesTimeout;

		public Unit (UnitAvatar avatar, TileVector position, CardinalDirection facing)
		{
			_avatar = avatar;

			Position = position;
			Facing = facing;

			MaxHealth = _avatar.MaxHealth;
			Health = Health;

			MaxEnergy = _avatar.MaxEnergy;
			Energy = MaxEnergy;

			//TODO: temporary code until we can bundle AiMoveMethod with UnitAvatar
			GetMove = RandomMoveMethod;

		}

		public bool CanMove()
		{
			// TODO: this will probably need refinement!
			return _timeout >= 0 && Energy > 0;
		}

		public void Reset()
		{
			// TODO: refine
			ResetTimeout();
			Energy = MaxEnergy;
		}

		public void ResetTimeout()
		{
			_timeout = UnitBadMovesTimeout;
		}

		public bool Timeout()
		{
			return --_timeout == 0;
		}

		public void ApplyMove(Move move)
		{
			if (move.Unit != this) throw new ArgumentException("Applied move does not reference this unit");

			_avatar.ApplyMove(move); // animate avatar (do this first, as it needs to read fields here)

			Facing = Facing.Turn(move.Direction);
			Position = move.Destination;
			Energy -= move.EnergyCost;
		}

		public void ApplyCombat(Combat combat)
		{
			Unit other;		// determine which unit is not this one
			if (combat.Unit1 == this) 		other = combat.Unit2;
			else if (combat.Unit2 == this) 	other = combat.Unit1;
			else throw new ArgumentException("Applied combat does not reference this unit");

			var startingHealth = this.Health;	// trade energy for health with the other unit
			this.Health -= other.Energy;		// implementing combat in two halves like this guarantees symmetry
			other.Energy -= startingHealth;
		}

		public static Move RandomMoveMethod(Unit unit, World world)
		{
			return Move.Step(unit, (RelativeDirection) new Random(unit.Position.GetHashCode()).Next(6));
		}
	}
}