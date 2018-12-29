using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menulogic : MonoBehaviour {
    [SerializeField] FaderScript fader;
    [SerializeField] string firstGameScene;
    public GameObject confirmPopUp;

	void Start () {
        confirmPopUp.SetActive(false);
        StartCoroutine(fader.UnFade());
	}
	
    public void PlayButton() {
        if (GameLogic.instance.firstExecution) {
            confirmPopUp.SetActive(true);
        } else {
            ConfirmPlay();
        }
    }

    public void ConfirmTutorial() {
        StartCoroutine(fader.Fade());
        StartCoroutine(PlayGame(true));
    }

    public void ConfirmPlay() {
        StartCoroutine(fader.Fade());
        StartCoroutine(PlayGame(false));
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

    IEnumerator PlayGame(bool tutorial) {
        while (fader.doing) {
            yield return null;
        }
        if (tutorial) {
            SceneManager.LoadScene("TutorialScene");
        } else {
            SceneManager.LoadScene("MainScene");
        }
    }
}
