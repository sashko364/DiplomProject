using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {
	public Transform interactionTransform;
    public Animator animator;
	public DialogueTrigger dialogue;

    public int Id;

    public Text hint;
	bool hasInteracted = false;

	void Start()
	{   
		dialogue =  GetComponent<DialogueTrigger>();

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

	public void onDialogueTrigerEnter (int id)
	{
        if (this.Id == id) {
            animator.SetBool("ShowIcon", true);
            hint.text = "Press E to interact";
		    hasInteracted = false;
        }

	}

	public void onDialogueTrigerExit (int id)
	{
        if (this.Id == id) {
            animator.SetBool("ShowIcon", false);    
            dialogue.EndDialogue();
            hint.text = "";
            hasInteracted = false;
        }
	}

}
