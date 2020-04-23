using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    private Camera camera;
    private Transform hoverTransform;

    void Start()
    {
        this.camera = Camera.main;  
    }

    // Update is called once per frame
    void Update()
    {
        this.hover();
        if (Input.GetMouseButtonDown(0))
        {
            this.click();
        }
    }

    private void click()
    {
        RaycastHit hit;
        Ray ray = this.camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.transform != null)
            {
                EventManager.instance.onClick(hit.transform);
            }
        }
    }

    private void hover()
    {
        RaycastHit hit;
        Ray ray = this.camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (!hit.transform.Equals(this.hoverTransform))
            {
                if (this.hoverTransform != null)
                {
                    EventManager.instance.onHoverExit(this.hoverTransform);
                }
                this.hoverTransform = hit.transform;
                EventManager.instance.onHoverEnter(this.hoverTransform);
            }
        } else
        {
            if (this.hoverTransform != null)
            {
                EventManager.instance.onHoverExit(this.hoverTransform);
            }
            this.hoverTransform = null;
        }
    }
}
