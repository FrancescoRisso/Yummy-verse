using UnityEngine;
using System.Collections;

public class NPC_StomacoSopra : NPC {
	protected override int NumInteractions() {
		return 2;
	}

	[SerializeField]
	private AudioClip[] _audioClips = new AudioClip[2];

	[SerializeField]
	private Transform[] _waypoints;

	protected override void RunNthAnimation(int n) {
		switch(n) {
			case 0: StartCoroutine(Introduction()); break;
			case 1: StartCoroutine(ShootingComplete()); break;
		}
	}

	protected override AudioClip GetNthAudioClip(int n) {
		if(n < 2) return _audioClips[n];
		return null;
	}

	private IEnumerator Introduction() {
		_animator.SetTrigger("Greetings");
		_animator.SetBool("Talking", true);

		yield return new WaitUntil(() => !IsSpeaking());
		_animator.SetBool("Talking", false);
	}

	private IEnumerator ShootingComplete() {
		_animator.SetBool("Talking", true);

		yield return new WaitUntil(() => !IsSpeaking());
		_animator.SetBool("Talking", false);
	}
}
