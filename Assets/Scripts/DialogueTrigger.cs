using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

	public void TriggerDialogue()
	{
		Debug.Log("Dialog: "+ dialogue?.name);
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

	public void EndDialogue()
	{
		FindObjectOfType<DialogueManager>().EndDialogue();
	}

}