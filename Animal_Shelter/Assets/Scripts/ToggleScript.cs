using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleScript : MonoBehaviour {

    public enum ToggleType { NONE, SMALL, MEDIUM, BIG };
    public enum ToggleTheme { CLEANUP, FOOD, PUBLICITY, EXPENSES };

    public ToggleTheme toggleTheme;
    public ToggleType toggleType;
    Toggle toggle;
    private void Start() {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate {
            TellGameLogic();
        });
        //adoptButton.onClick.AddListener(() => AddAnimal(animal));

    }

    public void TellGameLogic() {
        if (toggle.isOn) {
            switch (toggleTheme) {
                case ToggleTheme.CLEANUP:
                    GameLogic.instance.cleanupToDo = toggleType;
                    break;
                case ToggleTheme.FOOD:
                    GameLogic.instance.foodToBuy = toggleType;
                    break;
                case ToggleTheme.PUBLICITY:
                    GameLogic.instance.publicityToInvest = toggleType;
                    break;
                case ToggleTheme.EXPENSES:
                    GameLogic.instance.expensesToPay = toggleType;
                    break;
            }
            Debug.Log("Told");
        }
    }

}
