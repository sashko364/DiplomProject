using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*	
	This component is for all objects that the player can
	interact with such as enemies, items etc. It is meant
	to be used as a base class.
*/

public class Interactable : MonoBehaviour {
		// How close do we need to be to interact?
	public Transform interactionTransform;	// The transform from where we interact in case you want to offset it
    public Animator animator;// Is this interactable currently being focused?
	public DialogueTrigger dialogue;

    public int Id;

    public Text hint;
    	// Reference to the player transform
	bool hasInteracted = false;	// Have we already interacted with the object?

	void Start()
	{   
		dialogue =  GetComponent<DialogueTrigger>();

		// This method is meant to be overwritten
		Debug.Log("Interacting with " + transform.name);
        animator.SetBool("ShowIcon", false);
		hasInteracted = false;
        hint.text = "";
        EventSystem.current.onDialogueTrigerEnter += onDialogueTrigerEnter;
        EventSystem.current.onDialogueInteracted += onDialogueInteracted;
        EventSystem.current.onDialogueTrigerExit += onDialogueTrigerExit;
	}

    void OnDestroy() {
        EventSystem.current.onDialogueTrigerEnter -= onDialogueTrigerEnter;
        EventSystem.current.onDialogueInteracted -= onDialogueInteracted;
        EventSystem.current.onDialogueTrigerExit -= onDialogueTrigerExit;
    }

    public void onDialogueInteracted (int id) {
        if (!hasInteracted && this.Id == id) {
             hasInteracted = true;   
            animator.SetBool("ShowIcon", false);
            dialogue.TriggerDialogue();
        }
    }
	// Called when the object starts being focused
	public void onDialogueTrigerEnter (int id)
	{
        if (this.Id == id) {
            animator.SetBool("ShowIcon", true);
            hint.text = "Press E to interact";
		    hasInteracted = false;
        }

	}

	// Called when the object is no longer focused
	public void onDialogueTrigerExit (int id)
	{
        if (this.Id ==id) {
            animator.SetBool("ShowIcon", false);    
            dialogue.EndDialogue();
            hint.text = "";
            hasInteracted = false;
        }
	}

}
