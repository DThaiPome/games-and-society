using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoOnHover : MonoBehaviour
{
    [SerializeField]
    [Tooltip("\"rise\": move up a litte\n" +
        "\"grow\": grow a little\n" +
        "\"tilt\": tilt a little")]
    private string action = "grow";
    [SerializeField]
    private float magnitude = 1.1f;

    private Vector3 rotation;
    private Vector3 position;
    private Vector3 scale;

    private void init()
    {
        this.rotation = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
        this.position = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z);
        this.scale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
    }

    private void returnToNormal()
    {
        switch(this.action)
        {
            case "rise":
                this.transform.localPosition = new Vector3(this.position.x, this.position.y, this.position.z);
                break;
            case "grow":
                this.transform.localScale = new Vector3(this.scale.x, this.scale.y, this.scale.z);
                break;
            case "tilt":
                this.transform.localEulerAngles = new Vector3(this.rotation.x, this.rotation.y, this.rotation.z);
                break;
        }
    }

    void Start()
    {
        this.init();
        EventManager.instance.onHoverEnterEvent += this.onHoverEnter;
        EventManager.instance.onHoverExitEvent += this.onHoverExit;
    }

    private void onHoverEnter(Transform t)
    {
        if (t.Equals(this.transform))
        {
            this.doAction(this.magnitude);
        }
    }

    private void doAction(float mag)
    {
        switch(this.action)
        {
            case "grow":
                this.transform.localScale = this.scale * mag;
                break;
            case "tilt":
                this.transform.Rotate(0, 0, mag);
                break;
            case "rise":
                this.transform.localPosition = this.position + new Vector3(0, mag);
                break;
        }
    }

    private void onHoverExit(Transform t)
    {
        if (t.Equals(this.transform)) {
            this.returnToNormal();
        }
    }

    void OnDestroy()
    {
        EventManager.instance.onHoverEnterEvent -= this.onHoverEnter;
        EventManager.instance.onHoverExitEvent -= this.onHoverExit;
    }
}
