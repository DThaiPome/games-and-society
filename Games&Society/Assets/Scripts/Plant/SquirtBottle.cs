using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirtBottle : MonoBehaviour
{
    [SerializeField]
    private bool holdingSquirtBottle;
    [SerializeField]
    private int maxSquirts;
    [SerializeField]
    private float secondsPerSquirt;
    [SerializeField]
    private int squirts;

    private TickEvent waterRefillTick;
    private Vector3 defaultPos;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onPlantClickedEvent += this.plantClicked;
        EventManager.instance.onBottleStandClickedEvent += this.bottleStandClicked;
        this.waterRefillTick = EventManager.instance.newTickEvent(this.secondsPerSquirt);
        this.defaultPos = transform.localPosition;
    }

    void OnDisable()
    {
        this.placeBottle();
    }

    private void bottleStandClicked()
    {
        if (this.holdingSquirtBottle)
        {
            this.placeBottle();
        } else
        {
            this.pickUpBottle();
        }
    }

    private void placeBottle()
    {
        this.holdingSquirtBottle = false;
    }

    private void pickUpBottle()
    {
        this.holdingSquirtBottle = true;
    }

    private void plantClicked()
    {
        if (this.holdingSquirtBottle && this.squirts > 0)
        {
            this.waterPlant();
        }
    }

    private void waterPlant()
    {
        EventManager.instance.onPlantWatered();
        this.squirts--;
    }

    // Update is called once per frame
    void Update()
    {
        this.manageBottlePos();
    }

    private void manageBottlePos()
    {
        if (holdingSquirtBottle)
        {
            this.transform.position = this.getMousePos();
        }
        else
        {
            this.transform.localPosition = this.defaultPos;
        }
    }

    private Vector3 getMousePos()
    {
        Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(v.x, v.y, this.defaultPos.z);
    }
}
