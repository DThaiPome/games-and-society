using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDoArrow : MonoBehaviour
{
    [SerializeField]
    [Tooltip("1 for right, -1 for left")]
    private int direction;
    [SerializeField]
    ToDoList toDoList;


    void Start()
    {
        EventManager.instance.onClickEvent += this.onClick;
        EventManager.instance.onToDoListUpdateEvent += this.pageAdvanceable;
    }

    private void onClick(Transform t)
    {
        if (t.Equals(this.transform))
        {
            switch(this.direction)
            {
                case 1:
                    this.toDoList.nextPage();
                    break;
                case -1:
                    this.toDoList.prevPage();
                    break;
            }
        }
    }

    void Update()
    {
        this.pageAdvanceable();
    }

    private void pageAdvanceable()
    {
        bool toShow;
        switch(this.direction)
        {
            case 1:
                toShow = this.toDoList.isNextPage();
                break;
            case -1:
                toShow = this.toDoList.isPrevPage();
                break;
            default:
                toShow = false;
                break;
        }
        if (toShow)
        {
            this.show();
        } else
        {
            this.hide();
        }
    }

    private void show()
    {
        this.gameObject.SetActive(true);
    }

    private void hide()
    {
        this.gameObject.SetActive(false);
    }
}
