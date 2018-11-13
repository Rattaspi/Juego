using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleRunObstacleGen : MonoBehaviour {
    [SerializeField] GameObject obstacle;
    [SerializeField] Transform[] positions;

    public List<GameObject> obstacles;
    enum GENERATOR_STATE { GENERATE, WAIT };
    GENERATOR_STATE gState;
    float generatorTimer;
    float timeForNextObstacle;

    [Header("Time between spawn")]
    [SerializeField] float min;
    [SerializeField] float max;

    bool play;

    void Awake () {
        play = false;

        generatorTimer = 0.0f;
        timeForNextObstacle = 0.0f;
        gState = GENERATOR_STATE.WAIT;
        timeForNextObstacle = float.MinValue;
    }

    void Update () {
        if (!play) return;

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

                generatorTimer += Time.deltaTime;
                break;

            case GENERATOR_STATE.GENERATE:
                GameObject go = Instantiate(obstacle, this.transform);
                go.GetComponent<ObstacleBehavior>().Init(this);
                go.transform.position = positions[Mathf.FloorToInt(Random.Range(0,3))].position;
                obstacles.Add(go);

                generatorTimer = float.MinValue;
                gState = GENERATOR_STATE.WAIT;
                break;
        }

    }

    public void Play() {
        play = true;

        generatorTimer = 0.0f;
        timeForNextObstacle = 0.0f;
        gState = GENERATOR_STATE.WAIT;
        timeForNextObstacle = float.MinValue;

        print("Obstacle gen PLAY");
    }

    public void Stop() {
        play = false;

        foreach (GameObject g in obstacles) {
            Destroy(g);
        }
        obstacles.Clear();
    }

    public void DestroyFromList(GameObject go) {
        obstacles.Remove(go);
    }
}
