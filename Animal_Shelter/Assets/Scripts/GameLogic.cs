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
    public float gasPrice;
    public float publictyPrice;
    public float cleanUpCost;

    public ToggleScript.ToggleType foodToBuy;
    public ToggleScript.ToggleType expensesToPay;
    public ToggleScript.ToggleType publicityToInvest;
    public ToggleScript.ToggleType cleanupToDo;
    public ToggleScript.ToggleType searchAmount;
    public OriginDataBase originDataBase;

    [SerializeField]
    List<Instalation> instalations;
    public float maxTimeOfEntry;
    public float timeOfEntry;
    public float timeForNextAnimal;

    //Stores all the animal prefabs to avoid loading from resources every time.
    [HideInInspector] public GameObject[] animalGraphics;

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
        gasPrice = 1.0f;
        publictyPrice = 1.0f;
        cleanUpCost = 100.0f;
        maxTimeOfEntry = 60;
        timeOfEntry =  0;
        timeForNextAnimal = 0;
        int timeToAdd = Random.Range(3, 9);
        timeForNextAnimal += timeToAdd;
        Instalation baseInstalation = new Instalation(100,"Base");
        instalations.Add(baseInstalation);
        cleanupToDo = ToggleScript.ToggleType.BIG;
        expensesToPay = ToggleScript.ToggleType.BIG;

        //Array which stores all the animal graphics prefabs
        //They are stored in the enum order so you can access it using the enum integer
        //EG. animalGraphics[(int)Animal.ESPECIE.GATO] --> it returns the cat graphics prefab
        animalGraphics = new GameObject[(int)Animal.ESPECIE.LENGTH];
        for(int i = 0; i < (int)Animal.ESPECIE.LENGTH; i++) {
            switch ((Animal.ESPECIE)i) {
                case Animal.ESPECIE.GATO:
                    animalGraphics[i] = Resources.Load<GameObject>("Prefabs/Animals/Gato");
                    break;

                case Animal.ESPECIE.HAMSTER:
                    animalGraphics[i] = Resources.Load<GameObject>("Prefabs/Animals/Hamster");
                    break;

                case Animal.ESPECIE.KOALA:
                    animalGraphics[i] = Resources.Load<GameObject>("Prefabs/Animals/Koala");
                    break;

                case Animal.ESPECIE.NARVAL:
                    animalGraphics[i] = Resources.Load<GameObject>("Prefabs/Animals/Narval");
                    break;

                case Animal.ESPECIE.PALOMA:
                    animalGraphics[i] = Resources.Load<GameObject>("Prefabs/Animals/Paloma");
                    break;

                case Animal.ESPECIE.PERRO:
                    animalGraphics[i] = Resources.Load<GameObject>("Prefabs/Animals/Perro");
                    break;

                case Animal.ESPECIE.TIGRE:
                    animalGraphics[i] = Resources.Load<GameObject>("Prefabs/Animals/Tigre");
                    break;

                default:
                    animalGraphics[i] = Resources.Load<GameObject>("Prefabs/Animals/Perro");
                    break;
            }

        }
    }

    

    void Update() {

        if (CanvasScript.canvasScript != null) {

            switch (gameState) {
                case GameState.WEEKSTART:
                    //CanvasScript.canvasScript.DisplayIncomingAnimals();
                    gameState = GameState.WEEK;
                    timeOfEntry = 0;
                    timeForNextAnimal = 0;
                    break;
                case GameState.WEEK:
                    //Here we'd set the entrance and management of clients

                    if (timeOfEntry < maxTimeOfEntry) {
                        timeOfEntry += GameTime.deltaTime;
                        if(timeOfEntry > timeForNextAnimal) {
                            int timeToAdd = Random.Range(3,9);
                            timeForNextAnimal += timeToAdd;
                            CanvasScript.canvasScript.AddEnteringAnimal();
                        }
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
        //Event random = new PuppyBagEvent();
        //instance.incomingEvents.Add(random);


        switch (currentWeek) {
            case 0:

                break;
            case 1:
                Event donationEvent = new TownHallMoneyEvent();
                instance.incomingEvents.Add(donationEvent);
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                CardMiniGame_Logic.cardLogic.Play(RunnerLogic.DIFFICULTY.EASY);
                break;
            case 5:
                //Evento Positivo
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                //RunnerPlay
                break;
            case 9:
                //Evento aleatorio
                break;
            case 10:
                break;
            case 11:
                //Evento positivo
                break;
            case 12:
                //PouGame Play
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                //Evento Aleatorio
                break;
            case 16:
                //MiniJuego Tres Carriles
                break;
            case 17:
                //Evento Aleatorio
                break;
            case 18:
                //Evento Aleatorio
                break;
            case 19:
                break;
            case 20:
                CardMiniGame_Logic.cardLogic.Play(RunnerLogic.DIFFICULTY.NORMAL);

                break;
            case 21:
                //Event donationEvent = new TownHallMoneyEvent();
                //instance.incomingEvents.Add(donationEvent);

                break;
            case 22:
                //Evento negativo
                break;
            case 23:
                //Evento Aleatorio
                break;
            case 24:
                //Runner II
                break;
            case 25:
                break;
            case 26:
                break;
            case 27:
                break;
            case 28:
                //Pou II 
                break;
            case 29:
                //Evento negativo
                break;
            case 30:
                break;
            case 31:
                //Evento Aleatorio
                break;
            case 32:
                //3 Carriles
                break;
            case 33:

                break;
            case 34:
                //Evento positivo
                break;
            case 35:
                //Evento negativo
                break;
            case 36:
                CardMiniGame_Logic.cardLogic.Play(RunnerLogic.DIFFICULTY.HARD);
                break;
            case 37:
                //Evento Aleatorio
                break;
            case 38:
                //Evento Negativo
                break;
            case 39:
                //Evento Negativo
                break;
            case 40:
                //Runner III
                break;
            case 41:
                break;
            case 42:
                break;
            case 43:
                break;
            case 44:
                //Pou III
                break;
            case 45:
                //Evento Negativo
                break;
            case 46:
                //Evento Negativo
                break;
            case 47:
                //Evento Negativo
                break;
            case 48:
                //3 Carriles III
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
                totalExpense += publictyPrice * 50.0f;
                break;
            case ToggleScript.ToggleType.MEDIUM:
                totalExpense += publictyPrice * 100.0f;
                break;
            case ToggleScript.ToggleType.BIG:
                totalExpense += publictyPrice * 150.0f;
                break;
        }

        switch (cleanupToDo) {
            case ToggleScript.ToggleType.NONE:
                break;
            default:
                totalExpense += cleanUpCost;
                break;
        }

        switch (expensesToPay) {
            case ToggleScript.ToggleType.NONE:
                break;
            default:
                totalExpense += GetInstalationsUpKeep();
                break;
        }

        switch (searchAmount) {
            case ToggleScript.ToggleType.NONE:

                break;
            case ToggleScript.ToggleType.SMALL:
                totalExpense += gasPrice * 50;

                break;
            case ToggleScript.ToggleType.MEDIUM:
                totalExpense += gasPrice * 100;

                break;
            case ToggleScript.ToggleType.BIG:
                totalExpense += gasPrice * 150;


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
