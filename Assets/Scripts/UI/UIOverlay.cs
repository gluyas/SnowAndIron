using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlay : MonoBehaviour {

    public GameController gameController;

    public Player p1;
    public Player p2;

    public Text roundNumber;
    public Text p1objectives;
    public Text p2objectives;
    public Text p1killed;
    public Text p2killed;

    private string roundNum;
    private string p1objs;
    private string p2objs;
    private string p1kill;
    private string p2kill;

	private int totalRounds;
	public int RoundSeconds;
	public Image RoundBar;
	public Image TimerBar;

	private int currentRound;	//current round

    // Use this for initialization
    void Start () {
		totalRounds = gameController.NumberOfRounds;
	}
	
	// Update is called once per frame

	void Update () {

        
        roundNum = "" + (gameController.NumberOfRounds- gameController.RoundNumber ).ToString();

        roundNumber.text = roundNum;

        p1objs = "" + p1.CapturedObjectives.ToString();
        p1objectives.text = p1objs;

        p2objs = "" + p2.CapturedObjectives.ToString();
        p2objectives.text = p2objs;

        p1kill = "" + p1.DestroyedUnits.ToString();
        p1killed.text = p1kill;

        p2kill = "" + p2.DestroyedUnits.ToString();
        p2killed.text = p2kill;

		Round ();
		TimerBar.fillAmount = gameController.ElapsedTime / gameController.CurrentTurnTime;
    }

	void Round(){
		currentRound = gameController.RoundNumber;
		RoundBar.fillAmount = (float)(currentRound) / totalRounds;
	}
}
