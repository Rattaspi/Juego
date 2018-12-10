using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAdoptante : MonoBehaviour {
    [SerializeField] int speed = 20;

    enum State {WAIT, MOVING};
    State state;

	void Start () {
        state = State.MOVING;

        Rigidbody2D rb = this.gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        this.transform.position = new Vector2(Random.Range(400, Screen.width - 200), -50);
	}
	
	void Update () {
        switch (state) {
            case State.WAIT:
                break;

            case State.MOVING:
                this.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
                break;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        print("trigger enter");
        if(collision.tag == "movement_bounds"){
            state = State.WAIT;
        }
    }


}
