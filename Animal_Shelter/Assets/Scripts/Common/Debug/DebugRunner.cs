using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS IS A DEBUG CLASS
public class DebugRunner : MonoBehaviour {
    [SerializeField] RunnerLogic runnerlogic;

	void Start () {
        runnerlogic.gameObject.SetActive(true);
        runnerlogic.Play(RunnerLogic.DIFFICULTY.HARD);
	}
	
	void Update () {
		
	}
}
