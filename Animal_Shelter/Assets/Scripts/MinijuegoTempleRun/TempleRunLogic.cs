using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempleRunLogic : MonoBehaviour {
    bool run;
    float gameTimer;
    //public enum DIFFICULTY { EASY, NORMAL, HARD };
    public enum STATE { START, GAME, END };
    STATE state;
    public int monedas;

    TempleRunAvatar avatar;
    TempleRunObstacleGen obstacleGen;
    [HideInInspector] public TempleRunPriceGenerator priceGen;
    [SerializeField] RunnerLogic.DIFFICULTY difficulty;
    [SerializeField] float maxTime;
    [SerializeField] Text timeText;
    [SerializeField] Text monedasText;
    [SerializeField] GameObject startCanvas;

    private void Awake() {
        avatar = GetComponentInChildren<TempleRunAvatar>();
        if (avatar == null) Debug.LogError("TempleRunAvatar not found from TempleRunLogic");

        obstacleGen = GetComponentInChildren<TempleRunObstacleGen>();
        if (obstacleGen == null) Debug.LogError("TempleRunObstacleGen not found from TempleRunLogic");

        priceGen = GetComponentInChildren<TempleRunPriceGenerator>();
        if (priceGen == null) Debug.LogError("TempleRunPriceGen not found from TempleRunLogic");
    }

    void Start () {
        run = false;
        gameTimer = 0;
        monedas = 0;
        state = STATE.START;
        startCanvas.SetActive(true);

        Play(difficulty);
    }

    private void Update() {
        if (!run) return;

        switch (state) {
            case STATE.START:
                if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow)) {
                    state = STATE.GAME;
                    avatar.SetInputBlocked(false);
                    obstacleGen.Play();
                    priceGen.Play();
                    gameTimer = 0;
                    startCanvas.SetActive(false);
                }
                break;

            case STATE.GAME:
                timeText.text = "" + Mathf.Clamp(Mathf.Floor(maxTime - gameTimer), 0.0f, Mathf.Infinity);
                monedasText.text = "Monedas: " + monedas;
                if (gameTimer > maxTime) Restart();

                gameTimer += (GameTime.deltaTime/ Time.timeScale);
                break;

            case STATE.END:
                break;
        }
    }

    public void Play(RunnerLogic.DIFFICULTY diff) {
        run = true;
        state = STATE.START;
        gameTimer = 0;
        timeText.text = "" + Mathf.Clamp(Mathf.Floor(maxTime - gameTimer), 0.0f, Mathf.Infinity);
        startCanvas.SetActive(true);
        difficulty = diff;

        switch (difficulty) {
            case RunnerLogic.DIFFICULTY.EASY:
                Time.timeScale = 1.0f;
                break;
            case RunnerLogic.DIFFICULTY.NORMAL:
                Time.timeScale = 2.0f;
                break;
            case RunnerLogic.DIFFICULTY.HARD:
                Time.timeScale = 3.0f;
                break;
        }
        print("Temple logic PLAY");
    }

    public void Stop() {
        run = false;
        obstacleGen.Stop();
        priceGen.Stop();
        avatar.SetInputBlocked(true);
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

    private void OnEnable() {
        Stop();
        startCanvas.SetActive(true);
    }
}
