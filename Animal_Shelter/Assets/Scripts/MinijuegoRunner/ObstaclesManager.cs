using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour {

    [SerializeField] GameObject obstacle;
    [SerializeField] Transform spawnPoint;

    public List<GameObject> obstacles;
    enum GENERATOR_STATE { GENERATE, WAIT };
    GENERATOR_STATE gState;
    float generatorTimer;
    float timeForNextObstacle;

    [Header("Time between spawn")]
    [SerializeField] float min;    
    [SerializeField] float max;    

    bool run;

    void Start () {
        run = false;

        generatorTimer = 0.0f;
        timeForNextObstacle = 0.0f;
        gState = GENERATOR_STATE.WAIT;
        timeForNextObstacle = float.MinValue;
    }

    // Update is called once per frame
    void Update () {
        if (!run) return;
        //OBSTACLE GENERATION
        switch (gState) {
            case GENERATOR_STATE.WAIT:
                if (generatorTimer == float.MinValue) {
                    generatorTimer = 0.0f;
                    timeForNextObstacle = Random.Range(min, max);
                }

                if (generatorTimer > timeForNextObstacle) {
                    gState = GENERATOR_STATE.GENERATE;
                    return;
                }

                generatorTimer += GameTime.deltaTime;
                break;

            case GENERATOR_STATE.GENERATE:
                GameObject go = Instantiate(obstacle);
                go.GetComponent<ObstacleBehavior>().Init(this);
                go.transform.position = spawnPoint.position;
                go.transform.parent = transform;
                obstacles.Add(go);

                generatorTimer = float.MinValue;
                gState = GENERATOR_STATE.WAIT;
                break;
        }
    }

    public void Play() {
        run = true;

        generatorTimer = 0.0f;
        timeForNextObstacle = 0.0f;
        gState = GENERATOR_STATE.WAIT;
        timeForNextObstacle = float.MinValue;
    }

    public void Stop() {
        run = false;
        foreach (GameObject g in obstacles) {
            Destroy(g);
        }
        obstacles.Clear();
    }

    public void DestroyFromList(GameObject go) {
        foreach (GameObject g in obstacles) {
            if (g.Equals(go)) {
                obstacles.Remove(g);
            }
        }
    }
}
