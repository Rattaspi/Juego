using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouAvatarController : MonoBehaviour {

    [SerializeField] float movementForce=20.0f;
    public Vector3 spawn;
    PouLogic pl;

    bool inputBlocked;
    Rigidbody2D rb;

    // Use this for initialization
    void Start() {
        //inputBlocked = true;
        movementForce = 20.0f;
        spawn = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rb = GetComponent<Rigidbody2D>();
        pl = GetComponentInParent<PouLogic>();
        pl.pouAvatar = this;
        if (pl == null) Debug.LogError("Pou logic not referenced in Pou avatar controller");
    }

    private void FixedUpdate() {
        if (inputBlocked) return;
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0) {
            if (horizontal > 0) {
                rb.AddForce(Vector2.right * movementForce, ForceMode2D.Impulse);
            } else{ 
                rb.AddForce(Vector2.left * movementForce, ForceMode2D.Impulse);
            }
        } else {
            if (Mathf.Abs(rb.velocity.x) > 0.3f) {
                rb.AddForce(new Vector2(-rb.velocity.x * 0.5f, 0), ForceMode2D.Impulse);
            } else {
                rb.velocity = new Vector2(0, 0);
            }
        }
    }

    public void SetInputBlocked(bool b) {
        inputBlocked = b;
    }

    

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "kill") {
            pl.LifeDown();
        } else if (collider.gameObject.tag == "food") {
            pl.Eat(collider.gameObject);
        }
    }

}
