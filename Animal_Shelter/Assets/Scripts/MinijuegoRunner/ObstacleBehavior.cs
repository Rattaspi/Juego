using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour {

    [SerializeField] float speed;
    ObstaclesManager om;
	
	void Update () {
        transform.localPosition -= new Vector3(speed, 0.0f, 0.0f) * Time.deltaTime;

        //left camera localposition bound -1050
        if (transform.localPosition.x < -1050) {
            om.DestroyFromList(this.gameObject);
            Destroy(this.gameObject);
        }
	}

    public void Init(ObstaclesManager om) {
        this.om = om;
    }
}
