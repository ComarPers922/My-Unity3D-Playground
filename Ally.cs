using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{

    private GameObject target = null;
    public void getAngryWith(GameObject enemy)
    {
        target = enemy;
    }
	// Use this for initialization
	void Start ()
    {
        int count = GameObject.FindGameObjectsWithTag("Enemy").Length;
        target = GameObject.FindGameObjectsWithTag("Enemy")[Random.Range(0,count)];
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Random.Range(0,10000)>9990)
        {
            int count = GameObject.FindGameObjectsWithTag("Enemy").Length;
            target = GameObject.FindGameObjectsWithTag("Enemy")[Random.Range(0, count)];
        }
        var diff = target.transform.localPosition - transform.localPosition;
        Rigidbody rigi = GetComponent<Rigidbody>();
        diff = new Vector3(diff.x, 0, diff.z);
        diff.Normalize();
        var curr = transform.localRotation * Vector3.forward;
        var ang = Mathf.Acos(Vector3.Dot(curr, diff) / (diff.magnitude * curr.magnitude));
        Vector3 axis = Vector3.Cross(curr, diff);
        if (Vector3.Dot(curr, diff) < -1 + 0.001f)
        {
            // special case when vectors in opposite directions:
            // there is no "ideal" rotation axis
            // So guess one; any will do as long as it's perpendicular to start
            axis = Vector3.Cross(new Vector3(0.0f, 0.0f, 1.0f), curr);
            if (axis.magnitude < 0.01) // bad luck, they were parallel, try again!
                axis = Vector3.Cross(new Vector3(1.0f, 0.0f, 0.0f), curr);

            axis.Normalize();
            ang = Mathf.PI;
        }
        if (ang > .1f)
        {
            float angle = ang / 2;
            axis.Normalize();
            var newR = new Quaternion(axis.x * Mathf.Sin(angle),
                   axis.y * Mathf.Sin(angle), axis.z * Mathf.Sin(angle), Mathf.Cos(angle));
            var loc = transform.localRotation;
            transform.localRotation = Quaternion.Slerp(loc, newR * loc, Time.deltaTime * 10);
        }
        Debug.DrawLine(transform.position, transform.position + transform.localRotation * Vector3.forward * 1000, Color.red);
        if (Vector3.Distance(transform.transform.position, target.transform.position) > 5 || target.tag != "Enemy")
        {
            transform.transform.position = Vector3.Slerp(transform.position, transform.position + transform.localRotation * Vector3.forward, Time.deltaTime * 70);
        }
        if (Vector3.Distance(transform.position, target.transform.position) < 7)
        {
            var eMass = target.GetComponent<Rigidbody>().mass;
            target.GetComponent<Rigidbody>().AddForce((transform.localRotation*Vector3.forward).normalized*eMass*500);
            GetComponent<AudioSource>().Play();
        }
    }
}
