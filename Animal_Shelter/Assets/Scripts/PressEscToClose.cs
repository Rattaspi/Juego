using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressEscToClose : MonoBehaviour {
    public bool setsCanvasState;
    public bool blocked;
    public CanvasScript.CanvasState stateToSet;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (gameObject.activeInHierarchy&& !blocked) {
            //Debug.Log("Neh");
            GameTime.pauseBlocked = true;
            blocked = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            //StartCoroutine(GameTime.unBlockPause());
            GameTime.pauseBlocked = false;
            if (setsCanvasState) {
                CanvasScript.canvasScript.canvasState = stateToSet;
            }
            gameObject.SetActive(false);
            blocked = false;

        }
    }
}
