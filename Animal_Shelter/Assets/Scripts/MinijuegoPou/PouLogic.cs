using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouLogic : MonoBehaviour {
    public float difficultyMultiplier;
    public int lives;
    public int totalScore;
    public float spawnTimer;
    public float spawnTime;
    public GameObject itemPrefabObject;
    public List<GameObject> currentItems;
    public PouAvatarController pouAvatar;
    // Use this for initialization
    void Start() {
        lives = 3;
        totalScore = 0;
        difficultyMultiplier=1.0f;
        spawnTime = 2.5f;
        spawnTimer = 0;
        itemPrefabObject = Resources.Load<GameObject>("Prefabs/PouObjectPrefab");
        currentItems = new List<GameObject>();
    }

    // Update is called once per frame
    void Update() {
        if (spawnTimer > spawnTime) {
            spawnTimer = 0;
            int isGoodRandom = Random.Range(0, 3);
            if (isGoodRandom == 0) {
                currentItems.Add(SpawnItem(false));
            } else {
                currentItems.Add(SpawnItem(true));
            }
        }
        spawnTimer += Time.deltaTime;
        currentItems.RemoveAll(isNull);
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
