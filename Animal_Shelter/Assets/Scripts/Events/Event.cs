﻿using UnityEngine;

public class Event {
    protected string title;
    protected string description;
    protected string acceptMessage;
    protected string declineMessage;

    protected bool canBeAccepted;
    protected bool canBeDenied;
    protected Texture2D associatedTexture;

    public Event(string description, bool d) {
        this.description = description;
        canBeDenied = d;
    }

    public Event() {
        acceptMessage = "Aceptar";
        declineMessage = "Declinar";
        canBeAccepted = true;
        canBeDenied = true;
    }

    public string GetTitle() {
        return title;
    }

    public void SetTitle(string t) {
        title = t;
    }

    public string GetDescription() {
        return description;
    }

    public bool GetCanBeAccepted() {
        return canBeAccepted;
    }

    public void SetCanBeAccepted(bool b) {
        canBeAccepted = b;
    }

    public bool GetCanBeDenied() {
        return canBeDenied;
    }

    public void SetDescription(string d) {
        description = d;
    }

    public void SetCanBeDenied(bool c) {
        canBeDenied = c;
    }

    public virtual void OnAccept() {
        GameLogic.instance.currentEventIndex++;
    }

    public virtual void OnDeny() {
        GameLogic.instance.currentEventIndex++;
    }

    public string GetDeclineText() {
        return declineMessage;
    }

    public string GetAcceptText() {
        return acceptMessage;
    }

}
