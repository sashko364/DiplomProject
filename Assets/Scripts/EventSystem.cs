using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public static EventSystem current;

    private void Awake() {
        if (current == null) {
            current = this;
        }
    }

    public event Action<int> onDialogueTrigerEnter;  

    public event Action<int> onDialogueTrigerExit;

    public event Action<int> onDialogueInteracted;


    public void DialogueTrigerEnter(int id) {
        if (onDialogueTrigerEnter != null) {
            onDialogueTrigerEnter(id);
        }
    }

    public void DialogueTrigerExit(int id) {
        if (onDialogueTrigerExit != null) {
            onDialogueTrigerExit(id);
        }
    }

      public void DialogueInteracted(int id) {
        if (onDialogueInteracted != null) {
            onDialogueInteracted(id);
        }
    }

}

