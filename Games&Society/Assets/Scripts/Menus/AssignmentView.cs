﻿using System.Collections;
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
    }

    void Update()
    {
        this.manageSelectedIndex();
        this.manageSelectedAssignment();
        this.viewControls();
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
}
