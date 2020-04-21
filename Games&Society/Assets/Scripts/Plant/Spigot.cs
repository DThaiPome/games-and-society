using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spigot : MonoBehaviour
{
    void Start()
    {
        EventManager.instance.onClickEvent += this.onClick;
    }

    private void onClick(Transform t)
    {
        if (t.Equals(this.transform))
        {
            EventManager.instance.onSpigotClicked();
        }
    }
}
