using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
	public class WorldController
	{
		private World _world;
		private List<Unit> _units;

		public WorldController()
		{
			_world = new World(50, 50);
			_units = new List<Unit>();
		}

		/// <summary>
		/// Add a Unit to the game. Note that Unit objects are instantiated alongside UnitAvatars,
		/// so creation should be done elsewhere, and plug back into here.
		/// Units passed to this method should not be aliased by the caller.
		/// </summary>
		/// <param name="newUnit"></param>
		/// <returns></returns>
		public bool AddUnit(Unit newUnit)
		{
			foreach (var unit in _units)
			{
				if (newUnit == unit || newUnit.Position == unit.Position) return false;
			}
			_units.Add(newUnit);
			_world[newUnit.Position].Occupant = newUnit;
			return true;
		}

		/// <summary>
		/// Simulate the game for a single turn.
		/// </summary>
		public void DoTurn()
		{
			var activeUnits = new List<Unit>(_units);

			do // main loop: iterate through each simulation frame
			{
				var moveDestinations = new Dictionary<TileVector, List<MoveResolver>>(); // where units want to move to
				var moveOrigins 	 = new Dictionary<TileVector, MoveResolver>();		 // where units are moving from

				// TODO: also check units which are in combat at the start of the turn

				// STEP 1: AI PROCESSING
				foreach (var unit in _units)	// collect all units' moves into the Dictionary
				{
					// get unit's planned move - if it can't, then assign a default move. NB: this operation must hit
					// every unit in the game, as step 2 MUST use the Dictionary, and not the map.
					var move = unit.CanMove() ? unit.GetMove(unit, _world) : Move.Halt(unit);

					List<MoveResolver> movesToTarget;
					if (moveDestinations.ContainsKey(move.Destination))
					{
						movesToTarget = moveDestinations[move.Destination];
					}
					else // initialise the list if it doesn't exist yet
					{
						movesToTarget = new List<MoveResolver>(6);
						moveDestinations.Add(move.Destination, movesToTarget);
					}

					var resolver = new MoveResolver(move);			// wrap so we can easily solve complex dependencies
					moveOrigins.Add(move.Unit.Position, resolver);	// store for access later
					movesToTarget.Add(resolver);
				}

				// STEP 2: MOVE CONFLICT RESOLUTION
				foreach (var entry in moveDestinations)	// check all future mech positions, finalise moves
				{
					var destination = entry.Key;
					var moves = entry.Value;

					MoveResolver dependency;	// find the move from the destination tile
					if (moveOrigins.ContainsKey(destination))
					{
						dependency = moveOrigins[destination];

						// if the destination tile is occupied, then automatically reject all moves to it
						if (!dependency.Move.IsStep())
						{
							dependency.Resolve(true);
							foreach (var move in moves)
							{
								move.Resolve(false);
							}
						}
					}
					else
					{
						dependency = null;
					}

					var best = 0;
					for (var i = 1; i < moves.Count; i++)
					{
						if (Move.PriorityComparer.Compare(moves[i].Move, moves[best].Move) > 0)
						{
							moves[best].Resolve(false);
							best = i;
						}
						else
						{
							moves[i].Resolve(false);
						}
					}

					if (dependency != null) moves[best].SetParent(dependency);
					else moves[best].Resolve(true);
					// TODO: the World is not actually updated with unit positions, figure out a good way to implement
				}

				// STEP 3: COMBAT DETERMINATION
				var pendingCombat = new HashSet<Combat>();	// stops doubling up of the same combat

				for (var i = activeUnits.Count - 1; i >= 0; i--)	// iterate backwards so we can remove elements
				{
					var unit = activeUnits[i];
					// TODO: implement more complex combat engagement rules here
					foreach (var adj in unit.Position.Adjacent())
					{
						var neighbour = _world[adj].Occupant;
						if (neighbour != null && neighbour.Owner != unit.Owner)
						{
							pendingCombat.Add(new Combat(unit, neighbour));
						}
					}
					if (!unit.CanMove()) activeUnits.RemoveAt(i);	// unit no longer active
				}

				// STEP 4: COMBAT RESOLUTION
				foreach (var combat in pendingCombat.OrderBy(c => c, Combat.PriorityComparer))	// sort by priortiy
				{
					if (combat.Unit1.Health > 0 && combat.Unit2.Health > 0)
					{
						combat.Apply();
					}
				}
			} while (activeUnits.Count > 0);

			foreach (var unit in _units)
			{
				unit.Reset();
			}
		}

		private class MoveResolver
		{
			public readonly Move Move;

			private bool? _resolved = null;
			private MoveResolver _root;	// so we can detect cycles
			private List<MoveResolver> _dependents = new List<MoveResolver>();

			public MoveResolver(Move move)
			{
				_root = this;
				Move = move;
			}

			public bool Resolve(bool outcome)
			{
				if (_resolved.HasValue) return false;	// already been resolved

				_resolved = outcome;
				if (outcome)
				{
					Move.Apply();
				}
				else
				{
					Move.ApplyWithoutStep();
				}

				foreach (var dep in _dependents)
				{
					dep.Resolve(outcome);
				}
				return true;
			}

			public void SetParent(MoveResolver parent)
			{
				if (this._root != this) throw new Exception("Assigned multiple dependencies");

				if (parent._resolved.HasValue) // already been decided: propogate result
				{
					Resolve(parent._resolved.Value);
				}
				else if (parent._root == this)			// cyclic, unresolved dependencies: resolve true
				{
					Resolve(true);
				}
				else 							// wait until a decision is made
				{
					SetRoot(parent._root);
					parent._root._dependents.Add(this);	// add to root to shorten call stack
				}
			}

			private void SetRoot(MoveResolver root)
			{
				if (root == _root) throw new Exception("I think this implies an unresolved cycle");
				_root = root;
				foreach (var dependent in _dependents)
				{
					dependent.SetRoot(root);
				}
			}
		}
	}

}