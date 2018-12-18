using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleReactor : MonoBehaviour {
    //public Vector3 targetScale;
    public float effectTime;
    float effectTimer;
    Vector3 originalScale;
    float lerpValue;
    public bool reacting;
    public float amplitude = 0.2f;

    public void React() {
        reacting = true;
    }
	// Use this for initialization
	void Start () {
        originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        if (reacting) {
            effectTimer += GameTime.deltaTime;

            if (effectTimer - effectTime / 2 > 0) {
                lerpValue = effectTime - effectTimer;
            } else {
                lerpValue = effectTimer / effectTime;
            }

            //Mathf.Sin();

            Vector3 scale = new Vector3();

            scale.x = Mathf.Abs(amplitude * Mathf.Sin(lerpValue) + originalScale.x);
            scale.y = Mathf.Abs(amplitude * Mathf.Sin(lerpValue) + originalScale.y);
            //scale.z = amplitude * Mathf.Sin(Time.time) + 1;

            transform.localScale = scale;

            //transform.localScale = Vector3.Lerp(originalScale, targetScale, lerpValue);

            if (effectTimer >= effectTime) {
                effectTimer = 0;
                reacting = false;
            }

        }
	}
}
