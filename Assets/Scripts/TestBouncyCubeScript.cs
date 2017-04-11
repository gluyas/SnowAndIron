using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBouncyCubeScript : MonoBehaviour
{

    public GameObject BouncyCube;

    private Rigidbody _rb;

	// Use this for initialization
	void Start ()
	{
	    _rb = BouncyCube.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        _rb.velocity = new Vector3(0, 10, 0);
    }
}
