using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Model;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
	public readonly UnityEvent OnRoundTimeOut = new UnityEvent();
	
	public WorldGenerator WorldGenerator;
    public GameObject GameOverMenu;
	public World World { get { return WorldGenerator.World; }}
	[FMODUnity.EventRef]
	public string roundSound = "event:/GameStart";
	public Player[] Players = new Player[2];
	private Dictionary<Player, bool> _playerUnitPlaced; // TODO: refactor resource management into Player class

    public int MapSize = 20;
    public int NumberOfMaps = 1;
    public int NumberOfRounds;

	public GameObject red;
	public GameObject blue;

	private bool _gameOver;
    
	public float MaxTurnTime = 15;
	public float MinTurnTime = 2;
	public float TurnTimeDecayRate = 0.5f;
	public float ElapsedTime { get; private set; }
	public float CurrentTurnTime { get; private set; }
 
	public int RoundNumber { get { return _worldController != null ? _worldController.RoundNumber : 0; } }
	private WorldController _worldController;

	private HashSet<UnitAvatar> _allAvatars = new HashSet<UnitAvatar>();
	private bool AnimationsComplete { get { return _allAvatars.All(avatar => avatar.CurrentAnimation == null); } }
	
	private void Start()
	{
		CurrentTurnTime = MaxTurnTime;
		_worldController = new WorldController(WorldGenerator.World);
        GameOverMenu.SetActive(false);
		_playerUnitPlaced = new Dictionary<Player, bool>();
		foreach (var player in Players)
		{
			_playerUnitPlaced[player] = false;
		}
		_gameOver = false;
	}

	private void Update()
	{
		if (_gameOver) return;
		#if DEBUG
		if (Input.GetKeyDown(KeyCode.BackQuote)) DoTurn();
		#endif

		checkGameOver();
		
		if (AnimationsComplete)	// stop timer when animations are happening
		{
			ElapsedTime += Time.deltaTime;
			if (ElapsedTime >= CurrentTurnTime)
			{
				var round = RoundNumber;
				OnRoundTimeOut.Invoke();
				if (round == RoundNumber) DoTurn();	// if invoking the event did not trigger an end of round
			}
		}
	}
	
	public void DoTurn()
	{
		_worldController.DoTurn();
		FMODUnity.RuntimeManager.PlayOneShot (roundSound, new Vector3 (0, 0, 0));

		if (!_gameOver)
		{
			ElapsedTime = 0;
			CurrentTurnTime *= TurnTimeDecayRate;
			if (CurrentTurnTime < MinTurnTime) CurrentTurnTime = MinTurnTime;
			
			Debug.Log(CurrentTurnTime);
			
			foreach (var player in Players)
			{
				_playerUnitPlaced[player] = false;
			}
		}
	}

	/// <summary>
	/// Creates a new Unit from a prefab. This includes adding it to the game logic, and creating a visual
	/// representation. The prefab provided should have a UnitAvatar script attatched, as well as any
	/// requirements of that script. This operation will fail if the given position is already occupied.
	/// </summary>
	/// <param name="unitPrefab">the prefab of the unit to create</param>
	/// <param name="pos">the TileVector position to spawn it</param>
	/// <param name="dir">the Direction for it to be facing</param>
	/// <param name="owner">the Player owner of the Unit</param>
	/// <param name="mirrored">whether or not the Unit is mirrored</param>
	/// <returns>true if the operation was successful; false if not</returns>
	public bool MakeUnit(GameObject unitPrefab, Player owner, TileVector pos, CardinalDirection dir, bool mirrored)
	{
		if (_gameOver || _playerUnitPlaced[owner]) return false;	// stop players placing more than one unit
		
		var avatar = Instantiate(unitPrefab).GetComponent<UnitAvatar>();			
		var unit = avatar.CreateUnit(owner, pos, dir, mirrored);
		
		if (!_worldController.AddUnit(unit)) // oops! bad unit placement, so delete the unit as if nothing happened
		{
			Destroy(avatar.gameObject);
			return false;
		}
		else 	// successful placement
		{
			avatar.SetUnit(unit);
			_playerUnitPlaced[owner] = true;	// set player as placed a unit
			
			_allAvatars.Add(avatar);
			
			avatar.EnqueueAnimation(new DeployAnimation(avatar));
			
			var allPlaced = true;				// check if all players have placed a unit
			foreach (var placed in _playerUnitPlaced.Values)
			{
				if (!placed)
				{
					allPlaced = false;
					break;
				}
			}
			if (allPlaced)						// reset players' placement status and run game
			{
				DoTurn();
			}
			return true;
		}
	}

    private void checkGameOver()
    {
        if (RoundNumber >= NumberOfRounds && AnimationsComplete)
        {
			_gameOver = true;
            GameOverMenu.SetActive(true);

			int p1Objectives = Players [0].CapturedObjectives;
			int p2Objectives = Players [1].CapturedObjectives;
			int p1killed = Players [0].DestroyedUnits;
			int p2killed = Players [1].DestroyedUnits;

			if (p1Objectives > p2Objectives) {

				red.SetActive (false);
				blue.SetActive (true);

			} else if (p2Objectives > p1Objectives) {

				blue.SetActive (false);
				red.SetActive (true);

			} else { // if tie
				if (p1killed > p2killed) {
					red.SetActive (false);
					blue.SetActive (true);
				} else {
					blue.SetActive (false);
					red.SetActive (true);
				}
			}
        }
    }
	
	/*
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
	private void CleanWorld()
	{
		if (instancedTiles == null) {
			instancedTiles = GameObject.FindGameObjectsWithTag ("EditorTile");
		}
		foreach (GameObject tile in instancedTiles) {
			DestroyImmediate (tile);
		}
	}
	*/
}
