using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

public class SceneLoader : MonoBehaviour {
	public static IEnumerator SwitchScene(SceneReference loadScene, SceneReference unloadScene) {
		AsyncOperation loadOperation = SceneManager.LoadSceneAsync(loadScene.SceneName, LoadSceneMode.Additive);
		while(!loadOperation.isDone) yield return null;

		SceneManager.SetActiveScene(SceneManager.GetSceneByName(loadScene.SceneName));

		loadOperation = SceneManager.UnloadSceneAsync(unloadScene.SceneName);
		while(!loadOperation.isDone) yield return null;
	}

	public static IEnumerator LoadScene(SceneReference scene) {
		AsyncOperation loadOperation = SceneManager.LoadSceneAsync(scene.SceneName, LoadSceneMode.Additive);
		while(!loadOperation.isDone) yield return null;
	}

	public static IEnumerator LoadSceneAndThen(SceneReference scene, Action callback) {
		AsyncOperation loadOperation = SceneManager.LoadSceneAsync(scene.SceneName, LoadSceneMode.Additive);
		while(!loadOperation.isDone) yield return null;
		callback();
	}

	public static IEnumerator UnloadScene(SceneReference scene) {
		AsyncOperation loadOperation = SceneManager.UnloadSceneAsync(scene.SceneName);
		while(!loadOperation.isDone) yield return null;
	}

	public static IEnumerator LoadSceneReplace(SceneReference scene) {
		AsyncOperation loadOperation = SceneManager.LoadSceneAsync(scene.SceneName, LoadSceneMode.Single);
		while(!loadOperation.isDone) yield return null;
	}

	public static void SetActiveScene(SceneReference scene) {
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene.SceneName));
	}

	public static void MoveObjToScene(GameObject gameObject, SceneReference scene) {
		SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByName(scene.SceneName));
	}
}
