﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzzleGen : MonoBehaviour
{
    [SerializeField]
    private GameObject lightPrefab;
    [SerializeField]
    private GameObject lightPuzzlePrefab;

    public static LightPuzzleGen instance;

    //Creates a lightPuzzleNode at the given grid position
    public LightPuzzleNode newNode(Vector2 gridPos)
    {
        GameObject g = Object.Instantiate(this.lightPrefab);
        LightPuzzleNode lpn = g.GetComponent<LightPuzzleNode>();
        lpn.setGridPos(gridPos);
        return lpn;
    }

    //Creates a lightPuzzle at the given position with the given number of nodes, and makes it a child
    public LightPuzzle newPuzzle(Vector2 pos, int nodeCount, Transform t)
    {
        GameObject g = Object.Instantiate(this.lightPuzzlePrefab);
        LightPuzzle lp = g.GetComponent<LightPuzzle>();
        g.transform.SetParent(t);
        g.transform.localEulerAngles = new Vector3(0, 0, 0);
        g.transform.localScale = new Vector3(1, 1, 1);
        lp.setLocalPos(pos);
        lp.initPuzzle(nodeCount);
        return lp;
    }

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        /*
        this.newPuzzle(new Vector2(5, 0), 4);
        this.newPuzzle(new Vector2(0, 0), 5);
        this.newPuzzle(new Vector2(-5, 0), 6);
        */
    }
}
