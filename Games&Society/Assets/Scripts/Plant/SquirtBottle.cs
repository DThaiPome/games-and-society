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
    private float secondsPerSquirtRefill;
    [SerializeField]
    private int squirts;

    private TickEvent waterRefillTick;
    private TickEvent squirtTick;
    private Vector3 defaultPos;

    private bool squirting;
    private bool refilling;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onPlantClickedEvent += this.plantClicked;
        EventManager.instance.onBottleStandClickedEvent += this.bottleStandClicked;
        this.waterRefillTick = EventManager.instance.newTickEvent(this.secondsPerSquirt);
        this.squirtTick = EventManager.instance.newTickEvent(this.secondsPerSquirt);
        this.squirtTick.register(this.doneSquirting);
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
        this.waterPlant();
    }

    private void waterPlant()
    {
        if (!this.squirting && this.holdingSquirtBottle && this.squirts > 0)
        {
            this.startSquirt();
            EventManager.instance.onPlantWatered();
            this.squirts--;
        }
    }

    private void startSquirt()
    {
        this.squirting = true;
        this.squirtTick.resetAndStop();
        this.squirtTick.start();
    }

    private void doneSquirting()
    {
        this.squirting = false;
        this.squirtTick.stop();
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
