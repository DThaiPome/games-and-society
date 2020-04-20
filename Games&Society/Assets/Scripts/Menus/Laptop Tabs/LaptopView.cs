using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopView : MonoBehaviour
{
    [SerializeField]
    private string defaultTab;

    // Start is called before the first frame update
    void OnEnable()
    {
        this.switchToDefaultTab();
    }

    private void switchToDefaultTab()
    {
        EventManager.instance.switchToTab(this.defaultTab);
    }
}
