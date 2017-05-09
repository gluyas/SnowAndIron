using UnityEngine;
using Model;

public class GameController : MonoBehaviour
{
	public GameObject TestUnit;

	public int UnitCount = 3;

	private WorldController _worldController;

	private void Start()
	{
		_worldController = new WorldController();
		for (var i = 0; i < UnitCount; i++)
		{
			GameObject ua = Instantiate(TestUnit);
			_worldController.AddUnit(new Unit(ua.GetComponent<UnitAvatar>(),
				new TileVector(25-UnitCount/2+i, 25), CardinalDirection.North));
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
		return _worldController.AddUnit(unit);
	}
}
