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
        EventManager.instance.switchToMenuEvent += this.hideIfLaptop;
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

    private void hideIfLaptop(string viewName)
    {
        if (viewName == "Laptop")
        {
            this.hide();
        }
    }
}
