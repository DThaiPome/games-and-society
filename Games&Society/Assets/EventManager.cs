using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This game is going to be time based so we can have events like that here or something
public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private event Action onTickEvent;
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

    //Register a method with onTickEvent
    public void registerToTickEvent(Action a)
    {
        onTickEvent += a;
    }

    //Unregister a method with onTickEvent
    public void unregisterToTickEvent(Action a)
    {
        onTickEvent -= a;
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
