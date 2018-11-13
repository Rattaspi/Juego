using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleRunLogic : MonoBehaviour {
    TempleRunAvatar avatar;
    TempleRunObstacleGen obstacleGen;

	void Start () {
        avatar = GetComponentInChildren<TempleRunAvatar>();
        if (avatar == null) Debug.LogError("TempleRunAvatar not found from TempleRunLogic");

        obstacleGen = GetComponentInChildren<TempleRunObstacleGen>();
        if (obstacleGen == null) Debug.LogError("TempleRunObstacleGen not found from TempleRunLogic");

        Play();
	}

    public void Play() {
        avatar.Play();
        obstacleGen.Play();

        print("Temple logic PLAY");
    }

    public void Stop() {
    }

}
