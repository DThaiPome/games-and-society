using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private string defaultView;
    [SerializeField]
    private float viewWidth;

    private List<GameObject> viewObjects;

    private Vector2 currentOrigin;

    void Start()
    {
        this.initViewList();
        this.initPosition();
        EventManager.instance.onGameOverEvent += this.onGameOver;
        EventManager.instance.switchToMenuEvent += this.switchView;
        EventManager.instance.switchToMenu(this.defaultView);
    }

    private void initViewList()
    {
        this.viewObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("View Object"));
    }

    private void initPosition()
    {
        /*
        float fT = this.viewWidth / Screen.width * Screen.height;
        fT = fT / (2.0f * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad));
        Vector3 v3T = Camera.main.transform.localPosition;
        v3T.z = -fT;
        Camera.main.transform.localPosition = v3T;*/
        float width = Mathf.Max(5, (10 * (Screen.height + this.viewWidth)) / (Screen.width));
        Camera.main.orthographicSize = width;
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
        this.initPosition();
    }

    private void onGameOver()
    {
        EventManager.instance.switchToMenu("Game Over");
    }
}
