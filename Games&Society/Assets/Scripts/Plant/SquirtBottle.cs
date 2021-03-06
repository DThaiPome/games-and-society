﻿using System.Collections;
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
    private float secondsPerRefill;
    [SerializeField]
    private int squirts;
    [SerializeField]
    private Sprite defaultSprite;
    [SerializeField]
    private Sprite emptySprite;

    private TickEvent waterRefillTick;
    private TickEvent squirtTick;
    private Vector3 defaultPos;

    private bool squirting;
    private bool refilling;

    private Vector3 rotation;
    private Vector3 tilted;

    void Awake()
    {
        this.rotation = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
        this.tilted = this.rotation + new Vector3(0, 0, 30);
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onPlantClickedEvent += this.plantClicked;
        EventManager.instance.onBottleStandClickedEvent += this.bottleStandClicked;
        EventManager.instance.onSpigotClickedEvent += this.spigotClicked;

        EventManager.instance.onPauseGameEvent += this.onPause;
        EventManager.instance.onUnpauseGameEvent += this.onUnpause;

        this.waterRefillTick = EventManager.instance.newTickEvent(this.secondsPerRefill);
        this.waterRefillTick.register(this.refillSquirt);
        this.squirtTick = EventManager.instance.newTickEvent(this.secondsPerSquirt);
        this.squirtTick.register(this.doneSquirting);
        this.defaultPos = transform.localPosition;
    }

    void OnDisable()
    {
        this.placeBottle();
    }

    private void spigotClicked()
    {
        if (!this.refilling && !this.squirting && !this.holdingSquirtBottle && this.squirts != this.maxSquirts)
        {
            this.startRefill();
        }
    }

    private void startRefill()
    {
        this.refilling = true;
        this.waterRefillTick.resetAndStop();
        this.waterRefillTick.start();
    }

    private void refillSquirt()
    {
        this.squirts++;
        if (this.squirts == this.maxSquirts)
        {
            this.endRefill();
        }
    }

    private void endRefill()
    {
        this.waterRefillTick.stop();
        this.refilling = false;
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
        if (!this.refilling)
        {
            this.holdingSquirtBottle = true;
        }
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
        this.lazyAnimate();
    }

    private void lazyAnimate()
    {
        SpriteRenderer sp = this.gameObject.GetComponent<SpriteRenderer>();
        if (this.squirting || this.refilling)
        {
            sp.sprite = this.defaultSprite;
            this.transform.localEulerAngles = this.tilted;
        }
        else if (this.squirts > 0)
        {
            sp.sprite = this.defaultSprite;
            this.transform.localEulerAngles = this.rotation;
        } else
        {
            sp.sprite = this.emptySprite;
            this.transform.localEulerAngles = this.rotation;
        }
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

    private void onPause()
    {
        this.waterRefillTick.stop();
    }

    private void onUnpause()
    {
        if (this.refilling)
        {
            this.waterRefillTick.start();
        }
    }
}
