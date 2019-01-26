using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportHandler : MonoBehaviour {


    //public Transform playerTransform;
    public Transform nextStartingPoint;
    //public Transform floor2StartingPoint;
    public GameObject nextFloor;
    public GameObject previusFloor;



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            nextFloor.SetActive(true);
            GameManager.instance.playerTransform.position = nextStartingPoint.position;
            GameManager.instance.playerTransform.rotation = nextStartingPoint.rotation;
            previusFloor.SetActive(false);
        }

    }
}
