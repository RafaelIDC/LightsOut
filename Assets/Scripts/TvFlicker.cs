using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvFlicker : MonoBehaviour {

    Renderer tRenderer;
    Vector2 textureOffset;
    WaitForSeconds waitFlickerTime = new WaitForSeconds(0.05f);
    bool isOn;
    IEnumerator tvFlickerCo;



	void Awake () {
        tRenderer = GetComponent<Renderer>();
	}


    private void OnEnable()
    {
        isOn = true;
        if (tvFlickerCo == null)
        {
            tvFlickerCo = TvFlickerCo();
        }
        StartCoroutine(tvFlickerCo);
    }


    private void OnDisable()
    {
        isOn = false;
        if (tvFlickerCo != null)
        {
            StopCoroutine(tvFlickerCo);
        }
    }





    IEnumerator TvFlickerCo()
    {
        while (isOn)
        {
            textureOffset.x = Random.Range(0f, 1f);
            textureOffset.y = Random.Range(0f, 1f);
            tRenderer.material.mainTextureOffset = textureOffset;
            Debug.Log("textureOffset= " + textureOffset);
            yield return waitFlickerTime;
        }
    }



}
