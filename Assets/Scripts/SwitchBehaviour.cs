using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehaviour : CoverBehaviour {

    GameObject[] interactables; 
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        highlight = Shader.Find("Self-Illumin/Outlined Diffuse");
        interactables = GameObject.FindGameObjectsWithTag("Interactable"); 
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GetTakeCover() == false)
            rend.material.color = Color.yellow;
        else
            rend.material.color = Color.red;
    }

    void OnMouseDown()
    {
        //If Clicked
        foreach (GameObject x in interactables)
        {
            //Check if the interactable is a moving platform 
            if (x.GetComponent<MovingPlatBehaviour>() != null)
                x.GetComponent<MovingPlatBehaviour>().activate = false;
        }
        SetTakeCover(!GetTakeCover());
    }
}
