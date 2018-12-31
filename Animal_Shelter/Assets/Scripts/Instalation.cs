using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Instalation {
    float upKeep;
    string name;
    string description;
	// Use this for initialization

    public Instalation(float upKeep,string name) {
        this.upKeep = upKeep;
        this.name = name;
    }


    public string GetName() {
        return name;
    }

    public void SetUpKeep(float k) {
        upKeep = k;
    }

    public float GetUpKeep() {
        return upKeep;
    }


}
