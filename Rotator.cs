using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private GameObject targetPlayer = null;
    private LineRenderer line;
    private Queue<GameObject> enemies = new Queue<GameObject>();
	// Use this for initialization
	void Start ()
    {
        line = GetComponent<LineRenderer>();
        if(targetPlayer == null)
        {
            targetPlayer = GameObject.FindGameObjectWithTag("Player");
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        var target = targetPlayer;
        foreach (var item in GameObject.FindGameObjectsWithTag("Ally"))
        {
            if (Vector3.Distance(transform.position,item.transform.position)<10)
            {
                enemies.Enqueue(item);
            }
        }
        if(enemies.Count>0)
        {
            if(Vector3.Distance(enemies.Peek().transform.position,transform.position)<20)
            {
                target = enemies.Peek();
            }
            else
            {
                enemies.Dequeue();
            }
        }

        var diff = target.transform.localPosition - transform.localPosition;
        Rigidbody rigi = GetComponent<Rigidbody>();
        diff = new Vector3(diff.x,0,diff.z);
        diff.Normalize();
        //var curr =  transform.forward.normalized;
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
            transform.localRotation = Quaternion.Slerp(loc,newR * loc,Time.deltaTime*10);
        }
        Debug.DrawLine(transform.position, transform.position + transform.localRotation* Vector3.forward*1000, Color.red);
        line.SetPosition(0,transform.position);
        line.SetPosition(1, transform.position + transform.localRotation * Vector3.forward * 1000);
      
        if (Vector3.Distance(transform.transform.position,target.transform.position) > 20 || target.tag != "Player")
        {
            transform.transform.position = Vector3.Slerp(transform.position, transform.position +  transform.localRotation * Vector3.forward,Time.deltaTime*10);
        }
        if(target.tag == "Ally" && Vector3.Distance(transform.position, target.transform.position) < 8)
        {
            var eMass = target.GetComponent<Rigidbody>().mass;
            target.GetComponent<Rigidbody>().AddForce((transform.localRotation * Vector3.forward).normalized * eMass * 1000);
            target.GetComponent<Ally>().getAngryWith(gameObject);
            GetComponent<AudioSource>().Play();
        }
    }
    Quaternion v2q(Vector3 vec)
    {
        return new Quaternion(vec.x,vec.y,vec.z,0);
    }
    Vector3 q2v(Quaternion qua)
    {
        return new Vector3(qua.x,qua.y,qua.z);
    }
}
