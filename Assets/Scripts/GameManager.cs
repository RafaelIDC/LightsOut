using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int health = 100;
    static public GameManager instance;

    public Light[] lights;

    Color globalLightColor;

    public Transform obstaclesParent;

    public DealDamage[] obstacles;
    //public List<DealDamage> obstacles = new List<DealDamage>();

    public RaycastHandler raycastHandler;

    public bool lightsOn = true;

    IEnumerator lightningCo;

    public int lightningMin = 5;
    public int lightningMax = 15;




    void Awake()
    {
        instance = this;

        lights = FindObjectsOfType<Light>();
        obstacles = FindObjectsOfType<DealDamage>();

        globalLightColor = RenderSettings.ambientLight;
    }




    void Start()
    {
       
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


    public void TurnLightsDown(bool isLightning = true)
    {
        LightsOff();

        lightsOn = false;

        if (isLightning)
        {
            if (lightningCo != null)
            {
                StopCoroutine(lightningCo);
            }
            lightningCo = WaitForLightning();
            StartCoroutine(lightningCo);
        }

    }



    public void TurnLightsOn()
    {
        LightsOn();

        lightsOn = true;
    }


    float randomTime;

    IEnumerator WaitForLightning()
    {
        if (lightsOn) yield break;

        randomTime = Random.Range((float)lightningMin, (float)lightningMax);
        yield return new WaitForSeconds(randomTime);

        if (lightsOn) yield break;


        LightsOn();


        randomTime = Random.Range(0.05f, 0.5f);
        yield return new WaitForSeconds(randomTime);

        LightsOff();


        randomTime = Random.Range(0.05f, 0.5f);
        yield return new WaitForSeconds(randomTime);

        LightsOn();


        randomTime = Random.Range(0.05f, 0.5f);
        yield return new WaitForSeconds(randomTime);

        LightsOff();


        lightningCo = WaitForLightning();
        StartCoroutine(lightningCo);
    }



    void LightsOn()
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
    }


    void LightsOff()
    {
        foreach (Light mlight in lights)
        {
            mlight.enabled = false;
        }
        RenderSettings.ambientLight = Color.black;

        foreach (DealDamage obs in obstacles)
        {
            obs.HideObj();
        }
    }
}
