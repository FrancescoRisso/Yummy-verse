using UnityEngine;
using UnityEngine.Assertions;

public class Chain : EkeyInteractable {
	private PercentageToggleManager _lowering;
	private AudioSource _audioSource;


	public override void StartProcessing() {
		_lowering._toggle.Invoke();

		if (_audioSource != null && !_audioSource.isPlaying) {
			_audioSource.Play();
		}
	}

	void Start() {
		_lowering = GetComponent<PercentageToggleManager>();
		Assert.IsNotNull(_lowering, $"{name} does not have the lowering mechanism");

		_audioSource = GetComponent<AudioSource>();
		Assert.IsNotNull(_audioSource, $"{name} does not have an Audiosource");
	}
}
