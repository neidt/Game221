using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportIfClicked : MonoBehaviour
{
    public TilesByGeneration generatedTiles;
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit info;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out info))
            {
                if(info.transform == this.transform)
                {
                    print("ray hit at" + info.transform.position.ToString());

                    Node myNode = generatedTiles.tilesToNode[this.gameObject];

                }
            }
        }
	}
}
