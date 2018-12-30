using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToPlace : MonoBehaviour {
    Vector3 destinedPosition;
    Vector3 newPosition;
    RectTransform myRectTransform;
    Vector3 velocity;
	// Use this for initialization
	void Start () {
        myRectTransform = GetComponent<RectTransform>();
        destinedPosition = new Vector3(myRectTransform.position.x, myRectTransform.position.y, myRectTransform.position.z);
        myRectTransform.position -= new Vector3(2500, 0, 0);
        velocity = new Vector3(1300, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (myRectTransform.position.x < destinedPosition.x) {
            newPosition = myRectTransform.position + velocity * Time.unscaledDeltaTime;
            myRectTransform.position = newPosition;
        } else {
            myRectTransform.position = destinedPosition;
        }
	}
}
