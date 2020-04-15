using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickForMenu : MonoBehaviour
{
    [SerializeField]
    private MenuController mc;

    [Tooltip("For menu action: Use \"open\", \"close\", or \"switch\"")]
    [SerializeField]
    private string menuAction;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onClickEvent += this.onClick;
    }

    private void onClick(Transform t)
    {
        if (t.Equals(this.transform))
        {
            switch(this.menuAction)
            {
                case "open":
                    this.openMenu();
                    break;
                case "close":
                    this.closeMenu();
                    break;
                case "switch":
                    this.switchMenu();
                    break;
            }
        }
    }

    private void switchMenu()
    {
        this.mc.switchMenu();
    }

    private void openMenu()
    {
        this.mc.open();
    }

    private void closeMenu()
    {
        this.mc.close();
    }
}
