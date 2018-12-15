using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Waypoint
{
    public float transTime;
    public EventType eventType;

    public Waypoint(float _transTime, EventType _eventType)
    {
        transTime = _transTime;
        eventType = _eventType;
    }
}

// Enum of event types
public enum EventType
{
    None,
    Dismount,
    Wave,
    WaveAndPre,
    Wait,
    End
}
