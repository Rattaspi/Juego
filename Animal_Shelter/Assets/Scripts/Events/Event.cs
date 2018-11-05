public class Event {
    protected string description;
    protected bool canBeDenied;

    public Event(string description, bool d) {
        this.description = description;
        this.canBeDenied = d;
    }

    public string GetDescription() {
        return description;
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

    public Event() {

    }

    public virtual void OnAccept() {
        GameLogic.instance.currentEventIndex++;
    }

    public virtual void OnDeny() {
        GameLogic.instance.currentEventIndex++;
    }

}
