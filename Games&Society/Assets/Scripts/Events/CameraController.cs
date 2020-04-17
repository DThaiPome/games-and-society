﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Vector2 deskViewOrigin;
    [SerializeField]
    private Vector2 assignmentViewOrigin;

    private Vector2 currentOrigin;

    void Awake()
    {
        this.currentOrigin = this.deskViewOrigin;
    }

    void Start()
    {
        EventManager.instance.switchToMenuEvent += this.switchView;
    }

    void Update()
    {
        this.moveCamera();
    }

    private void switchView(string view)
    {
        switch(view)
        {
            case "Desk View":
                this.currentOrigin = this.deskViewOrigin;
                break;
            case "Assignment View":
                this.currentOrigin = this.assignmentViewOrigin;
                break;
        }
    }

    private void moveCamera()
    {
        this.transform.position = this.currentOrigin;
    }
}
