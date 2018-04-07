using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatBehaviour : CoverBehaviour {

    private bool activate;
    public Transform platform;
    public Transform pos1;
    public Transform pos2;
    Vector3 directionOfTravel;
    Quaternion rotationOfTravel; 
    float smooth = 0.5f; 
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        highlight = Shader.Find("Self-Illumin/Outlined Diffuse");
        activate = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (activate == true)
        {
            directionOfTravel = pos2.position;
            rotationOfTravel = pos2.rotation; 
        }
        else
        {
            directionOfTravel = pos1.position;
            rotationOfTravel = pos1.rotation; 
        }
	}

	public void Activate(bool active)
	{
		activate = active; 
	}

    void FixedUpdate()
    {
        platform.position = Vector3.Lerp(platform.position, directionOfTravel, smooth * Time.deltaTime);
        platform.rotation = Quaternion.Lerp(platform.rotation, rotationOfTravel, smooth * Time.deltaTime); 
    }
}
