using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_Animals : MonoBehaviour {

    public int animalsToGenerate;

	void Start () {
        for (int i = 0; i < animalsToGenerate; i++) {
            GameObject g = new GameObject();
            g.transform.parent = this.transform;
            g.transform.position = new Vector3(400, 400);
            g.gameObject.name = "Animal"+i;
            g.AddComponent<Animal>();
        }
	}
}
