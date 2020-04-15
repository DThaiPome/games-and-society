using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This game is going to be time based so we can have events like that here or something
public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    //** MAIN EVENTS
    //Tick event
    public event Action onTickEvent;
    //Do something when clicked
    public event Action<Transform> onClickEvent;

    //**LIGHT SWITCH PUZZLE EVENTS
    //Whenever a switch is changed
    public event Action<bool> onLightPuzzleSwitchEvent;

    private float deltaTime;

    //Invokes the event every ticksPerSecond
    private void updateTick()
    {
        float ticksPerSecond = 1;
        this.deltaTime += Time.deltaTime;
        if (this.deltaTime >= ticksPerSecond)
        {
            this.deltaTime = 0;
            if (onTickEvent != null)
            {
                onTickEvent();
            }
        }
    }

    public void onClick(Transform transform)
    {
        if (onClickEvent != null)
        {
            onClickEvent(transform);
        }
    }

    public void onLightPuzzleSwitch(bool newState)
    {
        if (onLightPuzzleSwitchEvent != null)
        {
            onLightPuzzleSwitchEvent(newState);
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

    void Update()
    {
        this.updateTick();
    }

    private void testTicker()
    {
        Debug.Log("E");
    }
}
