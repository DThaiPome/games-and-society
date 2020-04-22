using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockDisplay : MonoBehaviour
{
    private Text text;

    void Awake()
    {
        this.text = this.gameObject.GetComponent<Text>();
    }

    void Start()
    {
        EventManager.instance.onMinuteEvent += this.displayTime;
    }

    private void displayTime(int day, int totalMinute, int minutesPerDay)
    {
        int hour = Utils.minutesToMilitaryHour(totalMinute);
        int minute = Utils.minutesToHourlyMinute(totalMinute);
        string hourText = hour < 10 ? "0" + hour : "" + hour;
        string minuteText = minute < 10 ? "0" + minute : "" + minute;
        this.text.text = "Day " + (day + 1) + " - " + hourText + ":" + minuteText;
    }
}
