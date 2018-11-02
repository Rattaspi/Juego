using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerAvatarController : MonoBehaviour {

    [SerializeField] float jumpForce;
    [SerializeField] Transform spawn;
    RunnerLogic rl;

    bool ground;
    bool inputBlocked;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        ground = true;
        inputBlocked = true;
        transform.position = spawn.position;

        rb = GetComponent<Rigidbody2D>();
        rl = GetComponentInParent<RunnerLogic>();
        if (rl == null) Debug.LogError("Runner logic not referenced in Runner avatar controller");
	}

    private void FixedUpdate() {
        if (inputBlocked) return;
        float jump = Input.GetAxis("Jump");

        if (jump != 0.0f && ground){
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            ground = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "ground") {
            ground = true;
        }else if(collision.gameObject.tag == "kill") {
            rl.Restart();
            transform.position = spawn.position;
        }
    }

    public void SetInputBlocked(bool b) {
        inputBlocked = b;
    }
}
