using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int health = 100;
    static public GameManager instance;

    public Light[] lights;




    void Awake()
    {
        instance = this;

        lights = FindObjectsOfType<Light>();

        
    }



	
	void Start () {
        	
	}
	


    public void TakeDamage(int amount)
    {
        this.health -= amount;
        Debug.Log(health);

        if (health <= 0)
        {
            LoseGame();
        }
    }




    public void LoseGame()
    {
        //???
    }


    public void StartGame()
    {

    }


    public void TurnLightsDown()
    {
        foreach(Light mlight in lights)
        {
            mlight.enabled = false;
        }
        RenderSettings.ambientLight = Color.black;
    }
}
