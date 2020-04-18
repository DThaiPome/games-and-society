using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Assignment
{
    protected static int nextID;

    protected Transform transform;
    protected int minuteStart;
    protected int minuteDue;
    protected float difficulty;
    public int id { get; private set; }


    public Assignment(Transform transform, int minuteStart, int minuteDue, float difficulty)
    {
        this.transform = transform;
        this.minuteStart = minuteStart;
        this.minuteDue = minuteDue;
        this.difficulty = difficulty;
        this.id = nextID;
        nextID++;
    }

    //Generate the assignment
    public abstract void generate();

    //The string to display on the notification email
    public abstract string emailHeader();

    //Grade the assignment
    public abstract float grade();

    //Show all assignment components
    public abstract void show();

    //Hide all assignment components
    public abstract void hide();

    //Destroy assignment
    public abstract void destroy();
}
