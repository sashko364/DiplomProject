using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {

	public Text nameText;

	public Animator animator;

	private Queue<string> sentences;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
		animator.SetBool("IsOpen",  false);


	}

	public void StartDialogue (Dialogue dialogue)
	{

		nameText.name = dialogue.name;
		
		animator.SetBool("IsOpen",  true);
		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
			nameText.text = sentence;

		}

		 DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		nameText.text = sentence;
		yield return null;
		// foreach (char letter in sentence.ToCharArray())
		// {
		// 	nameText.text += letter;
		// 	yield return null;
		// }
	}

	public void EndDialogue()
	{
		nameText.text = "";
		sentences.Clear();
		animator.SetBool("IsOpen", false);
	}

}