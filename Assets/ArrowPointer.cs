using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ArrowPointer : MonoBehaviour {

	Transform player; 
	public GameObject target; 
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform; 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (target.transform.parent.GetChild(0).gameObject.activeSelf == false) 
		{
			GetComponent<Image> ().color = Color.red; 
			return;
		}
		Vector3 dir = target.transform.position - player.position;
		float angle = Vector3.Angle (dir, player.transform.forward); 
		float whichWay = Vector3.Cross (player.transform.forward, dir).y; 

		if (whichWay > 0)
			angle = -angle; 
		
		transform.eulerAngles = new Vector3 (0, 0, angle); 
	}
}