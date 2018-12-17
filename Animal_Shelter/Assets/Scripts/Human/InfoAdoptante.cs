using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoAdoptante : MonoBehaviour {
    Adoptante adoptante;
    Canvas canvas;
    GraphicsAdoptante graphics;

    [SerializeField] Button rechazar;

    [Header("Info to display")]
    [SerializeField] TextMeshProUGUI adopterName;
    [SerializeField] Image adopterDisplayImage;
    [SerializeField] TextMeshProUGUI specieText;
    [SerializeField] TextMeshProUGUI sizeText;
    [SerializeField] TextMeshProUGUI edadText;

    private void Awake() {
        adoptante = GetComponentInParent<Adoptante>();
        if (adoptante == null) Debug.LogError("Adoptante class not found from " + this.gameObject.name + " gameobject");

        canvas = GetComponentInParent<Canvas>();
        if (canvas == null) Debug.LogError("Canvas not found from " + this.gameObject.name + " gameobject");

        graphics = GetComponentInParent<GraphicsAdoptante>();
        if (graphics == null) Debug.LogError("GraphicsAdoptante class not found from " + this.gameObject.name + " gameobject");
    }

    void Start () {
        adopterDisplayImage.sprite = graphics.adopterImage;
        adopterName.text = adoptante.adopterName;

        specieText.text = adoptante.speciePreferred.ToString();

        switch (adoptante.sizePreferred) {
            case Animal.SIZE.BIG:
                sizeText.text = "GRANDE";
                break;

            case Animal.SIZE.MEDIUM:
                sizeText.text = "MEDIANO";
                break;

            case Animal.SIZE.SMALL:
                sizeText.text = "PEQUEÑO";
                break;
        }

        edadText.text = adoptante.agePreferred.ToString();

        rechazar.onClick.AddListener(delegate {
            this.gameObject.SetActive(false);
            Destroy(adoptante.gameObject);
        });
	}

    private void OnEnable() {
        this.transform.parent = canvas.transform;
        this.transform.localPosition = Vector3.zero;
    }

    private void OnDisable() {
        this.transform.parent = graphics.transform;
    }
}
