using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossSceneMenuHandler : MonoBehaviour {


    public static CrossSceneMenuHandler instance;

    public bool creditsOn;



	void Awake () {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
	}
	
	
}
