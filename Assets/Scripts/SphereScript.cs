using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour
{

    public GameObject thisSphere;
    public GameObject otherSphere;
    public float force = 0.5f;

    private Rigidbody _rb;
    private Transform _ot;

	// Use this for initialization
	void Start ()
	{
	    _rb = thisSphere.GetComponent<Rigidbody>();
	    _ot = otherSphere.GetComponent<Transform>();
	    _rb.velocity = new Vector3(Random.value*10-5, Random.value*10-5);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    _rb.AddForce((_ot.position - _rb.position)*force);
	}
}
