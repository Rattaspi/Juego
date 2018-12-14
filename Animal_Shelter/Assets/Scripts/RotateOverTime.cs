using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour {
    public float periodTime;
    float timer;
    public bool spinning;
    public bool spinning2;
    public bool restarting;
    //public float delta;
    Quaternion currentRotation;
    // Use this for initialization
    void Start () {
        spinning = true;
    }
	
	// Update is called once per frame
	void Update () {


        if (spinning) {
            timer += GameTime.deltaTime;

            float delta = timer / (periodTime / 2);
            currentRotation = Quaternion.AngleAxis(delta * 180, new Vector3(0, 0, 1));
            transform.rotation = currentRotation;

            if (timer > periodTime / 2) {
                spinning = false;
                currentRotation = Quaternion.AngleAxis(1 * 180, new Vector3(0, 0, 1));
                transform.rotation = currentRotation;
            }
        } else {
            if (!spinning2&&!restarting) {
                timer -= GameTime.deltaTime;
                if (timer <= 0) {
                    timer = 0;
                    spinning2 = true;
                }
            } else if(!restarting){
                timer += GameTime.deltaTime;

                float delta = timer / (periodTime / 2);
                currentRotation = Quaternion.AngleAxis(delta * 180+180, new Vector3(0, 0, 1));
                transform.rotation = currentRotation;

                if (timer > periodTime / 2) {
                    currentRotation = Quaternion.AngleAxis(1 * 180+180, new Vector3(0, 0, 1));
                    transform.rotation = currentRotation;
                    spinning2 = false;
                    restarting = true;
                }
            } else {
                timer -= GameTime.deltaTime;
                if (timer <= 0) {
                    timer = 0;
                    spinning = true;
                    restarting = false;
                }
            }
        }







	}
}
