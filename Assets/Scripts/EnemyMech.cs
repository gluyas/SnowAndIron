using System.Collections.Generic;
using Model;
using UnityEngine;

public class EnemyMech : Mech
{
	private void Start()
	{
		Animator = GetComponent<Animator>();
		TilePos = new TileVector(0,0);
		RotationOffset = Quaternion.Euler(RotationOffsetEuler);
	}

}