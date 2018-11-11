using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_Animals : MonoBehaviour {

	void Start () {
        GameObject g = new GameObject();
        Instantiate(g);
        g.transform.parent = this.transform;
        g.gameObject.name = "Animal";
        g.AddComponent<Animal>();
	}
}
