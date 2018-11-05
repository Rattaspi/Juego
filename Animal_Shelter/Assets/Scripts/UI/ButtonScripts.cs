using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScripts : MonoBehaviour {

    public enum ButtonFunction { ENDWEEK,PAUSE };
    public ButtonFunction buttonFunction;
    private Button myselfButton;

    void Start() {
        myselfButton = GetComponent<Button>();
        switch (buttonFunction) {
            case ButtonFunction.ENDWEEK:
                myselfButton.onClick.AddListener(() => EndWeek());
                break;
            case ButtonFunction.PAUSE:

                break;
            default:
                break;
        }
        
    }

    void EndWeek() {
        if(GameLogic.instance != null) {
            GameLogic.instance.EndWeek();
        }
    }


}
