using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField]
    private KeyCode pauseButton;

    private bool paused;

    void Start()
    {
        this.paused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(this.pauseButton))
        {
            this.paused = !this.paused;
            if (this.paused)
            {
                EventManager.instance.onPauseGame();
            } else
            {
                EventManager.instance.onUnpauseGame();
            }
        }
    }
}
