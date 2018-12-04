using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adoptante : MonoBehaviour {
    Canvas canvas;
    [Header("Adopter preferences")]
    [SerializeField] Animal.SIZE sizePreferred;

	void Start () {
        canvas = GetComponentInParent<Canvas>();
        this.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);

        GameObject g = Resources.Load<GameObject>("Prefabs/Humans/HumanGraphics");
        g = Instantiate(g, this.transform);
        g.name = "Graphics";

        //Initialize adopter preferences
        sizePreferred = (Animal.SIZE)Random.Range(0, (int)Animal.SIZE.LENGTH);
	}
}
