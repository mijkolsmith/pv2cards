using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
	protected State state;
	public void SetState(State _state)
	{
		if (state != null)
		{ StartCoroutine(state.Exit()); }
		state = _state;
		StartCoroutine(state.Start());
	}

	public State GetState()
    {
		return state;
    }

	public void Update()
	{
		StartCoroutine(state.Update());
	}
}
