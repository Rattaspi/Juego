using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseSceen : MonoBehaviour {
    [SerializeField] float timeUntilCredits = 5.0f;

	void Start () {
        FaderScript.instance.unFade = true;
        StartCoroutine(LoadSceneAndWait());
	}

    IEnumerator LoadSceneAndWait() {
        AsyncOperation op = SceneManager.LoadSceneAsync("Credits");
        op.allowSceneActivation = false;

        yield return new WaitForSeconds(timeUntilCredits);
        FaderScript.instance.fade = true;
        yield return new WaitForSeconds(1.0f);
        while (FaderScript.instance.doing) yield return null;

        op.allowSceneActivation = true;
    }
}
