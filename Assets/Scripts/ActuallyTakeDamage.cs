using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActuallyTakeDamage : MonoBehaviour {


    DealDamage currentDealDamage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player hit " + collision.gameObject.tag);

        if (collision.gameObject.tag == "Obstacle")
        {
            currentDealDamage = collision.gameObject.GetComponent<DealDamage>();
            if (currentDealDamage)
            {
                Debug.Log("Player takes Damage: " + currentDealDamage.damage);
                GameManager.instance.TakeDamage(currentDealDamage.damage);
                currentDealDamage.FadeRed();
            }

        }
    }
}
