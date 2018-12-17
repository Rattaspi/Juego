using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GraphicsAdoptante : MonoBehaviour {
    [HideInInspector] public Sprite adopterImage;

	void Start () {
        GameObject humanSprite = new GameObject();
        humanSprite.name = "PjImage";
        humanSprite.transform.parent = this.transform;
        humanSprite.transform.localPosition = Vector2.zero;

        Image i = humanSprite.AddComponent<Image>();
        adopterImage = Resources.Load<Sprite>("Sprites/Humans/pj_" + Random.Range(1, 30));
        i.sprite = adopterImage;

        RectTransform rectTransform = humanSprite.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(250, 250);

        if (TutorialOverrider.instance != null) {
            EventTrigger trigger = GetComponentInParent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) => { TutorialOverrider.instance.GoToNextEvent(); });
            trigger.triggers.Add(entry);
        }

	}
}
