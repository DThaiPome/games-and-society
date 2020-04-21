using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickForBottle : MonoBehaviour
{
    void Start()
    {
        EventManager.instance.onClickEvent += this.onClick;
    }

    public void onClick(Transform t)
    {
        if (t.Equals(this.transform))
        {
            EventManager.instance.onBottleStandClicked();
        }
    }
}
