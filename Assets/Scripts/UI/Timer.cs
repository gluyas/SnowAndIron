using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour {
	
	public GameController gameController;
	public int RoundNumber;
	public int Seconds;
	public Image RoundBar;
	public Image TimerBar;
	private int round;	//current round
	private float sec;	//current sec
	// Use this for initialization
	void Start () {
		sec = 0.01f;
	}
	
	// Update is called once per frame
	void Update () {
		round = gameController.RoundNumber;
		RoundBar.fillAmount = (float)round / RoundNumber;
		RoundTimer ();
	}

	void RoundTimer(){
		if (0 < sec && sec <= Seconds) {
			sec += Time.deltaTime;
			TimerBar.fillAmount = (float)sec / Seconds;
		}
	}
}
