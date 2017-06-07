using UnityEngine;

[CreateAssetMenu(fileName = "Assets/NewPlayer")]	// TODO: choose a better folder
public sealed class Player : ScriptableObject
{
	public string Name;
	public Color Color;
	public Loadout Loadout;

	public int Score = 0;

	public GameObject[] Units	// simple alias for the loadout
	{
		get { return Loadout.Units; }
	}
}



