using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public event Action ScrollMapActiveEvent;
    public event Action Map2RuneActiveEvent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void TriggerScrollMapActive()
    {
        ScrollMapActiveEvent?.Invoke();
    }
    public void TriggerMap2RuneActive()
    {
        Map2RuneActiveEvent?.Invoke();
    }
}
