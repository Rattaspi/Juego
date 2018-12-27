using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {
    float timeToMainMenu = 10.0f;

	void Start () {
        FaderScript.instance.unFade = true;
        StartCoroutine(GoToMainMenu());
	}

    IEnumerator GoToMainMenu() {
        AsyncOperation op = SceneManager.LoadSceneAsync("MainMenu");
        op.allowSceneActivation = false;
        yield return new WaitForSeconds(timeToMainMenu);
        FaderScript.instance.fade = true;
        yield return new WaitForSeconds(1.0f);
        op.allowSceneActivation = true;
    }
}
