using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlay : MonoBehaviour {

    public GameController gameController;
	public UnitPlacer p1UnitPlacer;
	public UnitPlacer p2UnitPlacer;
    public Player p1;
    public Player p2;

    public Text roundNumber;
    public Text p1objectives;
    public Text p2objectives;
    public Text p1killed;
    public Text p2killed;
	public Text p1Spear;
	public Text p1Scythe;
	public Text p1Traktor;
	public Text p2Spear;
	public Text p2Scythe;
	public Text p2Traktor;


    private string roundNum;
    private string p1objs;
    private string p2objs;
    private string p1kill;
    private string p2kill;
	private string p1_Spear;
	private string p1_Scythe;
	private string p1_Traktor;
	private string p2_Spear;
	private string p2_Scythe;
	private string p2_Traktor;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        
        roundNum = "" + gameController.RoundNumber.ToString();
        roundNumber.text = roundNum;

        p1objs = "" + p1.CapturedObjectives.ToString();
        p1objectives.text = p1objs;

        p2objs = "" + p2.CapturedObjectives.ToString();
        p2objectives.text = p2objs;

        p1kill = "" + p1.DestroyedUnits.ToString();
        p1killed.text = p1kill;

        p2kill = "" + p2.DestroyedUnits.ToString();
        p2killed.text = p2kill;

		p1_Spear = "" + p1UnitPlacer.UnitSelectionKeys [0];
		p1Spear.text = p1_Spear;

		p1_Scythe = "" + p1UnitPlacer.UnitSelectionKeys [2];
		p1Scythe.text = p1_Scythe;

		p1_Traktor = "" + p1UnitPlacer.UnitSelectionKeys [1];
		p1Traktor.text = p1_Traktor;

		p2_Spear = "" + p2UnitPlacer.UnitSelectionKeys [0];
		p2Spear.text = p2_Spear;

		p2_Scythe = "" + p2UnitPlacer.UnitSelectionKeys [2];
		p2Scythe.text = p2_Scythe;

		p2_Traktor = "" + p2UnitPlacer.UnitSelectionKeys [1];
		p2Traktor.text = p2_Traktor;
    }
}
