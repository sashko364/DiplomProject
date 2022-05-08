using UnityEngine;

public class Interactable : MonoBehaviour {
    [SerializeField]
    private Animator animator;
	[SerializeField]
    private DialogueTrigger dialogue;
    [SerializeField]
    public int Id;

	bool hasInteracted = false;

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
            hasInteracted = true;
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
