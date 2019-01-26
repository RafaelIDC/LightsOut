using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour {


    public UnityEvent MainEvent;
    public UnityEvent DelayedEvent;
    public string[] nextMessege;
    public float eventDelay;
    int msgIndex = 0;
    string currentMsg;
    float nextMsgDelay;
    //[System.Serializable]
    //public struct MessegeItem
    //{
    //    public string[] nextMessege;
    //    public float[] nextDelay;
    //}

    //public MessegeItem[] myMessegeItems;



    public void OnMainEvent()
    {
        Debug.Log("Interact with object! " + gameObject.name);
        MainEvent.Invoke();

        if (UIManager.instance && nextMessege.Length > msgIndex && !String.IsNullOrEmpty(nextMessege[msgIndex]))
        {
            if (nextMessege[msgIndex].Contains("_"))
            {
                string timeVal = nextMessege[msgIndex].Substring(nextMessege[msgIndex].IndexOf('_') + 1);
                //Debug.Log(timeVal);
                 nextMsgDelay = int.Parse(timeVal);
                currentMsg = nextMessege[msgIndex].Substring(0, nextMessege[msgIndex].IndexOf('_'));
            }
            else
            {
                nextMsgDelay = 0;
                currentMsg = nextMessege[msgIndex];
            }
            //UIManager.instance.ShowNextMessege(currentMsg);
            if (!String.IsNullOrEmpty(currentMsg))
            {
                UIManager.instance.ShowNextMessege(currentMsg);
            }

        }

        msgIndex++;

        if(nextMsgDelay > 0)
        {
            currentMsg = nextMessege[msgIndex];
            UIManager.instance.ShowNextMessege(currentMsg, nextMsgDelay);
            nextMsgDelay = 0;

            msgIndex++;
        }

        if (eventDelay > 0)
        {
            Invoke("OnDelayedEvent", eventDelay);
        }
    }


    void OnDelayedEvent()
    {
        DelayedEvent.Invoke();
    }


}
