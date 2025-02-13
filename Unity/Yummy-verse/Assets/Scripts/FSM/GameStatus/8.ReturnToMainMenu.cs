using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Utilities;

public class ReturnToMainMenu : MonoBehaviour {
	[SerializeField]
	private SceneReference _menu_scene;

	[SerializeField]
	private VideoPlayerManager _video;

	void Start() {
		Assert.AreNotEqual(_menu_scene.SceneName, "", $"{name} does not have the menu scene reference");
		Assert.IsNotNull(_video, $"{name} does not have the video player assinged");
		_video.VideoFinished += () => StartCoroutine(SceneLoader.LoadSceneReplace(_menu_scene));
	}
}
