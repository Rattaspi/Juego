using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeLerpTime : MonoBehaviour {
    public float periodTime;
    public float maxSize;
    public float minSize;
    float timer;
    Vector3 currentScale;
    bool growing=true;
	// Use this for initialization
	void Start () {
        currentScale = new Vector3(1,1,1);
    }
	
	// Update is called once per frame
	void Update () {

        if (growing) {
            timer += GameTime.deltaTime;
            if (timer > periodTime / 2) {
                growing = false;
            }
        } else {
            timer -= GameTime.deltaTime;
            if (timer <= 0) {
                timer = 0;
                growing = true;
            }
        }
        float delta = timer/(periodTime/2);


        float vectorValue = Mathf.Lerp(minSize, maxSize, delta);
        currentScale.x = vectorValue;
        currentScale.y = vectorValue;
        currentScale.z = vectorValue;

        transform.localScale = currentScale;

    }
}
