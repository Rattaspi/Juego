using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instalation : MonoBehaviour {
    float upKeep;
    string description;
	// Use this for initialization

    Instalation(float upKeep) {
        this.upKeep = upKeep;
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetUpKeep(float k) {
        upKeep = k;
    }

    public float GetUpKeep() {
        return GetUpKeep();
    }


}
