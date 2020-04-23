using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDoList : MonoBehaviour
{
    [SerializeField]
    private GameObject toDoItemPrefab;
    [SerializeField]
    private int maxItems;

    private List<ToDoItem> toDoItems;
    private int page;

    void Awake()
    {
        this.toDoItems = new List<ToDoItem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
