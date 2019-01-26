using System;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text title;
    public Text end;

    public enum MenuType
    {
        Start,
        End,
    }

    public void Init(MenuType type)
    {
        title.gameObject.SetActive(type == MenuType.Start);
        end.gameObject.SetActive(type == MenuType.End);
    }

    public void StartGame()
    {
        GameManager.instance.StartGame();
    }

    public void Credits()
    {
       
    }
}
