using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

	public Image img;

	public Button goNext;

	public Animator animator;

	private Queue<string> sentences;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
		goNext.onClick.AddListener(DisplayNextSentence);
	}
	void Update() {
		 if(Input.GetKeyDown("n")){
			StartCoroutine(WaitFor());

            DisplayNextSentence();
    	}
	}

	void OnDestroy() {
		goNext.onClick.RemoveListener(DisplayNextSentence);	
	}

	public void StartDialogue (Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);

		if (dialogue.img != null) {
			goNext.gameObject.SetActive(false);
			
			goNext.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
			img.GetComponent<RectTransform>().sizeDelta = new Vector2(1500, 1200);
			img.sprite = dialogue.img.sprite;
		} else {
			goNext.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 500);
			img.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

		}

		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}
		
		DisplayNextSentence();

	}

	IEnumerator WaitFor() {
		yield return new WaitForSeconds(5);
	}

	public void DisplayNextSentence ()
	{
		Debug.Log("senteces:"+ sentences.Count);
		if (sentences.Count == 0)
		{
			StartCoroutine(WaitFor());
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		dialogueText.text = sentence;
	}

	public void EndDialogue()
	{
		sentences.Clear();
		StopAllCoroutines();
		dialogueText.text = "";
		nameText.text = "";
		animator.SetBool("IsOpen", false);
	}

}