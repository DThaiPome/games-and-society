using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour
{
    [SerializeField]
    private bool active;

    void Start()
    {
        EventManager.instance.onNotificationEvent += this.show;
        EventManager.instance.switchToMenuEvent += this.hideIfLaptopOrAssignment;
        this.hide();
    }

    private void show()
    {
        this.active = true;
        this.gameObject.SetActive(true);
    }

    private void hide()
    {
        this.active = false;
        this.gameObject.SetActive(false);
    }

    private void hideIfLaptopOrAssignment(string viewName)
    {
        if (viewName == "Laptop" || viewName == "Assignment")
        {
            this.hide();
        }
    }
}
