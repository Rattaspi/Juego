using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menulogic : MonoBehaviour {
    [SerializeField] FaderScript fader;
    [SerializeField] string firstGameScene;

	void Start () {
        StartCoroutine(fader.UnFade());
	}
	
    public void PlayButton() {
        StartCoroutine(fader.Fade());
        StartCoroutine(PlayGame());
    }

    public void ExitButton() {
        StartCoroutine(fader.Fade());
        StartCoroutine(ExitGame());
    }

    IEnumerator ExitGame() {
        while (fader.doing) {
            yield return null;
        }
        Application.Quit();
        yield return new WaitForSeconds(5.0f);
    }

    IEnumerator PlayGame() {
        while (fader.doing) {
            yield return null;
        }
        SceneManager.LoadScene(firstGameScene);
    }
}
