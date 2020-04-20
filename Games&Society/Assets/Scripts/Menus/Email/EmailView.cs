using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailView : MonoBehaviour
{
    [SerializeField]
    private GameObject emailPrefab;
    [SerializeField]
    private int maxEmails;

    private List<Email> emails;

    void Awake()
    {
        this.emails = new List<Email>();
    }

    void Start()
    {
        EventManager.instance.onAssignmentCreatedEvent += this.createEmail;
        EventManager.instance.onAssignmentSubmitEvent += this.removeEmail;
    }

    private void createEmail(Assignment a)
    {
        GameObject g = Object.Instantiate(this.emailPrefab);
        g.transform.SetParent(this.transform);
        Email e = g.GetComponent<Email>();
        e.setAssignment(a);
        this.emails.Add(e);
        this.manageEmailList();
    }

    private void removeEmail(Assignment a, float grade)
    {
        for(int i = 0; i < this.emails.Count; i++)
        {
            Email e = this.emails[i];
            if (e.getID() == a.id)
            {
                this.emails.RemoveAt(i);
                e.destroy();
                break;
            }
        }
        this.manageEmailList();
    }

    private void manageEmailList()
    {
        for(int i = 0; i < this.emails.Count; i++)
        {
            if (i < this.maxEmails)
            {
                this.emails[i].show();
            } else
            {
                this.emails[i].hide();
            }
        }
    }
}
