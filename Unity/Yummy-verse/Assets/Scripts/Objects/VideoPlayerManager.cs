using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Video;

public class VideoPlayerManager : MonoBehaviour {
	private VideoPlayer _player;
	public Action VideoFinished;

	private bool _init_called;

	void Start() {
		_player = GetComponent<VideoPlayer>();
		Assert.IsNotNull(_player, $"{name} cannot find its video player");

		_player.loopPointReached += (VideoPlayer p) => { VideoFinished?.Invoke(); };

		_init_called = true;
	}

	public void Play() {
		if(!_init_called) Start();

		_player.Play();
	}
}
