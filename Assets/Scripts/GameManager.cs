using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int health = 100;
    static public GameManager instance;

    Light[] lights;
    public Light[] lightninglights;

    Color globalLightColor;
    public Color lightningColor;

    //public Transform floor1ObstaclesParent;
    //public Transform floor2ObstaclesParent;
    //public Transform floor3ObstaclesParent;

    public DealDamage[] obstacles;
    //public List<DealDamage> obstacles = new List<DealDamage>();

    public RaycastHandler raycastHandler;

    public bool lightsOn = true;
    bool takingDamage;

    IEnumerator lightningCo;

    public int lightningMin = 5;
    public int lightningMax = 15;

    public Text healthTxt;




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
        if (takingDamage) return;

        takingDamage = true;

        if (health > 0)
        {
            this.health -= amount;
            if (health <= 0)
            {
                health = 0;
                LoseGame();
            }
            else
            {
                Invoke("WaitAfterDamage", 1);
            }
            if (healthTxt)
            {
                healthTxt.text = health.ToString();
            }

        }
        else
        {
            health = 0;
            LoseGame();
        }

    }


    void WaitAfterDamage()
    {
        takingDamage = false;
    }




    public void LoseGame()
    {
        Invoke("StartGame", 1);
    }


    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



    public void WaitAndLightsDown(int waitTime = 0)
    {
        Debug.Log("WaitAndLightsDow , waitTime= " + waitTime);
        Invoke("TurnLightsDown", (float)waitTime);
    }

    public void TurnLightsDown()
    {
        Debug.Log("TurnLightsDown()");

        LightsOff();

        lightsOn = false;

        if (lightningCo != null)
        {
            StopCoroutine(lightningCo);
        }
        lightningCo = WaitForLightning();
        StartCoroutine(lightningCo);

        //if (isLightning)
        //{

        //}

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


        foreach (DealDamage obs in obstacles)
        {
            obs.ShowObj();
        }
        foreach (Light mlight in lightninglights)
        {
            mlight.enabled = true;
        }
        RenderSettings.ambientLight = globalLightColor;


        randomTime = Random.Range(0.05f, 0.5f);
        yield return new WaitForSeconds(randomTime);


        foreach (DealDamage obs in obstacles)
        {
            obs.HideObj();
        }
        foreach (Light mlight in lightninglights)
        {
            mlight.enabled = false;
        }
        RenderSettings.ambientLight = Color.black;


        randomTime = Random.Range(0.05f, 0.5f);
        yield return new WaitForSeconds(randomTime);


        foreach (DealDamage obs in obstacles)
        {
            obs.ShowObj();
        }
        foreach (Light mlight in lightninglights)
        {
            mlight.enabled = true;
        }
        RenderSettings.ambientLight = globalLightColor;


        randomTime = Random.Range(0.05f, 0.5f);
        yield return new WaitForSeconds(randomTime);


        foreach (DealDamage obs in obstacles)
        {
            obs.HideObj();
        }
        foreach (Light mlight in lightninglights)
        {
            mlight.enabled = false;
        }
        RenderSettings.ambientLight = Color.black;


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
