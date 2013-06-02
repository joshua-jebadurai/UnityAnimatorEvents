using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
[RequireComponent (typeof (Animator))]
public class AnimatorEvents : MonoBehaviour {
	
	[HideInInspector]
	public Animator animator;
	
	public AnimatorEventLayer[] layers;
	
	#region Events and Delegates
	public delegate void StateChangeHandler (int layer, AnimatorStateInfo previous, AnimatorStateInfo current);
	public event StateChangeHandler OnStateChanged;
	
	public delegate void TransitionHandler (int layer, AnimatorTransitionInfo transitionInfo);
	public event TransitionHandler OnTransition;
	#endregion
	
	
	void Start () {
		foreach (AnimatorEventLayer animatorLayer in layers)
			animatorLayer.MakeDictionaries();
	}
	
	void FixedUpdate () {
		for ( int layer = 0; layer < layers.Length; layer++) {
			if (layers[layer].isListening) {
				// State Change Verification
				layers[layer].currentState = animator.GetCurrentAnimatorStateInfo(layer);
				
				if (layers[layer].previousState.nameHash != layers[layer].currentState.nameHash) {
					if (OnStateChanged != null)
						OnStateChanged (layer, layers[layer].previousState, layers[layer].currentState);
					layers[layer].previousState = layers[layer].currentState;
				}
				
				// Transition Change Verification
				if (animator.IsInTransition(layer)) {
					if (OnTransition != null)
						OnTransition(layer, animator.GetAnimatorTransitionInfo(layer));
				}
			}
		}
	}
	
#if UNITY_EDITOR
	public bool CheckRedudancy() {
		AnimatorEvents exisitingAnimatorEvents = GetComponent<AnimatorEvents>();
		
		if (exisitingAnimatorEvents != this && exisitingAnimatorEvents != null) {
			Debug.LogError("There can be only one AnimatorEvents per Animator");
			DestroyImmediate(this);
			return true;
		}
		return false;
	}
#endif
}
