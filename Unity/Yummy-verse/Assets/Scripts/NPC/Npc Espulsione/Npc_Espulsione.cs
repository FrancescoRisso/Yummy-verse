using UnityEngine;
using System.Collections;

public class NPC_Espulsione : NPC {
	protected override int NumInteractions() {
		return 1;
	}

	[SerializeField]
	private AudioClip _audioClip;

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
		_animator.SetBool("Talking", true);

		yield return new WaitUntil(() => !IsSpeaking());
		_animator.SetBool("Talking", false);
	}
}
