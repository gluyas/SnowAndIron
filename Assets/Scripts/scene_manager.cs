using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_manager : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void start_game () {
        SceneManager.LoadScene("arena");
	}

    public void end()
    {
        Application.Quit();
    }
}
