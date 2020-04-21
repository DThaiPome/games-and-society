using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSystem : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float totalGrade;
    private int gradeCount;

    [Header("Weights")]
    [SerializeField]
    private float difficultyWeight;
    [SerializeField]
    private float ongoingAssignmentWeight;
    [SerializeField]
    private float overdueAssignmentWeight;
    [SerializeField]
    private float gradeWeight;

    [Header("Healthy gates")]
    [SerializeField]
    private float neutralRoof;
    [SerializeField]
    private float midHealthRoof;
    [SerializeField]
    private float maxHealth;

    [Header("Unhealthy Gates")]
    [SerializeField]
    private float neutralFloor;
    [SerializeField]
    private float midUnhealthFloor;
    [SerializeField]
    private float minHealth;

    [Header("Water")]
    [SerializeField]
    private float healthFromWatering;
    [SerializeField]
    private float defaultSquirts;
    [SerializeField]
    private float squirts;

    private List<Assignment> assignments;
    private float difficulty;
    private TickEvent waterFillTickEvent;

    void Awake()
    {
        this.assignments = new List<Assignment>();
        this.totalGrade = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onAssignmentCreatedEvent += this.addAssignment;
        EventManager.instance.onAssignmentSubmitEvent += this.assignmentSubmitted;
        EventManager.instance.onMinuteEvent += this.onMinute;
        EventManager.instance.onDifficultyChangedEvent += this.updateDifficulty;
        this.waterFillTickEvent = EventManager.instance.newTickEvent(1);
    }

    private void onMinute(int day, int minute, int minutesPerDay)
    {
        this.updatePlantHealth();
        this.tickPlantValues();
    }

    private void tickPlantValues()
    {

    }

    private void waterPlant()
    {
        this.addToHealth(this.healthFromWatering);
    }

    private void updatePlantHealth()
    {
        int ongoingAssignments = this.assignments.Count;
        int overdueAssignments = this.getOverdueAssignmentCount();

        float ongoingAssignmentEffect = -(Mathf.Pow(Mathf.Max(ongoingAssignments - 1, 0), 2)) * this.ongoingAssignmentWeight;
        float overdueAssignmentEffect = -(Mathf.Pow(overdueAssignments, 2)) * this.overdueAssignmentWeight;
        float gradeEffect = -Mathf.Sqrt(1 - this.totalGrade) * this.gradeWeight;

        float deltaHealth = ((this.difficulty * this.difficultyWeight) + 1)
            * (ongoingAssignmentEffect
            + overdueAssignmentEffect
            + gradeEffect);

        this.addToHealth(deltaHealth);
    }

    private void addToHealth(float dH)
    {
        this.health = Mathf.Clamp(this.health + dH, this.minHealth, this.maxHealth);
    }

    private int getOverdueAssignmentCount()
    {
        int count = 0;
        foreach(Assignment a in this.assignments)
        {
            if (a.overdue)
            {
                count++;
            }
        }
        return count;
    }

    private void addAssignment(Assignment a)
    {
        this.assignments.Add(a);
    }

    private void assignmentSubmitted(Assignment a, float grade)
    {
        this.updateGrade(grade);
        for (int i = 0; i < this.assignments.Count; i++)
        {
            if (this.assignments[i].id == a.id)
            {
                this.assignments.RemoveAt(i);
                break;
            }
        }
    }

    private void updateGrade(float grade)
    {
        float totalPoints = this.totalGrade * this.gradeCount;
        gradeCount++;
        this.totalGrade = (totalPoints + grade) / this.gradeCount;
    }

    private void updateDifficulty(float difficulty)
    {
        this.difficulty = difficulty;
    }

    private int healthToState()
    {
        if (this.health <= this.neutralRoof && this.health >= this.neutralFloor)
        {
            return 0;
        } else if (this.health > this.neutralRoof && this.health <= this.midHealthRoof)
        {
            return 1;
        } else if (this.health > this.midHealthRoof)
        {
            return 2;
        } else if (this.health < this.neutralFloor && this.health >= this.midUnhealthFloor)
        {
            return -1;
        } else
        {
            return -2;
        }
    }
}
