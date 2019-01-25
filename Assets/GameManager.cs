using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int health;
    static public GameManager instance;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
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


	// Update is called once per frame
	void Update () {
        
	}

    void LoseGame()
    {
        //???
    }
}
