﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adoptante : MonoBehaviour {
    Canvas canvas;
    GameLogic gamelogic;
    [Header("Adopter preferences")]
    [SerializeField] Animal.SIZE sizePreferred;
    [SerializeField] Animal.ESPECIE speciePreferred;

    private void Awake() {
        gamelogic = FindObjectOfType<GameLogic>();
    }

    void Start () {
        canvas = GetComponentInParent<Canvas>();
        this.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);

        this.gameObject.AddComponent(typeof(MovementAdoptante));

        CircleCollider2D circleCol = (CircleCollider2D) this.gameObject.AddComponent(typeof(CircleCollider2D));
        circleCol.radius = 150.0f;

        GameObject g = Resources.Load<GameObject>("Prefabs/Humans/HumanGraphics");
        g = Instantiate(g, this.transform);
        g.name = "Graphics";

        //Initialize adopter preferences
        sizePreferred = (Animal.SIZE)Random.Range(0, (int)Animal.SIZE.LENGTH);
        if(gamelogic.shelterAnimals.Count > 0) {
            float rng = Random.Range(0.0f, 1.0f);
            if(rng < 0.6f) {
                int id = Random.Range(0, gamelogic.shelterAnimals.Count);
                speciePreferred = gamelogic.shelterAnimals[id].especie;
            }
            else {
                speciePreferred = (Animal.ESPECIE)Random.Range(0, (int) Animal.ESPECIE.LENGTH);
            }
        }
        else {
            speciePreferred = (Animal.ESPECIE)Random.Range(0, (int)Animal.ESPECIE.LENGTH);
        }

        //ERROR COMPROVATION
        if (this.transform.parent.transform.position != Vector3.zero) {
            Debug.LogWarning("The parent of the Adopter object is not on (0,0,0). It might cause errors");
        }
    }
}
