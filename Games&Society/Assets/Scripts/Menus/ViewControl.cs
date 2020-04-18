using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewControl : MonoBehaviour
{
    [SerializeField]
    private string setViewName;

    public string viewName { get; private set; }

    void Awake()
    {
        this.viewName = this.setViewName;
    }

    void Start()
    {
        EventManager.instance.switchToMenuEvent += viewSwitched;
    }

    private void viewSwitched(string view)
    {
        if (view == this.viewName)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
