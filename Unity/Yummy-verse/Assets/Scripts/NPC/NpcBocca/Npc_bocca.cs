using UnityEngine;
using System.Collections;

public class NPC_Bocca : NPC {
	protected override int NumInteractions() {
		return 2;
	}

	[SerializeField]
	private AudioClip[] _audioClips = new AudioClip[2];

	[SerializeField]

	protected override void RunNthAnimation(int n) {
		switch(n) {
			case 0: StartCoroutine(Introduction()); break;
			case 1: StartCoroutine(ChewingComplete()); break;
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

	private IEnumerator ChewingComplete() {
		_animator.SetBool("Talking", true);

		yield return new WaitUntil(() => !IsSpeaking());
		_animator.SetBool("Talking", false);
	}
}
