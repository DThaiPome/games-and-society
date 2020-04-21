using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TickEvent : MonoBehaviour
{
    public event Action tickEvent;

    private float deltaTime;
    private float secondsPerTick;
    private bool active;

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            this.updateTick();
        }
    }

    private void updateTick()
    {
        this.deltaTime += Time.deltaTime;
        if (this.deltaTime >= this.secondsPerTick)
        {
            while (this.deltaTime >= this.secondsPerTick)
            {
                this.deltaTime -= this.secondsPerTick;
                if (this.tickEvent != null)
                {
                    this.tickEvent();
                }
            }
        }
    }

    public void register(Action a)
    {
        this.tickEvent += a;
    }

    void Awake()
    {
        this.active = false;
        this.deltaTime = 0;
    }

    public void init(float secondsPerTick)
    {
        this.secondsPerTick = secondsPerTick != 0 ? secondsPerTick : 1;
    }

    public void start()
    {
        this.active = true;
    }

    public void stop()
    {
        this.active = false;
    }

    public void reset()
    {
        this.deltaTime = 0;
    }

    public void resetAndStop()
    {
        this.stop();
        this.reset();
    }
}
