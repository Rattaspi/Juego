using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleRunAvatar : MonoBehaviour {
    [SerializeField] Transform[] positions;
    int currentPos;
    public bool play;

	void Awake () {
        currentPos = 1;
        this.transform.position = positions[currentPos].position;
        play = false;
	}
	
	void Update () {
        if (!play) return;
        //MOVEMENT
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            currentPos--;
            if (currentPos < 0) currentPos = 2;
            this.transform.position = positions[currentPos].position;
            print(currentPos);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            currentPos = ++currentPos % 3;
            this.transform.position = positions[currentPos].position;
        }
    }

    public void Play() {
        play = true;

        print("Avatar PLAY");
    }

    public void Stop() {
        play = false;
    }
}
