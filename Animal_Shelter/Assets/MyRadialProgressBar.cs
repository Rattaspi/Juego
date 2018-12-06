using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyRadialProgressBar : MonoBehaviour {
    Image LoadingBar;
    //public float maxValue;
    //public float currentValue;
    // Use this for initialization
    void Start() {
        LoadingBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
        LoadingBar.fillAmount = GameLogic.instance.timeOfEntry / GameLogic.instance.maxTimeOfEntry;
    }
}