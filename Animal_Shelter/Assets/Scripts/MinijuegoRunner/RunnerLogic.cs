using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RunnerLogic : MonoBehaviour {

    bool run;
    float gameTimer;
    public enum DIFFICULTY { EASY, NORMAL, HARD};
    public enum STATE { START, GAME, END};
    STATE state;
    ObstaclesManager om;
    RunnerAvatarController rac;
    [SerializeField] DIFFICULTY difficulty;
    [SerializeField] float maxTime;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] GameObject startCanvas;

	void Start () {
        run = false;
        gameTimer = 0;
        state = STATE.START;
        om = GetComponentInChildren<ObstaclesManager>();
        rac = GetComponentInChildren<RunnerAvatarController>();

        if (om == null) Debug.LogError("Obstacle manager from the runner is not referenced");
        if (rac == null) Debug.LogError("RunnerAvatarController not referenced in RunnerLogic");
        //Play(difficulty);
	}
	
	void Update () {
        if (!run) return;

        switch (state) {
            case STATE.START:
                print("Start state");
                if (Input.GetKeyUp(KeyCode.Space)) {
                    print("Pressed space");
                    state = STATE.GAME;
                    rac.SetInputBlocked(false);
                    om.Play();
                    gameTimer = 0;
                    startCanvas.SetActive(false);
                }
                break;

            case STATE.GAME:
                timeText.text = "" + Mathf.Clamp(Mathf.Floor(maxTime - gameTimer), 0.0f, Mathf.Infinity);
                gameTimer += (GameTime.deltaTime / Time.timeScale);

                if (gameTimer > maxTime) {
                    state = STATE.END;
                }

                break;

            case STATE.END:
                GameLogic.instance.money += gameTimer * 2;
                Stop();
                break;
        }
	}

    public void Play(DIFFICULTY diff) {
        run = true;
        state = STATE.START;
        print("RUNNERLOGIC: START");
        gameTimer = 0;
        timeText.text = "" + Mathf.Clamp(Mathf.Floor(maxTime - gameTimer), 0.0f, Mathf.Infinity);
        startCanvas.SetActive(true);
        difficulty = diff;

        switch (difficulty) {
            case DIFFICULTY.EASY:
                Time.timeScale = 1.0f;
                break;
            case DIFFICULTY.NORMAL:
                Time.timeScale = 2.0f;
                break;
            case DIFFICULTY.HARD:
                Time.timeScale = 3.0f;
                break;
        }
    }

    public void Stop() {
        run = false;
        om.Stop();
        rac.SetInputBlocked(true);
        state = STATE.START;
        gameObject.SetActive(false);
        GameLogic.instance.gameState = GameLogic.GameState.WEEKSTART;

    }

    public void Restart() {
        Stop();
        Play(difficulty);
    }

    public void Close() {
        Stop();
        this.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void OnDisable() {
        Stop();
    }
}
