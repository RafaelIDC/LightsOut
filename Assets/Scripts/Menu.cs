using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public Text GameOverText;
    public Text CreditsText;
    public GameObject StartButton;
    public GameObject CreditsButton;
    public GameObject BackButton;
    public GameObject Logo;
    public AudioSource titleMusic;


    private void Start()
    {
        titleMusic.Play();

        if (CrossSceneMenuHandler.CreditsOn)
        {
            Credits();
        }
        else
        {
            GoBack();
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void StartGame()
    {
        titleMusic.Stop();
        CrossSceneMenuHandler.CreditsOn = false;
        SceneManager.LoadScene("Main");
    }

    public void Credits()
    {
        Logo.SetActive(false);
        StartButton.SetActive(false);
        CreditsButton.SetActive(false);
        CreditsText.gameObject.SetActive(true);
        BackButton.SetActive(true);
    }

    public void GoBack()
    {
        BackButton.SetActive(false);
        CreditsText.gameObject.SetActive(false);
        Logo.SetActive(true);
        StartButton.SetActive(true);
        CreditsButton.SetActive(true);
        CrossSceneMenuHandler.CreditsOn = false;
    }
}
