﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    
    static public GameManager instance = null;
    public int health = 100;
    Light[] lights;
    //public Light[] lightninglights;
    List<Light> lightningLights = new List<Light>();
    List<Light> homeLights = new List<Light>();

    Color globalLightColor;
    public Color lightningColor;

    public Material redMat;

    public DealDamage[] obstacles;
    //public List<DealDamage> obstacles = new List<DealDamage>();

    public RaycastHandler raycastHandler;

    public bool lightsOn = true;
    bool takingDamage;

    IEnumerator lightningCo;

    public int lightningMin = 5;
    public int lightningMax = 15;

    public Text healthTxt;

    public Transform playerTransform;

    public GameObject floor1;
    public GameObject floor2;

    public bool isFinishGame;

    public AudioSource damageSound;
    public AudioSource lightningSound;
    public AudioSource lightSwitchSound;
    public AudioSource microWaveSound;
    public AudioSource tvSound;
    public AudioSource teddySound;
    public AudioSource powerdownSound;
    public AudioSource lightMusic;
    public AudioSource darkMusic;


    public bool isTestMode;
    public GameObject testModeIndication;




    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);


        floor1.SetActive(true);
        floor2.SetActive(true);

        lights = FindObjectsOfType<Light>();

        obstacles = FindObjectsOfType<DealDamage>();

        globalLightColor = RenderSettings.ambientLight;
    }




    void Start()
    {
       foreach(Light mlight in lights)
        {
            if(mlight.type == LightType.Directional)
            {
                lightningLights.Add(mlight);
            }
            else
            {
                homeLights.Add(mlight);
            }
        }

        floor2.SetActive(false);

        lightMusic.Play();

        if (isTestMode)
        {
            testModeIndication.SetActive(true);
        }
        else
        {
            testModeIndication.SetActive(false);
        }
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

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            isTestMode = !isTestMode;
            if (isTestMode)
            {
                testModeIndication.SetActive(true);
            }
            else
            {
                testModeIndication.SetActive(false);
            }
        }
    }




    public void TakeDamage(int amount)
    {
        if (takingDamage) return;

        takingDamage = true;

        if (health > 0)
        {
            Debug.Log("Player takes Damage: " + amount);
            this.health -= amount;
            damageSound.Play();
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

            //if(amount > 0)
            //{
            //    damageSound.Play();
            //}

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
        if (isTestMode) return;

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

        powerdownSound.Play();

        lightMusic.Stop();
        darkMusic.Play();
    }



    public void TurnLightsOn()
    {
        lightsOn = true;

        if (lightningsOn)
        {
            if (lightningCo == null)
            {
                lightningCo = WaitForLightning();
            }
           
            StopCoroutine(lightningCo);

            foreach (DealDamage obs in obstacles)
            {
                obs.HideObj();
            }
            foreach (Light mlight in lightningLights)
            {
                mlight.enabled = false;
            }
            RenderSettings.ambientLight = Color.black;

            lightningsOn = false;
        }

        LightsOn();
        
        lightSwitchSound.Play();

        lightMusic.Play();
        darkMusic.Stop();
    }


    bool lightningsOn;
    float randomTime;

    IEnumerator WaitForLightning()
    {
        if (lightsOn) yield break;

        randomTime = Random.Range((float)lightningMin, (float)lightningMax);
        yield return new WaitForSeconds(randomTime);

        if (lightsOn) yield break;

        lightningSound.Play();
        lightningsOn = true;

        foreach (DealDamage obs in obstacles)
        {
            obs.ShowObj();
        }
        foreach (Light mlight in lightningLights)
        {
            mlight.enabled = true;
        }
        RenderSettings.ambientLight = lightningColor;

        randomTime = Random.Range(0.05f, 0.5f);
        yield return new WaitForSeconds(randomTime);


        foreach (DealDamage obs in obstacles)
        {
            obs.HideObj();
        }
        foreach (Light mlight in lightningLights)
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
        foreach (Light mlight in lightningLights)
        {
            mlight.enabled = true;
        }
        RenderSettings.ambientLight = lightningColor;


        randomTime = Random.Range(0.05f, 0.5f);
        yield return new WaitForSeconds(randomTime);


        foreach (DealDamage obs in obstacles)
        {
            obs.HideObj();
        }
        foreach (Light mlight in lightningLights)
        {
            mlight.enabled = false;
        }
        RenderSettings.ambientLight = Color.black;

        lightningsOn = false;

        lightningCo = WaitForLightning();
        StartCoroutine(lightningCo);
    }



    void LightsOn()
    {
        foreach (Light mlight in homeLights)
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
        foreach (Light mlight in homeLights)
        {
            mlight.enabled = false;
        }
        RenderSettings.ambientLight = Color.black;

        foreach (DealDamage obs in obstacles)
        {
            obs.HideObj();
        }
    }



    public void PreperToFinishGame()
    {
        isFinishGame = true;
    }


    public void OnFinishGame()
    {
        Debug.Log("Game Finished!");
        CrossSceneMenuHandler.CreditsOn = true;
        SceneManager.LoadScene(0);
    }

}
