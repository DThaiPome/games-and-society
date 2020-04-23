using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToDoItem : MonoBehaviour
{
    [SerializeField]
    Text text;

    private Assignment assignment;

    void Start()
    {
        EventManager.instance.onClickEvent += this.onClick;
    }

    public void setAssignment(Assignment a)
    {
        this.assignment = a;
        this.text.text = this.assignment.assignmentCode();
    }
    
    public void clearAssignment()
    {
        this.assignment = null;
        this.text.text = "";
    }

    private void onClick(Transform t)
    {
        if (t.Equals(this.transform))
        {
            if (this.assignment != null)
            {
                EventManager.instance.toDoListItemClicked(this);
            }
        }
    }

    public int getID()
    {
        return this.assignment.id;
    }
}
