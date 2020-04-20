using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabButton : MonoBehaviour
{
    [SerializeField]
    private string tabName;

    void Start()
    {
        EventManager.instance.onClickEvent += this.buttonClicked;
    }

    private void buttonClicked(Transform t)
    {
        if (t.Equals(this.transform))
        {
            this.switchToTab();
        }
    }

    private void switchToTab()
    {
        EventManager.instance.switchToTab(this.tabName);
    }
}
