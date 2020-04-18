using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitAssignment : MonoBehaviour
{
    void Start()
    {
        EventManager.instance.onClickEvent += this.onClick;
    }

    private void onClick(Transform t)
    {
        if (t.Equals(this.transform))
        {
            this.submit();
        }
    }

    private void submit()
    {
        EventManager.instance.onSubmitClicked();
    }
}
