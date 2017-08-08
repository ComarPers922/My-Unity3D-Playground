using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotator : MonoBehaviour
{
    [SerializeField]
    private bool Direction = true;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float angle = (Direction?1:-1)*Mathf.PI / 2 / 2 * Time.deltaTime;
        Vector3 axis = new Vector3(0,1,0);
        Quaternion axisQ = new Quaternion(axis.x * Mathf.Sin(angle),
                                                                    axis.y * Mathf.Sin(angle), axis.z * Mathf.Sin(angle), Mathf.Cos(angle));
        var mag = Mathf.Sqrt(axisQ.x * axisQ.x + axisQ.y * axisQ.y + axisQ.z * axisQ.z + axisQ.w * axisQ.w);
        var inversion = new Quaternion(-axisQ.x / mag, -axisQ.y / mag, -axisQ.z / mag, axisQ.w / mag);
        var ex = v2q(transform.position);
        var newR = axisQ * ex * inversion;
        transform.position = new Vector3(newR.x, newR.y, newR.z);
    }
    Quaternion v2q(Vector3 vec)
    {
        return new Quaternion(vec.x, vec.y, vec.z, 0);
    }
}
