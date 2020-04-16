using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    [SerializeField]
    private float difficulty;

    private float prevDifficulty;

    void Start()
    {
        this.prevDifficulty = this.difficulty;
        EventManager.instance.onMinuteEvent += this.timeToDifficulty;
    }

    void Update()
    {
        if (this.prevDifficulty != this.difficulty)
        {
            EventManager.instance.onDifficultyChanged(this.difficulty);
            Debug.Log(this.difficulty);
        }
        this.prevDifficulty = this.difficulty;
    }

    private void timeToDifficulty(int day, int minute, int minutesPerDay)
    {
        float mod = 1 / 3.0f;
        float dayMod = 0.5f;
        this.difficulty = mod * ((dayMod * day) + Mathf.Pow((float)minute / (float)minutesPerDay, 2));
    }
}
