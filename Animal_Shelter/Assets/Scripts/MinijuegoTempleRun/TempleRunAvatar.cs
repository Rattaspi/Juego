using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TempleRunAvatar : MonoBehaviour {
    [SerializeField] Transform[] positions;
    int currentPos;
    public bool inputBlocked;
    TempleRunLogic templeLogic;

	void Awake () {
        currentPos = 1;
        this.transform.position = positions[currentPos].position;
        inputBlocked = false;
	}

    private void Start() {
        templeLogic = GetComponentInParent<TempleRunLogic>();
        if (templeLogic == null) Debug.LogError("TempleRunLogic not found from TempleRunAvatar");
    }

    void Update () {
        if (inputBlocked) return;

        //MOVEMENT
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            print("UP");
            currentPos--;
            if (currentPos < 0) currentPos = 0;
            this.transform.position = positions[currentPos].position;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            print("DOWN");
            currentPos++;
            if (currentPos > 2) currentPos = 2;
            this.transform.position = positions[currentPos].position;
        }
    }
    public void SetInputBlocked(bool b) {
        inputBlocked = b;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "food") {
            templeLogic.monedas++;
            templeLogic.priceGen.DestroyFromList(collision.gameObject);
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "kill") {
            templeLogic.Restart();
        }
    }
}
