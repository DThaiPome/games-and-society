using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickForMenu : MonoBehaviour
{
    [SerializeField]
    private string menuName;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onClickEvent += this.onClick;
    }

    private void onClick(Transform t)
    {
        if (t.Equals(this.transform))
        {
            EventManager.instance.switchToMenu(this.menuName);
        }
    }
}
