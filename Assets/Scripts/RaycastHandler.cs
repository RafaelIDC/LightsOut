using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHandler : MonoBehaviour {

    public List<GameObject> interactiveObjects;

    int nextObjectToInteractWith;

    Camera camera;

    Vector3 centerOfView;

    bool interacting = false;

    private void Start()
    {
        camera = GetComponent<Camera>();
        centerOfView = new Vector3(0.5F, 0.5F, 0);
        nextObjectToInteractWith = 0;
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            Interact();
        }
    }



     public void Interact()
    {
        if (interacting) return;

        interacting = true;

        RaycastHit hit;
        //Ray ray = camera.ScreenPointToRay(transform.position);
        Ray ray = camera.ViewportPointToRay(centerOfView);

        // if (Physics.Raycast(ray, out hit))
        if (Physics.Raycast(ray, out hit, 4f))
        {
            Debug.Log("Raycast Hit! " + hit.transform.name);

            bool hasNextInteractiveObject = (interactiveObjects != null && nextObjectToInteractWith >= 0 && nextObjectToInteractWith < interactiveObjects.Count);

            if (hasNextInteractiveObject && interactiveObjects[nextObjectToInteractWith] == hit.transform.gameObject) {
                nextObjectToInteractWith++;
                InteractiveObject currentInteractiveObject = hit.transform.GetComponent<InteractiveObject>();
                currentInteractiveObject.OnMainEvent();
            }
        }

        Invoke("TurnOfInteracting", 1);
    }



    void TurnOfInteracting()
    {
        interacting = false;
    }

}
