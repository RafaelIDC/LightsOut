using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {


    public static UIManager instance;

    public Text msgTxt;

    WaitForSeconds waitForMessege = new WaitForSeconds(2);



    private void Awake()
    {
        instance = this;
    }



    private void Start()
    {
        ShowNextMessege("Welcome home! hang your hat and feel cozy (Press E to use)");
    }


    public void ShowNextMessege(string msg, float timeToWait = 0) {
        StartCoroutine(ShowNextMessegeCo(msg, timeToWait));
	}
	


    IEnumerator ShowNextMessegeCo(string msg, float moreTimeToWait = 0)
    {

        HideMessege();

        if (moreTimeToWait > 0)
        {
            yield return new WaitForSeconds(moreTimeToWait);
        }

        yield return waitForMessege;

        ShowMessege(msg);
    }


    void ShowMessege(string msg)
    {
        if (String.IsNullOrEmpty(msg)) return;

        msgTxt.transform.parent.gameObject.SetActive(true);
        msgTxt.text = msg;
        //Invoke("HideMessege", 4);
    }



    void HideMessege()
    {
        msgTxt.transform.parent.gameObject.SetActive(false);
        msgTxt.text = "";
    }
}
