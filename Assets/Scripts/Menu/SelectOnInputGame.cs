using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectOnInputGame : MonoBehaviour {

	public EventSystem eventSystem;
	public GameObject selectedObject;

	//public Button button1;
	//public Button button2;


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
		if (Input.GetAxisRaw ("Vertical") != 0 && buttonSelected == false) 
		{

			eventSystem.SetSelectedGameObject(selectedObject);
			buttonSelected = true;
		}


	}

	public void replay(){

		Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);


	}

	public void mainMenu(){

		SceneManager.LoadScene ("menu");

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