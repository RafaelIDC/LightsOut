using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour {

    public int damage = 5;

    public Renderer[] renderers;

    WaitForFixedUpdate waitFixedUpdate = new WaitForFixedUpdate();

    float fadeSpeed = 0.001f;
    float fadeVal;

    Color fadeColor;



	void Start () {
        renderers = GetComponentsInChildren<Renderer>();

        foreach(Renderer rend in renderers)
        {
            rend.material.SetFloat("_Mode", 3f);
        }
    }




    public void HideObj()
    {
        foreach(Renderer rend in renderers)
        {
            rend.enabled = false;
        }
    }



    public void ShowObj()
    {
        foreach (Renderer rend in renderers)
        {
            rend.enabled = true;
        }
    }


    public void FadeRed()
    {
        StartCoroutine(FadeRedCo());
    }


    IEnumerator FadeRedCo()
    {
        ShowObj();
        foreach (Renderer rend in renderers)
        {
            rend.material.color = Color.red;
        }
        fadeColor = Color.red;
        fadeVal = 1;
        while (fadeVal > 0)
        {
            foreach (Renderer rend in renderers)
            {
                rend.material.color = fadeColor;
            }
            fadeVal -= fadeSpeed;
            fadeColor.a = fadeVal;
            yield return waitFixedUpdate;
        }

        HideObj();
        foreach (Renderer rend in renderers)
        {
            rend.material.color = Color.white;
        }
    }
}
