using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jammer : MonoBehaviour {

	GameObject[] interactables; 
	// Use this for initialization
	void Start () {
		interactables = GameObject.FindGameObjectsWithTag ("Interactable"); 
	}
	
	// Update is called once per frame
	void OnTriggerStay(Collider other)
	{
		if (Input.GetKeyUp (KeyCode.E))
			foreach (GameObject g in interactables)
				if (g.GetComponent<SwitchBehaviour> () != null) 
				{
					g.GetComponent<SwitchBehaviour> ().switchJammed = !g.GetComponent<SwitchBehaviour> ().switchJammed; 
					if (g.GetComponent<SwitchBehaviour> ().switchJammed)
						GetComponent<Renderer> ().material.color = Color.red;
					else
						GetComponent<Renderer> ().material.color = Color.blue;
				}
	}

}
