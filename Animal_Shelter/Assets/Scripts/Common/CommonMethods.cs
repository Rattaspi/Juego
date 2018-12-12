using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonMethods : MonoBehaviour {

    public static string GetNumberWithDots(int num) {
        string temp = num.ToString("N0");
        return temp;
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
