using System;
using Model;
using UnityEngine;

public static class ModelExtensions
{
	// modify these values to change the orientation of the board.
	public const float Scale = 2;
	public static readonly Vector3 North = Vector3.forward * Scale;
	public static readonly Vector3 Up 	  = Vector3.up 		* Scale;

	// the following are defined in terms of the above values
	public static readonly Vector3 EVector = Quaternion.AngleAxis(120, Up) * North;	// 120 from North
	public static readonly Vector3 WVector = Quaternion.AngleAxis(120, Up) * EVector;	// 120 from Southeast

	public static Vector3 ToVector3(this TileVector tv)
	{
		return tv.W * WVector + tv.E * EVector;
	}

	public static Vector3 ToVector3(this CardinalDirection d)
	{
		return d.GetTileVector().ToVector3();
	}

	public static Quaternion GetBearingRotation(this TileVector tv)
	{
		Vector3 facing = tv.ToVector3();
		float angle = Vector3.Angle(North, facing);
		if (angle == 180) return Quaternion.AngleAxis(angle, Up);
		else return Quaternion.FromToRotation(North, facing);
	}

	public static Quaternion GetBearingRotation(this CardinalDirection d)
	{
		return Quaternion.AngleAxis(d.GetBearing(), Up);
	}

	public static Quaternion GetPivotRotation(this RelativeDirection d)
	{
		return GetBearingRotation((CardinalDirection) d);
	}
}