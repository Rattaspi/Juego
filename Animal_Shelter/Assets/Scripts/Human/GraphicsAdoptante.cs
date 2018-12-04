using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsAdoptante : MonoBehaviour {

	void Start () {
        GameObject humanSprite = new GameObject();
        humanSprite.name = "PjImage";
        humanSprite.transform.parent = this.transform;
        humanSprite.transform.localPosition = Vector2.zero;

        Image i = humanSprite.AddComponent<Image>();
        i.sprite = Resources.Load<Sprite>("Sprites/Humans/pj_" + Random.Range(1, 30));

        RectTransform rectTransform = humanSprite.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(250, 250);
	}
}
