using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Unsurprisingly generates assignments
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

    //This is time-based, so add the respective events
    void Start()
    {
        EventManager.instance.onDifficultyChangedEvent += this.setDifficulty;
        EventManager.instance.onMinuteEvent += this.generateAssignmentAtTime;
        EventManager.instance.onNextDayEvent += this.onNextDay;
        this.initGenerator();
    }

    //First time to generate an assignment
    private void initGenerator()
    {
        this.nextAssignmentTime = this.getNextAssignmentTime();
    }
    
    //Set the starting time every day
    private void onNextDay(int day)
    {
        this.initGenerator();
    }

    //Randomizes the time at which an assignment is generated
    //Also generates the assignment and broadcasts it as an event
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

    //Math for getting the new time to make an assignment
    private int getNextAssignmentTime()
    {
        float mod = 0.25f;
        return (int)Mathf.Round((float)this.baseAssignmentDelay / ((mod * this.difficulty) + 1));
    }

    //Math to determine randomness
    private int getMaxDeviation()
    {
        float mod = (float)this.baseDeviation / (float)this.baseAssignmentDelay;
        return Mathf.Max((int)Mathf.Round(mod * this.getNextAssignmentTime()), this.minDeviation);
    }

    //Generations an assignment however it needs to be generated, based on the assignment
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

    //The difficulty is changed every minute (real-time second)
    private void setDifficulty(float difficulty)
    {
        this.difficulty = difficulty;
    }
}
