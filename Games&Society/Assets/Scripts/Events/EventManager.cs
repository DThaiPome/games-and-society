using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This game is going to be time based so we can have events like that here or something
public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    //** MAIN EVENTS
    //Tick events
    [SerializeField]
    private GameObject tickEventPrefab;
    //Do something when clicked
    public event Action<Transform> onClickEvent;
    //Notices of changes in difficulty
    public event Action<float> onDifficultyChangedEvent;

    //** MENU EVENTS
    //Whenever the player tries to switch to a menu
    public event Action<string> switchToMenuEvent;

    //** GAME TIME EVENTS
    //Every minute
    public event Action<int, int, int> onMinuteEvent;
    //At the end of the day
    public event Action<int> onNextDayEvent;

    //** LIGHT SWITCH PUZZLE EVENTS
    //Whenever a switch is changed
    public event Action<LightPuzzleNode> onLightPuzzleSwitchEvent;

    //** ASSIGNMENT EVENTS
    //Whenever an assignment is created
    public event Action<Assignment> onAssignmentCreatedEvent;
    //Assignment selection changed in the assignment view
    public event Action<Assignment, int> assignmentViewSelectionChangeEvent;
    //Submit is clicked
    public event Action onSubmitClickedEvent;
    //Whenever an assignment is submitted
    public event Action<Assignment, float> onAssignmentSubmitEvent;
    //Clicking the left or right scroll arrows
    public event Action<int> onAssignmentArrowClickEvent;
    //Clicking on a to-do list item
    public event Action<ToDoItem> toDoListItemClickedEvent;
    //To-Do list is changed in any way
    public event Action onToDoListUpdateEvent;

    //** LAPTOP EVENTS
    //Whenever a tab button is pressed
    public event Action<string> switchToTabEvent;
    //Whenever a notification is necessary
    public event Action onNotificationEvent;

    //** PLANT EVENTS
    //Whenever the plant is clicked
    public event Action onPlantClickedEvent;
    //Plant is watered
    public event Action onPlantWateredEvent;
    //Bottle Stand clicked
    public event Action onBottleStandClickedEvent;
    //Spigot clicked
    public event Action onSpigotClickedEvent;
    //Plant state is changed
    public event Action<int> onPlantStateChangedEvent;

    private float deltaTime;

    public TickEvent newTickEvent(float secondsPerTick)
    {
        GameObject g = UnityEngine.Object.Instantiate(this.tickEventPrefab);
        g.transform.SetParent(this.transform);
        TickEvent te = g.GetComponent<TickEvent>();
        te.init(secondsPerTick);
        return te;
    }

    public void onClick(Transform transform)
    {
        if (onClickEvent != null)
        {
            onClickEvent(transform);
        }
    }

    public void onLightPuzzleSwitch(LightPuzzleNode lpn)
    {
        if (onLightPuzzleSwitchEvent != null)
        {
            onLightPuzzleSwitchEvent(lpn);
        }
    }

    public void onMinute(int day, int minute, int minutesPerDay)
    {
        if (onMinuteEvent != null)
        {
            onMinuteEvent(day, minute, minutesPerDay);
        }
    }

    public void onDifficultyChanged(float difficulty)
    {
        if (onDifficultyChangedEvent != null)
        {
            onDifficultyChangedEvent(difficulty);
        }
    }

    public void onAssignmentCreated(Assignment assignment)
    {
        if (onAssignmentCreatedEvent != null)
        {
            onAssignmentCreatedEvent(assignment);
        }
    }

    public void switchToMenu(string menu)
    {
        if (switchToMenuEvent != null)
        {
            switchToMenuEvent(menu);
        }
    }

    public void assignmentViewSelectionChanged(Assignment assignment, int index)
    {
        if (assignmentViewSelectionChangeEvent != null)
        {
            assignmentViewSelectionChangeEvent(assignment, index);
        }
    }

    public void onAssignmentSubmit(Assignment assignment, float grade)
    {
        if (onAssignmentSubmitEvent != null)
        {
            onAssignmentSubmitEvent(assignment, grade);
        }
    }

    public void onSubmitClicked()
    {
        if (onSubmitClickedEvent != null)
        {
            onSubmitClickedEvent();
        }
    }

    public void switchToTab(string tab)
    {
        if (switchToTabEvent != null)
        {
            switchToTabEvent(tab);
        }
    }

    public void onPlantClicked()
    {
        if (onPlantClickedEvent != null)
        {
            onPlantClickedEvent();
        }
    }

    public void onPlantWatered()
    {
        if (onPlantWateredEvent != null)
        {
            onPlantWateredEvent();
        }
    }

    public void onBottleStandClicked()
    {
        if (onBottleStandClickedEvent != null)
        {
            onBottleStandClickedEvent();
        }
    }

    public void onSpigotClicked()
    {
        if (onSpigotClickedEvent != null)
        {
            onSpigotClickedEvent();
        }
    }

    public void onNextDay(int day)
    {
        if (onNextDayEvent != null)
        {
            onNextDayEvent(day);
        }
    }

    public void onNotification()
    {
        if (onNotificationEvent != null)
        {
            onNotificationEvent();
        }
    }

    public void onAssignmentArrowClick(int direction)
    {
        if (onAssignmentArrowClickEvent != null)
        {
            onAssignmentArrowClickEvent(direction);
        }
    }

    public void onPlantStateChanged(int state)
    {
        if (onPlantStateChangedEvent != null)
        {
            onPlantStateChangedEvent(state);
        }
    }

    public void toDoListItemClicked(ToDoItem toDoItem)
    {
        if (toDoListItemClickedEvent != null)
        {
            toDoListItemClickedEvent(toDoItem);
        }
    }

    public void onToDoListUpdate()
    {
        if (onToDoListUpdateEvent != null)
        {
            onToDoListUpdateEvent();
        }
    }

    private void init()
    {
        this.deltaTime = 0;
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        this.init();
        //EventManager.instance.registerToTickEvent(testTicker);
    }

    private void testTicker()
    {
        Debug.Log("E");
    }
}
