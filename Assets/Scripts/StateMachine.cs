using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static State;

public class StateMachine : MonoBehaviour
{
    protected State _state;
    protected bool hasInteracted;
    protected Transform interactionTransform;
    // Start is called before the first frame update
    void Start()
    {
        _state = State.NotInteracting;
        hasInteracted = false;
        interactionTransform = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
