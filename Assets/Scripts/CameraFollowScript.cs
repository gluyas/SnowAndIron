using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{

    public GameObject Camera;
    public GameObject FollowTarget;

    private Transform _ft;
    private Transform _ct;

	// Use this for initialization
	void Start ()
	{
	    _ft = FollowTarget.GetComponent<Transform>();
	    _ct = Camera.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Vector3 facing = _ct.position - _ft.position;
	    _ct.forward = -facing;
	}
}
