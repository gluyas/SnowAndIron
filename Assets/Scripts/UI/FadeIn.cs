using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

	public Image gameOver1;
	public Image gameOver2;
	public Image playAgain;
	public Image mainMenu;
	private float fadeSpeed1 = 0.03f;
	private float fadeSpeed2 = 0.1f;

	Color thisAlpha1;
	Color thisAlpha2;

	// Use this for initialization
	void Start () {
		
		thisAlpha1 = gameOver1.color;
		thisAlpha1.a = 0;
		thisAlpha2 = playAgain.color;
		thisAlpha2.a = 0;

		gameOver1.color = thisAlpha1;
		gameOver2.color = thisAlpha1;

		playAgain.color = thisAlpha2;
		mainMenu.color = thisAlpha2;
	}
	
	// Update is called once per frame
	void Update () {

		thisAlpha1.a += fadeSpeed1;
		thisAlpha2.a += fadeSpeed2;

		gameOver1.color = thisAlpha1;
		gameOver2.color = thisAlpha1;

		playAgain.color = thisAlpha2;
		mainMenu.color = thisAlpha2;


		
		
	}


}
