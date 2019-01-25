using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int health = 100;
    static public GameManager instance;

    public Light[] lights;

    public Color globalLightColor;

    public Transform obstaclesParent;

    public DealDamage[] obstacles;
    //public List<DealDamage> obstacles = new List<DealDamage>();

    public RaycastHandler raycastHandler;

    public bool lightsOn = true;

    //public Material fadeMat;




    void Awake()
    {
        instance = this;

        lights = FindObjectsOfType<Light>();
        obstacles = FindObjectsOfType<DealDamage>();
    }




    void Start()
    {
        //foreach (Transform obj in obstaclesParent)
        //{
        //    if(obj.GetComponent<DealDamage>())
        //    {
        //        obstacles.Add(obj.GetComponent<DealDamage>());
        //    }
        //}
    }



    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            raycastHandler.Interact();
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            lightsOn = !lightsOn;
            if (lightsOn)
            {
                TurnLightsOn();
            }
            else
            {
                TurnLightsDown();
            }

        }
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

        foreach(DealDamage obs in obstacles)
        {
            obs.HideObj();
        }

        lightsOn = false;
    }


    public void TurnLightsOn()
    {
        foreach (Light mlight in lights)
        {
            mlight.enabled = true;
        }
        RenderSettings.ambientLight = globalLightColor;

        foreach (DealDamage obs in obstacles)
        {
            obs.ShowObj();
        }

        lightsOn = true;
    }
}
