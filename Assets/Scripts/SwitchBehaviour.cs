using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehaviour : CoverBehaviour {

    GameObject door; 
	public bool switchJammed = false; 
	// Use this for initialization
	void Start () 
	{
        rend = GetComponent<Renderer>();
        highlight = Shader.Find("Self-Illumin/Outlined Diffuse");
		GameObject[] interactables = GameObject.FindGameObjectsWithTag("Interactable"); 
		foreach (GameObject g in interactables)
			if (g.GetComponent<MovingPlatBehaviour> () != null)
				door = g;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (GetTakeCover () == false) 
		{
			rend.material.color = Color.yellow;
			if (!switchJammed)
				door.GetComponent<MovingPlatBehaviour> ().Activate (false); 
		}
        else
            rend.material.color = Color.red;

		transform.GetChild (0).gameObject.SetActive (switchJammed);
    }

    void OnMouseDown()
    {
        SetTakeCover(!GetTakeCover());
    }
}
