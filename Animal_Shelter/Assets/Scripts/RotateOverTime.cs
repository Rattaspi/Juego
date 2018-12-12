using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour {
    public float periodTime;
    float timer;
    //public float delta;
    Quaternion currentRotation;
    // Use this for initialization
    void Start () {
        periodTime = 1 / periodTime;

    }
	
	// Update is called once per frame
	void Update () {
        float delta = timer / periodTime;
        timer += GameTime.deltaTime;
        if (timer >= periodTime) {
            timer -= periodTime;
        }
        currentRotation = Quaternion.AngleAxis(delta * 360, new Vector3(0, 0, 1));
        transform.rotation = currentRotation;
	}
}
