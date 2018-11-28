using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardMiniGame_Logic : MonoBehaviour {
    [HideInInspector]
    public static CardMiniGame_Logic cardLogic;
    public CardMiniGame_Card currentCard;
    public GameObject cardPrefab;
    CardMiniGame_Card card1, card2;
    public CardMiniGame_Card[] cards;
    public int score;
    public int tries;
    public int maxTries;
    public int numOfCards;
    public int cardsPerRow;
    Material backMaterial;
    bool play;
    RunnerLogic.STATE state;
    RunnerLogic.DIFFICULTY difficulty;
    public Canvas overallCanvas;
    public Canvas personalCanvas;
    public bool playButton;
    bool wasSetUp;
    public Text triesText;
    // Use this for initialization

    public void Play(RunnerLogic.DIFFICULTY diff) {
        wasSetUp = false;
        state = RunnerLogic.STATE.START;
        difficulty = diff;
        play = true;
        personalCanvas.gameObject.SetActive(true);
        overallCanvas.gameObject.SetActive(false);
    }

    private void Awake() {
        if (cardLogic == null) {
            cardLogic = this;
        } else {
            Destroy(this);
        }
    }


    void FindFreeId(CardMiniGame_Card card) {
        bool found = false;
        int controlInt = 0;
        while (!found && controlInt < 200) {
            int randomNumber = Random.Range(0, numOfCards);
            if (cards[randomNumber].sister == null && card != cards[randomNumber]) {
                found = true;
                cards[randomNumber].sister = card;
                card.sister = cards[randomNumber];
            }
            controlInt++;
        }
    }


    void SetUpCards() {

        if ((cards)!= null){
            if (cards.Length > 0) {
                for(int i = 0; i < cards.Length; i++) {
                    if (cards[i] != null) {
                        Destroy(cards[0].gameObject);
                    }
                }
            }
        }

        cards = new CardMiniGame_Card[numOfCards];

        for (int i = 0; i < numOfCards; i++) {
            if (i < cardsPerRow) {
                Vector3 tempPos = new Vector3(-7.5f + i * 3, 2f, -1);
                cards[i] = Instantiate(cardPrefab, tempPos, Quaternion.identity).GetComponent<CardMiniGame_Card>();
            } else {
                Vector3 tempPos = new Vector3(-7.5f + i % 6 * 3, -2.5f, -1);
                cards[i] = Instantiate(cardPrefab, tempPos, Quaternion.identity).GetComponent<CardMiniGame_Card>();
            }
            cards[i].gameObject.GetComponent<MeshRenderer>().material = backMaterial;
        }

        for (int i = 0; i < numOfCards; i++) {
            if (cards[i].GetComponent<CardMiniGame_Card>().sister == null) {
                FindFreeId(cards[i]);
                //Color tempColor;

                Material tempMaterial = Resources.Load<Material>("Materials/CardBackMaterial");

                int randomMaterial = Random.Range(0, 4);

                float randomColorR = Random.Range(50, 250);
                float randomColorG = Random.Range(50, 250);
                float randomColorB = Random.Range(50, 250);

                randomColorB = randomColorB / 255;
                randomColorR = randomColorR / 255;
                randomColorG = randomColorG / 255;

                switch (randomMaterial) {
                    case 0:
                        tempMaterial = new Material(Resources.Load<Material>("Materials/BearCardMaterial"));
                        tempMaterial.color = new Color(randomColorR, randomColorG, randomColorB);
                        break;
                    case 1:
                        tempMaterial = new Material(Resources.Load<Material>("Materials/DeerCardMaterial"));
                        tempMaterial.color = new Color(randomColorR, randomColorG, randomColorB);

                        break;
                    case 2:
                        tempMaterial = new Material(Resources.Load<Material>("Materials/ElephantCardMaterial"));
                        tempMaterial.color = new Color(randomColorR, randomColorG, randomColorB);

                        break;
                    case 3:
                        tempMaterial = new Material(Resources.Load<Material>("Materials/GiraffeCardMaterial"));
                        tempMaterial.color = new Color(randomColorR, randomColorG, randomColorB);

                        break;
                    case 4:
                        tempMaterial = new Material(Resources.Load<Material>("Materials/PandaCardMaterial"));
                        tempMaterial.color = new Color(randomColorR, randomColorG, randomColorB);

                        break;
                }

                //cards[i].gameObject.GetComponentInChildren<MeshRenderer>().material = cards[i].sister.gameObject.GetComponentInChildren<MeshRenderer>().material = tempMaterial;
                //Debug.Log(cards[i].gameObject.GetComponentInChildren<EmptyScript>());


                cards[i].gameObject.GetComponentInChildren<EmptyScript>().gameObject.GetComponent<MeshRenderer>().material = tempMaterial;
                cards[i].sister.gameObject.GetComponentInChildren<EmptyScript>().gameObject.GetComponent<MeshRenderer>().material = tempMaterial;

                //cards[i].gameObject.GetComponentInChildren<EmptyScript>().gameObject.GetComponent<MeshRenderer>().material.color = new Color(randomColorR, randomColorG, randomColorB);
                //cards[i].sister.gameObject.GetComponentInChildren<EmptyScript>().gameObject.GetComponent<MeshRenderer>().material.color = new Color(randomColorR, randomColorG, randomColorB);


            }
        }

    }

    void Start() {
        backMaterial = Resources.Load<Material>("Materials/CardBackMaterial");

        cardPrefab = Resources.Load<GameObject>("Prefabs/CardPrefab");
    }

    void CheckCards() {
        tries++;
        if (card1.sister == card2) {
            score += card1.cardValue;
        } else {
            card1.clicked = card1.highlighted = false;
            card2.clicked = card1.highlighted = false;
        }
        card1 = null;
        card2 = null;

        if (IsGameOver()) {
            state = RunnerLogic.STATE.END;
        }

    }

    bool IsGameOver() {
        bool allCorrect=true;
        for(int i = 0; (i < cards.Length&&allCorrect); i++) {
            if (!cards[i].clicked) {
                allCorrect = false;
            }
        }
        return (tries >= maxTries||allCorrect);
    }

    // Update is called once per frame
    void Update() {

        if (playButton) {
            playButton = false;
            Play(RunnerLogic.DIFFICULTY.EASY);

        }
        if (!play) return;
        else {
            switch (state) {
                case RunnerLogic.STATE.START:
                    if (!wasSetUp) {
                        if (overallCanvas != null) {
                            overallCanvas.gameObject.SetActive(false);
                        }
                        tries = 0;
                        score = 0;
                        score = tries = 0;
                        numOfCards = 12;
                        cardsPerRow = 6;
                        SetUpCards();
                        wasSetUp = true;
                    }

                    if (Input.GetMouseButtonDown(0)) {
                        state = RunnerLogic.STATE.GAME;

                        switch (difficulty) {
                            case RunnerLogic.DIFFICULTY.EASY:
                                maxTries = 10;
                                break;
                            case RunnerLogic.DIFFICULTY.NORMAL:
                                maxTries = 8;
                                break;
                            case RunnerLogic.DIFFICULTY.HARD:
                                maxTries = 6;
                                break;
                        }
                    }

                    break;
                case RunnerLogic.STATE.GAME:
                    triesText.text = "Intentos: " + tries + "/" + maxTries;
                    if (card2 != null) {
                        if (card2.rotated) {
                            CheckCards();
                        }
                    }
                    if (Input.GetMouseButtonDown(0)) {
                        if (card1 == null || card2 == null) {
                            if (currentCard != null) {
                                if (!currentCard.clicked) {
                                    currentCard.clicked = true;
                                    currentCard.rotated = false;
                                    currentCard.sigma2 = 0;
                                    if (card1 == null) {
                                        card1 = currentCard;
                                    } else if (currentCard != card1) {
                                        card2 = currentCard;
                                    }
                                }
                            }
                        }
                    }

                    break;
                case RunnerLogic.STATE.END:
                    overallCanvas.gameObject.SetActive(true);
                    GameLogic.instance.gameState = GameLogic.GameState.WEEKSTART;
                    personalCanvas.gameObject.SetActive(false);
                    break;
            }
        }
    }


}
