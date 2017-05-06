// Author
using System;
using System.Collections.Generic;

namespace Model
{
	/// <summary>
	/// Struct which represents a Unit's *planned* move for a single simulation frame.
	/// Moves should not be applied until their results have been tested and resolved.
	/// Cannot be instantiated directly - use one of the static creator methods.
	/// </summary>
	public sealed class Move
	{
		/// <summary>
		/// The Unit which this Move represents an action on.
		/// </summary>
		public readonly Unit Unit;
		/// <summary>
		/// The position that the Unit will move to.
		/// </summary>
		public readonly TileVector Destination;
		// TODO: potentialy add an origin position field, so that you don't need to read it from the unit itself
		/// <summary>
		/// The direction the Unit will turn to make the move.
		/// </summary>
		public readonly RelativeDirection Direction;

		public readonly int EnergyCost;

		private Move(Unit unit, RelativeDirection direction, TileVector destination, int energyCost)
		{
			if (unit == null) throw new ArgumentNullException("unit");
			Unit = unit;
			Direction = direction;
			Destination = destination;
			EnergyCost = energyCost;
		}

		public void Apply()
		{
			Unit.ApplyMove(this);
		}

		public void ApplyWithoutStep()
		{
			var newMove = Turn(Unit, Direction);
			newMove.Apply();
		}

		/// <summary>
		/// Constructe a Move which represents a turn followed by a single tile step.
		/// </summary>
		/// <param name="unit">the Unit to create the Move for</param>
		/// <param name="direction">the RelativeDirection for it to turn and move in</param>
		/// <returns></returns>
		public static Move Step(Unit unit, RelativeDirection direction)
		{
			TileVector destination = unit.Position + unit.Facing.Turn(direction);
			return new Move(unit, direction, destination, 1);
		}

		/// <summary>
		/// Determine if this Move is a Unit taking a step.
		/// </summary>
		/// <returns>true if it is</returns>
		public bool IsStep()
		{
			return Destination != Unit.Position;
		}

		/// <summary>
		/// Construct a Move which represents a unit turning in place.
		/// </summary>
		/// <param name="unit">the Unit to create the Move for</param>
		/// <param name="direction">the RelativeDirection for it to turn to</param>
		/// <returns></returns>
		public static Move Turn(Unit unit, RelativeDirection direction)
		{
			return new Move(unit, direction, unit.Position, 0);
		}

		/// <summary>
		/// Determine if this Move is a Unit turning in place.
		/// </summary>
		/// <returns>true if it is</returns>
		public bool IsTurn()
		{
			return Destination == Unit.Position;
		}

		/// <summary>
		/// Construct a Move which represents a unit staying completely stationary.
		/// </summary>
		/// <param name="unit">the Unit to create the Move for</param>
		/// <returns></returns>
		public static Move Halt(Unit unit)
		{
			return new Move(unit, RelativeDirection.Forward, unit.Position, 0);
		}

		/// <summary>
		/// Determine if this Move is a Unit remaining in place, and not turning.
		/// </summary>
		/// <returns>true or false</returns>
		public bool IsHalt()
		{
			return Destination == Unit.Position && Direction == RelativeDirection.Forward;
		}

		// MOVE PRIORITY FUNCTIONS

		private static readonly Comparer<Move> PriorityComparerInstance = new MovePriorityComparer();

		private sealed class MovePriorityComparer : Comparer<Move>
		{
			public override int Compare(Move x, Move y)
			{
				// IMPLEMENT MOVE PRIORITY HERE
				return x.Unit.Energy - y.Unit.Energy;
			}
		}

		public static Comparer<Move> PriorityComparer
		{
			get { return PriorityComparerInstance; }
		}
	}
}