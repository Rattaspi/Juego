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
    bool namesFlag;
    GameObject dramaticNamePrefab;

    // Use this for initialization
    void Start() {
        dramaticNamePrefab = Resources.Load<GameObject>("Prefabs/WinScreenName");
        FaderScript.instance.StartUnfade(false);
        string msg1 = "Has asignado a " + GameLogic.instance.adoptedAnimalNames.Count.ToString() + " mascotas a nuevos hogares...";
        string msg2 = "Dejando fuera a " + GameLogic.instance.leftOutAnimalNames.Count.ToString() + " animales, de los cuales no supiste más...";
        string msg3 = "Sacrificaste a " + GameLogic.instance.sacrificedAnimalNames.Count.ToString() + " seres vivos...";
        string msg4 = "En el proceso obtuviste " + GameLogic.instance.totalObtainedMoney.ToString() + " euros, de formas más o menos lícitas...";

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
        "Adopta, no compres"
        };



        currentMessage = -1;
        SetNextMessage();
    }

    IEnumerator DisplaySomeNames(List<string> listOfNames) {
        int counter = 0;
        while (namesFlag && counter < listOfNames.Count) {
            GameObject aName = Instantiate<GameObject>(dramaticNamePrefab, gameObject.transform);
            WinScreenName winScreenName = aName.GetComponent<WinScreenName>();
            winScreenName.StartUp(listOfNames[counter]);
            float randomX, randomY;
            randomX = Random.Range(100, Screen.width - 100);

            int up = Random.Range(0, 2);
            if (up == 0) {
                randomY = Random.Range(100, Screen.height / 2 - 200);
            } else {
                randomY = Random.Range(Screen.height - 100, Screen.height / 2 + 200);
            }
            aName.GetComponent<RectTransform>().position = new Vector3(randomX, randomY, 0);
            aName.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 1), Random.Range(-90, 90));
            counter++;
            yield return new WaitForSeconds(0.2f);
        }
    }

    // Update is called once per frame
    void Update() {
        if (!namesFlag) {
            switch (currentMessage) {
                case 2:
                    if (timer >= nextMaxTime / 4) {
                        namesFlag = true;
                        StartCoroutine(DisplaySomeNames(GameLogic.instance.adoptedAnimalNames));
                    }
                    break;
                case 3:
                    if (timer >= nextMaxTime / 4) {
                        namesFlag = true;
                        StartCoroutine(DisplaySomeNames(GameLogic.instance.leftOutAnimalNames));
                    }
                    break;
                case 4:
                    if (timer >= nextMaxTime / 4) {
                        namesFlag = true;
                        StartCoroutine(DisplaySomeNames(GameLogic.instance.sacrificedAnimalNames));
                    }
                    break;
                case 5:
                    if (timer >= nextMaxTime / 4) {
                        namesFlag = true;
                        StartCoroutine(DisplaySomeNames(GameLogic.instance.deadAnimalNames));
                    }
                    break;
            }
        }

        if (currentMessage < arr2.Length) {
            timer += Time.unscaledDeltaTime;

            if (timer > nextMaxTime && !FaderScript.instance.doing) {
                timer = 0;
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
        FaderScript.instance.StartUnfade(false);
        Debug.Log("UnfadeStart");
        namesFlag = false;
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
