using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinScreen : MonoBehaviour {
    [SerializeField] float timeUntilCredits = 5.0f;
    public TextMeshProUGUI mensaje;
    public float timer;
    public float nextMaxTime;
    int currentMessage;
    //public string[] messages = {
    //};
    bool calledFinish;
    public string[] arr2;



    // Use this for initialization
    void Start() {

        string msg1 = "Has asignado a " + GameLogic.instance.adoptedAnimalCounter.ToString() + " mascotas a nuevos hogares...";
        string msg2 = "Dejando fuera a " + GameLogic.instance.leftOutAnimalCounter.ToString() + " animales, de los cuales no supiste más...";
        string msg3 = "Sacrificaste a " + GameLogic.instance.sacrificedAnimalCounter.ToString() + " seres vivos...";
        string msg4 = "En el proceso obtuviste " + GameLogic.instance.totalObtainedMoney.ToString() + "euros, de formas más o menos lícitas...";

        arr2 = new string[] {
        "Has ganado...",
        "Pero, ¿a qué precio?",
        msg1,
        msg2,
        msg3,
        msg4,
        //"Has asignado a " + GameLogic.instance.adoptedAnimalCounter.ToString() + " mascotas a nuevos hogares...",
        //"Dejando fuera a " + GameLogic.instance.leftOutAnimalCounter.ToString() + " animales, de los cuales no supiste más...",
        //"Sacrificaste a " + GameLogic.instance.sacrificedAnimalCounter.ToString() + " seres vivos...",
        //"En el proceso obtuviste " + GameLogic.instance.totalObtainedMoney.ToString() + "euros, de formas más o menos lícitas...",
        "¿Podrías haberlo invertido mejor?",
        "¿Te habrían ayudado más donaciones?",
        "A las protectoras de animales del mundo real tambien...",
        "Cada año aparecen 137.000 animales nuevos en los refugios...",
        "Solo un 44% de ellos son adoptados...",
        "Al resto les esperan destinos distintos...",
        "Los animales no son juguetes...",
        "Los animales son dependientes...",
        "Adopta, no compres"
        };



        currentMessage = -1;
        SetNextMessage();
    }

    // Update is called once per frame
    void Update() {

        if (currentMessage < arr2.Length) {

            timer += Time.unscaledDeltaTime;

            if (timer > nextMaxTime) {
                FaderScript.instance.StartFade(true);
            }

            if (FaderScript.instance.finishFlag) {
                FaderScript.instance.finishFlag = false;
                SetNextMessage();
            }
        } else if (!calledFinish) {
            calledFinish = true;
            StartCoroutine(LoadSceneAndWait());
        }
    }

    void SetNextMessage() {
        currentMessage++;
        nextMaxTime = (float)arr2[currentMessage].Length / 10 + 3;
        mensaje.text = arr2[currentMessage];
        FaderScript.instance.UnFade(false);
    }

    IEnumerator LoadSceneAndWait() {
        AsyncOperation op = SceneManager.LoadSceneAsync("Credits");
        op.allowSceneActivation = false;

        yield return new WaitForSeconds(timeUntilCredits);
        FaderScript.instance.fade = true;
        yield return new WaitForSeconds(1.0f);
        while (FaderScript.instance.doing) yield return null;

        op.allowSceneActivation = true;
    }

}
