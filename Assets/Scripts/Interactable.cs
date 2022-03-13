using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 2f;
    public Transform interactionTransform;
	public DialogueTrigger dialogue;
    bool isFocused;
    bool hasInteracted;
    Transform drone;

    public virtual void Interact() {
      if (interactionTransform != null) {

        if(isFocused){
            dialogue = interactionTransform.GetComponentInParent<DialogueTrigger>();
            float distance = Vector3.Distance(drone.position, interactionTransform.position);
            Debug.Log("hasInteracted: "+hasInteracted);
            if(!hasInteracted && distance <= radius){
                hasInteracted = true;
                Debug.Log("==============TriggerDialogue==================");
                dialogue?.TriggerDialogue();
            } else {
                 dialogue?.EndDialogue();
            }
        } else {
            OnDefocused();
        }
      }
    }

    void Update()
    {
        Interact();
    }

    // Called when the object starts being focused
	public void OnFocused (Transform droneTransform){
		isFocused = true;
		hasInteracted = false;
		drone = droneTransform;
    }

	// Called when the object is no longer focused
	public void OnDefocused (){
		isFocused = false;
		hasInteracted = true;
		drone = null;
	}
}
