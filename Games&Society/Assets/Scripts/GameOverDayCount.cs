using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverDayCount : MonoBehaviour
{
    [SerializeField]
    Text text;

    void Start()
    {
        EventManager.instance.onNextDayEvent += this.updateDay;
        this.updateDay(0);
    }

    private void updateDay(int day)
    {
        this.text.text = "Days Studied: " + day;
    }
}
