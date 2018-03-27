using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : CoverBehaviour {

	// Use this for initialization
	void Start () {
        //Initialize shaders
        rend = GetComponent<Renderer>();
        highlight = Shader.Find("Self-Illumin/Outlined Diffuse");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GetTakeCover() == false)
            rend.material.color = Color.yellow;
        else
            rend.material.color = Color.red;
    }
}
