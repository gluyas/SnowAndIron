using UnityEngine;
using Model;

public class GameController : MonoBehaviour
{
	public GameObject[] TestUnits;

	public int UnitCount = 3;

	private WorldController _worldController;

	private void Start()
	{
		_worldController = new WorldController();
		for (var i = 0; i < UnitCount; i++)
		{
			MakeMech(Instantiate(TestUnits[i%TestUnits.Length]), new TileVector(0, 2*i), CardinalDirection.North);
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A)) _worldController.DoTurn();
	}

	public bool MakeMech(GameObject unitAvatar, TileVector pos, CardinalDirection dir)
	{
		UnitAvatar avatar = unitAvatar.GetComponent<UnitAvatar>();
		Unit unit = new Unit(avatar, pos, dir);
		avatar.SetPositionAndOrientation(pos, dir);
		return _worldController.AddUnit(unit);
	}
}
