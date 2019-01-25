using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBox : MonoBehaviour {


    Renderer renderer;
    public GameObject floor;


	void Start () {
        renderer = GetComponent<Renderer>();
	}
	
	
	void Update () {
		
	}



    private void OnCollisionEnter(Collision collision)
    {
        renderer.material.color = Color.blue;
    }



}
