using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

	public EventSystem eventSystem;
	public GameObject selectedObject;
	private bool buttonSelected;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () 
	{
		SelectButton ();
	}
	void SelectButton(){
		//eventSystem.GetComponent<FMODUnity.StudioEventEmitter>().Play();
			if (Input.GetAxisRaw ("Vertical") != 0 && buttonSelected == false) 
			{

			eventSystem.SetSelectedGameObject(selectedObject);
			buttonSelected = true;
			}

		
	}

//	void OnEnable()
//	{
//		Debug.Log("I am Onenabled: "+buttonSelected);
//	}

	void OnDisable()
	{
		buttonSelected = false;
//		Debug.Log ("I am OnDisable "+buttonSelected);
	}
}