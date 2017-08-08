using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMiniMap : MonoBehaviour
{
    [SerializeField]
    private Texture MyTex;
    [SerializeField]
    private Material MyMat;
	// Use this for initialization
	void Start ()
    {
       
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnGUI()
    {
        if(Event.current.type == EventType.Repaint)
        {
            
        }
    }
}
