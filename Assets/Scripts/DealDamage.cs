using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour {

    public int damage = 5;

    public Renderer[] renderers;

    WaitForFixedUpdate waitFixedUpdate = new WaitForFixedUpdate();

    float fadeSpeed = 0.01f;
    float fadeVal;

    Color fadeColor;

    bool isFading;

    //Shader fadeShader;
   //Shader standardShader;

    //Material fadeMat;
    //Material defoultMat;



    void Start () {
        renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in renderers)
        {
            ToRegularMat(rend.material);
        }

        //fadeShader = Shader.Find("Legacy Shaders/Transparent/VertexLit");
        //standardShader = renderers[0].material.shader;


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
        if (GameManager.instance.lightsOn) return;

        if (isFading) return;

        isFading = true;
        StartCoroutine(FadeRedCo());

    }


    IEnumerator FadeRedCo()
    {

        ShowObj();
        foreach (Renderer rend in renderers)
        {
            ToFadeMat(rend.material);
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
            ToRegularMat(rend.material);
        }
        isFading = false;
    }


    void ToFadeMat(Material mat)
    {
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", Color.red);
    }

    void ToRegularMat(Material mat)
    {
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        mat.SetInt("_ZWrite", 1);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.DisableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = -1;
        mat.DisableKeyword("_EMISSION");
    }

}
