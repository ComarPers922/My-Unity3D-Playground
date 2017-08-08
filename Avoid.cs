using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid : MonoBehaviour
{
    [SerializeField]
    private string targetTag = "";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private bool allowJump = true;
    void OnTriggerEnter(Collider collider)
    {   
        if(collider.tag == "Terrain")
        {
            allowJump = true;
        }
        if (collider.tag == targetTag && allowJump)
        {
            //transform.parent.Translate(transform.parent.localRotation * transform.parent.up  *100 * Time.deltaTime);
            var mMass = transform.parent.GetComponent<Rigidbody>().mass;
            transform.parent.GetComponent<Rigidbody>().AddForce((transform.parent.localRotation * transform.parent.up).normalized * 500 * mMass);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Terrain")
        {
            allowJump = false;
        }
    }
}
