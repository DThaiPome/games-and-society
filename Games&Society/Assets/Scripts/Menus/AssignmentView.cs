using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignmentView : MonoBehaviour
{
    private List<Assignment> assignments;

    [SerializeField]
    private int selectedIndex;
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
        EventManager.instance.switchToMenuEvent += this.viewSwitched;
        EventManager.instance.assignmentViewSelectionChangeEvent += this.changeSelectedAssignmentTo;
    }

    void Update()
    {
        
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

    private Assignment getAssignment(int index)
    {
        if (index < -1 || index >= this.assignments.Count)
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

    private void viewSwitched(string view)
    {
        if (view == "Assignment View")
        {
            this.gameObject.SetActive(true);
        } else
        {
            this.gameObject.SetActive(false);
        }
    }
}
