using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

	public void TriggerDialogue()
	{
        Debug.Log("Trigger dialogue");
		Debug.Log(dialogue.name);
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

	public void EndDialogue()
	{
        Debug.Log("Trigger enddialogue");
		Debug.Log(dialogue.name);
		FindObjectOfType<DialogueManager>().EndDialogue();
	}

}