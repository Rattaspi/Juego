using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceBehavior : MonoBehaviour {

    [SerializeField] float speed = 500;
    ObstaclesManager om;
    TempleRunObstacleGen trog;
    TempleRunPriceGenerator trpg;

    void Update() {
        transform.localPosition -= new Vector3(speed, 0.0f, 0.0f) * Time.deltaTime;

        //left camera localposition bound -1050
        if (transform.localPosition.x < -1050) {
            if (om != null) om.DestroyFromList(this.gameObject);
            else if (trog != null) trog.DestroyFromList(this.gameObject);
            else if (trpg != null) trpg.DestroyFromList(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void Init(ObstaclesManager om) {
        this.om = om;
    }

    public void Init(TempleRunObstacleGen trog) {
        this.trog = trog;
    }

    public void Init(TempleRunPriceGenerator trog) {
        this.trpg = trpg;
    }
}
