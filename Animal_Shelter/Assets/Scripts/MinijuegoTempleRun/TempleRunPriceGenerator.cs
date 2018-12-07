using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleRunPriceGenerator : MonoBehaviour {
    [SerializeField] GameObject price;
    [SerializeField] Transform[] positions;

    public List<GameObject> objects;
    enum GENERATOR_STATE { GENERATE, WAIT };
    GENERATOR_STATE gState;
    float generatorTimer;
    float timeForNextObject;

    [Header("Time between spawn")]
    [SerializeField] float min;
    [SerializeField] float max;

    bool play;


    void Start () {
        play = false;

        generatorTimer = 0.0f;
        timeForNextObject = 0.0f;
        gState = GENERATOR_STATE.WAIT;
        timeForNextObject = float.MinValue;
    }

    void Update () {
        if (!play) return;

        //Price generation
        switch (gState) {
            case GENERATOR_STATE.WAIT:
                if (generatorTimer == float.MinValue) {
                    generatorTimer = 0.0f;
                    timeForNextObject = Random.Range(min, max);
                }

                if (generatorTimer > timeForNextObject) {
                    gState = GENERATOR_STATE.GENERATE;
                    return;
                }

                generatorTimer += GameTime.deltaTime;
                break;

            case GENERATOR_STATE.GENERATE:
                GameObject go = Instantiate(price, this.transform);
                go.GetComponent<PriceBehavior>().Init(this);
                go.transform.position = positions[Mathf.FloorToInt(Random.Range(0, 3))].position;
                objects.Add(go);

                generatorTimer = float.MinValue;
                gState = GENERATOR_STATE.WAIT;
                break;
        }

    }

    public void Play() {
        play = true;

        generatorTimer = 0.0f;
        timeForNextObject = 0.0f;
        gState = GENERATOR_STATE.WAIT;
        timeForNextObject = float.MinValue;

        print("Obstacle gen PLAY");
    }

    public void Stop() {
        play = false;

        foreach (GameObject g in objects) {
            Destroy(g);
        }
        objects.Clear();
    }

    public void DestroyFromList(GameObject go) {
        objects.Remove(go);
    }
}
