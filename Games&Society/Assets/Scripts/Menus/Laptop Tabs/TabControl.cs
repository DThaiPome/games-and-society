using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabControl : MonoBehaviour
{
    [SerializeField]
    private string tabName;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.switchToTabEvent += this.tabSwitched;
    }

    public void tabSwitched(string tabName)
    {
        if (this.tabName == tabName)
        {
            this.gameObject.SetActive(true);
        } else
        {
            this.gameObject.SetActive(false);
        }
    }
}
