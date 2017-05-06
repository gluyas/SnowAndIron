using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;
using NUnit.Framework.Internal;

public class PlayerMech : UnitAvatar
{
	// Game state related stuff (not in final project)
	public Stack<GameObject> Inventory = new Stack<GameObject>();

	public int Score;
	public int Health;
/*
	protected void Update () {
		if (Input.GetKeyDown(KeyCode.W)) Move(RelativeDirection.Forward);
		if (Input.GetKeyDown(KeyCode.S)) Move(RelativeDirection.Back);
		if (Input.GetKeyDown(KeyCode.A)) Move(RelativeDirection.BackLeft);
		if (Input.GetKeyDown(KeyCode.D)) Move(RelativeDirection.BackRight);
		if (Input.GetKeyDown(KeyCode.Q)) Move(RelativeDirection.ForwardLeft);
		if (Input.GetKeyDown(KeyCode.E)) Move(RelativeDirection.ForwardRight);
	}
*/
}
