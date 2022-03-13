using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {

	public Text nameText;

	public Animator animator;

	private Queue<string> sentences;

	private bool waiting;

	float waitTime = 2f;
	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
		animator.SetBool("IsOpen",  false);
	}

	public void StartDialogue (Dialogue dialogue)
	{
		nameText.name = dialogue.name;
		Debug.Log("=============Start Dialog===================");
		animator.SetBool("IsOpen",  true);
		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplaySentences();
	}

	public void DisplaySentences ()
	{
 		if (sentences.Count == 0)
 		{
 			EndDialogue();
 			return;
 		}

		while(sentences.Count > 0) {
			string sentence = sentences.Dequeue();
			Debug.Log("Dequueing"+ sentence);
			Debug.Log("Quee"+ sentences);

			nameText.text += sentence + "\n";
			//  if(!waiting){
 			// 	StartCoroutine(WaitForSeconds(waitTime, sentence));
			//  }
		}


	}

	IEnumerator WaitForSeconds(float waitTime, string sentence){
		waiting = true;
		yield return new WaitForSeconds(waitTime);
		//dieing animation here
		waiting = !waiting;
	}

	public void EndDialogue()
	{
		nameText.text = "";
		sentences.Clear();
		StopAllCoroutines();
		animator.SetBool("IsOpen", false);
	}

}