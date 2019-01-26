using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour {


    public UnityEvent MainEvent;
    public string[] nextMessege;
    public int textDelay;
    int msgIndex = 0;
	

   


    public void OnMainEvent()
    {
        Debug.Log("Interact with object! " + gameObject.name);
        MainEvent.Invoke();

        if (UIManager.instance && nextMessege.Length > msgIndex && !String.IsNullOrEmpty(nextMessege[msgIndex]))
        {
            UIManager.instance.ShowNextMessege(nextMessege[msgIndex], textDelay);
        }

        msgIndex++;
    }


}
