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
		foreach (var pos in new TileVector(25, 25).Adjacent())
		{
			GameObject ua = Instantiate(TestUnit);
			_worldController.AddUnit(new Unit(ua.GetComponent<UnitAvatar>(),
				pos, CardinalDirection.North));
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A)) _worldController.DoTurn();
	}
}
