using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour {

    public float speed = 20;
    public float speedVariation = 5;
    public float directionChangeInterval = 2;
    public float maxHeadingChange = 45;

    [SerializeField] Vector3 direction;

    void Awake() {
        direction = new Vector3(0, Random.Range(0, 360), 0);
        speed = Random.Range(speed - speedVariation, speed + speedVariation);
        StartCoroutine(NewHeading());
    }

    void Update() {
        this.transform.position += (direction.normalized * speed * GameTime.deltaTime);
    }

    IEnumerator NewHeading() {
        while (true) {
            NewHeadingRoutine();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    void NewHeadingRoutine() {
        int heading = Random.Range((int)-maxHeadingChange, (int)maxHeadingChange);
        direction = Quaternion.Euler(0.0f, 0.0f, heading) * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "animal_bounds") {
            direction = -direction;
        }
    }
}
