using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    public static GameTime instance;

    [SerializeField]
    private int day;
    [SerializeField]
    private int minute;
    [SerializeField]
    private int minutesInDay;

    private TickEvent ticker;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        this.ticker = EventManager.instance.newTickEvent(1);
        this.resetGameTime();
        this.ticker.register(advanceMinute);
        this.ticker.reset();
        this.ticker.start();
        EventManager.instance.onMinute(this.day, this.minute, this.minutesInDay);
    }

    public void resetGameTime()
    {
        this.day = 0;
        this.resetMinute();
    }

    public void resetMinute()
    {
        this.minute = 0;
    }

    private void advanceMinute()
    {
        this.minute++;
        if (this.minute == this.minutesInDay)
        {
            this.day++;
            this.minute = 0;
            EventManager.instance.onNextDay(this.day);
        }
        EventManager.instance.onMinute(this.day, this.minute, this.minutesInDay);
    }
}
