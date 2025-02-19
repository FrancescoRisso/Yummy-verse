using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(PercentageToggleManager))]
public class FadingAudio : MonoBehaviour {
	private PercentageToggleManager _perc;
	private AudioSource _audio;
	private float _max_volume;

	void Start() {
		_perc = GetComponent<PercentageToggleManager>();
		Assert.IsNotNull(_perc, $"{name} cannot find its percentage manager");

		_audio = GetComponent<AudioSource>();
		Assert.IsNotNull(_audio, $"{name} cannot find its audio source");

		_max_volume = _audio.volume;

		_perc.OnPercentageChange += (float val) => _audio.volume = val * _max_volume;
	}
}
