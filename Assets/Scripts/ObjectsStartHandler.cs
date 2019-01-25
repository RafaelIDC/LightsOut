using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsStartHandler : MonoBehaviour {




    public GameObject[] objectsToShow;



     
	void Start () {
		foreach(GameObject obj in objectsToShow)
        {
            obj.SetActive(true);
        }
    }
	
	



}
