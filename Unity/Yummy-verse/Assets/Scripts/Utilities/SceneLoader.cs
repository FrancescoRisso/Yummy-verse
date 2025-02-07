using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	public static IEnumerator SwitchScene(string loadSceneName, string unloadSceneName) {
		AsyncOperation loadOperation = SceneManager.LoadSceneAsync(loadSceneName, LoadSceneMode.Additive);
		while(!loadOperation.isDone) yield return null;

		SceneManager.SetActiveScene(SceneManager.GetSceneByName(loadSceneName));

		loadOperation = SceneManager.UnloadSceneAsync(unloadSceneName);
		while(!loadOperation.isDone) yield return null;
	}
}
