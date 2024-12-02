using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public event Action ScrollMapActiveEvent;
    public event Action Map2RuneActiveEvent;
    public event Action Map2AppearMonsterActiveEvent;
    public event Action CameraMoveEvent;

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
    public void TriggerMap2AppearMonster()
    {
        Map2AppearMonsterActiveEvent?.Invoke();
    }
    public void TriggerCameraMove()
    {
        CameraMoveEvent?.Invoke();
    }
}
