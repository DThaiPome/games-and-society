using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField]
    private KeyCode pauseButton;

    private bool paused;
    private string lastView;

    void Start()
    {
        this.paused = false;

        EventManager.instance.switchToMenuEvent += this.updateLastView;
    }

    void Update()
    {
        if (Input.GetKeyDown(this.pauseButton))
        {
            this.paused = !this.paused;
            if (this.paused)
            {
                EventManager.instance.switchToMenu("Pause");
                EventManager.instance.onPauseGame();
            } else
            {
                EventManager.instance.switchToMenu(this.lastView);
                EventManager.instance.onUnpauseGame();
            }
        }
    }

    private void updateLastView(string view)
    {
        if (view != "Pause") {
            this.lastView = view;
        }
    }
}
