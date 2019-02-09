using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CrossSceneMenuHandler {

    private static bool creditsOn;

    public static bool CreditsOn {
        get {
            return creditsOn;
        }
        set {
            creditsOn = value;

            if (creditsOn) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
