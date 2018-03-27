using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverBehaviour : MonoBehaviour {

    public Shader highlight;
    public Renderer rend;
    private bool takeCover = false;
    private bool occupied = false; 
    public float interactDirection = 0.0f; 

    public bool GetTakeCover() { return takeCover; }
    public bool GetOccupied() { return occupied; }
    public void SetTakeCover(bool tc) { takeCover = tc; }
    public void SetOccupied(bool oc) { occupied = oc; }


    void Start ()
    {
        //Initialize shaders
        rend = GetComponent<Renderer>(); 
        highlight = Shader.Find("Self-Illumin/Outlined Diffuse");
    }
	
	void Update ()
    {
        if (takeCover == false)
            rend.material.color = Color.yellow;
        else
            rend.material.color = Color.red;
    }

    public void DeselectCover()
    {

    }
    void OnMouseOver()
    {
        //Change shader to highlight
        rend.material.shader = highlight; 
    }
    void OnMouseExit()
    {
        //Change shader back to normal
        rend.material.shader = Shader.Find("Diffuse");  
    }
    void OnMouseDown()
    {
        takeCover = !takeCover; 
    }
}
