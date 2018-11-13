using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleRunAvatar : MonoBehaviour {
    [SerializeField] Transform[] positions;
    int currentPos;

	void Start () {
        currentPos = 1;
        this.transform.position = positions[currentPos].position;
	}
	
	void Update () {
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
}
