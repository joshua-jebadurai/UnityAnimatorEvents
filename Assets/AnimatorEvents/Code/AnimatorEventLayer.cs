using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AnimatorEventLayer {
	[SerializeField]
	public bool isListening = true;
	
	[SerializeField]
	public int[] _stateKeys;
	[SerializeField]
	public string[] _stateNames;
	
	[SerializeField]
	public int[] _transitionKeys;
	[SerializeField]
	public string[] _transitionNames;
	
	[HideInInspector]
	public AnimatorStateInfo previousState, currentState;
	
	#if UNITY_EDITOR
	[HideInInspector]
	public bool foldLayer = true, foldStates = true, foldTransitions = true;
	
	public AnimatorEventLayer ( int[] stateKeys, string[] stateNames, int[] transitionKeys, string[] transitionNames ) {
		this._stateKeys = stateKeys;
		this._stateNames = stateNames;
		
		this._transitionKeys = transitionKeys;
		this._transitionNames = transitionNames;
	}
	#endif
	
	Dictionary <int, string> stateNames;
	Dictionary <int, string> transitionNames;
	
	public void MakeDictionaries () {
		stateNames = new Dictionary<int, string>();
		for (int i = 0; i < _stateKeys.Length; i++)
			stateNames.Add( _stateKeys[i], _stateNames[i]);
		
		
		transitionNames = new Dictionary<int, string>();
		for (int i = 0; i < _transitionKeys.Length; i++)
			transitionNames.Add (_transitionKeys[i], _transitionNames[i]);
		
		#if !UNITY_EDITOR
		_stateKeys = _transitionKeys = null;
		_transitionNames = _stateNames = null;
		#endif
	}
	
	/// <summary>
	/// Gets the name of the state using name hash from AnimatorStateInfo.
	/// </summary>
	/// <returns>
	/// The state name.
	/// </returns>
	/// <param name='nameHash'>
	/// Name Hash.
	/// </param>
	public string GetStateName (int nameHash) {
		return stateNames[nameHash];	
	}
	
	/// <summary>
	/// Gets the name of the transition using name hash from AnimatorTransitionInfo.
	/// </summary>
	/// <returns>
	/// The transition name.
	/// </returns>
	/// <param name='nameHash'>
	/// Name hash.
	/// </param>
	public string GetTransitionName (int nameHash) {
		return transitionNames[nameHash];	
	}
}
