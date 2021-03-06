﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDoList : MonoBehaviour
{
    [SerializeField]
    private GameObject toDoItemPrefab;
    [SerializeField]
    private int maxItems;

    private List<ToDoItem> toDoItems;
    private List<Assignment> assignments;
    private int page;

    void Awake()
    {
        this.initToDoItems();
        this.assignments = new List<Assignment>();
    }
    
    void OnEnable()
    {
        this.manageToDoList();
    }

    private void initToDoItems()
    {
        this.toDoItems = new List<ToDoItem>();
        for (int i = 0; i < this.maxItems; i++)
        {
            GameObject g = Object.Instantiate(this.toDoItemPrefab);
            g.transform.SetParent(this.transform);
            ToDoItem tdi = g.GetComponent<ToDoItem>();
            this.toDoItems.Add(tdi);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onAssignmentCreatedEvent += this.newAssignment;
        EventManager.instance.onAssignmentSubmitEvent += this.assignmentSubmitted;
    }

    void Update()
    {
        
    }

    private void newAssignment(Assignment a)
    {
        this.assignments.Add(a);
        this.manageToDoList();
    }

    private void assignmentSubmitted(Assignment a, float grade)
    {
        for(int i = 0; i < this.assignments.Count; i++)
        {
            if (this.assignments[i].id == a.id)
            {
                this.assignments.RemoveAt(i);
                if (this.assignments.Count <= this.page * this.toDoItems.Count)
                {
                    this.prevPage();
                }
                this.manageToDoList();
                return;
            }
        }
    }

    private void manageToDoList()
    {
        int startIndex = this.toDoItems.Count * this.page;
        for (int i = 0; i < this.toDoItems.Count; i++)
        {
            ToDoItem tdi = this.toDoItems[i];
            int assignmentIndex = startIndex + i;
            if (assignmentIndex < this.assignments.Count)
            {
                Assignment a = this.assignments[assignmentIndex];
                tdi.setAssignment(a);
            } else
            {
                tdi.clearAssignment();
            }
        }
        EventManager.instance.onToDoListUpdate();
    }

    public void nextPage()
    {
        if (this.isNextPage())
        {
            this.page++;
            this.manageToDoList();
        }
    }

    public void prevPage()
    {
        if (this.isPrevPage())
        {
            this.page--;
            this.manageToDoList();
        }
    }

    private int pageCount()
    {
        return Mathf.Max(0, (this.assignments.Count - 1) / this.toDoItems.Count);
    }

    public bool isNextPage()
    {
        return this.page < this.pageCount();
    }

    public bool isPrevPage()
    {
        return this.page > 0;
    }
}
