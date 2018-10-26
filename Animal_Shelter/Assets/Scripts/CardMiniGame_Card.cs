using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMiniGame_Card : MonoBehaviour {
    public int id;
    public CardMiniGame_Card sister;
    public int cardValue;
    public bool highlighted;
    public bool clicked;
    public bool wasClicked;
    public bool rotated;
    float highlightTimer=0;
    Vector3 originalScale;
    Vector3 highlightedScale;
    Quaternion originalRotation;
    float originalRotationAngle;
    Vector3 originalRotationiVector;
    float destinedRotation = 180.0f;
    public float sigma,sigma2;
    Vector3 tempScale;

    // Use this for initialization
    void Start () {
        originalRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        transform.rotation.ToAngleAxis(out originalRotationAngle, out originalRotationiVector);

        originalScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        highlightedScale=new Vector3(originalScale.x+originalScale.x*0.1f , originalScale.y + originalScale.y * 0.1f , originalScale.z + originalScale.z * 0.1f);
        tempScale = new Vector3(0, 0, 0);
        sigma = 0;
        highlighted = clicked = false;
        rotated = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!clicked) {
            if (wasClicked) {
                UnClickBehavior();
            }
            HighlightManagement();
            HighlightLerp();
        } else {
            ClickedBehavior();
        }
    }

    private void OnMouseOver() {
        highlighted = true;
        highlightTimer = 0;
    }



    void UnClickBehavior() {
        if (sigma2 > 0) {
            float tempAngle;
            Vector3 rotationVector;
            transform.rotation.ToAngleAxis(out tempAngle, out rotationVector);
            transform.rotation = Quaternion.AngleAxis(Mathf.Lerp(originalRotationAngle, tempAngle, sigma2), new Vector3(0, 1, 0));
            sigma2 -= Time.deltaTime;
        } else {
            sigma2 = 0;
            wasClicked = false;
        }
    }

    void ClickedBehavior() {
        wasClicked = true;
        if (sigma2 < 1) {
            float tempAngle;
            Vector3 rotationVector;
            transform.rotation.ToAngleAxis(out tempAngle, out rotationVector);
            transform.rotation = Quaternion.AngleAxis(Mathf.Lerp(tempAngle, destinedRotation, sigma2), new Vector3(0, 1, 0));
            sigma2 += Time.deltaTime;
        } else {
            sigma2 = 1;
            rotated = true;
        }
    }

    private void OnMouseEnter() {
        if (CardMiniGame_Logic.cardLogic != null) {
            CardMiniGame_Logic.cardLogic.currentCard = this;
        }
    }

    private void OnMouseExit() {
        if (CardMiniGame_Logic.cardLogic != null) {
            if (CardMiniGame_Logic.cardLogic.currentCard == this) {
                CardMiniGame_Logic.cardLogic.currentCard = null;
            }
        }
    }

    void HighlightManagement() {
        if (highlighted) {
            highlightTimer += Time.deltaTime;
            if (sigma < 1) {
                sigma += Time.deltaTime * 3;
            } else {
                sigma = 1;
            }
        } else {
            if (sigma > 0) {
                sigma -= Time.deltaTime * 3;
            } else {
                sigma = 0;
            }
        }
        if (highlightTimer > 0.15f) {
            highlighted = false;
        }
    }

    void HighlightLerp() {
        tempScale.x = Mathf.Lerp(originalScale.x, highlightedScale.x, sigma);
        tempScale.y = Mathf.Lerp(originalScale.y, highlightedScale.y, sigma);
        tempScale.z = Mathf.Lerp(originalScale.z, highlightedScale.z, sigma);
        transform.localScale = tempScale;
    }

}
