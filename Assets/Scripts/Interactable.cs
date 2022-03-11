using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 2f;
    public Transform interactionTransform;
	public DialogueTrigger dialogue;
    bool isFocused = false;
    bool hasInteracted = false;
    Transform drone;

    void Start() {
		dialogue =  GetComponent<DialogueTrigger>();
    }

    void Update()
    {
        if(isFocused){
            float distance = Vector3.Distance(drone.position, interactionTransform.position);
            
            if(!hasInteracted && distance >= radius){
                hasInteracted = true;
                dialogue.TriggerDialogue();
            } 
        } else {
            OnDefocused();
            hasInteracted = false;
            dialogue.EndDialogue();
        }
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
		hasInteracted = false;
		drone = null;
	}
}
