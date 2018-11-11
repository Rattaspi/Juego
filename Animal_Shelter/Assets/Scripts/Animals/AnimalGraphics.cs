using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimalGraphics : MonoBehaviour {
    GameObject info; //object containing preview info and complete info

    [SerializeField] PreviewInfoAnimal previewInfo;
    [SerializeField] ShowInfoAnimal showInfo;

    void Start () {
        info = Resources.Load<GameObject>("Prefabs/AnimalInfoPreview");
        info = Instantiate(info, this.transform);
        
        previewInfo = GetComponentInChildren<PreviewInfoAnimal>();
        showInfo = GetComponentInChildren<ShowInfoAnimal>();

        info.SetActive(false);
    }

    public void ActivatePreview(bool b) {
        info.SetActive(b);
        if (b) {
            previewInfo.gameObject.SetActive(true);
            showInfo.gameObject.SetActive(false);
        }
    }

    public void ActivateInfo() {
        previewInfo.gameObject.SetActive(false);
        showInfo.gameObject.SetActive(true);
    }
}
