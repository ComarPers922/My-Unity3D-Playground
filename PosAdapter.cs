using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosAdapter : MonoBehaviour
{
    [SerializeField]
    private Camera MainCamera;

    // Use this for initialization
    void Start ()
    {
        transform.position = MainCamera.ScreenToWorldPoint(new Vector3(GetComponent<MeshRenderer>().bounds.size.x * 18 / transform.localScale.x, Screen.height - GetComponent<MeshRenderer>().bounds.size.y * 18 / transform.localScale.y, 15));
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
