using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private bool startOpen;

    void Start()
    {
        if (this.startOpen)
        {
            this.open();
        } else
        {
            this.close();
        }
    }

    public void open()
    {
        this.gameObject.SetActive(true);
    }

    public void close()
    {
        this.gameObject.SetActive(false);
    }

    public void switchMenu()
    {
        if (this.isOpen())
        {
            this.close();
        } else
        {
            this.open();
        }
    }

    private bool isOpen()
    {
        return this.gameObject.activeInHierarchy;
    }
}
