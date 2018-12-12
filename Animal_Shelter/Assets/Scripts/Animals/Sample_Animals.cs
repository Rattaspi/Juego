using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_Animals : MonoBehaviour {

    //public int animalsToGenerate;
    public bool generate;

	void Update () {
        if(generate) {
            GameObject g = new GameObject();
            g.transform.parent = this.transform;
            g.transform.position = new Vector3(400, 400);
            g.gameObject.name = "Animal";
            Animal a = g.AddComponent<Animal>();
            a.StartStats();

            generate = false;
        }
	}
}
