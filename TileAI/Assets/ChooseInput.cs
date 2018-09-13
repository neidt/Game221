using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseInput : MonoBehaviour
{
    public static bool isMouseEnabled;

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    
    public void swapBool()
    {
        isMouseEnabled = !isMouseEnabled;
    }
}
