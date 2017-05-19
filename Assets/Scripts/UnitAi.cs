﻿using Model;
using UnityEngine;

/// <summary>
/// Class from which all AI implementations should derive.
///
/// Each UnitAi class should represent a kind of Unit behaviour. Any public fields in this
/// class should modify the behaviour in a generalised way, so that multiple instances of it
/// can exhibit similar, but slightly different behaviour. These will be accessable in the Unity editor.
/// Each Unit type will then reference an instance of this class which it will use to generate its movement plans.
///
/// Assume that multiple Units may reference the same instance of a UnitAi, so do not store
/// state relating to specific Units here.
/// </summary>
public abstract class UnitAi : ScriptableObject
{
	/// <summary>
	/// The main method of this interface. Produce a MovementPlan for the given Unit that corresponds to
	/// the parameters (if any) of this UnitAi instance.
	/// </summary>
	/// <param name="unit">the Unit to construct the move for</param>
	/// <param name="world">the game World which the Unit is in</param>
	/// <returns>a MovementPlan for this Unit, for the current turn</returns>
	public abstract TurnPlan GetMovementPlan(Unit unit, World world);
}