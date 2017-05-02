using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	public int Radius;
	public float TileEdgeLength = 0.01f;
	private float _scaleFactor;

	private List<TileVector> _map = new List<TileVector>();

	public float JitterFactor = 0;

	public GameObject[] TileObjects;

	public Int32 Seed;
	private System.Random _rng;

	public List<TileVector> GetTiles()
	{
		return _map;
	}

	// Use this for initialization
	void Start()
	{
		if (TileObjects.Length == 0 || TileObjects == null) throw new Exception("Missing tile prefabs");

		if (Seed == 0) _rng = new System.Random();
		else _rng = new System.Random(Seed);

		_scaleFactor = ModelExtensions.Scale / TileEdgeLength / 2;

		var origin = new TileVector(0, 0);

		for (var w = 0; w < Radius*2-1; w++)
		{
			for (var e = 0; e < Radius*2-1; e++)
			{
				if (Math.Abs(w - e) >= Radius) continue;
				MakeTile(origin + new TileVector(w, e));
			}
		}
	}

	void MakeTile(TileVector pos)
	{
		var newObj = Instantiate(TileObjects[_rng.Next(TileObjects.Length)]);
		// position and scale
		newObj.transform.position = pos.ToVector3();
		newObj.transform.localScale = new Vector3(_scaleFactor, _scaleFactor, _scaleFactor);
		// rotate, to break up repetition
		newObj.transform.rotation = RandomDirection().GetBearingRotation();
		// apply jitter (if enabled)
		if (JitterFactor > 0) newObj.transform.position += GetJitter(pos);
		// finally, add to our set of tile vectors
		_map.Add(pos);
	}

	CardinalDirection RandomDirection()
	{
		return (CardinalDirection) _rng.Next(6);
	}

	Vector3 GetJitter(TileVector pos)
	{
		System.Random jitterGen = new System.Random(pos.GetHashCode());
		return new Vector3(
			(float) jitterGen.NextDouble()*JitterFactor - JitterFactor/2,
			(float) jitterGen.NextDouble()*JitterFactor - JitterFactor/2,
			(float) jitterGen.NextDouble()*JitterFactor - JitterFactor/2
		);
	}
}
