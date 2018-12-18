using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FaderScript : MonoBehaviour {
    public static FaderScript instance;
    public bool doing;
    public Color localColor;
    public RawImage image;
    public bool fade;
    public bool unFade;
    //[ExecuteInEditMode]
    //private void OnValidate() {
    //    Debug.Log(image.color);
    //}

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    // Use this for initialization
    void Start() {
        image = GetComponent<RawImage>();
        doing = false;
        localColor = image.color;
        localColor.a = 1.0f;
        image.color = localColor;
        //StartCoroutine(UnFade());
    }

    private void Update() {
        if (fade) {
            StartCoroutine(Fade());
            fade = false;
        }
        if (unFade) {
            StartCoroutine(UnFade());
            unFade = false;
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.I)) {
            print("FADE");
            fade = true;
        } else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.U)){ 
            unFade = true;
            print("UNFADE");
        }
    }

    public void Pause() {
        if (localColor.a != 0.3f) {
            localColor.a = 0.3f;
        }
    }

    public IEnumerator Fade() {
        image.raycastTarget = true;
        if (doing) {
            yield return null;
        } else {
            doing = true;
            while (image.color.a < 1) {
                localColor.a += Time.deltaTime*2;
                image.color = localColor;
                yield return null; //yield return new WaitForSeconds(0.1f);
            }
            if (image.color.a > 1) {
                localColor.a = 1;
                image.color = localColor;
            }
            doing = false;
        }
    }

    public IEnumerator UnFade() {
        if (doing) {
            yield return null;
        } else {
            doing = true;
            while (image.color.a > 0) {
                localColor.a -= Time.deltaTime*2;
                image.color = localColor;
                yield return null;// new WaitForSeconds(0.1f);
            }
            if (image.color.a < 0) {
                localColor.a = 0;
                image.color = localColor;
            }
            doing = false;
            image.raycastTarget = false;
        }
    }

}
