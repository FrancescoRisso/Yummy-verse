using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class NPC_StomacoSotto : NPC {
	protected override int NumInteractions() {
		return 1;
	}

	[SerializeField]
	private AudioClip _audioClip;

	[SerializeField]
	private Transform[] _waypoints;

	[SerializeField]
	private float _move_speed = 1;

	[SerializeField]
	private float _rotation_speed = 1;

	protected override void Assertions() {
		Assert.AreNotEqual(_waypoints.Length, 0, $"{name} does not have any waypoint assigned");
		for(int i = 0; i < _waypoints.Length; i++) Assert.IsNotNull(_waypoints[i], $"{name} waypoint ${i} is null");

		_move_speed_npc = _move_speed;
		_rotation_speed_npc = _rotation_speed;
	}

	protected override void RunNthAnimation(int n) {
		switch(n) {
			case 0: StartCoroutine(Introduction()); break;
		}
	}

	protected override AudioClip GetNthAudioClip(int n) {
		if(n == 0) return _audioClip;
		return null;
	}

	private IEnumerator Introduction() {
		_animator.SetBool("Walking", true);

		foreach(Transform wp in _waypoints) {
			_dest = wp;
			_is_walking = true;
			yield return new WaitUntil(() => !_is_walking);
		}

		_animator.SetBool("Talking", true);
		_animator.SetBool("Walking", false);

		yield return new WaitUntil(() => !IsSpeaking());
		_animator.SetBool("Talking", false);
	}
}
