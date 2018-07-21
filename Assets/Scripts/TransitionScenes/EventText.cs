using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventText : MonoBehaviour {

    [SerializeField]
    private string eventText, eventPrompt;

    [SerializeField]
    private EventOptionStats[] events;

    [SerializeField]
    private bool _isContinue;

    public string GetEventText()
    {
        return eventText;
    }

    public string GetEventPrompt()
    {
        return eventPrompt;
    }

    public bool isContinue()
    {
        return _isContinue;
    }

    public EventOptionStats GetEventOption(int i)
    {
        return events[i];
    }

}
