using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletHandler : MonoBehaviour {

    public GameObject toiletPaper;
    public string toiletMsg;
    public string lightsMsg;
    //public Transform dumpTransform;
    float lerpStep = 0.015f;
    float lerpIndex;
    public UnityStandardAssets.Characters.FirstPerson.CustomFirstPersonController firstPersonControllerf;
    public Transform playerTransform;
    public Transform playerHeadTransform;
    public Transform playerToiletTrnsfrm;
    public Transform playerHeadToiletTrnsfrm;
    Vector3 playerOldPos;
    Vector3 playerOldRot;
    Vector3 playerHeadOldRot;
    WaitForFixedUpdate waitFixedUpdate = new WaitForFixedUpdate();
    bool isTakingDump;

    public void ToiletEvent()
    {
        if (toiletPaper.activeSelf)
        {
            UIManager.instance.ShowNextMessege(toiletMsg);
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
        playerHeadOldRot = playerHeadTransform.eulerAngles;
        while (lerpIndex < 1)
        {
            playerTransform.position = Vector3.Lerp(playerOldPos, playerToiletTrnsfrm.position, lerpIndex);
            playerTransform.eulerAngles = Vector3.Lerp(playerOldRot, playerToiletTrnsfrm.eulerAngles, lerpIndex);
            playerHeadTransform.eulerAngles = Vector3.Lerp(playerHeadOldRot, playerHeadToiletTrnsfrm.eulerAngles, lerpIndex);
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
