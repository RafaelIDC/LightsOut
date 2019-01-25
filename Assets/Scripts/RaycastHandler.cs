using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHandler : MonoBehaviour {

    Camera camera;

    InteractiveObject currentInteractiveObject;

    Vector3 centerOfView;

    private void Start()
    {
        camera = GetComponent<Camera>();
        centerOfView = new Vector3(0.5F, 0.5F, 0);
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            Interact();
        }
    }



    void Interact()
    {
        RaycastHit hit;
        //Ray ray = camera.ScreenPointToRay(transform.position);
        Ray ray = camera.ViewportPointToRay(centerOfView);

        // if (Physics.Raycast(ray, out hit))
        if (Physics.Raycast(ray, out hit, 4f))
        {
            Debug.Log("Raycast Hit! " + hit.transform.name);
            currentInteractiveObject = hit.transform.GetComponent<InteractiveObject>();

            if(currentInteractiveObject)
                currentInteractiveObject.OnMainEvent();
        }
    }


}
