using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignmentGenerator : MonoBehaviour
{
    [SerializeField]
    private int baseCompletionMinutes;
    [SerializeField]
    private int minCompletionMinutes;
    [SerializeField]
    private int baseAssignmentDelay;
    [SerializeField]
    private int baseDeviation;
    [SerializeField]
    private int minDeviation;

    [SerializeField]
    private Transform assignmentTransform;

    [Header("Light Switch Puzzle")]
    [SerializeField]
    private float spaceBetweenPuzzles;

    private int nextAssignmentTime;

    private float difficulty;

    private List<string> assignmentTypes = new List<string>()
    {
        "Light Switch Puzzle"
    };

    void Start()
    {
        EventManager.instance.onDifficultyChangedEvent += this.setDifficulty;
        EventManager.instance.onMinuteEvent += this.generateAssignmentAtTime;
        this.nextAssignmentTime = this.getNextAssignmentTime();
    }

    private void generateAssignmentAtTime(int day, int minute, int minutesPerDay)
    {
        if (minutesPerDay - minute > this.getNextCompletionTime())
        {
            int deviation = Random.Range(0, this.getMaxDeviation());
            if (minute >= this.nextAssignmentTime - deviation)
            {
                Assignment a = this.generateAssignment(minute);
                EventManager.instance.onAssignmentCreated(a);
                this.nextAssignmentTime = minute + this.getNextAssignmentTime();
            }
        }
    }

    private int getNextAssignmentTime()
    {
        float mod = 2;
        return (int)Mathf.Round((float)this.baseAssignmentDelay / ((mod * this.difficulty) + 1));
    }

    private int getMaxDeviation()
    {
        float mod = (float)this.baseDeviation / (float)this.baseAssignmentDelay;
        return Mathf.Max((int)Mathf.Round(mod * this.getNextAssignmentTime()), this.minDeviation);
    }

    private Assignment generateAssignment(int minute)
    {
        switch (this.getRandomType()) {
            case "Light Switch Puzzle":
                return new LightPuzzleAssignment(
                    this.assignmentTransform, 
                    this.nextAssignmentTime, 
                    minute + this.getNextCompletionTime(), 
                    this.difficulty, 
                    this.spaceBetweenPuzzles);
            default:
                return null;
        }
    }

    private string getRandomType()
    {
        int i = Random.Range(0, this.assignmentTypes.Count);
        return this.assignmentTypes[i];
    }

    private int getNextCompletionTime()
    {
        float mod = 1;
        return Mathf.Max((int)Mathf.Round((float)this.baseCompletionMinutes / ((mod * this.difficulty) + 1)), this.minCompletionMinutes);
    }

    private void setDifficulty(float difficulty)
    {
        this.difficulty = difficulty;
    }
}
