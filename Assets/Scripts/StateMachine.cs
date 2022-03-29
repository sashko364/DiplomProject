using System.Collections;
using UnityEngine;

public abstract class StateMachineMB : MonoBehaviour
{
	public State CurrentState { get; private set; }
	public State _previousState;

	bool _inTransition = false;

	public void ChangeState(State newState)
	{
		// ensure we're ready for a new state
		if (CurrentState == newState || _inTransition)
			return;

		ChangeStateRoutine(newState);
	}

	public void RevertState()
	{
		if (_previousState != null)
			ChangeState(_previousState);
	}

	void ChangeStateRoutine(State newState)
	{
		_inTransition = true;
		// begin our exit sequence, to prepare for new state
		if (CurrentState != null)
			CurrentState.Exit();
		// save our current state, in case we want to return to it
		if (_previousState != null)
			_previousState = CurrentState;

		CurrentState = newState;

		// begin our new Enter sequence
		if (CurrentState != null)
			CurrentState.Enter();

		_inTransition = false;
	}

	// pass down Update ticks to States, since they won't have a MonoBehaviour
	public void Update()
	{
		// simulate update ticks in states
		if (CurrentState != null && !_inTransition)
			CurrentState.Tick();
	}

    public void FixedUpdate()
    {
		// simulate fixedUpdate ticks in states
		if (CurrentState != null && !_inTransition)
			CurrentState.FixedTick();
    }
}
