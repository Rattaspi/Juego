using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class GameTime {
    public static bool isPaused;
    public static float deltaTime { get { return isPaused ? 0 : Time.deltaTime; } }
    public static void Pause() {
        isPaused = !isPaused;
    }
}

public class GameLogic : MonoBehaviour {

    //Singleton
    public enum GameState { EVENT, WEEK, ENDWEEK, WEEKSTART, MINIGAME };
    public static GameLogic instance;
    public GameState gameState;
    public float money, reputation;
    public int currentWeek, amountOfFood, foodCapacity, maxAnimalCapacity, currentAnimalCapacity;
    public List<Animal> shelterAnimals;
    public List<Event> incomingEvents;
    public int currentEventIndex;
    public GameObject animalObjectParent;
    public float foodPrice;
    public float publictyPrice;
    public float cleanUpCost;

    public ToggleScript.ToggleType foodToBuy;
    public ToggleScript.ToggleType expensesToPay;
    public ToggleScript.ToggleType publicityToInvest;
    public ToggleScript.ToggleType cleanupToDo;
    public OriginDataBase originDataBase;

    [SerializeField]
    List<Instalation> instalations;
    public float maxTimeOfEntry;
    public float timeOfEntry;

    public bool CanEndWeek() {
        return timeOfEntry >= maxTimeOfEntry;
    }

