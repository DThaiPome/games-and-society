using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignmentView : MonoBehaviour
{
    private List<Assignment> assignments;

    [SerializeField]
    private int selectedIndex;
    [Tooltip("Switch to this scene by pressing ESC")]
    [SerializeField]
    private string escapeToScene;
    private int prevSelectedIndex;

    private Assignment selectedAssignment;

    void Awake()
    {
        this.assignments = new List<Assignment>();
        this.selectedAssignment = null;
        this.selectedIndex = -1;
        this.prevSelectedIndex = this.selectedIndex;
    }

    void Start()
    {
        EventManager.instance.onAssignmentCreatedEvent += this.assignmentAdded;
        EventManager.instance.onSubmitClickedEvent += this.submit;
        EventManager.instance.onNextDayEvent += this.onNextDay;
    }

    void Update()
    {
        this.manageSelectedIndex();
        this.manageSelectedAssignment();
        this.viewControls();
    }

    private void onNextDay(int day)
    {
        this.submitAll();
    }

    private void submitAll()
    {
        while(this.assignments.Count > 0)
        {
            this.submitAt(0);
        }
    }

    private void viewControls()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.selectedIndex++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.selectedIndex--;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.returnToDesk();
        }
    }

    private void returnToDesk()
    {
        EventManager.instance.switchToMenu(this.escapeToScene);
    }

    private void manageSelectedIndex()
    {
        if (this.selectedIndex >= this.assignments.Count)
        {
            this.selectedIndex = this.assignments.Count - 1;
        }
        if (this.selectedIndex < 0 && this.assignments.Count > 0)
        {
            this.selectedIndex = 0;
        }
        if (this.selectedIndex != this.prevSelectedIndex)
        {
            Assignment a = this.getAssignment(this.selectedIndex);
            EventManager.instance.assignmentViewSelectionChanged(a, this.selectedIndex);
        }
        this.prevSelectedIndex = this.selectedIndex;
    }

    private void changeSelectedAssignmentTo(Assignment assignment, int index)
    {
        if (this.selectedAssignment != null)
        {
            this.selectedAssignment.hide();
        }
        if (assignment != null)
        {
            assignment.show();
        }
        this.selectedAssignment = assignment;
    }

    private void manageSelectedAssignment()
    {
        for (int i = 0; i < this.assignments.Count; i++)
        {
            if (this.selectedIndex == i)
            {
                this.assignments[i].show();
            } else
            {
                this.assignments[i].hide();
            }
        }
        this.selectedAssignment = this.getAssignment(this.prevSelectedIndex);
    }

    private void submit()
    {
        if (this.assignments.Count > 0)
        {
            this.submitAt(this.selectedIndex);
        }
    }

    private void submitAt(int index)
    {
        Assignment a = this.assignments[index];
        float grade = a.grade();
        this.removeAssignment(index);
        EventManager.instance.onAssignmentSubmit(a, grade);
    }

    private void removeAssignment(int index)
    {
        this.assignments[index].destroy();
        this.assignments.RemoveAt(index);
        this.manageSelectedAssignment();
    }

    private Assignment getAssignment(int index)
    {
        if (index < 0 || index >= this.assignments.Count)
        {
            return null;
        } else
        {
            return this.assignments[index];
        }
    }

    private void assignmentAdded(Assignment assignment)
    {
        this.assignments.Add(assignment);
    }
}
