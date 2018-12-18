using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {
    [Header("Offsets")]
    [SerializeField] float X = 0.0f;
    [SerializeField] float Y = 0.0f;

	void FixedUpdate () {
        this.transform.position = Input.mousePosition + new Vector3(X,Y);
	}
}
