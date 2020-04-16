﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This game is going to be time based so we can have events like that here or something
public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    //** MAIN EVENTS
    //Tick events
    [SerializeField]
    private GameObject tickEventPrefab;
    //Do something when clicked
    public event Action<Transform> onClickEvent;
    //Notices of changes in difficulty
    public event Action<float> onDifficultyChangedEvent;

    //** GAME TIME EVENTS
    public event Action<int, int, int> onMinuteEvent;

    //** LIGHT SWITCH PUZZLE EVENTS
    //Whenever a switch is changed
    public event Action<LightPuzzleNode> onLightPuzzleSwitchEvent;

    //** ASSIGNMENT EVENTS

    private float deltaTime;

    public TickEvent newTickEvent(int secondsPerTick)
    {
        GameObject g = UnityEngine.Object.Instantiate(this.tickEventPrefab);
        g.transform.SetParent(this.transform);
        TickEvent te = g.GetComponent<TickEvent>();
        te.init(secondsPerTick);
        return te;
    }

    public void onClick(Transform transform)
    {
        if (onClickEvent != null)
        {
            onClickEvent(transform);
        }
    }

    public void onLightPuzzleSwitch(LightPuzzleNode lpn)
    {
        if (onLightPuzzleSwitchEvent != null)
        {
            onLightPuzzleSwitchEvent(lpn);
        }
    }

    public void onMinute(int day, int minute, int minutesPerDay)
    {
        if (onMinuteEvent != null)
        {
            onMinuteEvent(day, minute, minutesPerDay);
        }
    }

    public void onDifficultyChanged(float difficulty)
    {
        if (onDifficultyChangedEvent != null)
        {
            onDifficultyChanged(difficulty);
        }
    }

    private void init()
    {
        this.deltaTime = 0;
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        this.init();
        //EventManager.instance.registerToTickEvent(testTicker);
    }

    private void testTicker()
    {
        Debug.Log("E");
    }
}
