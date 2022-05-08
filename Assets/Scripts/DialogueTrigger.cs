
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
	
	[SerializeField]
	private Dialogue dialogue;

	public void TriggerDialogue()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

	public void EndDialogue()
	{
		FindObjectOfType<DialogueManager>().EndDialogue();
	}

}