    void Awake() {
        if (instance == null) {
            instance = this;
            StartCoroutine(SetAnimalObjectParentToCanvas());
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    IEnumerator SetAnimalObjectParentToCanvas() {
        while (CanvasScript.canvasScript == null) {
            yield return null;
        }
        animalObjectParent = new GameObject();
        animalObjectParent.name = "Animals";
        animalObjectParent.transform.parent = CanvasScript.canvasScript.transform;
    }

    void Start() {
        //Until we start saving/loading file (it is coded, but not doing it yet) we start variable here
        StartVariables();
        currentEventIndex = 0;
        foodPrice = 1.0f;
        publictyPrice = 1.0f;
        cleanUpCost = 1.0f;
        maxTimeOfEntry = 60;
        timeOfEntry =  0;
    }

    

    void Update() {

        if (CanvasScript.canvasScript != null) {

            switch (gameState) {
                case GameState.WEEKSTART:
                    //CanvasScript.canvasScript.DisplayIncomingAnimals();
                    gameState = GameState.WEEK;
                    timeOfEntry = 0;

                    break;
                case GameState.WEEK:
                    //Here we'd set the entrance and management of clients

                    if (timeOfEntry < maxTimeOfEntry) {
                        timeOfEntry += GameTime.deltaTime;
                        if (timeOfEntry > maxTimeOfEntry) {
                            timeOfEntry = maxTimeOfEntry;
                        }
                    }
                        break;
                case GameState.EVENT:
                    if (currentEventIndex < incomingEvents.Count) {
                        if (!IsDisplayingCurrentEvent(incomingEvents[currentEventIndex])) {
                            DisplayCurrentEvent();
                        } else {
                        }
                    } else {
                        CanvasScript.canvasScript.StopEvent();
                        gameState = GameState.WEEKSTART;
                        currentEventIndex = 0;
                        incomingEvents.Clear();
                    }

                    break;
                case GameState.ENDWEEK:
                    ApplyWeekExpenses();
                    NewEvents();
                    gameState = GameState.EVENT;
                    break;
            }
        }
    }


    void NewEvents() {
        Event random = new PuppyBagEvent();
        instance.incomingEvents.Add(random);


        switch (currentWeek) {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            default:
                int randomInt = Random.Range(0, 10);
                switch (randomInt) {
                    case 0:
                        //Event random = new AnonymousDonerEvent();
                        //instance.incomingEvents.Add(random);
                        break;
                }
                break;
        }
    }

    void DisplayCurrentEvent() {
        //Here we need a reference to the CanvasScript, to set the current event to incomingEvents[currentIndex]
        CanvasScript.canvasScript.DisplayEvent(incomingEvents[currentEventIndex]);
    }

    bool IsDisplayingCurrentEvent(Event e) {
        //Here we need a reference to the CanvasScript, to check wether the current event is the one in the parameter
        return CanvasScript.canvasScript.currentDisplayedEvent == e;
    }

    public float GetToggleOptionsMoney() {
        float totalExpense = 0;
        switch (foodToBuy) {
            case ToggleScript.ToggleType.NONE:
                break;
            case ToggleScript.ToggleType.SMALL:
                totalExpense += foodPrice * 50.0f;
                break;
            case ToggleScript.ToggleType.MEDIUM:
                totalExpense += foodPrice * 100.0f;

                break;
            case ToggleScript.ToggleType.BIG:
                totalExpense += foodPrice * 150.0f;

                break;
        }

        switch (publicityToInvest) {
            case ToggleScript.ToggleType.NONE:

                break;
            case ToggleScript.ToggleType.SMALL:
                totalExpense += publictyPrice * 150.0f;

                break;
            case ToggleScript.ToggleType.MEDIUM:
                totalExpense += publictyPrice * 150.0f;

                break;
            case ToggleScript.ToggleType.BIG:
                totalExpense += publictyPrice * 150.0f;

                break;
        }

        switch (cleanupToDo) {
            case ToggleScript.ToggleType.NONE:
                totalExpense += cleanUpCost;
                break;
            default:
                break;
        }

        switch (expensesToPay) {
            case ToggleScript.ToggleType.NONE:
                break;
            default:
                totalExpense += GetInstalationsUpKeep();
                break;
        }

        return totalExpense;
    }

    //This method apply the total amount of food and money expenses to the resources
    public void ApplyWeekExpenses() {
        float moneyExpense = GetToggleOptionsMoney();
        money -= (moneyExpense);
    }

    //This method changes the GameState
    public void EndWeek() {
        gameState = GameState.ENDWEEK;
        currentWeek++;
    }

    //This method calculates the expenses generated by instalations
    float GetInstalationsUpKeep() {
        float temp = 0;
        foreach (Instalation i in instalations) {
            temp += i.GetUpKeep();
        }
        return temp;
    }



    #region File Permanency

    public void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        data.money = money;
        data.reputation = reputation;
        //data.publicityInversion = publicityInversion;
        data.currentWeek = currentWeek;
        data.amountOfFood = amountOfFood;
        data.foodCapacity = foodCapacity;
        data.maxAnimalCapacity = maxAnimalCapacity;
        data.currentAnimalCapacity = currentAnimalCapacity;
        data.instalations = instalations;
        data.animals = shelterAnimals;
        data.gameState = gameState;
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load() {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            money = data.money;
            reputation = data.reputation;
            //publicityInversion = data.publicityInversion;
            currentWeek = data.currentWeek;
            amountOfFood = data.amountOfFood;
            foodCapacity = data.foodCapacity;
            maxAnimalCapacity = data.maxAnimalCapacity;
            currentAnimalCapacity = data.currentAnimalCapacity;
            instalations = data.instalations;
            shelterAnimals = data.animals;
            gameState = data.gameState;
        } else {
            StartVariables();
            Save();
        }
    }

    void StartVariables() {
        money = 1000;
        reputation = 0;
        //publicityInversion = 0;
        currentWeek = 0;
        amountOfFood = 100;
        foodCapacity = 300;
        maxAnimalCapacity = 20;
        currentAnimalCapacity = 10;
        instalations = new List<Instalation>();
        gameState = GameState.WEEK;
        timeOfEntry = 0;
        incomingEvents = new List<Event>();
        shelterAnimals = new List<Animal>();
        currentEventIndex = 0;
    }

    public class PlayerData {
        public float money, reputation, publicityInversion;
        public int currentWeek, amountOfFood, foodCapacity, maxAnimalCapacity, currentAnimalCapacity;
        public List<Instalation> instalations;
        public List<Animal> animals;
        public GameState gameState;
    };
    #endregion
}
