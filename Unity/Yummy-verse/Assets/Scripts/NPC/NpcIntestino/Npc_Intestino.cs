using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class NPC_Intestino : NPC {
	protected override int NumInteractions() {
		return 3;
	}

	protected override bool NthAudioClipCanBeNull(int n) {
		return n == 1;
	}

	[SerializeField]
	private AudioClip[] _audioClips = new AudioClip[3];

	[SerializeField]
	private float _time_before_pointing;

	[SerializeField]
	private float _time_pointing;

	[SerializeField]
	private Transform[] _waypoints;

	[SerializeField]
	private float _move_speed = 1;

	[SerializeField]
	private float _rotation_speed = 1;

	protected override void RunNthAnimation(int n) {
		switch(n) {
			case 0: StartCoroutine(Introduction()); break;
			case 1: StartCoroutine(FollowPlayer()); break;
			case 2: StartCoroutine(BoxComplete()); break;
		}
	}

	protected override void Assertions() {
		Assert.AreNotEqual(_waypoints.Length, 0, $"{name} does not have any waypoint assigned");
		for(int i = 0; i < _waypoints.Length; i++) Assert.IsNotNull(_waypoints[i], $"{name} waypoint ${i} is null");

		_move_speed_npc = _move_speed;
		_rotation_speed_npc = _rotation_speed;
	}

	protected override AudioClip GetNthAudioClip(int n) {
		if(n < 3) return _audioClips[n];
		return null;
	}

	private IEnumerator Introduction() {
		_animator.SetBool("Talking", true);

		yield return new WaitForSeconds(_time_before_pointing);

		_animator.SetBool("Pointing", true);
		yield return new WaitForSeconds(_time_pointing);

		_animator.SetBool("Pointing", false);

		yield return new WaitUntil(() => !IsSpeaking());
		_animator.SetBool("Talking", false);
	}

	private IEnumerator FollowPlayer() {
		_animator.SetBool("Walking", true);

		foreach(Transform wp in _waypoints) {
			_dest = wp;
			_is_walking = true;
			yield return new WaitUntil(() => !_is_walking);
		}

		_animator.SetBool("Walking", false);
	}

	private IEnumerator BoxComplete() {
		_animator.SetBool("Talking", true);

		yield return new WaitUntil(() => !IsSpeaking());
		_animator.SetBool("Talking", false);
	}
}
