using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private string defaultView;

    private List<GameObject> viewObjects;

    private Vector2 currentOrigin;

    void Start()
    {
        this.initViewList();
        EventManager.instance.switchToMenuEvent += this.switchView;
        EventManager.instance.switchToMenu(this.defaultView);
    }

    private void initViewList()
    {
        this.viewObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("View Object"));
    }

    private Vector2 getViewPos(string viewName)
    {
        foreach (GameObject g in this.viewObjects)
        {
            if (g.GetComponent<ViewControl>().viewName == viewName)
            {
                return g.transform.position;
            }
        }

        return new Vector2(0, 0);
    }

    void Update()
    {
        this.moveCamera();
    }

    private void switchView(string view)
    {
        this.currentOrigin = this.getViewPos(view);
    }

    private void moveCamera()
    {
        this.transform.position = this.currentOrigin;
    }
}
