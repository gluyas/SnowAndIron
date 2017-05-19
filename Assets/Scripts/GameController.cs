using System.Collections.Generic;
using UnityEngine;
using Model;

public class GameController : MonoBehaviour
{
	public GameObject[] TestUnits;

	public GameObject[] HexModels;
	private List<GameObject> _hexInstances = new List<GameObject>();

	public int UnitCount = 3;

	public Player[] players = new Player[2];

    public int mapsize = 20;
    public int numberOfMaps = 1;

	private WorldController _worldController;

	private void Start()
	{
        int map = Random.Range(0, numberOfMaps);
		var world = new World(map, mapsize);
		_worldController = new WorldController(world);
		RenderWorld(world);
		players [0] = new Player (1);
		players [1] = new Player (2);

		for (var i = 0; i < UnitCount; i++)
		{
			var pos = i % 2 == 0 ? new TileVector(i, 0) : new TileVector(0, i);
			MakeMech(Instantiate(TestUnits[i%TestUnits.Length]), pos, CardinalDirection.North, players[i%2]);
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Z)) _worldController.DoTurn();
	}

	private void RenderWorld(World world)
	{
		for (var w = 0; w < world.W; w++)
		{
			for (var e = 0; e < world.E; e++)
			{
				var hex = world[w, e];
				if (hex != null)
				{
					var tile = Instantiate(HexModels[(int) hex.Type]);
					tile.transform.position = new TileVector(w, e).ToVector3();
					_hexInstances.Add(tile);
				}
			}
		}
	}

	public bool MakeMech(GameObject unitAvatar, TileVector pos, CardinalDirection dir, Player owner)
	{
		UnitAvatar avatar = unitAvatar.GetComponent<UnitAvatar>();
		Unit unit = new Unit(avatar, pos, dir, owner);
		owner.AddUnit (unit);
		avatar.SetPositionAndOrientation(pos, dir);
		return _worldController.AddUnit(unit);
	}
}
