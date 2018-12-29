using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WinScreenName : MonoBehaviour {
    public TextMeshProUGUI nombre;
    bool unFading;
    bool finishFlag;
    Color localColor;

    public void StartUp(string name) {
        nombre = GetComponent<TextMeshProUGUI>();
        nombre.text = name;
        localColor = new Color(nombre.color.r, nombre.color.g, nombre.color.b, 0);
        nombre.color = localColor;
        unFading = true;
        finishFlag = false;
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (unFading) {
            Debug.Log(localColor.a);
            if (localColor.a < 0.75f) {
                localColor.a += Time.deltaTime;
                nombre.color = localColor;
            } else if (!finishFlag) {
                finishFlag = true;
                unFading = false;
                localColor.a = 0.75f;
                nombre.color = localColor;
                StartCoroutine(SelfDestroy());
            }
        }
    }

    IEnumerator SelfDestroy() {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);

    }
}
