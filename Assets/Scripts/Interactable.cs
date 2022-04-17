using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {
	public Transform interactionTransform;
    public Animator animator;
	public DialogueTrigger dialogue;

    public int Id;

	bool hasInteracted = false;
    int count = 0;

	void Start()
	{   
		dialogue =  GetComponent<DialogueTrigger>();

        animator.SetBool("ShowIcon", false);
		hasInteracted = false;
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
            count++;
            hasInteracted = true;
            Debug.Log("Interacting... "+ dialogue.name);
            animator.SetBool("ShowIcon", false);
            dialogue.TriggerDialogue();
        }
    }

	public void onDialogueTrigerEnter (int id)
	{
        if (this.Id == id) {
            animator.SetBool("ShowIcon", true);
		    hasInteracted = false;
        }

	}

	public void onDialogueTrigerExit (int id)
	{
        if (this.Id == id) {
            animator.SetBool("ShowIcon", false);    
            dialogue.EndDialogue();
            hasInteracted = false;
        }
	}

}
