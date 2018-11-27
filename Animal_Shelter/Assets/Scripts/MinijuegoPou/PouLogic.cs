using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PouLogic : MonoBehaviour {
    bool play;
    public float difficultyMultiplier;
    public int lives;
    public int totalScore;
    public float spawnTimer;
    public float spawnTime;
    public float gameTimer;
    public float maxTime;
    public GameObject itemPrefabObject;
    public List<GameObject> currentItems;
    public PouAvatarController pouAvatar;
    //public RunnerLogic.DIFFICULTY difficulty;
    public RunnerLogic.STATE state;
    public GameObject startCanvas;
    public Text timeText;
    public Text startGameText;
    public Text instructionsText;
    public Text finalScore;
    public Text gameOverText;
    public Text continueText;
    // Use this for initialization
    void Start() {
        lives = 3;
        totalScore = 0;
        spawnTime = 2.5f;
        spawnTimer = 0;
        itemPrefabObject = Resources.Load<GameObject>("Prefabs/PouObjectPrefab");
        currentItems = new List<GameObject>();
        startCanvas.SetActive(false);
        instructionsText.text = "Pulsa A para moverte a la izquierda y D para moverte a la derecha";
        startGameText.text = "Pulsa A o D para comenzar";
        gameOverText.text = "Fin de partida";
        finalScore.text = "Puntuación final " + totalScore;
        continueText.text = "Pulsa Espacio para continuar";
        //Play(0);
        gameOverText.gameObject.SetActive(false);
        finalScore.gameObject.SetActive(false);
        continueText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (!play) return;
        else {
            switch (state) {
                case RunnerLogic.STATE.START:
                    startGameText.gameObject.SetActive(true);
                    instructionsText.gameObject.SetActive(true);
                    pouAvatar.transform.position = pouAvatar.spawn;
                    if (Input.GetAxis("Horizontal") != 0) {
                        startGameText.gameObject.SetActive(false);
                        instructionsText.gameObject.SetActive(false);

                        state = RunnerLogic.STATE.GAME;
                    }
                    break;
                case RunnerLogic.STATE.GAME:
                    if (spawnTimer > spawnTime) {
                        spawnTimer = 0;
                        int isGoodRandom = Random.Range(0, 9);
                        switch ((int)difficultyMultiplier) {
                            case 1:
                                if (isGoodRandom < 2) {
                                    currentItems.Add(SpawnItem(false));
                                } else {
                                    currentItems.Add(SpawnItem(true));
                                }
                                break;
                            case 3:
                                if (isGoodRandom <4) {
                                    currentItems.Add(SpawnItem(false));
                                } else {
                                    currentItems.Add(SpawnItem(true));
                                }
                                break;
                            case 5:
                                if (isGoodRandom <6) {
                                    currentItems.Add(SpawnItem(false));
                                } else {
                                    currentItems.Add(SpawnItem(true));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    spawnTimer += Time.deltaTime;
                    currentItems.RemoveAll(isNull);

                    if (gameTimer > maxTime) {
                        state = RunnerLogic.STATE.END;
                    }

                    break;
                case RunnerLogic.STATE.END:
                    Restart();
                    gameOverText.gameObject.SetActive(true);
                    finalScore.gameObject.SetActive(true);
                    continueText.gameObject.SetActive(true);

                    if (Input.GetAxis("Continue")>0) {
                        startCanvas.SetActive(false);
                        play = false;
                        gameOverText.gameObject.SetActive(false);
                        finalScore.gameObject.SetActive(false);
                        continueText.gameObject.SetActive(false);
                        state = RunnerLogic.STATE.START;
                    }
                    break;
            }
        }
    }

    public void Play(RunnerLogic.DIFFICULTY diff) {
        play = true;
        state = RunnerLogic.STATE.START;
        gameTimer = 0;
        timeText.text = "" + Mathf.Clamp(Mathf.Floor(maxTime - gameTimer), 0.0f, Mathf.Infinity);
        startCanvas.SetActive(true);
        //difficulty = diff;
        difficultyMultiplier = 2.0f * ((int)diff + 1);
        spawnTime = 2.5f - (int)diff;

    }

    public void Stop() {
        play = false;
        pouAvatar.SetInputBlocked(true);
    }


    void Restart() {
        foreach(GameObject g in currentItems) {
            if (g != null) {
                Destroy(g);
            }
        }
        currentItems.RemoveAll(isNull);
        if (pouAvatar != null) {
            pouAvatar.transform.position = pouAvatar.spawn;
        }
    }

    bool isNull(GameObject g) {
        return g == null;
    }

    public GameObject SpawnItem(bool positive) {
        Vector2 randomPos = new Vector2((int)(Random.Range(450,1408)),1028);
        GameObject pouGameObject = Instantiate<GameObject>(itemPrefabObject, transform);
        pouGameObject.GetComponent<RectTransform>().position = randomPos;
        PouObject pouObject = pouGameObject.GetComponent<PouObject>();
        if (positive) {
            pouGameObject.tag = "food";
            pouObject.score = 10;
        } else {
            pouGameObject.tag = "kill";
            pouObject.score = -1;
        }
        return pouGameObject;
    }

    public void LifeDown() {
        lives--;
        if (CheckGameOver()) {
            //Acaba la partida
            state = RunnerLogic.STATE.END;
        } else {
            Restart();
        }
    }

    public void Eat(GameObject g) {
        totalScore += g.GetComponent<PouObject>().score;
        Destroy(g);
    }

    public bool CheckGameOver() {
        return lives <= 0;
    }
}
