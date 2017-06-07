using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSCript : MonoBehaviour {

	public Player p1;
	public Player p2;
	public Text score1;
	public Text score2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		score1.text = p1.Score.ToString ();
		score2.text = p2.Score.ToString ();
	}
}
