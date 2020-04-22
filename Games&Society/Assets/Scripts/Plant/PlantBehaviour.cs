using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehaviour : MonoBehaviour
{
    [SerializeField]
    private Sprite neutralSprite;
    [SerializeField]
    private Sprite goodSprite;
    [SerializeField]
    private Sprite greatSprite;
    [SerializeField]
    private Sprite badSprite;
    [SerializeField]
    private Sprite deadSprite;

    void Start()
    {
        EventManager.instance.onClickEvent += this.onClick;
        EventManager.instance.onPlantStateChangedEvent += this.updateState;
    }

    private void updateState(int state)
    {
        SpriteRenderer sp = this.gameObject.GetComponent<SpriteRenderer>();
        switch(state)
        {
            case -2:
                sp.sprite = this.deadSprite;
                break;
            case -1:
                sp.sprite = this.badSprite;
                break;
            case 0:
                sp.sprite = this.neutralSprite;
                break;
            case 1:
                sp.sprite = this.goodSprite;
                break;
            case 2:
                sp.sprite = this.greatSprite;
                break;
        }
    }

    private void onClick(Transform t)
    {
        if (t.Equals(this.transform))
        {
            EventManager.instance.onPlantClicked();
        }
    }
}
