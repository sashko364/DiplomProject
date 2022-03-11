using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class FocusHandler : MonoBehaviour
{
    public delegate void OnFocusChanged(Interactable newFocus);
	public OnFocusChanged onFocusChangedCallback;

	public Interactable focus;	// Our current focus: Item, Enemy etc.

	//public LayerMask movementMask;		// The ground
	public LayerMask interactionMask;	// Everything we can interact with

	DroneMovement1 movement;		// Reference to our motor
	Camera cam;

    void Start()
    {
        movement = GetComponent<DroneMovement1>();
	
        cam = Camera.main;
    }

    void Update()
    {
        /*Debug.Log("Cam");

        Debug.Log(cam);
        Debug.Log("EventSystem");
        Debug.Log(EventSystem.current);*/

        if (EventSystem.current.IsPointerOverGameObject())
			return;
        
        GetFocus();
        GoToFocus();
    }

    void SetFocus(Interactable newFocus){
		if (onFocusChangedCallback != null)
			onFocusChangedCallback.Invoke(newFocus);

		// If our focus has changed
		if (focus != newFocus && focus != null)
		{
			// Let our previous focus know that it's no longer being focused
			focus.OnDefocused();
		}

		// Set our focus to what we hit
		// If it's not an interactable, simply set it to null
		focus = newFocus;

		if (focus != null)
		{
			// Let our focus know that it's being focused
			focus.OnFocused(transform);
		}

	}

    void GetFocus(){
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
		if (Physics.Raycast(ray, out hit, interactionMask)){
			//movement.move(hit.point);
            SetFocus(null);
		}
    }

    void GoToFocus(){
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 100f, interactionMask)){
            SetFocus(hit.collider.GetComponent<Interactable>());
		}
    }
}
