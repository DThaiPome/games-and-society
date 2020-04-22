using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignmentArrow : MonoBehaviour
{
    [SerializeField]
    [Tooltip("1 for right, -1 for left")]
    private int direction;

    void Start()
    {
        EventManager.instance.onClickEvent += this.onClick;
    }

    private void onClick(Transform t)
    {
        if (t.Equals(this.transform))
        {
            this.arrowClick();
        }
    }

    private void arrowClick()
    {
        EventManager.instance.onAssignmentArrowClick(this.direction);
    }
}
