using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "OriginDatabase.asset", menuName = "AnimalShelter/OriginDataBase")]
public class OriginDataBase : ScriptableObject {

    public List<string> originStart;
    public List<string> originMiddle;
    public List<string> originEnd;

    public string GetRandomOrigin() {
        string temp ="";
        int randomInt = Random.Range(0, originStart.Count - 1);
        temp += originStart[randomInt];
        randomInt = Random.Range(0, originMiddle.Count - 1);
        temp += " " + originMiddle[randomInt];
        randomInt = Random.Range(0, originEnd.Count - 1);
        temp += " " + originEnd[randomInt];

        return temp;
    }

}
