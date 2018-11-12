using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PouObject : MonoBehaviour {
    public int score;
    public RectTransform rectTransform;
    private void Start() {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -100*GetComponentInParent<PouLogic>().difficultyMultiplier);
        rectTransform = GetComponent<RectTransform>();
        if (score < 0) {
            GetComponent<RawImage>().color = new Color(255, 0, 0);
        } else {
            GetComponent<RawImage>().color = new Color(0, 255, 0);
        }
    }
    private void Update() {
        if (rectTransform.position.y < -100) {
            Destroy(gameObject);
        }
    }
}
