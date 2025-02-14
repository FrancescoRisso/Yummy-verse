using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovementSequence : MonoBehaviour {
	private Dictionary<Movimenti, NPCPredefinedPathCharacterController> _dict;

	public Action OnMovementFinish;

	void Start() {
		_dict = new Dictionary<Movimenti, NPCPredefinedPathCharacterController>();
		foreach(NPCPredefinedPathCharacterController seq in gameObject.GetComponents<NPCPredefinedPathCharacterController>()) {
			Movimenti mov = seq.Movimento;
			_dict.Add(mov, seq);
		}
	}

	public void ExecMovement(Movimenti mov) {
		NPCPredefinedPathCharacterController seq = _dict[mov];
		seq.enabled = true;
	}

	public void ExecMovementWithCallback(Movimenti mov, Action callback) {
		NPCPredefinedPathCharacterController seq = _dict[mov];
		seq.enabled = true;

		bool called = false;

		OnMovementFinish += () => {
			if(!called) callback();
			called = true;
		};
	}
}
