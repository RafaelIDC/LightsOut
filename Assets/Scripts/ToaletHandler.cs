using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToaletHandler : MonoBehaviour {

    public GameObject toaletPaper;
    public string toaletMsg;
    public string lightsMsg;
    //public Transform dumpTransform;
    float lerpStep = 0.005f;
    float lerpIndex;
    public UnityStandardAssets.Characters.FirstPerson.CustomFirstPersonController firstPersonControllerf;
    public Transform playerTransform;
    public Transform playerToaletPosition;
    Vector3 playerOldPos;
    Vector3 playerOldRot;
    WaitForFixedUpdate waitFixedUpdate = new WaitForFixedUpdate();
    bool isTakingDump;

    public void ToaletEvent()
    {
        if (toaletPaper.activeSelf)
        {
            UIManager.instance.ShowNextMessege(toaletMsg);
        }
        else
        {
            TakeADump();
        }
    }


    void TakeADump()
    {
        if (isTakingDump) return;
        isTakingDump = true;
        StartCoroutine(TakeDumpCo());
    }

    IEnumerator TakeDumpCo()
    {
        UIManager.instance.ShowNextMessege("");
        lerpIndex = 0;
        firstPersonControllerf.isLocked = true;
        playerOldPos = playerTransform.position;
        playerOldRot = playerTransform.eulerAngles;
        while (lerpIndex < 1)
        {
            playerTransform.position = Vector3.Lerp(playerOldPos, playerToaletPosition.position, lerpIndex);
            playerTransform.eulerAngles = Vector3.Lerp(playerOldRot, playerToaletPosition.eulerAngles, lerpIndex);
            lerpIndex += lerpStep;
            yield return waitFixedUpdate;
        }
        yield return new WaitForSeconds(3);

        firstPersonControllerf.isLocked = false;

        GameManager.instance.TurnLightsDown();
        UIManager.instance.ShowNextMessege(lightsMsg);
        isTakingDump = false;
    }
}
