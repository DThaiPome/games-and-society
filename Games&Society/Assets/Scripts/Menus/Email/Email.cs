using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Email : MonoBehaviour
{
    [SerializeField]
    Text text;

    private Assignment assignment;

    public void setAssignment(Assignment a)
    {
        this.assignment = a;
        this.text.text = a.emailHeader();
    }

    public int getID()
    {
        return this.assignment.id;
    }

    public void hide()
    {
        this.gameObject.SetActive(false);
    }

    public void show()
    {
        this.gameObject.SetActive(true);
    }

    public void destroy()
    {
        Object.Destroy(this.gameObject);
    }
}
