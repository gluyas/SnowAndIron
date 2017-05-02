using System;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class Mech : MonoBehaviour
{
	// Editor variables
	public float MoveSpeed = 5f;
	public float TurnSpeed = 180f;

	public Vector3 PositionOffset = Vector3.zero;
	public Vector3 RotationOffsetEuler = Vector3.zero;	// to expose to unity editor
	public Quaternion RotationOffset { get; protected set; }

	// Unity components
	public Animator Animator { get; protected set; }
	//public Transform Transform { get; protected set; }

	// Movement state trackers
	private Queue<MoveAnimation> _moveQueue = new Queue<MoveAnimation>();
	private CardinalDirection _facing = CardinalDirection.South;

	// Plug for other parts of the Model
	public TileVector TilePos { get; protected set; }

	public Quaternion Rotation
	{
		set { transform.rotation = value * RotationOffset; }
		get { return transform.rotation * Quaternion.Inverse(RotationOffset); }
	}

	public Vector3 Position
	{
		set { transform.position = value; }
		get { return transform.position; }
	}

	protected virtual void Start ()
	{
		//Transform = GetComponent<Transform>();
		Animator = GetComponent<Animator>();
		TilePos = new TileVector(0,0);
		RotationOffset = Quaternion.Euler(RotationOffsetEuler);
	}

	protected virtual void FixedUpdate()
	{
		if (_moveQueue.Count > 0)
		{
			if (_moveQueue.Peek().ApplyAnimation(Time.deltaTime))
			{
				_moveQueue.Dequeue();
			}
		}
	}

	public void SetPositionAndOrientation(TileVector tv, CardinalDirection dir)
	{
		Position = tv.ToVector3();
		Rotation = dir.GetBearingRotation();
		TilePos = tv;
		_facing = dir;
	}

	public void Move(CardinalDirection dir)
	{
		//todo: implement logic if move is allowed
		TileVector newPos = TilePos + dir;
		_moveQueue.Enqueue(new MoveAnimation(this, TilePos, newPos));
		TilePos = newPos;
		_facing = dir;
		OnMove();
	}

	public void Move(RelativeDirection dir)
	{
		Move(_facing.Turn(dir));
	}

	protected virtual void OnMove()
	{

	}
}