using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.transform.position = Vector3.Slerp(transform.position, transform.position + transform.localRotation * Vector3.forward, Time.deltaTime * 50);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            transform.transform.position = Vector3.Slerp(transform.position, transform.position + transform.localRotation * -Vector3.forward, Time.deltaTime * 50);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.localRotation *= Quaternion.Euler(0,-100*Time.deltaTime,0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localRotation *= Quaternion.Euler(0, 100 * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.Space)&&allowJump)
        {
            GetComponent<Rigidbody>().AddForce((transform.localRotation * transform.up).normalized * 1000);
        }
    }
    private bool allowJump = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Terrain")
        {
            allowJump = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag == "Terrain")
        {
            allowJump = false;
        }
    }
   
}
